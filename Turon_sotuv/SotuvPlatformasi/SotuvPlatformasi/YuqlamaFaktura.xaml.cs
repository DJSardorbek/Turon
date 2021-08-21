using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Text.Json;
using System.Net.Http;
using MySql.Data.MySqlClient;

namespace SotuvPlatformasi
{
    /// <summary>
    /// Interaction logic for YuqlamaFaktura.xaml
    /// </summary>
    public partial class YuqlamaFaktura : UserControl
    {
        public YuqlamaFaktura()
        {
            InitializeComponent();
            
            string queryProduct = "select product.product_id, product.name,product.t_price, product.price, val.name as val_ul, product.quantity, product.barcode, product.measurement, product.preparer from product " +
                "inner join val " +
                "on product.val_id = val.id limit 1";
            tbProducts = new DataTable();
            objDBAccess.readDatathroughAdapter(queryProduct, tbProducts);
            dataGridProduct.ItemsSource = GetProductList();
            tbProducts.Dispose();

            tbYuqFaktura = new DataTable();
            tbYuqFaktura.Columns.Add("product_id", typeof(int));
            tbYuqFaktura.Columns.Add("pr_name");
            tbYuqFaktura.Columns.Add("quantity", typeof(double));
            tbYuqFaktura.Columns.Add("barcode");
            tbYuqFaktura.Dispose();
            dataGridBasket.ItemsSource = GetYuqFakturaList();
        }
        
        DBAccess objDBAccess = new DBAccess();
        public static DataTable tbProducts;
        public static DataTable tbYuqFaktura;
        public static string product_id = "", pr_name = "", quantity = "", barcode = "";
        public static MySqlCommand cmd;

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

        public class YuqFaktura
        {
            public int product_id { get; set; }
            public string pr_name { get; set; }
            public double quantity { get; set; }
            public string barcode { get; set; }
        }

        public class Item
        {
            public string barcode { get; set; }
            public double quantity { get; set; }
        }

        public class YuqFakturaSubmit
        {
            public int filial { get; set; }
            public IList<Item> item { get; set; }
        }

        YuqFakturaSubmit submit = new YuqFakturaSubmit();
        List<YuqFaktura> yuqFakturaList = new List<YuqFaktura>();
        List<Item> yuqSubmitList = new List<Item>();

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

        public List<YuqFaktura> GetYuqFakturaList()
        {
            yuqFakturaList = (from DataRow dr in tbYuqFaktura.Rows
                              select new YuqFaktura()
                              {
                                  product_id = Convert.ToInt32(dr["product_id"]),
                                  pr_name = dr["pr_name"].ToString(),
                                  quantity = Convert.ToDouble(dr["quantity"]),
                                  barcode = dr["barcode"].ToString()
                              }).ToList();

            return yuqFakturaList;
        }

