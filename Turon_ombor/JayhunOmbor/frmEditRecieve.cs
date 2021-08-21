using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JayhunOmbor
{
    public partial class frmEditRecieve : MetroFramework.Forms.MetroForm
    {
        public frmEditRecieve()
        {
            InitializeComponent();
        }

        public string recieveItem_id = "", product_id = "",name="", som = "",sotish_som="", sotish_dollar="", dollar = "", kurs = "", quantity = "", recieve_id="";
        public string faktura_id = "";
        private void frmEditRecieve_Load(object sender, EventArgs e)
        {
            txtName.Text = name;
            txtSom.Text = som;
            txtSotishSom.Text = sotish_som;
            txtDollar.Text = dollar;
            txtSotishDollar.Text = sotish_dollar;
            txtKurs.Text = kurs;
            txtQuantity.Text = quantity;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
            frmRecieveProduct.edit = false;
        }

        public class Product
        {
            public int id { get; set; }
            public string name { get; set; }
            public string measurement { get; set; }
            public string preparer { get; set; }
            public double min_count { get; set; }
        }

        public class FakturaItem
        {
            public int id { get; set; }
            public Product product { get; set; }
            public string name { get; set; }
            public double body_som { get; set; }
            public double body_dollar { get; set; }
            public double som { get; set; }
            public double dollar { get; set; }
            public double quantity { get; set; }
            public string barcode { get; set; }
            public int faktura { get; set; }
            public int group { get; set; }
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

        static async Task<string> GetObject(string restCallURL)
        {
            HttpClient apiCallClient = new HttpClient();

            string authToken = "token b4e829ee7f3616338ec69381da368634759394f4";
            HttpRequestMessage apirequest = new HttpRequestMessage(HttpMethod.Get, restCallURL);
            apirequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            apirequest.Headers.Add("Authorization", authToken);
            HttpResponseMessage apiCallResponse = await apiCallClient.SendAsync(apirequest);

            string requestresponse = await apiCallResponse.Content.ReadAsStringAsync();
            return requestresponse;
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if(txtQuantity.Text=="")
            {
                MessageBox.Show("Микдорни киритинг!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string som = txtSom.Text;
            string sotishSom = txtSotishSom.Text;
            string dollar = txtDollar.Text;
            string sotishDollar = txtSotishDollar.Text;
            string kurs = txtKurs.Text;
            string quantity = txtQuantity.Text;
            try
            {
                Uri u = new Uri("http://turonsavdo.backoffice.uz/api/recieveitem/up/");
                var payload = "{\"item\": \"" + recieveItem_id + "\",\"som\": \"" + som + "\", \"dollar\": \"" + dollar + "\",\"kurs\": \"" + kurs + "\",\"quantity\": \"" + quantity + "\",\"sotish_som\": \""+sotishSom+"\",\"sotish_dollar\": \""+sotishDollar+"\"}";
                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");
                var t = Task.Run(() => PostURI(u, content));
                t.Wait();
                if(t.Result != "Error!" && t.Result.Length != 0)
                {
                    MessageBox.Show("Махсулот муваффакиятли ўзгартирилди!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmRecieveProduct.edit_som = som;
                    frmRecieveProduct.edit_somSotish = sotishSom;
                    frmRecieveProduct.edit_dollar = dollar;
                    frmRecieveProduct.edit_dollarSotish = sotishDollar;
                    frmRecieveProduct.edit_kurs = kurs;
                    frmRecieveProduct.edit_quantity = quantity;
                    frmRecieveProduct.edit = true;
                }
                else
                {
                    MessageBox.Show("Интeрнeт билан богланишни тeкширинг!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
