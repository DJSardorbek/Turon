using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JayhunOmbor
{
    public partial class frmCreateRecieve : MetroFramework.Forms.MetroForm
    {
        public frmCreateRecieve()
        {
            InitializeComponent();
        }

        static async Task<string> GetObject(string restCallURL)
        {
            HttpClient apiCallClient = new HttpClient();

            string authToken = "token d0347b90933d3d4b4fbd2d30fb2dd79d824091bc";
            HttpRequestMessage apirequest = new HttpRequestMessage(HttpMethod.Get, restCallURL);
            apirequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            apirequest.Headers.Add("Authorization", authToken);
            HttpResponseMessage apiCallResponse = await apiCallClient.SendAsync(apirequest);

            string requestresponse = await apiCallResponse.Content.ReadAsStringAsync();
            return requestresponse;
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

        public class Fakturaobject
        {
            public int id { set; get; }
            public string date { set; get; }
            public double summa { set; get; }
            public int status { set; get; }
            public int difference { set; get; }
            public int filial { set; get; }
        }

        public static DataTable tbDeliver;
        public static CurrencyManager managerDeliver;
        private async void frmCreateRecieve_Load(object sender, EventArgs e)
        {
            waitForm.Show(this);
            try
            {
                tbDeliver = new DataTable();
                string responceDeliver = await GetObject("http://turonsavdo.backoffice.uz/api/deliver/");
                JArray arrayDeliver = JArray.Parse(responceDeliver);
                if (arrayDeliver != null)
                {
                    tbDeliver = JsonConvert.DeserializeObject<DataTable>(responceDeliver);
                    managerDeliver = (CurrencyManager)BindingContext[tbDeliver];
                }
                comboDeliver.DataSource = tbDeliver;
                comboDeliver.DisplayMember = "name";

                    //tbFilial = new DataTable();
                    //tbFilial.Columns.Add("id", typeof(int));
                    //tbFilial.Columns.Add("name");
                    //tbFilial.Columns.Add("address");
                    //string FilialContent = await GetObject("http://turonsavdo.backoffice.uz/api/filial/");
                    //JArray filialArray = JArray.Parse(FilialContent);
                    //if (filialArray != null)
                    //{
                    //    foreach (var filialItem in filialArray)
                    //    {
                    //        DataRow rowProduct = tbFilial.NewRow();
                    //        rowProduct["id"] = filialItem["id"];
                    //        rowProduct["name"] = filialItem["name"];
                    //        rowProduct["address"] = filialItem["address"];
                    //        tbFilial.Rows.Add(rowProduct);
                    //    }
                    //}
                    //managerFilial = (CurrencyManager)BindingContext[tbFilial];
                    //comboFilial.DataSource = tbFilial;
                    //comboFilial.DisplayMember = "name";
                    //Form1.filial = true;
            }
            catch (Exception)
            {

            }
            waitForm.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmCreateRecieve_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                Close();
            }
        }

        WaitForm waitForm = new WaitForm();

        public class Filial
        {
            public int id { get; set; }
            public string name { get; set; }
            public string address { get; set; }
            public int qarz_som { get; set; }
            public int qarz_dol { get; set; }
            public int savdo_puli_som { get; set; }
            public int savdo_puli_dol { get; set; }
        }

        public class new_recieve
        {
            public int id { get; set; }
            public Filial filial { get; set; }
            public string name { get; set; }
            public string date { get; set; }
            public double som { get; set; }
            public double sum_sotish_som { get; set; }
            public double dollar { get; set; }
            public double sum_sotish_dollar { get; set; }
            public int status { get; set; }
            public string deliver { get; set; }
        }


        /// <summary>
        /// Creating a new recieve
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnStart_Click(object sender, EventArgs e)
        {
            if (txtRecieveName.Text == "" || managerDeliver.Count == 0)
            {
                MessageBox.Show("Малумотлар тўлик eмас, илтимос тeкшириб кайтадан уриниб кўринг!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboDeliver.Visible)
            {
                try
                {
                    string name = txtRecieveName.Text;
                    string deliver = tbDeliver.Rows[managerDeliver.Position]["id"].ToString();

                    var payload = "{\"name\": \"" + name + "\",\"deliver\": \"" + deliver + "\"}";
                    Uri u = new Uri("http://turonsavdo.backoffice.uz/api/recieve/"); // yangi recieve_id ni olish uchun
                    HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");
                    var t = Task.Run(() => PostURI(u, content));
                    t.Wait();
                    if (t.Result != "Error!" && t.Result.Length != 0)
                    {
                        frmRecieveProduct.tbRecieve_id = new DataTable();
                        frmRecieveProduct.tbRecieve_id.Columns.Add("id", typeof(int));
                        frmRecieveProduct.tbRecieve_id.Columns.Add("date");
                        frmRecieveProduct.tbRecieve_id.Columns.Add("som");
                        frmRecieveProduct.tbRecieve_id.Columns.Add("dollar");
                        frmRecieveProduct.tbRecieve_id.Columns.Add("status");

                        string RecieveIdContent = t.Result;
                        new_recieve objectRecieveId = JsonConvert.DeserializeObject<new_recieve>(RecieveIdContent);
                        if (objectRecieveId != null)
                        {
                            DataRow rowRecieve_id = frmRecieveProduct.tbRecieve_id.NewRow();
                            rowRecieve_id["id"] = objectRecieveId.id;
                            rowRecieve_id["date"] = objectRecieveId.date;
                            rowRecieve_id["som"] = objectRecieveId.som;
                            rowRecieve_id["dollar"] = objectRecieveId.dollar;
                            rowRecieve_id["status"] = objectRecieveId.status;
                            frmRecieveProduct.tbRecieve_id.Rows.Add(rowRecieve_id);

                            //try
                            //{
                            //    Uri ur = new Uri("http://turonsavdo.backoffice.uz/api/faktura/");
                            //    string filial_id = tbFilial.Rows[managerFilial.Position]["id"].ToString();
                            //    var payloadr = "{\"filial\": \"" + filial_id + "\"}";
                            //    HttpContent contentr = new StringContent(payloadr, Encoding.UTF8, "application/json");
                            //    var tr = Task.Run(() => PostURI(ur, contentr));
                            //    tr.Wait();
                            //    if (tr.Result != "Error!" && tr.Result.Length != 0)
                            //    {
                            //        string FakturaContent = tr.Result;
                            //        Fakturaobject faktura = JsonConvert.DeserializeObject<Fakturaobject>(FakturaContent);
                            //        string faktura_id = faktura.id.ToString();
                            //        frmRecieveProduct.faktura_id = faktura_id;
                            //    }
                                
                            //}
                            //catch (Exception) { }

                            MessageBox.Show("Qabul muvaffaqiyatli boshlandi!", "Xabar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            frmRecieveProduct.recieve_id = objectRecieveId.id.ToString();
                            Form1.recieve_id = frmRecieveProduct.recieve_id;
                            Form1.recieve = false;
                            frmRecieveProduct.tbRecieve_id.Dispose();
                        }

                        
                        frmRecieveProduct.tbRecieveItem = new DataTable();
                        frmRecieveProduct.tbRecieveItem.Columns.Add("Кабул_ид", typeof(int)); // recieve_id
                        frmRecieveProduct.tbRecieveItem.Columns.Add("Сўм");
                        frmRecieveProduct.tbRecieveItem.Columns.Add("Сотиш_сўм");
                        frmRecieveProduct.tbRecieveItem.Columns.Add("Доллар");
                        frmRecieveProduct.tbRecieveItem.Columns.Add("Сотиш_доллар");
                        frmRecieveProduct.tbRecieveItem.Columns.Add("Курс");
                        frmRecieveProduct.tbRecieveItem.Columns.Add("Микдори");
                        frmRecieveProduct.tbRecieveItem.Columns.Add("Махсулот");
                        frmRecieveProduct.tbRecieveItem.Columns.Add("id");
                        frmRecieveProduct.tbRecieveItem.Columns.Add("product");
                        frmRecieveProduct.tbRecieveItem.Dispose();
                        
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
            else if (txtDeliver.Visible)
            {
                if(txtDeliver.Text == "" || txtPhone.Text=="")
                {
                    MessageBox.Show("Ma'lumotlar to'liq emas!\nIltimos tekshirib qaytadan urinib ko'ring!");
                    return;
                }
                try
                {
                    string name = txtDeliver.Text;
                    string phone = txtPhone.Text;
                    var payload = "{\"name\": \"" + name + "\",\"phone1\": \""+phone+"\"}";
                    Uri u = new Uri("http://turonsavdo.backoffice.uz/api/deliver/");
                    HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");
                    var t = Task.Run(() => PostURI(u, content));
                    t.Wait();
                    if (t.Result != "Error" && t.Result.Length != 0)
                    {
                        string DeliverResponce = t.Result;
                        JObject objectDeliver = JObject.Parse(DeliverResponce);
                        string deliver = objectDeliver["id"].ToString();
                        string reieveName = txtRecieveName.Text;

                        try
                        {
                            var payload1 = "{\"name\": \"" + name + "\",\"deliver\": \"" + deliver + "\"}";
                            Uri u1 = new Uri("http://turonsavdo.backoffice.uz/api/recieve/"); // yangi recieve_id ni olish uchun
                            HttpContent content1 = new StringContent(payload1, Encoding.UTF8, "application/json");
                            var t1 = Task.Run(() => PostURI(u1, content1));
                            t1.Wait();
                            if (t1.Result != "Error!" && t1.Result.Length != 0)
                            {
                                frmRecieveProduct.tbRecieve_id = new DataTable();
                                frmRecieveProduct.tbRecieve_id.Columns.Add("id", typeof(int));
                                frmRecieveProduct.tbRecieve_id.Columns.Add("date");
                                frmRecieveProduct.tbRecieve_id.Columns.Add("som");
                                frmRecieveProduct.tbRecieve_id.Columns.Add("dollar");
                                frmRecieveProduct.tbRecieve_id.Columns.Add("status");

                                string RecieveIdContent = t1.Result;
                                JObject objectRecieveId = JObject.Parse(RecieveIdContent);
                                if (objectRecieveId != null)
                                {
                                    DataRow rowRecieve_id = frmRecieveProduct.tbRecieve_id.NewRow();
                                    rowRecieve_id["id"] = objectRecieveId["id"];
                                    rowRecieve_id["date"] = objectRecieveId["date"];
                                    rowRecieve_id["som"] = objectRecieveId["som"];
                                    rowRecieve_id["dollar"] = objectRecieveId["dollar"];
                                    rowRecieve_id["status"] = objectRecieveId["status"];
                                    frmRecieveProduct.tbRecieve_id.Rows.Add(rowRecieve_id);

                                    frmRecieveProduct.recieve_id = objectRecieveId["id"].ToString();
                                    Form1.recieve_id = frmRecieveProduct.recieve_id;
                                    Form1.recieve = false;
                                    frmRecieveProduct.tbRecieve_id.Dispose();

                                    //try
                                    //{
                                    //    Uri ur = new Uri("http://turonsavdo.backoffice.uz/api/faktura/");
                                    //    string filial_id = tbFilial.Rows[managerFilial.Position]["id"].ToString();
                                    //    var payloadr = "{\"filial\": \"" + filial_id + "\"}";
                                    //    HttpContent contentr = new StringContent(payloadr, Encoding.UTF8, "application/json");
                                    //    var tr = Task.Run(() => PostURI(ur, contentr));
                                    //    tr.Wait();
                                    //    if (tr.Result != "Error!" && tr.Result.Length != 0)
                                    //    {
                                    //        string FakturaContent = tr.Result;
                                    //        Fakturaobject faktura = JsonConvert.DeserializeObject<Fakturaobject>(FakturaContent);
                                    //        string faktura_id = faktura.id.ToString();
                                    //        frmRecieveProduct.faktura_id = faktura_id;
                                    //    }

                                    //    
                                    //}
                                    //catch (Exception) { }
                                    MessageBox.Show("Qabul muvaffaqiyatli boshlandi!", "Xabar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                                frmRecieveProduct.tbRecieveItem = new DataTable();
                                frmRecieveProduct.tbRecieveItem.Columns.Add("Кабул_ид", typeof(int)); // recieve_id
                                frmRecieveProduct.tbRecieveItem.Columns.Add("Сўм");
                                frmRecieveProduct.tbRecieveItem.Columns.Add("Сотиш_сўм");
                                frmRecieveProduct.tbRecieveItem.Columns.Add("Доллар");
                                frmRecieveProduct.tbRecieveItem.Columns.Add("Сотиш_доллар");
                                frmRecieveProduct.tbRecieveItem.Columns.Add("Курс");
                                frmRecieveProduct.tbRecieveItem.Columns.Add("Микдори");
                                frmRecieveProduct.tbRecieveItem.Columns.Add("Махсулот");
                                frmRecieveProduct.tbRecieveItem.Columns.Add("id");
                                frmRecieveProduct.tbRecieveItem.Columns.Add("product");
                                frmRecieveProduct.tbRecieveItem.Dispose();
                                
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
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnNewGroup_Click(object sender, EventArgs e)
        {
            comboDeliver.Visible = false;
            txtDeliver.Visible = true;
            lblPhone.Visible = true;
            txtPhone.Visible = true;
            txtDeliver.Focus();
        }

        private void txtRecieveName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (comboDeliver.Visible)
                {
                    comboDeliver.Focus();
                    e.Handled = true;
                }
                else
                {
                    txtDeliver.Focus();
                    e.Handled = true;
                }
            }
        }

        private void comboDeliver_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtRecieveName.Text != "")
            {
                btnStart.Focus();
            }
        }

        private void txtDeliver_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                txtPhone.Focus();
                e.Handled = true;
            }
        }

        private void txtPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnStart.Focus();
                e.Handled = true;
            }
        }

    }
}