        public List<Item> GetSubmitList()
        {
            yuqSubmitList = (from DataRow dr in tbYuqFaktura.Rows
                              select new Item()
                              {
                                  quantity = Convert.ToDouble(dr["quantity"]),
                                  barcode = dr["barcode"].ToString()
                              }).ToList();

            return yuqSubmitList;
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

        public void RefreshBasket()
        {
            tbYuqFaktura.Rows.Add(Convert.ToInt32(product_id), pr_name, Convert.ToDouble(quantity), barcode);
            dataGridBasket.ItemsSource = GetYuqFakturaList();
            product_id = ""; pr_name = ""; quantity = ""; barcode = "";
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearch.Text.Length >= 2)
            {
                string querySearch = "";
                try
                {
                    double number = double.Parse(txtSearch.Text, CultureInfo.InvariantCulture);
                    if (txtSearch.Text.Length >= 2)
                    {
                        querySearch = "select product.product_id, product.name,product.t_price, product.price, val.name as val_ul, product.quantity, product.barcode, product.measurement, product.preparer from product " +
                            "inner join val " +
                            "on product.val_id = val.id " +
                            "where product.barcode LIKE'%" + txtSearch.Text + "%'";
                        tbProducts.Clear();
                        objDBAccess.readDatathroughAdapter(querySearch, tbProducts);
                        dataGridProduct.ItemsSource = GetProductList();
                        TbProduct.Visibility = Visibility.Visible;
                        Basket.Visibility = Visibility.Collapsed;
                        if (tbProducts.Rows.Count == 0)
                        {
                            querySearch = "select product.product_id, product.name,product.t_price, product.price, val.name as val_ul, product.quantity, product.barcode, product.measurement, product.preparer from product " +
                        "inner join val " +
                        "on product.val_id = val.id " +
                        "where product.name LIKE'%" + txtSearch.Text + "%'";
                            tbProducts.Clear();
                            objDBAccess.readDatathroughAdapter(querySearch, tbProducts);
                            dataGridProduct.ItemsSource = GetProductList();
                            TbProduct.Visibility = Visibility.Visible;
                            Basket.Visibility = Visibility.Collapsed;
                        }

                    }
                }
                catch (Exception)
                {
                    querySearch = "select product.product_id, product.name,product.t_price, product.price, val.name as val_ul, product.quantity, product.barcode, product.measurement, product.preparer from product " +
                        "inner join val " +
                        "on product.val_id = val.id " +
                        "where product.name LIKE'%" + txtSearch.Text + "%'";
                    tbProducts.Clear();
                    objDBAccess.readDatathroughAdapter(querySearch, tbProducts);
                    dataGridProduct.ItemsSource = GetProductList();
                    TbProduct.Visibility = Visibility.Visible;
                    Basket.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                //dbgridProducts.Visible = false;
                objDBAccess.createConn();
                string queryProduct = "select product.product_id, product.name,product.t_price, product.price, val.name as val_ul, product.quantity, product.barcode, product.measurement, product.preparer from product " +
                    "inner join val " +
                    "on product.val_id = val.id limit 1";
                tbProducts.Clear();
                objDBAccess.readDatathroughAdapter(queryProduct, tbProducts);
                TbProduct.Visibility = Visibility.Collapsed;
                Basket.Visibility = Visibility.Visible;
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

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                submit.filial = Convert.ToInt32(MainWindow.filial_id);
                submit.item = GetSubmitList();
                
                var json = System.Text.Json.JsonSerializer.Serialize(submit);
                Uri u = new Uri("http://turonsavdo.backoffice.uz/api/fakturaitem/add_kamomad/");
                HttpContent content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
                var t = Task.Run(() => PostURI(u, content));
                t.Wait();
                if (t.Result == "Error!")
                {
                    System.Windows.Forms.MessageBox.Show("Server bilan bog'lanishda xatolik, iltimos internetni tekshiring!", "Xatolik", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    DataTable tbUpdateProduct = new DataTable();
                    string queryUpdateProduct = "";
                    foreach (DataRow item in tbYuqFaktura.Rows)
                    {
                        queryUpdateProduct = "select quantity from product where barcode='" + item["barcode"] + "'";
                        objDBAccess.readDatathroughAdapter(queryUpdateProduct, tbUpdateProduct);
                        if(tbUpdateProduct.Rows.Count != 0)
                        {
                            cmd = new MySqlCommand("update product set quantity='" + item["quantity"] + "' where barcode='" + item["barcode"] + "'");
                            objDBAccess.executeQuery(cmd);
                            cmd.Dispose();
                        }
                    }
                    tbUpdateProduct.Dispose();
                    tbYuqFaktura.Clear();
                    dataGridBasket.ItemsSource = GetYuqFakturaList();
                    System.Windows.MessageBox.Show("Faktura yo'qlamasi muvaffaqiyatli yakunlandi!", "Xabar", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        private void UserControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                txtSearch.Focus();
            }
        }

        public static bool Yadd = false;

        
        private void dataGridProduct_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                Product selectedProduct = dataGridProduct.SelectedItems[0] as Product;
                Yadd = false;
                YFakturaAdd yFakturaAdd = new YFakturaAdd();
                yFakturaAdd.pr_name = selectedProduct.name;
                yFakturaAdd.barcode = selectedProduct.barcode;
                yFakturaAdd.quantity = selectedProduct.quantity.ToString();
                product_id = selectedProduct.product_id.ToString();
                yFakturaAdd.ShowDialog();

                if (Yadd)
                {
                    RefreshBasket();
                    Yadd = false;
                }

                txtSearch.Text = "";
                txtSearch.Focus();
            }
        }
    }
}
