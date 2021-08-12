using System;
using System.Collections.Generic;
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
using System.Globalization;

namespace SotuvPlatformasi
{
    /// <summary>
    /// Interaction logic for ChangePrice_ucont.xaml
    /// </summary>
    public partial class ChangePrice_ucont : UserControl
    {
        public ChangePrice_ucont()
        {
            InitializeComponent();
        }
        public static MySqlCommand cmdShopId, cmdChangedPrice;
        public static DataTable tbProducts, tbChangedprice, tbPricecart, tbPrice;
        public static bool edit = false;
        public static int edit_id = 0;
        public static bool editPrice = false;
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
        public class PriceCart
        {
            public int id { get; set; }
            public int ch_id { get; set; }
            public string pr_name { get; set; }
            public double old_price { get; set; }
            public double new_price { get; set; }
            public double residue { get; set; }
            public double difference { get; set; }
            public double total_diff { get; set; }
            public string val_ul { get; set; }
        }
        List<PriceCart> priceCartList = new List<PriceCart>();
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

        public List<PriceCart> GetPriceCartList()
        {
            priceCartList = (from DataRow dr in tbPrice.Rows
                             select new PriceCart()
                             {
                                 id = Convert.ToInt32(dr["id"]),
                                 ch_id = Convert.ToInt32(dr["ch_id"]),
                                 pr_name = dr["pr_name"].ToString(),
                                 old_price = Convert.ToDouble(dr["old_price"]),
                                 new_price = Convert.ToDouble(dr["new_price"]),
                                 residue = Convert.ToDouble(dr["residue"]),
                                 difference = Convert.ToDouble(dr["difference"]),
                                 total_diff = Convert.ToDouble(dr["total_diff"]),
                                 val_ul = dr["val_ul"].ToString()
                             }).ToList();
            return priceCartList;
        }

        public void RefreshPrice()
        {
            DateTime dt_now = DateTime.Now;
            string dt_n = dt_now.ToString("yyyy-MM-dd");
            string queryPriceCart = "select pricecart.id, pricecart.ch_id, pricecart.pr_name, pricecart.old_price, pricecart.new_price, pricecart.residue, pricecart.difference, pricecart.total_diff, val.name as val_ul from pricecart " +
                "inner join changedprice on pricecart.ch_id = changedprice.id " +
                "inner join val on pricecart.val_id = val.id " +
                "where changedprice.date like '" + dt_n + "%'";
            try { tbPrice.Clear(); } catch (Exception) { }
            objDBAccess.readDatathroughAdapter(queryPriceCart, tbPrice);
            dataGridBasket.ItemsSource = GetPriceCartList();
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
                        tbProducts.Clear();
                        objDBAccess.readDatathroughAdapter(querySearch, tbProducts);
                        dataGridProduct.ItemsSource = GetProductList();
                        TbProduct.Visibility = Visibility.Visible;
                        Basket.Visibility = Visibility.Collapsed;
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

        private void dataGridBasket_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void dataGridProduct_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                if (edit == false)
                {
                    string query_changedId = "select id from changedprice order by id desc limit 1";
                    DataTable tbId = new DataTable();
                    objDBAccess.readDatathroughAdapter(query_changedId, tbId);
                    DateTime dt_now = DateTime.Now;
                    string dt_n = dt_now.ToString("yyyy-MM-dd");
                    if (tbId.Rows.Count == 0)
                    {
                        edit_id = 1;
                        edit = true;
                        cmdChangedPrice = new MySqlCommand("insert into changedprice (id,date,difference_som,difference_dollar) values(1,'" + dt_n + "',0,0)");
                        objDBAccess.executeQuery(cmdChangedPrice);
                    }
                    else
                    {
                        int nId = int.Parse(tbId.Rows[0]["id"].ToString(), CultureInfo.InvariantCulture);
                        string query_changeddif = "select * from changedprice where date like '" + dt_n + "%'";
                        DataTable tbCheck = new DataTable();
                        objDBAccess.readDatathroughAdapter(query_changeddif, tbCheck);
                        if (tbCheck.Rows.Count == 0)
                        {
                            nId += 1;
                            edit_id = nId;
                            edit = true;
                            cmdChangedPrice = new MySqlCommand("insert into changedprice (id,date,difference_som,difference_dollar) values('" + nId + "','" + dt_n + "',0,0)");
                            objDBAccess.executeQuery(cmdChangedPrice);
                        }
                        else
                        {
                            edit_id = nId;
                            edit = true;
                        }
                        tbCheck.Clear();
                        tbCheck.Dispose();
                    }
                    tbId.Clear();
                    tbId.Dispose();
                }
                EditPrice editPrice1 = new EditPrice();
                Product selectedProduct = dataGridProduct.SelectedItems[0] as Product;

                editPrice1.product = selectedProduct.name;
                editPrice1.old_price = selectedProduct.price.ToString();
                editPrice1.barcode = selectedProduct.barcode;
                editPrice1.edit_id = edit_id.ToString();
                editPrice1.val_ul = selectedProduct.val_ul;
                editPrice = false;
                editPrice1.ShowDialog();
                if (editPrice)
                {
                    RefreshPrice();
                }
                txtSearch.Clear();
                txtSearch.Focus();
            }
        }

