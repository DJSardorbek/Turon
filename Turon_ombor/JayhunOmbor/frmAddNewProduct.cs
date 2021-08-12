using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestClient.Net;

namespace JayhunOmbor
{
    public partial class s : MetroFramework.Forms.MetroForm
    {
        public s()
        {
            InitializeComponent();
        }
        public string last_barcode = "", faktura_id;

        private void btnBarcode_Click(object sender, EventArgs e)
        {
            //string group_id = tbGroups.Rows[managerGroups.Position]["id"].ToString();
            //DataRow rowBarcode = Form1.tbProduct.Select("Гурух='" + group_id + "'", "Штрих_код ASC").Last();
            //last_barcode = rowBarcode["Штрих_код"].ToString();

            Int64 barcode = Int64.Parse(last_barcode);
            barcode += 1;
            txtBarcode.Text = barcode.ToString();
            txtSom.Focus();
            //int barcode = int.Parse(last_barcode);
            //barcode += 1;
            //txtBarcode.Text = barcode.ToString();
            //btnBarcode.Enabled = false;
        }

        static async Task<string> PostURI(Uri u, HttpContent c)
        {
            var response = string.Empty;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "token d0347b90933d3d4b4fbd2d30fb2dd79d824091bc");
                try
                {
                    HttpResponseMessage result = await client.PostAsync(u, c);
                    if (result.IsSuccessStatusCode)
                    {
                        using (HttpContent content = result.Content)
                        {
                            response = await content.ReadAsStringAsync();
                        }
                    }
                    else
                    {
                        response = "Error!";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return response;
        }

        public class GroupObject
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        public class ProductObject
        {
            public int id { get; set; }
            public string name { get; set; }
            public string preparer { get; set; }
            public double som { get; set; }
            public double dollar { get; set; }
            public double kurs { get; set; }
            public double quantity { get; set; }
            public string barcode { get; set; }
            public int group { get; set; }
           
        }

        WaitForm waitForm = new WaitForm();

        public string DoubleToStr(string s)
        {
            if(s.IndexOf(',') > -1)
            {
                int index = s.IndexOf(',');
                string first = s.Substring(0, index);
                string last = s.Substring(index + 1);
                s = first + "." + last;
            }
            return s;
        }


        private async void iconButton1_Click(object sender, EventArgs e)
        {
            waitForm.Show(this);
            if (comboGroup.Visible)
            {
                if (txtName.Text == "" || txtPreparer.Text == "" || comboMeasurement.Text=="" || txtMinCount.Text=="" || txtBarcode.Text == "" || comboGroup.Text == "" || txtQuantity.Text =="")
                {
                    MessageBox.Show("Малумотлар тўлик eмас, илтимос тeкшириб кайтадан уриниб кўринг!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if(txtQuantity.Text.IndexOf(',') > -1)
                {
                    txtQuantity.Text = DoubleToStr(txtQuantity.Text);
                }
                string som = "0", dollar = "0", kurs = "0", sotish_som="0", sotish_dollar="0";
                string name = txtName.Text;
                string preparer = txtPreparer.Text;
                string barcode = txtBarcode.Text;
                string measurement = comboMeasurement.Text;
                string min_count = txtMinCount.Text;
                string group = tbGroups.Rows[managerGroups.Position]["id"].ToString();
                if(txtSom.Text != "0" && txtSom.Text!="") { som = txtSom.Text; }
                if(txtSotishSom.Text != "0" && txtSotishSom.Text!="") { sotish_som = txtSotishSom.Text; }
                if(txtDollar.Text != "0" && txtDollar.Text !="") { dollar = txtDollar.Text; }
                if(txtSotishDollar.Text != "0" && txtSotishDollar.Text !="") { sotish_dollar = txtSotishDollar.Text; }
                if(txtKurs.Text !="0" && txtKurs.Text !="") { kurs = txtKurs.Text; }
                try
                {
                    Uri u = new Uri("http://turonsavdo.backoffice.uz/api/productfilial/");
                    var payload = "{\"name\": \"" + name + "\",\"preparer\": \"" + preparer + "\",\"som\": \""+som+"\",\"sotish_som\": \""+sotish_som+"\",\"dollar\": \""+dollar+"\",\"sotish_dollar\": \""+sotish_dollar+"\",\"kurs\": \""+kurs+"\",\"quantity\": 0,\"barcode\": \"" + barcode + "\",\"group\": \""+group+"\",\"measurement\": \""+measurement+"\",\"min_count\": \""+min_count+"\",\"filial_id\": \"1\"}";
                    HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");
                    var t = Task.Run(() => PostURI(u, content));
                    t.Wait();
                    if (t.Result != "Error!" && t.Result.Length != 0)
                    {
                        string productIDContent = t.Result;
                        ProductObject obj = JsonConvert.DeserializeObject<ProductObject>(productIDContent); ;
                        string productId = obj.id.ToString();
                        try
                        {
                            string sotishSom = "0", sotishDollar = "0";
                            if (txtSotishSom.Text != "0") { sotishSom = txtSotishSom.Text; }
                            if (txtSotishDollar.Text != "0") { sotishDollar = txtSotishDollar.Text; }
                            Uri u1 = new Uri("http://turonsavdo.backoffice.uz/api/recieveitem/add/");
                            var payload1 = "{\"recieve\": \"" + frmRecieveProduct.recieve_id + "\",\"product\": \"" + productId + "\",\"som\": \"" + som + "\",\"sotish_som\": \""+sotishSom+"\",\"dollar\": \"" + dollar + "\",\"sotish_dollar\": \""+sotishDollar+"\",\"kurs\": \"" + kurs + "\",\"quantity\": \"" + txtQuantity.Text + "\"}";
                            HttpContent content1 = new StringContent(payload1, Encoding.UTF8, "application/json");
                            var t1 = Task.Run(() => PostURI(u1, content1));
                            t1.Wait();
                            if(t1.Result != "Error!" && t1.Result.Length != 0)
                            {
                                string Responce = t1.Result;
                                JObject objectRecieve = JObject.Parse(Responce);
                                
                                // recieveitem details for recieveitem table
                                frmRecieveProduct.id = objectRecieve["id"].ToString();
                                frmRecieveProduct.som = objectRecieve["som"].ToString();
                                frmRecieveProduct.sotish_som = objectRecieve["sotish_som"].ToString();
                                frmRecieveProduct.dollar = objectRecieve["dollar"].ToString();
                                frmRecieveProduct.sotish_dollar = objectRecieve["sotish_dollar"].ToString();
                                frmRecieveProduct.kurs = objectRecieve["kurs"].ToString();
                                frmRecieveProduct.quantity = objectRecieve["quantity"].ToString();
                                frmRecieveProduct.recieve = objectRecieve["recieve"].ToString();
                                frmRecieveProduct.product1 = objectRecieve["product"].ToString();
                                Form1.recieve = false;

                                //if (faktura_id != "0")
                                //{
                                //    string sotishSom = "0", sotishDollar = "0", Pname = "";
                                //    if (txtSotishSom.Text != "0") { sotishSom = txtSotishSom.Text; }
                                //    if (txtSotishDollar.Text != "0") { sotishDollar = txtSotishDollar.Text; }
                                //    Pname = txtName.Text;

                                //    Uri ur = new Uri("http://turonsavdo.backoffice.uz/api/fakturaitem/add/");
                                //    var payloadr = "{\"name\": \"" + Pname + "\",\"faktura\": \"" + faktura_id + "\",\"product\": \"" + productId + "\",\"som\": \"" + sotishSom + "\",\"dollar\": \"" + sotishDollar + "\",\"group\": \"" + group + "\",\"barcode\": \"" + barcode + "\",\"quantity\": \"" + txtQuantity.Text + "\",\"body_som\": \"" + som + "\",\"body_dollar\": \"" + dollar + "\"}";

                                //    HttpContent contentr = new StringContent(payloadr, Encoding.UTF8, "application/json");
                                //    var tr = Task.Run(() => PostURI(ur, contentr));
                                //    tr.Wait();
                                //    if (tr.Result != "Error!" && tr.Result.Length != 0)
                                //    {
                                //        MessageBox.Show("Янги товар муваффакиятли кўшилди!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //        waitForm.Close();
                                //        Form1.faktura = false;
                                //        Form1.filial = false;
                                //    }
                                //    else
                                //    {
                                //        MessageBox.Show("Server bilan bo'glanishda xatolik", "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                //        return;
                                //    }
                                //}
                                //else
                                //{
                                //    MessageBox.Show("Янги товар муваффакиятли кўшилди!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    waitForm.Close();
                                //}

                                MessageBox.Show("Янги товар муваффакиятли кўшилди!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                waitForm.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        // A new product details for product table
                        frmRecieveProduct.product_id = obj.id.ToString();
                        frmRecieveProduct.product_name = obj.name.ToString();
                        frmRecieveProduct.product_preparer = obj.preparer.ToString();
                        frmRecieveProduct.product_som = obj.som.ToString();
                        frmRecieveProduct.product_dollar = obj.dollar.ToString();
                        frmRecieveProduct.product_kurs = obj.kurs.ToString();
                        frmRecieveProduct.product_quantity = obj.quantity.ToString();
                        frmRecieveProduct.product_barcode = obj.barcode.ToString();
                        frmRecieveProduct.product_group = obj.group.ToString();

                        frmRecieveProduct.added = true;
                        Close();
                    }
                    
                    else
                    {
                        MessageBox.Show("Сeрвeр билан богланишда хатолик, илтимос интeрнeтни тeкширинг!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                catch (Exception ex)
                {
                    
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if(txtGroup.Visible)
            {
                if (txtName.Text == "" || txtPreparer.Text == "" || txtBarcode.Text == "" || txtGroup.Text == "")
                {
                    MessageBox.Show("Малумотлар тўлик eмас, илтимос тeкшириб кайтадан уриниб кўринг!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (txtQuantity.Text.IndexOf(',') > -1)
                {
                    MessageBox.Show("Микдорни нукта билан киритинг!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string txtname = txtGroup.Text;
                try
                {
                    Uri uGroup = new Uri("http://turonsavdo.backoffice.uz/api/groups/");
                    var payloadGroup = "{\"name\": \"" + txtname + "\"}";
                    HttpContent contentGroup = new StringContent(payloadGroup, Encoding.UTF8, "application/json");
                    var tGroup = Task.Run(() => PostURI(uGroup, contentGroup));
                    tGroup.Wait();
                    if (tGroup.Result != "Error!" && tGroup.Result.Length != 0)
                    {
                        string GroupContent = tGroup.Result;
                        string groupId = "";
                        GroupObject group = JsonConvert.DeserializeObject<GroupObject>(GroupContent);
                        groupId = group.id.ToString();
                        string som = "0", dollar = "0", kurs = "0";
                        string name = txtName.Text;
                        string preparer = txtPreparer.Text;
                        string barcode = txtBarcode.Text;
                        if (txtSom.Text != "0") { som = txtSom.Text; }
                        if (txtDollar.Text != "0") { dollar = txtDollar.Text; }
                        if (txtKurs.Text != "0") { kurs = txtKurs.Text; }

                        try
                        {
                            Uri u = new Uri("http://turonsavdo.backoffice.uz/api/product/");
                            var payload = "{\"name\": \"" + name + "\",\"preparer\": \"" + preparer + "\",\"som\": \"" + som + "\",\"dollar\": \"" + dollar + "\",\"kurs\": \"" + kurs + "\",\"quantity\": 0,\"barcode\": \"" + barcode + "\",\"group\": \"" + groupId + "\"}";
                            HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");
                            var t = Task.Run(() => PostURI(u, content));
                            t.Wait();
                            if (t.Result != "Error!" && t.Result.Length != 0)
                            {
                                string productIDContent = t.Result;
                                ProductObject obj = JsonConvert.DeserializeObject<ProductObject>(productIDContent);
                                string productId = obj.id.ToString();
                             try
                                {
                                    Uri u1 = new Uri("http://turonsavdo.backoffice.uz/api/recieveitem/add/");
                                    var payload1 = "{\"recieve\": \"" + frmRecieveProduct.recieve_id + "\",\"product\": \"" + productId + "\",\"som\": \"" + som + "\",\"dollar\": \"" + dollar + "\",\"kurs\": \"" + kurs + "\",\"quantity\": \"" + txtQuantity.Text + "\"}";
                                    HttpContent content1 = new StringContent(payload1, Encoding.UTF8, "application/json");
                                    var t1 = Task.Run(() => PostURI(u1, content1));
                                    t1.Wait();
                                    if (t1.Result != "Error!" && t1.Result.Length != 0)
                                    {

                                        MessageBox.Show("Янги товар муваффакиятли кўшилди!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        waitForm.Close();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                frmRecieveProduct.product_id = obj.id.ToString();
                                frmRecieveProduct.product_name = obj.name.ToString();
                                frmRecieveProduct.product_preparer = obj.preparer.ToString();
                                frmRecieveProduct.product_som = obj.som.ToString();
                                frmRecieveProduct.product_dollar = obj.dollar.ToString();
                                frmRecieveProduct.product_kurs = obj.kurs.ToString();
                                frmRecieveProduct.product_quantity = obj.quantity.ToString();
                                frmRecieveProduct.product_barcode = obj.barcode.ToString();
                                frmRecieveProduct.product_group = obj.group.ToString();
                                frmRecieveProduct.added = true;
                                Close();
                            }
                            else
                            {
                                MessageBox.Show("Сeрвeр билан богланишда хатолик, илтимос интeрнeтни тeкширинг!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Сeрвeр билан богланишда хатолик, илтимос интeрнeтни тeкширинг!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }


        private void btnNewGroup_Click(object sender, EventArgs e)
        {
            comboGroup.Visible = false;
            txtGroup.Visible = true;
            btnNewGroup.Enabled = false;
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        static async Task<string> GetObject()
        {
            HttpClient apiCallClient = new HttpClient();
            String restCallURL = "http://turonsavdo.backoffice.uz/api/groups/";
            string authToken = "token d0347b90933d3d4b4fbd2d30fb2dd79d824091bc";
            HttpRequestMessage apirequest = new HttpRequestMessage(HttpMethod.Get, restCallURL);
            apirequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            apirequest.Headers.Add("Authorization", authToken);
            HttpResponseMessage apiCallResponse = await apiCallClient.SendAsync(apirequest);

            String requestresponse = await apiCallResponse.Content.ReadAsStringAsync();
            return requestresponse;
        }

        public static DataTable tbGroups;
        public static CurrencyManager managerGroups;
        private async void frmAddNewProduct_Load(object sender, EventArgs e)
        {
            tbGroups = new DataTable();
            tbGroups.Columns.Add("id", typeof(int));
            tbGroups.Columns.Add("name");
            string GroupsContent = await GetObject();
            JArray GroupsArray = JArray.Parse(GroupsContent);
            if (GroupsArray != null)
            {
                foreach (var GroupsItem in GroupsArray)
                {
                    DataRow rowProduct = tbGroups.NewRow();
                    rowProduct["id"] = GroupsItem["id"];
                    rowProduct["name"] = GroupsItem["name"];
                    tbGroups.Rows.Add(rowProduct);
                }
            }
            managerGroups = (CurrencyManager)BindingContext[tbGroups];
            tbGroups.Dispose();
            comboGroup.DataSource = tbGroups;
            comboGroup.DisplayMember = "name";
            comboMeasurement.SelectedIndex = 0;

        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                txtPreparer.Focus();
                e.Handled = true;
            }
            else
            {
                return;
            }
        }

        private void txtPreparer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMinCount.Focus();
                e.Handled = true;
            }
        }

        private void txtMinCount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBarcode.Focus();
                e.Handled = true;
            }
        }

        private void txtGroup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBarcode.Focus();
                e.Handled = true;
            }
        }

        private void txtSom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSotishSom.Focus();
                e.Handled = true;
            }
        }

        private void txtDollar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSotishDollar.Focus();
                e.Handled = true;
            }
        }

        private void txtKurs_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtQuantity.Focus();
                e.Handled = true;
            }
        }

        private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                iconButton1.Focus();
                e.Handled = true;
            }
        }

        private void comboMeasurement_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(txtName.Text!="")
            txtMinCount.Focus();
            
        }

        private void comboGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(txtMinCount.Text!="")
            btnBarcode.Focus();
        }

        private void frmAddNewProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void txtSotishSom_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                txtDollar.Focus();
                e.Handled = true;
            }
        }

        private void txtSotishDollar_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                txtKurs.Focus();
                e.Handled = true;
            }
        }
    }
}