using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using MySql.Data.MySqlClient;
using System.Net.Http;
using Newtonsoft.Json;

namespace SotuvPlatformasi
{
    /// <summary>
    /// Interaction logic for ReturnDeliver_ucont.xaml
    /// </summary>
    public partial class ReturnDeliver_ucont : UserControl
    {
        public ReturnDeliver_ucont()
        {
            InitializeComponent();
        }

        public static int deliver_id = 0, retsum_id = 0;
        public static string deliver_name = "Klientni tanlang...";
        public static string total_som = "0", total_dollar = "0";
        public static DataTable tbProducts;
        public static DataTable tbRetDelProduct, tbRetDelSumma;
        public static MySqlCommand cmd;
        DBAccess objDBAccess = new DBAccess();

        public class Product
        {
            public int product_id { get; set; }
            public string name { get; set; }
            public double t_price { get; set; }
            public double price { get; set; }
            public string val_ul { get; set; }
            public double quantity { get; set; }
            public string barcode { get; set; }
            public string measurement { get; set; }
            public string preparer { get; set; }
        }

        List<Product> productList = new List<Product>();

        public List<Product> GetProductList()
        {
            productList = (from DataRow dr in tbProducts.Rows
                           select new Product()
                           {
                               product_id = Convert.ToInt32(dr["product_id"]),
                               name = dr["name"].ToString(),
                               t_price = Convert.ToDouble(dr["t_price"]),
                               price = Convert.ToDouble(dr["price"]),
                               val_ul = dr["val_ul"].ToString(),
                               quantity = Convert.ToDouble(dr["quantity"]),
                               barcode = dr["barcode"].ToString(),
                               measurement = dr["measurement"].ToString(),
                               preparer = dr["preparer"].ToString()
                           }).ToList();

            return productList;
        }

        public class ReturnDeliverProduct
        {
            public int id { get; set; }
            public int retsum_id { get; set; }
            public string pr_name { get; set; }
            public string preparer { get; set; }
            public string measurement { get; set; }
            public double tan_narx { get; set; }
            public double sotish_narx { get; set; }
            public string val_ul { get; set; }
            public string barcode { get; set; }
            public double qayt_miqdor { get; set; }
        }
        List<ReturnDeliverProduct> retdelProductList = new List<ReturnDeliverProduct>();

        public List<ReturnDeliverProduct> GetRetDelProductList()
        {
            retdelProductList = (from DataRow dr in tbRetDelProduct.Rows
                                 select new ReturnDeliverProduct()
                                 {
                                     id = Convert.ToInt32(dr["id"]),
                                     retsum_id = Convert.ToInt32(dr["retsum_id"]),
                                     pr_name = dr["pr_name"].ToString(),
                                     preparer = dr["preparer"].ToString(),
                                     measurement = dr["measurement"].ToString(),
                                     tan_narx = Convert.ToDouble(dr["tan_narx"]),
                                     sotish_narx = Convert.ToDouble(dr["sotish_narx"]),
                                     val_ul = dr["val_ul"].ToString(),
                                     barcode = dr["barcode"].ToString(),
                                     qayt_miqdor = Convert.ToDouble(dr["qayt_miqdor"])
                                 }).ToList();
            
            return retdelProductList;
        }