        private void dataGridProduct_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            string queryProduct = "select product.product_id, product.name,product.t_price, product.price, val.name as val_ul, product.quantity, product.barcode, product.measurement, product.preparer from product " +
                "inner join val " +
                "on product.val_id = val.id limit 1";
            tbProducts = new DataTable();
            objDBAccess.readDatathroughAdapter(queryProduct, tbProducts);
            DateTime dt_now = DateTime.Now;
            
            string dt_n = dt_now.ToString("yyyy-MM-dd");
            string queryPriceCart = "select pricecart.id, pricecart.ch_id, pricecart.pr_name, pricecart.old_price, pricecart.new_price, pricecart.residue, pricecart.difference, pricecart.total_diff, val.name as val_ul from pricecart " +
                "inner join changedprice on pricecart.ch_id = changedprice.id " +
                "inner join val on pricecart.val_id = val.id " +
                "where changedprice.date like '" + dt_n + "%'"; 
            
            tbPrice = new DataTable();
            objDBAccess.readDatathroughAdapter(queryPriceCart, tbPrice);
            dataGridBasket.ItemsSource = GetPriceCartList();


        }

        private void BtnAsosiy_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = (MainWindow)Application.Current.MainWindow;
            main.TabMenu.SelectedIndex = 1;
        }

        private void btnPriceReport_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = (MainWindow)Application.Current.MainWindow;
            main.TabMenu.SelectedIndex = 16;
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (edit == false)
                {
                    string query_changedId = "select id from changedprice order by id desc limit 1";
                    DataTable tbId = new DataTable();
                    objDBAccess.readDatathroughAdapter(query_changedId, tbId);
                    DateTime dt_now = DateTime.Now;
                    string dt_n = dt_now.ToString("yyyy-MM-dd");
                    if (tbId.Rows.Count == 0)
                    {
                        edit_id = 1;
                        edit = true;
                        cmdChangedPrice = new MySqlCommand("insert into changedprice (id,date,difference_som) values(1,'" + dt_n + "',0)");
                        objDBAccess.executeQuery(cmdChangedPrice);
                    }
                    else
                    {
                        int nId = int.Parse(tbId.Rows[0]["id"].ToString(), CultureInfo.InvariantCulture);
                        string query_changeddif = "select * from changedprice where date like '" + dt_n + "%'";
                        DataTable tbCheck = new DataTable();
                        objDBAccess.readDatathroughAdapter(query_changeddif, tbCheck);
                        if (tbCheck.Rows.Count == 0)
                        {
                            nId += 1;
                            edit_id = nId;
                            edit = true;
                            cmdChangedPrice = new MySqlCommand("insert into changedprice (id,date,difference_som) values('" + nId + "','" + dt_n + "',0)");
                            objDBAccess.executeQuery(cmdChangedPrice);
                        }
                        else
                        {
                            edit_id = nId;
                            edit = true;
                        }
                        tbCheck.Clear();
                        tbCheck.Dispose();
                    }
                    tbId.Clear();
                    tbId.Dispose();
                }
                try
                {
                    EditPrice editPrice = new EditPrice();
                    Product selectedProduct = dataGridProduct.SelectedItems[0] as Product;

                    editPrice.product = selectedProduct.name;
                    editPrice.old_price = selectedProduct.price.ToString();
                    editPrice.barcode = selectedProduct.barcode;
                    editPrice.edit_id = edit_id.ToString();
                    editPrice.ShowDialog();
                    txtSearch.Clear();
                }
                catch (Exception) { }
            }
        }
    }
}
