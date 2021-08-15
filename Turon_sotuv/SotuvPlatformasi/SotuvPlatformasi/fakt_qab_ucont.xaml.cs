using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using MySql.Data.MySqlClient;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Globalization;
using Newtonsoft.Json;
using System.Windows.Media.Effects;
using System.Threading;

namespace SotuvPlatformasi
{
    /// <summary>
    /// Interaction logic for xaml
    /// </summary>
    public partial class fakt_qab_ucont : UserControl
    {
        public fakt_qab_ucont()
        {
            InitializeComponent();
        }
        DBAccess objDBAccess = new DBAccess();
        MainWindow main1 = (MainWindow)Application.Current.MainWindow;
        public static DataTable tbFaktura;
        public static MySqlCommand cmdUpdateProd;

        private void kurish_Click(object sender, RoutedEventArgs e)
        {
            main1.TabMenu.SelectedIndex = 5;
            Recieve selectedFaktura = dataGridFaktura.SelectedItems[0] as Recieve;
            faktura_kurish_ucont.recieve_id = selectedFaktura.id.ToString();
        }
        private void BtnAsosiy_Click(object sender, RoutedEventArgs e)
        {
            main1.TabMenu.SelectedIndex = 1;
        }

        #region recieve class

        /// <summary>
        /// Recieve item classses
        /// </summary>
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

        public class Products
        {
            public int id { get; set; }
            public Filial filial { get; set; }
            public string name { get; set; }
            public double som { get; set; }
            public double sotish_som { get; set; }
            public double dollar { get; set; }
            public double sotish_dollar { get; set; }
            public double kurs { get; set; }
            public double quantity { get; set; }
            public string preparer { get; set; }
            public double min_count { get; set; }
            public string measurement { get; set; }
            public string barcode { get; set; }
            public int group { get; set; }
        }

        public class RecieveItem
        {
            public int id { get; set; }
            public int recieve { get; set; }
            public Products product { get; set; }
            public double som { get; set; }
            public double sotish_som { get; set; }
            public double dollar { get; set; }
            public double sotish_dollar { get; set; }
            public double kurs { get; set; }
            public double quantity { get; set; }
        }

        #endregion

        public List<Recieve> recieves = new List<Recieve>();

        public class Recieve
        {
            public int id { get; set; }
            public string name { get; set; }
            public string date { get; set; }
            public double som { get; set; }
            public double sotish_som { get; set; }
            public double dollar { get; set; }
            public double sotish_dollar { get; set; }
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
        List<FakturaItem> fakturaItems = new List<FakturaItem>();
        List<RecieveItem> recieveItems = new List<RecieveItem>();

        public List<Recieve> Get_Faktura()
        {
            recieves = (from DataRow dr in tbFaktura.Rows
                        select new Recieve()
                        {
                            id = Convert.ToInt32(dr["id"]),
                            name = dr["name"].ToString(),
                            date = dr["date"].ToString(),
                            som = Convert.ToDouble(dr["som"]),
                            sotish_som = Convert.ToDouble(dr["sotish_som"]),
                            dollar = Convert.ToDouble(dr["dollar"]),
                            sotish_dollar = Convert.ToDouble(dr["sotish_dollar"])
                        }).ToList();
            return recieves;
        }