        public string DoubleToStr(string s)
        {
            if (s.IndexOf(',') > -1)
            {
                int index = s.IndexOf(',');
                string first = s.Substring(0, index);
                string last = s.Substring(index + 1);
                s = first + "." + last;
            }
            return s;
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

        public class Item
        {
            public string barcode { get; set; }
            public double som { get; set; }
            public double dollar { get; set; }
            public double quantity { get; set; }
        }

        public class ReturnToDeliver
        {
            public int deliver { get; set; }
            public int filial { get; set; }
            public double som { get; set; }
            public double dollar { get; set; }
            public double kurs { get; set; }
            public IList<Item> items { get; set; }
        }

        public ReturnToDeliver returnToDeliver = new ReturnToDeliver();

        public void SetSomDollar()
        {
            string querySumSom = "select sum(tan_narx*qayt_miqdor) from retdelproduct " +
                "where retsum_id='" + retsum_id + "' and val_id=1";
            DataTable tbSumSom = new DataTable();
            objDBAccess.readDatathroughAdapter(querySumSom, tbSumSom);
            if(tbSumSom.Rows.Count > 0)
            {
                total_som = tbSumSom.Rows[0]["sum(tan_narx*qayt_miqdor)"].ToString();
                total_som = DoubleToStr(total_som);
                if(total_som == "" || total_som == null)
                {
                    total_som = "0";
                }
            }
            else
            {
                total_som = "0";
            }
            tbSumSom.Clear();
            tbSumSom.Dispose();

            string querySumDollar = "select sum(tan_narx*qayt_miqdor) from retdelproduct " +
                "where retsum_id='" + retsum_id + "' and val_id=2";
            DataTable tbSumDollar = new DataTable();
            objDBAccess.readDatathroughAdapter(querySumDollar, tbSumDollar);
            if (tbSumDollar.Rows.Count > 0) 
            {
                total_dollar = tbSumDollar.Rows[0]["sum(tan_narx*qayt_miqdor)"].ToString();
                total_dollar = DoubleToStr(total_dollar);
                if(total_dollar == "" || total_dollar == null)
                {
                    total_dollar = "0";
                }
            }
            else if(tbSumDollar.Rows.Count == 0)
            {
                total_dollar = "0";
            }
            tbSumDollar.Clear();
            tbSumDollar.Dispose();

        }

        public void SetReturnToDeliver()
        {
            returnToDeliver.deliver = deliver_id;
            returnToDeliver.filial = Convert.ToInt32(MainWindow.filial_id);
            SetSomDollar();
            
            returnToDeliver.som = double.Parse(total_som, CultureInfo.InvariantCulture);
            
            returnToDeliver.dollar = double.Parse(total_dollar, CultureInfo.InvariantCulture);

            if(txtKurs.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("Kursni kiriting!", "Xatolik", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }
            returnToDeliver.kurs = double.Parse(txtKurs.Text, CultureInfo.InvariantCulture);

            List<Item> itemList = new List<Item>();

            for (int i = 0; i<tbRetDelProduct.Rows.Count; i++)
            {
                Item item = new Item();
                item.barcode = tbRetDelProduct.Rows[i]["barcode"].ToString();
                
                if(tbRetDelProduct.Rows[i]["val_ul"].ToString() == "uz")
                {
                    item.som = Convert.ToDouble(tbRetDelProduct.Rows[i]["tan_narx"]);
                    item.dollar = 0;
                }
                else
                {
                    item.dollar = Convert.ToDouble(tbRetDelProduct.Rows[i]["tan_narx"]);
                    item.som = 0;
                }
                item.quantity = Convert.ToDouble(tbRetDelProduct.Rows[i]["qayt_miqdor"]);
                itemList.Add(item);
            }

            returnToDeliver.items = itemList;
        }

        public int GetId(string SqlTable)
        {
            int id = 0;
            string query = "select id from " + SqlTable + " order by id desc limit 1";
            DataTable tb = new DataTable();
            objDBAccess.readDatathroughAdapter(query, tb);
            if(tb.Rows.Count > 0)
            {
                id = Convert.ToInt32(tb.Rows[0]["id"]) + 1;
            }
            else
            {
                id = 1;
            }
            tb.Clear();
            tb.Dispose();
            return id;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if(txtDeliver.Text=="Klientni tanlang...")
            {
                System.Windows.Forms.MessageBox.Show("Klientni tanlang...", "Xatolik", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return; 
            }
            if(btnCreate.IsEnabled == true)
            {
                int id = GetId("retdelsumma");
                string dt_now = DateTime.Now.ToString("yyyy-MM-dd");
                cmd = new MySqlCommand("insert into retdelsumma (id, date, som, dollar, deliver, deliver_id,status,kurs) values('" + id + "','" + dt_now + "',0,0,'"+txtDeliver.Text+"','"+deliver_id+"',0,0)");
                objDBAccess.executeQuery(cmd);
                cmd.Dispose();
                retsum_id = id;
                string queryRetDelProduct = "select retdelproduct.id, retdelproduct.retsum_id, retdelproduct.pr_name, retdelproduct.preparer, retdelproduct.measurement, retdelproduct.tan_narx, retdelproduct.sotish_narx, val.name as val_ul, retdelproduct.barcode, retdelproduct.qayt_miqdor from retdelproduct " +
                    "inner join val on val.id = retdelproduct.val_id " +
                    "inner join retdelsumma on retdelproduct.retsum_id = retdelsumma.id " +
                    "where retdelproduct.retsum_id =" + retsum_id;
                tbRetDelProduct = new DataTable();
                objDBAccess.readDatathroughAdapter(queryRetDelProduct, tbRetDelProduct);
                dataGridBasket.ItemsSource = GetRetDelProductList();
                System.Windows.Forms.MessageBox.Show("Tovar qaytarish muvaffaqiyatli yaratildi!", "Xabar", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                btnCreate.IsEnabled = false;
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearch.Text.Length > 2)
            {
                string querySearch = "";
                try
                {
                    double number = double.Parse(txtSearch.Text, CultureInfo.InvariantCulture);
                    if (txtSearch.Text.Length > 5)
                    {
                        querySearch = "select product.product_id, product.name,product.t_price, product.price, val.name as val_ul, product.quantity, product.barcode, product.measurement, product.preparer from product " +
                            "inner join val " +
                            "on product.val_id = val.id " +
                            "where product.barcode LIKE'%" + txtSearch.Text + "%'";
                        tbProducts = new DataTable();
                        objDBAccess.readDatathroughAdapter(querySearch, tbProducts);
                        dataGridProduct.ItemsSource = GetProductList();
                        TbProduct.Visibility = Visibility.Visible;
                        Basket.Visibility = Visibility.Collapsed;
                        tbProducts.Dispose();


                    }
                }
                catch (Exception)
                {
                    querySearch = "select product.product_id, product.name,product.t_price, product.price, val.name as val_ul, product.quantity, product.barcode, product.measurement, product.preparer from product " +
                        "inner join val " +
                        "on product.val_id = val.id " +
                        "where product.name LIKE'%" + txtSearch.Text + "%'";
                    tbProducts = new DataTable();
                    objDBAccess.readDatathroughAdapter(querySearch, tbProducts);
                    dataGridProduct.ItemsSource = GetProductList();
                    TbProduct.Visibility = Visibility.Visible;
                    Basket.Visibility = Visibility.Collapsed;
                    tbProducts.Dispose();
                }
            }
            else
            {
                //dbgridProducts.Visible = false;
                objDBAccess.createConn();
                string queryProduct = "select product.product_id, product.name,product.t_price, product.price, val.name as val_ul, product.quantity, product.barcode, product.measurement, product.preparer from product " +
                    "inner join val " +
                    "on product.val_id = val.id limit 1";
                tbProducts = new DataTable();
                objDBAccess.readDatathroughAdapter(queryProduct, tbProducts);
                TbProduct.Visibility = Visibility.Collapsed;
                Basket.Visibility = Visibility.Visible;
                tbProducts.Dispose();

            }
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (TbProduct.Visibility == Visibility.Visible)
            {
                if (e.Key == Key.Down)
                {
                    dataGridProduct.SelectedIndex = 0;
                    var u = e.OriginalSource as UIElement;
                    e.Handled = true;
                    u.MoveFocus(new TraversalRequest(FocusNavigationDirection.Down));

                }
            }

            if (TbProduct.Visibility == Visibility.Collapsed)
            {
                if (e.Key == Key.Down)
                {
                    dataGridBasket.SelectedIndex = 0;
                    var u = e.OriginalSource as UIElement;
                    e.Handled = true;
                    u.MoveFocus(new TraversalRequest(FocusNavigationDirection.Down));

                }

            }
        }

        private void BtnAsosiy_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = (MainWindow)Application.Current.MainWindow;
            main.TabMenu.SelectedIndex = 1;
        }

        private void btnChoose_Click(object sender, RoutedEventArgs e)
        {
            Deliver deliver = new Deliver();
            deliver.ShowDialog();
            txtDeliver.Text = deliver_name;
        }

        private void dataGridBasket_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Delete)
            {
                int id = 0;
                ReturnDeliverProduct selected = dataGridBasket.SelectedItems[0] as ReturnDeliverProduct;
                id = selected.id;
                cmd = new MySqlCommand("delete from retdelproduct where id='" + id + "'");
                objDBAccess.executeQuery(cmd);
                cmd.Dispose();
                RefreshRetBasket();
            }
        }

        private void btnReturnReport_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = (MainWindow)Application.Current.MainWindow;
            main.TabMenu.SelectedIndex = 18;
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            SetReturnToDeliver();
            string s = JsonConvert.SerializeObject(returnToDeliver);
            Uri u = new Uri("http://turonsavdo.backoffice.uz/api/returnproducttodeliveritem/add/");

            HttpContent content = new StringContent(s, Encoding.UTF8, "application/json");
            var t = Task.Run(() => PostURI(u, content));
            t.Wait();
            if (t.Result != "Error!" && t.Result.Length != 0)
            {
                string Responce = t.Result;
                cmd = new MySqlCommand("update retdelsumma set som='" + total_som + "', dollar='" + total_dollar + "',kurs='"+DoubleToStr(txtKurs.Text)+"',status=1 where id='" + retsum_id + "'");
                objDBAccess.executeQuery(cmd);
                cmd.Dispose();
                retsum_id = 0;
                total_dollar = "0";
                total_som = "0";
                deliver_id = 0;
                deliver_name = "Klientni tanlang...";
                txtDeliver.Text = deliver_name;
                btnCreate.IsEnabled = true;

                string queryProduct = "", pr_quan="", delret_quan =""; 
                DataTable tbPr = new DataTable();
                for(int i = 0; i<tbRetDelProduct.Rows.Count; i++)
                {
                    delret_quan = tbRetDelProduct.Rows[i]["qayt_miqdor"].ToString();
                    delret_quan = DoubleToStr(delret_quan);
                    queryProduct = "select quantity from product where barcode='" + tbRetDelProduct.Rows[i]["barcode"] + "'";
                    objDBAccess.readDatathroughAdapter(queryProduct,tbPr);
                    pr_quan = tbPr.Rows[0]["quantity"].ToString();
                    pr_quan = DoubleToStr(pr_quan);
                    double Dpr_quan = double.Parse(pr_quan, CultureInfo.InvariantCulture);
                    double Ddelret_quan = double.Parse(delret_quan, CultureInfo.InvariantCulture);
                    double result_quan = Dpr_quan - Ddelret_quan;
                    cmd = new MySqlCommand("update product set quantity='" + DoubleToStr(result_quan.ToString()) + "' where barcode='" + tbRetDelProduct.Rows[i]["barcode"] + "'");
                    objDBAccess.executeQuery(cmd);
                    cmd.Dispose();
                    tbPr.Clear();

                }

                tbPr.Dispose();
                System.Windows.Forms.MessageBox.Show("Tovar muvaffaqiyatli qaytarildi", "Xabar", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                tbRetDelProduct.Clear();
                dataGridBasket.ItemsSource = GetRetDelProductList();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Server bilan bog'lanishda xatolik, iltimos internetni tekshiring!", "Xatolik", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

            }
        }

        public static bool ReturnBasket = false;

        private void dataGridProduct_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                e.Handled = true;
                if(btnCreate.IsEnabled == true)
                {
                    System.Windows.Forms.MessageBox.Show("Tovar qaytarish yaratilmagan!", "Xatolik", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return;
                }

                if (dataGridProduct.Items.Count == 0) return;

                Product selectedProduct = dataGridProduct.SelectedItems[0] as Product;
                ReturnBasket returnBasket = new ReturnBasket();
                returnBasket.name = selectedProduct.name;
                returnBasket.zavod = selectedProduct.preparer;
                returnBasket.ulchov = selectedProduct.val_ul;
                returnBasket.tan_narx = selectedProduct.t_price.ToString();
                returnBasket.sot_narx = selectedProduct.price.ToString();
                returnBasket.val_ul = selectedProduct.val_ul;
                returnBasket.retsum_id = retsum_id.ToString();
                returnBasket.barcode = selectedProduct.barcode;
                returnBasket.quantity = selectedProduct.quantity.ToString();
                ReturnBasket = false;
                returnBasket.ShowDialog();
                if(ReturnBasket)
                {
                    RefreshRetBasket();
                }
                txtSearch.Clear();
                txtSearch.Focus();

            }
        }

