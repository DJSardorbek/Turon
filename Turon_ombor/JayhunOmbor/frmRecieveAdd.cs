using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JayhunOmbor
{
    public partial class frmRecieveAdd : MetroFramework.Forms.MetroForm
    {
        public frmRecieveAdd()
        {
            InitializeComponent();
        }
        public event System.Windows.Forms.KeyEventHandler KeyDown;
        public string recieve_id = "";
        public string product_id = "";
        public string som = "", dollar = "", kurs = "";
        public string name = "";
        public string faktura_id = "0", group="", barcode="";

        public class Filial
        {
            public int id { get; set; }
            public string name { get; set; }
            public string address { get; set; }
            public double qarz_som { get; set; }
            public double qarz_dol { get; set; }
            public double savdo_puli_som { get; set; }
            public double savdo_puli_dol { get; set; }
        }

        public class Product
        {
            public int id { get; set; }
            public Filial filial { get; set; }
            public string name { get; set; }
            public string preparer { get; set; }
            public double som { get; set; }
            public double sotish_som { get; set; }
            public double dollar { get; set; }
            public double sotish_dollar { get; set; }
            public double kurs { get; set; }
            public string barcode { get; set; }
            public string measurement { get; set; }
            public double min_count { get; set; }
            public double quantity { get; set; }
            public int group { get; set; }
        }

        public class RecieveItem
        {
            public int id { get; set; }
            public int recieve { get; set; }
            public Product product { get; set; }
            public double som { get; set; }
            public double sotish_som { get; set; }
            public double dollar { get; set; }
            public double sotish_dollar { get; set; }
            public double kurs { get; set; }
            public double quantity { get; set; }
        }


        private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                txtSom.Focus();
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

        private void txtSotishSom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDollar.Focus();
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

        private void txtSotishDollar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtKurs.Focus();
                e.Handled = true;
            }
        }

        private void txtKurs_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                iconButton1.Focus();
                e.Handled = true;
            }
        }

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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        WaitForm waitForm = new WaitForm();
        private async void iconButton1_Click(object sender, EventArgs e)
        {
            if (txtQuantity.Text == "")
            {
                MessageBox.Show("Введите количество!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            waitForm.Show(this);
            try
            {
                string som = "0", dollar = "0", kurs = "0";
                string product = product_id;
                
                string quantity = txtQuantity.Text;
                quantity = DoubleToStr(quantity);
                if (txtSom.Text != "0" || txtSom.Text != "") { som = txtSom.Text; }
                if (txtDollar.Text != "0" || txtDollar.Text != "") { dollar = txtDollar.Text; }
                if (txtKurs.Text != "0" || txtKurs.Text != "") { kurs = txtKurs.Text; }
                string sotishSom = "0", sotishDollar = "0";
                if (txtSotishSom.Text != "0") { sotishSom = txtSotishSom.Text; }
                if (txtSotishDollar.Text != "0") { sotishDollar = txtSotishDollar.Text; }

                Uri u = new Uri("http://turonsavdo.backoffice.uz/api/recieveitem/add/");
                var payload = "{\"recieve\": \"" + recieve_id + "\",\"product\": \"" + product + "\",\"som\": \"" + som + "\",\"dollar\": \"" + dollar + "\",\"kurs\": \"" + kurs + "\",\"quantity\": \"" + quantity + "\",\"sotish_som\": \""+sotishSom+"\",\"sotish_dollar\": \""+sotishDollar+"\"}";
                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");
                var t = Task.Run(() => PostURI(u, content));
                t.Wait();
                if (t.Result != "Error" && t.Result.Length != 0)
                {
                    string Responce = t.Result;
                    RecieveItem objectRecieve = JsonConvert.DeserializeObject<RecieveItem>(Responce);
                    frmRecieveProduct.id = objectRecieve.id.ToString();
                    frmRecieveProduct.som = objectRecieve.som.ToString();
                    frmRecieveProduct.sotish_som = objectRecieve.sotish_som.ToString();
                    frmRecieveProduct.dollar = objectRecieve.dollar.ToString();
                    frmRecieveProduct.sotish_dollar = objectRecieve.sotish_dollar.ToString();
                    frmRecieveProduct.kurs = objectRecieve.kurs.ToString();
                    frmRecieveProduct.quantity = objectRecieve.quantity.ToString();
                    frmRecieveProduct.recieve = objectRecieve.recieve.ToString();
                    frmRecieveProduct.product1 = objectRecieve.product.id.ToString();
                    Form1.recieve = false;
                   

                    //if (faktura_id != "0")
                    //{
                    //    string sotishSom = "0", sotishDollar = "0", Pname = "";
                    //    if (txtSotishSom.Text != "0") { sotishSom = txtSotishSom.Text; }
                    //    if (txtSotishDollar.Text != "0") { sotishDollar = txtSotishDollar.Text; }
                    //    Pname = txtProduct.Text;

                    //    Uri ur = new Uri("http://turonsavdo.backoffice.uz/api/fakturaitem/add/");
                    //    var payloadr = "{\"name\": \"" + Pname + "\",\"faktura\": \"" + faktura_id + "\",\"product\": \"" + product + "\",\"som\": \"" + sotishSom + "\",\"dollar\": \"" + sotishDollar + "\",\"group\": \"" + group + "\",\"barcode\": \"" + barcode + "\",\"quantity\": \"" + txtQuantity.Text + "\",\"body_som\": \"" + som + "\",\"body_dollar\": \"" + dollar + "\"}";

                    //    HttpContent contentr = new StringContent(payloadr, Encoding.UTF8, "application/json");
                    //    var tr = Task.Run(() => PostURI(ur, contentr));
                    //    tr.Wait();
                    //    if (tr.Result != "Error!" && tr.Result.Length != 0)
                    //    {
                    //        waitForm.Close();
                    //        txtQuantity.Clear();
                    //        txtSom.Text = "0";
                    //        txtDollar.Text = "0";
                    //        txtKurs.Text = "0";

                    //        Form1.faktura = false;
                    //        Form1.filial = false;
                    //    }
                    //    else
                    //    {
                    //        MessageBox.Show("Server bilan bo'glanishda xatolik", "Xatolik", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //        return;
                    //    }
                    //}
                   
                    Close();
                }
                else
                {
                    MessageBox.Show("Ошибка при подключении к серверу, проверьте параметры сети", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmRecieveAdd_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            txtProduct.Text = name;
            txtSom.Text = som;
            txtDollar.Text = dollar;
            txtKurs.Text = kurs;
        }
        static async Task<string> PostURI(Uri u, HttpContent c)
        {
            var response = string.Empty;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "token b4e829ee7f3616338ec69381da368634759394f4");
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
    }
}