        public static async Task<string> GetObject(string restCallURL)
        {
            HttpClient apiCallClient = new HttpClient();
            string authToken = "token 249d4a8aa9ecf75844d87926b7b7ee4e1cd8b1da";
            HttpRequestMessage apirequest = new HttpRequestMessage(HttpMethod.Get, restCallURL);
            apirequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            apirequest.Headers.Add("Authorization", authToken);
            HttpResponseMessage apiCallResponse = await apiCallClient.SendAsync(apirequest);

            string requestresponse = await apiCallResponse.Content.ReadAsStringAsync();
            return requestresponse;
        }
        public string FakturaContent = "";
        void Simulator()
        {
            for (int i = 0; i < 250; i++)
                Thread.Sleep(5);
        }
        BlurEffect myEffect = new BlurEffect();
        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (MainWindow.fakt_qab_click)
            {
                if (MainWindow.Recieve == false)
                {
                    try
                    {
                        tbFaktura = new DataTable();
                        tbFaktura.Columns.Add("id", typeof(int));
                        tbFaktura.Columns.Add("name");
                        tbFaktura.Columns.Add("date");
                        tbFaktura.Columns.Add("som");
                        tbFaktura.Columns.Add("sotish_som");
                        tbFaktura.Columns.Add("dollar");
                        tbFaktura.Columns.Add("sotish_dollar");

                        string urlFaktura = "http://turonsavdo.backoffice.uz/api/recieve/get_status_1/";
                        FakturaContent = await GetObject(urlFaktura);
                        myEffect.Radius = 10;
                        Effect = myEffect;
                        using (LoadingWindow lw = new LoadingWindow(Simulator))
                        {
                            lw.ShowDialog();
                        }
                        myEffect.Radius = 0;
                        Effect = myEffect;
                        JArray arrayFaktura = JArray.Parse(FakturaContent);
                        if (arrayFaktura != null)
                        {
                            foreach (var item in arrayFaktura)
                            {
                                DataRow rowFaktura = tbFaktura.NewRow();
                                rowFaktura["id"] = item["id"];
                                rowFaktura["name"] = item["name"];
                                rowFaktura["date"] = item["date"];
                                rowFaktura["som"] = item["som"];
                                rowFaktura["sotish_som"] = item["sum_sotish_som"];
                                rowFaktura["dollar"] = item["dollar"];
                                rowFaktura["sotish_dollar"] = item["sum_sotish_dollar"];
                                tbFaktura.Rows.Add(rowFaktura);
                            }

                        }
                        tbFaktura.Dispose();
                        dataGridFaktura.ItemsSource = Get_Faktura();
                        MainWindow.Recieve = true;
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message, "Сообщение", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }

        private async void btnQabul_qilish_Click(object sender, RoutedEventArgs e)
        {
            if (tbFaktura.Rows.Count == 0) return;

            string recieve = "0";
            Recieve selectedRecieve = dataGridFaktura.SelectedItems[0] as Recieve;
            recieve = selectedRecieve.id.ToString();
            /// <summary>
            /// The request to get recieveitem
            /// </summary>
            try
            {
                string urlRecieveItem = "http://turonsavdo.backoffice.uz/api/recieveitem/rv1/?rec=" + recieve;
                string RecieveItemContent = await GetObject(urlRecieveItem);
                recieveItems = JsonConvert.DeserializeObject<List<RecieveItem>>(RecieveItemContent);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Сообщение", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }
            //Dastlab Fakturada kelgan tovarlarni maxsulot jadvaliga qabul qilamiz
            int CountRecieve = tbFaktura.Rows.Count;
            string Recieve_barcode = "", Recieve_price_som = "", Recieve_price_dollar = "", Recieve_tan_dollar = "", Recieve_tan_som = "", Recieve_quantity = "", Recieve_minCount = ""; // GetObject();
            string product_quantiy = "", val_id = "", recieve_price = "", recieve_tan_price = "";
            // the values for compute farq_som, farq_dollar
            double farq_som = 0, farq_dollar = 0;
            string t_price = "";
            DataTable tbProductOne = new DataTable();
            foreach (var item in recieveItems)
            {
                Recieve_barcode = item.product.barcode.ToString();

                Recieve_price_som = item.sotish_som.ToString();
                Recieve_price_som = DoubleToStr(Recieve_price_som);

                Recieve_price_dollar = item.sotish_dollar.ToString();
                Recieve_price_dollar = DoubleToStr(Recieve_price_dollar);

                Recieve_quantity = item.quantity.ToString();//
                Recieve_quantity = DoubleToStr(Recieve_quantity);

                Recieve_minCount = item.product.min_count.ToString();
                Recieve_minCount = DoubleToStr(Recieve_minCount);

                Recieve_tan_som = item.som.ToString();//
                Recieve_tan_som = DoubleToStr(Recieve_tan_som);

                Recieve_tan_dollar = item.dollar.ToString();//
                Recieve_tan_dollar = DoubleToStr(Recieve_tan_dollar);

                string queryProductOne = "select * from product where barcode='" + Recieve_barcode + "'";
                objDBAccess.readDatathroughAdapter(queryProductOne, tbProductOne);
                //Agar ushbu maxsulotdan avval olingan bo'lsa uni miqdorini , narxini, min_countini update qilamiz
                if (tbProductOne.Rows.Count > 0)
                {
                    product_quantiy = tbProductOne.Rows[0]["quantity"].ToString();//
                    product_quantiy = DoubleToStr(product_quantiy);

                    val_id = tbProductOne.Rows[0]["val_id"].ToString();

                    t_price = tbProductOne.Rows[0]["t_price"].ToString();

                    double Dproduct_quantity = double.Parse(product_quantiy, CultureInfo.InvariantCulture);
                    double Drecieve_quantity = double.Parse(Recieve_quantity, CultureInfo.InvariantCulture);
                    double result_quantity = Dproduct_quantity + Drecieve_quantity; // natijaviy miqdor
                    string str_result_quantity = result_quantity.ToString();
                    str_result_quantity = DoubleToStr(str_result_quantity);


                    if (val_id == "1")
                    {
                        recieve_price = Recieve_price_som;
                        recieve_price = DoubleToStr(recieve_price);

                        recieve_tan_price = Recieve_tan_som;

                        // compute farq_som
                        farq_som += Dproduct_quantity * (double.Parse(recieve_tan_price)-double.Parse(t_price));
                    }
                    else
                    {
                        recieve_price = Recieve_price_dollar;
                        recieve_price = DoubleToStr(recieve_price);

                        recieve_tan_price = Recieve_tan_dollar;

                        // compute farq_dollar
                        farq_dollar += Dproduct_quantity * (double.Parse(recieve_tan_price) - double.Parse(t_price));
                    }
                    cmdUpdateProd = new MySqlCommand("update product set price='" + recieve_price + "',t_price='" + DoubleToStr(recieve_tan_price) + "', quantity='" + str_result_quantity + "', min_count='" + Recieve_minCount + "' where barcode='" + Recieve_barcode + "'");
                    objDBAccess.executeQuery(cmdUpdateProd);
                    cmdUpdateProd.Dispose();
                }
                //Agar ushbu maxsulotdan avval olinmagan bo'lsa, product jadvaliga kiritamiz
                else
                {
                    string Recieve_prodId = item.product.id.ToString();
                    string Recieve_name = item.product.name;
                    string Recieve_group = item.product.group.ToString();
                    string Recieve_measurement = item.product.measurement;
                    string Recieve_preparer = item.product.preparer;
                    if (double.Parse(DoubleToStr(Recieve_price_som), CultureInfo.InvariantCulture) > 0)
                    {
                        recieve_price = Recieve_price_som;
                        recieve_price = DoubleToStr(recieve_price);
                        val_id = "1";
                        recieve_tan_price = Recieve_tan_som;
                    }
                    if (double.Parse(DoubleToStr(Recieve_price_dollar), CultureInfo.InvariantCulture) > 0)
                    {
                        recieve_price = Recieve_price_dollar;
                        recieve_price = DoubleToStr(recieve_price);
                        val_id = "2";
                        recieve_tan_price = Recieve_tan_dollar;
                    }
                    string queryProduct_id = "select * from product order by id desc limit 1";
                    DataTable tbProduct_id = new DataTable();
                    objDBAccess.readDatathroughAdapter(queryProduct_id, tbProduct_id);
                    if (tbProduct_id.Rows.Count == 0)
                    {
                        cmdUpdateProd = new MySqlCommand("insert into product (id,product_id,name,t_price,price,val_id,quantity,barcode,gurux, measurement, preparer, min_count) values(1,'" + Recieve_prodId + "','" + Recieve_name + "','" + DoubleToStr(recieve_tan_price) + "','" + DoubleToStr(recieve_price) + "','" + val_id + "','" + Recieve_quantity + "','" + Recieve_barcode + "','" + Recieve_group + "','" + Recieve_measurement + "','" + Recieve_preparer + "','" + Recieve_minCount + "')");
                        MessageBox.Show("insert into product (id,product_id,name,t_price,price,val_id,quantity,barcode,gurux, measurement, preparer, min_count) values(1,'" + Recieve_prodId + "','" + Recieve_name + "','" + DoubleToStr(recieve_tan_price) + "','" + DoubleToStr(recieve_price) + "','" + val_id + "','" + Recieve_quantity + "','" + Recieve_barcode + "','" + Recieve_group + "','" + Recieve_measurement + "','" + Recieve_preparer + "','" + Recieve_minCount + "')");
                        objDBAccess.executeQuery(cmdUpdateProd);
                    }
                    else
                    {
                        cmdUpdateProd = new MySqlCommand("insert into product (product_id,name,t_price,price,val_id,quantity,barcode,gurux, measurement, preparer, min_count) values('" + Recieve_prodId + "','" + Recieve_name + "','" + DoubleToStr(recieve_tan_price) + "','" + DoubleToStr(recieve_price) + "','" + val_id + "','" + Recieve_quantity + "','" + Recieve_barcode + "','" + Recieve_group + "','" + Recieve_measurement + "','" + Recieve_preparer + "','" + Recieve_minCount + "')");
                        objDBAccess.executeQuery(cmdUpdateProd);
                    }
                    tbProduct_id.Clear();
                    tbProduct_id.Dispose();
                }
                tbProductOne.Clear();
                tbProductOne.Dispose();
            }


            #region Sending farq_som, farq_dollar, after adding product to local DB
            /// <summary>
            /// The function to send post to add_recive
            /// </summary>
            try
            {
                string filial = MainWindow.filial_id;
                Uri u = new Uri("http://turonsavdo.backoffice.uz/api/productfilial/add_recive/");
                string payload = "{\"recieve\": \"" + recieve + "\",\"filial_id\": \"" + filial + "\", \"farq_som\": \"" + farq_som + "\", \"farq_dollar\": \"" + farq_dollar + "\"}";
                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");
                var t = Task.Run(() => PostURI(u, content));
                t.Wait();
                if (t.Result == "Error!")
                {
                    System.Windows.Forms.MessageBox.Show("Server bilan bog'lanishda xatolik, iltimos internetni tekshiring!", "Xatolik", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return;
                }

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Сообщение", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }

            #endregion

            System.Windows.Forms.MessageBox.Show("Faktura muvaffaqiyatli qabul qilindi!", "Xabar", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

            ShowRecieve();
        }

        public string DoubleToStr(string s)
        {


            if (s.IndexOf('.') > -1)
            {
                int index = s.IndexOf('.');
                string first = s.Substring(0, index);
                string last = s.Substring(index + 1);
                if (last == "0" || last == "00")
                {
                    s = first;
                }
                else
                {
                    s = first + "." + last;
                }
            }

            if (s.IndexOf(',') > -1)
            {
                int index = s.IndexOf(',');
                string first = s.Substring(0, index);
                string last = s.Substring(index + 1);
                if (last == "0" || last == "00")
                {
                    s = first;
                }
                else
                {
                    s = first + "." + last;
                }
            }



            return s;

        }

        static async Task<string> PostURI(Uri u, HttpContent c)
        {
            var response = string.Empty;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "token 249d4a8aa9ecf75844d87926b7b7ee4e1cd8b1da");
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

        public async void ShowRecieve()
        {
            try
            {
                if (MainWindow.Recieve) { tbFaktura.Clear(); }
                else
                {
                    tbFaktura = new DataTable();
                    tbFaktura.Columns.Add("id", typeof(int));
                    tbFaktura.Columns.Add("name");
                    tbFaktura.Columns.Add("date");
                    tbFaktura.Columns.Add("som");
                    tbFaktura.Columns.Add("sotish_som");
                    tbFaktura.Columns.Add("dollar");
                    tbFaktura.Columns.Add("sotish_dollar");
                }
                string urlFaktura = "http://turonsavdo.backoffice.uz/api/recieve/get_status_1/";
                string FakturaContent = await GetObject(urlFaktura);
                JArray arrayFaktura = JArray.Parse(FakturaContent);
                if (arrayFaktura != null)
                {
                    foreach (var item in arrayFaktura)
                    {
                        DataRow rowFaktura = tbFaktura.NewRow();
                        rowFaktura["id"] = item["id"];
                        rowFaktura["name"] = item["name"];
                        rowFaktura["date"] = item["date"];
                        rowFaktura["som"] = item["som"];
                        rowFaktura["sotish_som"] = item["sum_sotish_som"];
                        rowFaktura["dollar"] = item["dollar"];
                        rowFaktura["sotish_dollar"] = item["sum_sotish_dollar"];
                        tbFaktura.Rows.Add(rowFaktura);
                    }

                }

                tbFaktura.Dispose();
                dataGridFaktura.ItemsSource = Get_Faktura();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Сообщение", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }
        }

        private async void btnBekor_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://turonsavdo.backoffice.uz/");
                client.DefaultRequestHeaders.Add("Authorization", "token 249d4a8aa9ecf75844d87926b7b7ee4e1cd8b1da");
                Recieve selectedRecieve = dataGridFaktura.SelectedItems[0] as Recieve;
                string selectedRecieveId = selectedRecieve.id.ToString();
                string url = "api/recieve/" + selectedRecieveId + "/";
                var response = client.DeleteAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        if (MainWindow.Recieve) { tbFaktura.Clear(); }
                        else
                        {
                            tbFaktura = new DataTable();
                            tbFaktura.Columns.Add("id", typeof(int));
                            tbFaktura.Columns.Add("name");
                            tbFaktura.Columns.Add("date");
                            tbFaktura.Columns.Add("som");
                            tbFaktura.Columns.Add("sotish_som");
                            tbFaktura.Columns.Add("dollar");
                            tbFaktura.Columns.Add("sotish_dollar");
                        }
                        string urlFaktura = "http://turonsavdo.backoffice.uz/api/recieve/get_status_1/";
                        string FakturaContent = await GetObject(urlFaktura);
                        JArray arrayFaktura = JArray.Parse(FakturaContent);
                        if (arrayFaktura != null)
                        {
                            foreach (var item in arrayFaktura)
                            {
                                DataRow rowFaktura = tbFaktura.NewRow();
                                rowFaktura["id"] = item["id"];
                                rowFaktura["name"] = item["name"];
                                rowFaktura["date"] = item["date"];
                                rowFaktura["som"] = item["som"];
                                rowFaktura["sotish_som"] = item["sum_sotish_som"];
                                rowFaktura["dollar"] = item["dollar"];
                                rowFaktura["sotish_dollar"] = item["sum_sotish_dollar"];
                                tbFaktura.Rows.Add(rowFaktura);
                            }

                        }
                        tbFaktura.Dispose();
                        dataGridFaktura.ItemsSource = Get_Faktura();
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message, "Сообщение", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }
                    System.Windows.Forms.MessageBox.Show("Faktura bekor qilindi!", "Xabar", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Интeрнeт билан богланишни тeкширинг!", "Сообщение", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return;
                }
            }

            #region faktura cancel

            //try
            //{
            //    Recieve selectedFaktura = dataGridFaktura.SelectedItems[0] as Recieve;
            //    string selectedFakturaId = selectedFaktura.id.ToString();
            //    string url = "http://turonsavdo.backoffice.uz/api/faktura/otkaz/?fak=" + selectedFakturaId;
            //    string response = await GetObject(url);
            //    try
            //    {
            //        if (MainWindow.Recieve) { tbFaktura.Clear(); }
            //        else
            //        {
            //            tbFaktura = new DataTable();
            //            tbFaktura.Columns.Add("id", typeof(int));
            //            tbFaktura.Columns.Add("date");
            //            tbFaktura.Columns.Add("som");
            //            tbFaktura.Columns.Add("dollar");
            //        }
            //        string urlFaktura = "http://turonsavdo.backoffice.uz/api/faktura/st1/?fil=" + MainWindow.filial_id;
            //        string FakturaContent = await GetObject(urlFaktura);
            //        JArray arrayFaktura = JArray.Parse(FakturaContent);
            //        if (arrayFaktura != null)
            //        {
            //            foreach (var item in arrayFaktura)
            //            {
            //                DataRow rowFaktura = tbFaktura.NewRow();
            //                rowFaktura["id"] = item["id"];
            //                rowFaktura["date"] = item["date"];
            //                rowFaktura["som"] = item["som"];
            //                rowFaktura["dollar"] = item["dollar"];
            //                tbFaktura.Rows.Add(rowFaktura);
            //            }

            //        }

            //        tbFaktura.Dispose();
            //        dataGridFaktura.ItemsSource = Get_Faktura();
            //    }
            //    catch (Exception ex)
            //    {
            //        System.Windows.Forms.MessageBox.Show(ex.Message, "Сообщение", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            //        return;
            //    }
            //    System.Windows.Forms.MessageBox.Show("Faktura bekor qilindi!", "Xabar", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

            //}
            //catch (Exception ex)
            //{
            //    System.Windows.Forms.MessageBox.Show(ex.Message, "Сообщение", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            //    return;
            //}

            #endregion
        }
    }
}