        public void RefreshRetBasket()
        {
            string dt_now = DateTime.Now.ToString("yyyy-MM-dd");
            string queryRetDelProduct = "select retdelproduct.id, retdelproduct.retsum_id, retdelproduct.pr_name, retdelproduct.preparer, retdelproduct.measurement, retdelproduct.tan_narx, retdelproduct.sotish_narx, val.name as val_ul, retdelproduct.barcode, retdelproduct.qayt_miqdor from retdelproduct " +
                "inner join val on val.id = retdelproduct.val_id " +
                "inner join retdelsumma on retdelproduct.retsum_id = retdelsumma.id " +
                "where retdelsumma.date='" + dt_now + "' and retdelsumma.status=0";
            tbRetDelProduct.Clear();
            objDBAccess.readDatathroughAdapter(queryRetDelProduct, tbRetDelProduct);
            dataGridBasket.ItemsSource = GetRetDelProductList();
        }

        private void dataGridProduct_KeyDown(object sender, KeyEventArgs e)
        {
            //
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            string dt_now = DateTime.Now.ToString("yyyy-MM-dd");
            string queryRetDelSumma = "select * from retdelsumma where date='" + dt_now + "' and status=0";
            tbRetDelSumma = new DataTable();
            objDBAccess.readDatathroughAdapter(queryRetDelSumma, tbRetDelSumma);

            if(tbRetDelSumma.Rows.Count > 0)
            {
                txtDeliver.Text = tbRetDelSumma.Rows[0]["deliver"].ToString();
                deliver_id = Convert.ToInt32(tbRetDelSumma.Rows[0]["deliver_id"].ToString());
                deliver_name = tbRetDelSumma.Rows[0]["deliver"].ToString();
                string queryRetSumProduct = "select retdelproduct.id, retdelproduct.retsum_id, retdelproduct.pr_name, retdelproduct.preparer, retdelproduct.measurement, retdelproduct.tan_narx, retdelproduct.sotish_narx, val.name as val_ul, retdelproduct.barcode, retdelproduct.qayt_miqdor from retdelproduct " +
                    "inner join val on val.id = retdelproduct.val_id " +
                    "inner join retdelsumma on retdelproduct.retsum_id = retdelsumma.id " +
                    "where retdelsumma.id='" + tbRetDelSumma.Rows[0]["id"] + "'";
                tbRetDelProduct = new DataTable();
                objDBAccess.readDatathroughAdapter(queryRetSumProduct, tbRetDelProduct);
                dataGridBasket.ItemsSource = GetRetDelProductList();
                retsum_id = Convert.ToInt32(tbRetDelSumma.Rows[0]["id"].ToString());
                btnCreate.IsEnabled = false;
                tbRetDelProduct.Dispose();
            }
            tbRetDelSumma.Dispose();

            try
            {
                string queryValyuta = "select kurs from valyuta";
                DataTable tbValyuta = new DataTable();
                objDBAccess.readDatathroughAdapter(queryValyuta, tbValyuta);
                txtKurs.Text = tbValyuta.Rows[0]["kurs"].ToString();
                tbValyuta.Clear();
                tbValyuta.Dispose();
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

            }

        }

        private void UserControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                txtSearch.Focus();
            }
        }
    }
}
