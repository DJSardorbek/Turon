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
using MySql.Data.MySqlClient;
using System.Data;
using System.Globalization;
using DGVPrinterHelper;
using System.Drawing.Printing;
using System.Drawing;

namespace SotuvPlatformasi
{
    /// <summary>
    /// Interaction logic for Sotuv_ucont.xaml
    /// </summary>
    public partial class Sotuv_ucont : UserControl
    {
        public Sotuv_ucont()
        {
            valyuta_types = new List<string>();
            valyuta_types.Add("uz");
            valyuta_types.Add("$");

            InitializeComponent();

            ComboValyuta.ItemsSource = valyuta_types;
        }

        public static string header = "";
        public static string footer = "";
        public static string barcode = "";
        public static string price = "";
        public static string oldquantity = "";
        public static string product_id = "";
        public static string name = "",val_ul="",kurs="", quantity ="",debtor_id="1";
        public static bool shop = false, navbat=false;
        public static int shopID = 0;
        public static MySqlConnection con;
        public static MySqlDataAdapter adapterProduct, adapterBasket;
        public static MySqlCommand cmd, cmdCart, cmdShop, command;
        public static DataTable tbProducts, tbBasket, tbID, tbTulovSom, tbTulovDollar, tbGuruh, tbVal;
        public static DataTable tbShopId;

        public class Cart
        {
            public int id { get; set; }
            public int product_id { get; set; }
            public string name { get; set; }
            public double price { get; set; }
            public double quantity { get; set; }
            public double total { get; set; }
            public string val { get; set; }
        }

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

        List<Cart> cartList = new List<Cart>();
        List<Product> productList = new List<Product>();
        public List<string> valyuta_types { get; set; }

        public string total_all = "0"; 
        


        public List<Cart> GetCartList()
        {
            cartList = (from DataRow dr in tbBasket.Rows
                        select new Cart()
                        {
                            id = Convert.ToInt32(dr["id"]),
                            product_id = Convert.ToInt32(dr["product_id"]),
                            name = dr["name"].ToString(),
                            price = Convert.ToDouble(dr["price"]),
                            quantity = Convert.ToDouble(dr["quantity"]),
                            total = Convert.ToDouble(dr["total"]),
                            val =  dr["val"].ToString() 
                        }).ToList();

            return cartList;
        }

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

        public void SetTotal()
        {
            string queryTulovSom = "select sum(total) from cart inner join shopId on cart.shop_id=shopId.shop_id where cart.shop_id='" + shopID + "' and shopId.mac_address='" + Kirish_ucont.mac_address + "' and cart.val_id=1";
            tbTulovSom = new DataTable();
            objDBAccess.readDatathroughAdapter(queryTulovSom, tbTulovSom);
            if (tbTulovSom.Rows.Count > 0)
            {
                total_all = tbTulovSom.Rows[0]["sum(total)"].ToString();
                txtTulovSom.Text = total_all.ToString();
            }
            tbTulovSom.Dispose();

            string queryTulovDollar = "select sum(total) from cart inner join shopId on cart.shop_id=shopId.shop_id where cart.shop_id='" + shopID + "' and shopId.mac_address='" + Kirish_ucont.mac_address + "' and cart.val_id=2";
            tbTulovSom = new DataTable();
            objDBAccess.readDatathroughAdapter(queryTulovDollar, tbTulovSom);
            if (tbTulovSom.Rows.Count > 0)
            {
                total_all = tbTulovSom.Rows[0]["sum(total)"].ToString();
                txtTulovDollar.Text = total_all.ToString();
            }
            tbTulovSom.Dispose();
        }


        public void Refresh()
        {
            txtSearch.Clear();
            string queryProduct = "select product.product_id, product.name, product.t_price, product.price, val.name as val_ul, product.quantity, product.barcode, product.measurement, product.preparer from product " +
                "inner join val " +
                "on product.val_id = val.id limit 1";
            string queryCart = "select cart.id, cart.product_id, cart.name, cart.price, cart.quantity , cart.total, val.name as val from cart " +
                "inner join shop on cart.shop_id=shop.id " +
                "inner join shopId on shopId.shop_id=cart.shop_id " +
                "inner join val on cart.val_id = val.id " +
                "where shop.id='" + shopID + "' and shopId.mac_address='" + Kirish_ucont.mac_address + "' order by cart.id";
            tbProducts.Clear();
            tbBasket.Clear();
            objDBAccess.readDatathroughAdapter(queryProduct, tbProducts);
            objDBAccess.readDatathroughAdapter(queryCart, tbBasket);

            dataGridBasket.ItemsSource = GetCartList();
            dataGridProduct.ItemsSource = GetProductList();
            SetTotal();

            objDBAccess.closeConn();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            objDBAccess.createConn();
            string queryCart = "select cart.id,cart.product_id, cart.name, cart.price, cart.quantity, cart.total, val.name as val from cart " +
                "inner join shop on cart.shop_id=shop.id " +
                "inner join shopId on shopId.shop_id=cart.shop_id " +
                "inner join val on cart.val_id = val.id " +
                "where shop.id='" + shopID + "' and shopId.mac_address='" + Kirish_ucont.mac_address + "' order by cart.id";
            tbBasket = new DataTable();
            objDBAccess.readDatathroughAdapter(queryCart, tbBasket);

            string queryProduct = "select product.product_id, product.name,product.t_price, product.price, val.name as val_ul, product.quantity, product.barcode, product.measurement, product.preparer from product " +
                "inner join val " +
                "on product.val_id = val.id limit 1";
            tbProducts = new DataTable();
            objDBAccess.readDatathroughAdapter(queryProduct, tbProducts);
            dataGridProduct.ItemsSource = GetProductList();
            tbProducts.Dispose();



            dataGridBasket.ItemsSource = GetCartList();
            SetTotal();
            objDBAccess.closeConn();
            tbBasket.Dispose();

            string queryKurs = "select * from valyuta";
            tbVal = new DataTable();
            objDBAccess.readDatathroughAdapter(queryKurs, tbVal);
            txtKurs.Text = tbVal.Rows[0]["kurs"].ToString();

            header = MainWindow.header;
            footer = MainWindow.footer;
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
                        tbProducts = new DataTable();
                        objDBAccess.readDatathroughAdapter(querySearch, tbProducts);
                        dataGridProduct.ItemsSource = GetProductList();
                        TbProduct.Visibility = Visibility.Visible;
                        Basket.Visibility = Visibility.Collapsed;
                        tbProducts.Dispose();
                        if(tbProducts.Rows.Count ==0)
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

        public void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (tbProducts.Rows.Count == 0) return;
                //insertion new record to shop which has not been there before
                dataGridProduct.SelectedIndex = 0;
                if (shop == false)
                {
                    string queryShopID = "select id from shop order by id desc limit 1";
                    tbID = new DataTable();
                    objDBAccess.readDatathroughAdapter(queryShopID, tbID);
                    shopID = 0;
                    if (tbID.Rows.Count == 1)
                    {
                        shopID = int.Parse(tbID.Rows[0]["id"].ToString());
                        shopID = shopID + 1;
                    }
                    else { shopID = 1; }
                    shop = true;
                    DateTime dt = DateTime.Now;
                    cmdShop = new MySqlCommand("insert into shop values('" + shopID + "',0,0,0,0,0, '" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',0,0,0,0,0, '" + Kirish_ucont.sellar_id + "')");
                    objDBAccess.executeQuery(cmdShop);
                    //Yaratilgan yangi shopId ni shopId jadvaliga yozamiz
                    string queryShopId = "select * from shopId where password='" + Kirish_ucont.password1 + "'";
                    tbShopId = new DataTable();
                    objDBAccess.readDatathroughAdapter(queryShopId, tbShopId);
                    if (tbShopId.Rows.Count > 0)
                    {
                        cmdShopId = new MySqlCommand("update shopId set shop_id='" + shopID + "' where password='" + Kirish_ucont.password1 + "'");
                        objDBAccess.executeQuery(cmdShopId);
                    }
                    else
                    {
                        string queryId = "select * from shopId order by id desc limit 1";
                        DataTable tbSId = new DataTable();
                        objDBAccess.readDatathroughAdapter(queryId, tbSId);
                        if (tbSId.Rows.Count == 0)
                        {
                            cmdShopId = new MySqlCommand("insert into shopId values(1,'" + shopID + "','" + Kirish_ucont.mac_address + "','" + Kirish_ucont.password1 + "')");
                            objDBAccess.executeQuery(cmdShopId);
                        }
                        else
                        {
                            cmdShopId = new MySqlCommand("insert into shopId (shop_id,mac_address,password) values('" + shopID + "','" + Kirish_ucont.mac_address + "','" + Kirish_ucont.password1 + "')");
                            objDBAccess.executeQuery(cmdShopId);
                        }
                        try
                        {
                            tbSId.Clear();
                            tbSId.Dispose();
                        }
                        catch (Exception) { }
                    }
                    tbShopId.Clear();
                    tbShopId.Dispose();
                    tbID.Clear();
                    cmdShop.Dispose();
                    tbID.Dispose();
                }

                //Getting properties from selected product\
                Product selectedProduct = dataGridProduct.SelectedItems[0] as Product;
                price = selectedProduct.price.ToString();
                barcode = selectedProduct.barcode;
                oldquantity = selectedProduct.quantity.ToString();
                if (oldquantity == "0")
                {
                    System.Windows.Forms.MessageBox.Show("Ushbu maxsulot tugagan!", "Xabar", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    TbProduct.Visibility = Visibility.Collapsed;
                    Basket.Visibility = Visibility.Visible;
                    //Refresh();
                    return;
                }
                product_id = selectedProduct.product_id.ToString();
                name = selectedProduct.name;
                quantity = selectedProduct.quantity.ToString();
                can_refresh = false;
                Korzinka f3 = new Korzinka();
                f3.barcode = barcode;
                f3.price = price;
                f3.oldquantity = oldquantity;
                f3.product_id = product_id;
                f3.name = name;
                f3.quantity = quantity;
                f3.ShowDialog();
                if (can_refresh)
                {
                    Refresh();

                }
                txtSearch.Clear();
                TbProduct.Visibility = Visibility.Collapsed;
                Basket.Visibility = Visibility.Visible;
                txtSearch.Focus();
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

            if(TbProduct.Visibility == Visibility.Collapsed)
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

        private void dataGridProduct_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void dataGridProduct_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                e.Handled = true;
                btnCart_Click(sender, e); 

            }
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void UserControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                txtSearch.Focus();
            }
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

        private void dataGridBasket_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (tbBasket.Rows.Count == 0) return;
            if (e.Key == Key.Add) //Plus
            {
                Cart existingCart = dataGridBasket.SelectedItems[0] as Cart;
                int CurrentItem = dataGridBasket.SelectedIndex;
                DataTable tbQuantityProduct = new DataTable();
                string queryQuantityProduct = "select * from product where product_id='" + existingCart.product_id + "'";
                objDBAccess.readDatathroughAdapter(queryQuantityProduct, tbQuantityProduct);
                string quantityProduct = tbQuantityProduct.Rows[0]["quantity"].ToString(); // Product jadvalidan maxsulot sonini olamiz
                quantityProduct = DoubleToStr(quantityProduct);
                double DquantityProduct = double.Parse(quantityProduct, CultureInfo.InvariantCulture);
                tbQuantityProduct.Clear();
                tbQuantityProduct.Dispose();
                if (DquantityProduct >= 1)
                {
                    string quantityBasket = existingCart.quantity.ToString();
                    string price = existingCart.price.ToString();
                    price = DoubleToStr(price);
                    double Dprice = double.Parse(price, CultureInfo.InvariantCulture);
                    quantityBasket = DoubleToStr(quantityBasket);
                    double DquantityBasket = double.Parse(quantityBasket, CultureInfo.InvariantCulture);
                    DquantityBasket += 1;

                    string total = existingCart.total.ToString();
                    total = DoubleToStr(total);
                    double Dtotal = double.Parse(total);
                    Dtotal += Dprice;

                    DquantityProduct -= 1;
                    //Product jadvaliga o'zgargan miqdorni yozamiz
                    cmd = new MySqlCommand("update product set quantity='" + DoubleToStr(DquantityProduct.ToString()) + "' where product_id='" + existingCart.product_id + "'");
                    objDBAccess.executeQuery(cmd);
                    cmd.Dispose();

                    //Cart jadvaliga o'zgargan miqdorni va narxni yozamiz
                    cmdCart = new MySqlCommand("update cart set quantity='" + DoubleToStr(DquantityBasket.ToString()) + "', total='" + DoubleToStr(Dtotal.ToString()) + "' where id='" + existingCart.id + "' and shop_id='" + shopID + "'");
                    objDBAccess.executeQuery(cmdCart);
                    cmdCart.Dispose();
                    Refresh();

                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Tovar yetarli emas!", "Xabar", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return;
                }
                dataGridBasket.SelectedIndex = CurrentItem;
                dataGridBasket.Focus();
            }
            if (e.Key == Key.Subtract) // Minus
            {
                Cart existingCart = dataGridBasket.SelectedItems[0] as Cart;
                int CurrentItem = dataGridBasket.SelectedIndex;
                string quantityBasket = existingCart.quantity.ToString();
                quantityBasket = DoubleToStr(quantityBasket);
                double DquantityBasket = double.Parse(quantityBasket, CultureInfo.InvariantCulture);

                if (DquantityBasket >= 2)
                {
                    DataTable tbQuantityProduct = new DataTable();
                    string queryQuantityProduct = "select * from product where product_id='" + existingCart.product_id + "'";
                    objDBAccess.readDatathroughAdapter(queryQuantityProduct, tbQuantityProduct);
                    string quantityProduct = tbQuantityProduct.Rows[0]["quantity"].ToString(); // Product jadvalidan maxsulot sonini olamiz
                    quantityProduct = DoubleToStr(quantityProduct);
                    double DquantityProduct = double.Parse(quantityProduct, CultureInfo.InvariantCulture);
                    tbQuantityProduct.Clear();
                    tbQuantityProduct.Dispose();
                    string price = existingCart.price.ToString();
                    price = DoubleToStr(price);
                    double Dprice = double.Parse(price, CultureInfo.InvariantCulture);

                    DquantityBasket -= 1;

                    string total = existingCart.total.ToString();
                    total = DoubleToStr(total);
                    double Dtotal = double.Parse(total);
                    Dtotal -= Dprice;

                    DquantityProduct += 1;
                    //Product jadvaliga o'zgargan miqdorni yozamiz
                    cmd = new MySqlCommand("update product set quantity='" + DoubleToStr(DquantityProduct.ToString()) + "' where product_id='" + existingCart.product_id + "'");
                    objDBAccess.executeQuery(cmd);
                    cmd.Dispose();

                    //Cart jadvaliga o'zgargan miqdorni va narxni yozamiz
                    cmdCart = new MySqlCommand("update cart set quantity='" + DoubleToStr(DquantityBasket.ToString()) + "', total='" + DoubleToStr(Dtotal.ToString()) + "' where id='" + existingCart.id + "' and shop_id='" + shopID + "'");
                    objDBAccess.executeQuery(cmdCart);
                    cmdCart.Dispose();
                    Refresh();
                }

                else
                {
                    return;
                }
                dataGridBasket.SelectedIndex = CurrentItem;
                dataGridBasket.Focus();
            }

            if (e.Key == Key.Delete)
            {
                if (tbBasket.Rows.Count > 0)
                {
                    Cart existingCart = dataGridBasket.SelectedItems[0] as Cart;

                    string BasketDeleteQuantity = existingCart.quantity.ToString();
                    BasketDeleteQuantity = DoubleToStr(BasketDeleteQuantity);
                    string Product_idDelete = existingCart.product_id.ToString();
                    string cart_id = existingCart.id.ToString();
                    string queryProductQuantity = "select * from product where product_id='" + Product_idDelete + "'";
                    DataTable tbProductQuantity = new DataTable();
                    objDBAccess.readDatathroughAdapter(queryProductQuantity, tbProductQuantity);
                    string QuantityProduct = tbProductQuantity.Rows[0]["quantity"].ToString();   // Product jadvalidan maxsulot miqdorini olamiz
                    QuantityProduct = DoubleToStr(QuantityProduct);
                    tbProductQuantity.Clear();
                    tbProductQuantity.Dispose();
                    double DBasketDeleteQuantity = double.Parse(BasketDeleteQuantity);
                    double DQuantityProduct = double.Parse(QuantityProduct);
                    double ResultQuantity = DBasketDeleteQuantity + DQuantityProduct;  // Umumiy miqdorni hisoblaymiz

                    //Umumiy miqdorni Product jadvaliga yozamiz
                    cmd = new MySqlCommand("update product set quantity='" + DoubleToStr(ResultQuantity.ToString()) + "' where product_id='" + Product_idDelete + "'");
                    objDBAccess.executeQuery(cmd);
                    cmd.Dispose();

                    //Cart jadvalidan productni o'chiramiz
                    cmdCart = new MySqlCommand("delete from cart where id='" + cart_id + "' and shop_id='" + shopID + "'");
                    objDBAccess.executeQuery(cmdCart);
                    cmdCart.Dispose();
                    Refresh();

                }
                return;
            }
        }

        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
            Cart existingCart = dataGridBasket.SelectedItems[0] as Cart;
            int CurrentItem = dataGridBasket.SelectedIndex;
            string quantityBasket = existingCart.quantity.ToString();
            quantityBasket = DoubleToStr(quantityBasket);
            double DquantityBasket = double.Parse(quantityBasket, CultureInfo.InvariantCulture);

            if (DquantityBasket >= 2)
            {
                DataTable tbQuantityProduct = new DataTable();
                string queryQuantityProduct = "select * from product where product_id='" + existingCart.product_id + "'";
                objDBAccess.readDatathroughAdapter(queryQuantityProduct, tbQuantityProduct);
                string quantityProduct = tbQuantityProduct.Rows[0]["quantity"].ToString(); // Product jadvalidan maxsulot sonini olamiz
                quantityProduct = DoubleToStr(quantityProduct);
                double DquantityProduct = double.Parse(quantityProduct, CultureInfo.InvariantCulture);
                tbQuantityProduct.Clear();
                tbQuantityProduct.Dispose();
                string price = existingCart.price.ToString();
                price = DoubleToStr(price);
                double Dprice = double.Parse(price, CultureInfo.InvariantCulture);

                DquantityBasket -= 1;

                string total = existingCart.total.ToString();
                total = DoubleToStr(total);
                double Dtotal = double.Parse(total, CultureInfo.InvariantCulture);
                Dtotal -= Dprice;

                DquantityProduct += 1;
                //Product jadvaliga o'zgargan miqdorni yozamiz
                cmd = new MySqlCommand("update product set quantity='" + DoubleToStr(DquantityProduct.ToString()) + "' where product_id='" + existingCart.product_id + "'");
                objDBAccess.executeQuery(cmd);
                cmd.Dispose();

                //Cart jadvaliga o'zgargan miqdorni va narxni yozamiz
                cmdCart = new MySqlCommand("update cart set quantity='" + DoubleToStr(DquantityBasket.ToString()) + "', total='" + DoubleToStr(Dtotal.ToString()) + "' where id='" + existingCart.id + "' and shop_id='" + shopID + "'");
                objDBAccess.executeQuery(cmdCart);
                cmdCart.Dispose();
                Refresh();
            }

            else
            {
                return;
            }
            dataGridBasket.SelectedIndex = CurrentItem;
            dataGridBasket.Focus();
        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            Cart existingCart = dataGridBasket.SelectedItems[0] as Cart;
            int CurrentItem = dataGridBasket.SelectedIndex;
            DataTable tbQuantityProduct = new DataTable();
            string queryQuantityProduct = "select * from product where product_id='" + existingCart.product_id + "'";
            objDBAccess.readDatathroughAdapter(queryQuantityProduct, tbQuantityProduct);
            string quantityProduct = tbQuantityProduct.Rows[0]["quantity"].ToString(); // Product jadvalidan maxsulot sonini olamiz
            quantityProduct = DoubleToStr(quantityProduct);
            double DquantityProduct = double.Parse(quantityProduct, CultureInfo.InvariantCulture);
            tbQuantityProduct.Clear();
            tbQuantityProduct.Dispose();
            if (DquantityProduct >= 1)
            {
                string quantityBasket = existingCart.quantity.ToString();
                string price = existingCart.price.ToString();
                price = DoubleToStr(price);
                double Dprice = double.Parse(price, CultureInfo.InvariantCulture);
                quantityBasket = DoubleToStr(quantityBasket);
                double DquantityBasket = double.Parse(quantityBasket, CultureInfo.InvariantCulture);
                DquantityBasket += 1;

                string total = existingCart.total.ToString();
                total = DoubleToStr(total);
                double Dtotal = double.Parse(total, CultureInfo.InvariantCulture);
                Dtotal += Dprice;

                DquantityProduct -= 1;
                //Product jadvaliga o'zgargan miqdorni yozamiz
                cmd = new MySqlCommand("update product set quantity='" + DoubleToStr(DquantityProduct.ToString()) + "' where product_id='" + existingCart.product_id + "'");
                objDBAccess.executeQuery(cmd);
                cmd.Dispose();

                //Cart jadvaliga o'zgargan miqdorni va narxni yozamiz
                cmdCart = new MySqlCommand("update cart set quantity='" + DoubleToStr(DquantityBasket.ToString()) + "', total='" + DoubleToStr(Dtotal.ToString()) + "' where id='" + existingCart.id + "' and shop_id='" + shopID + "'");
                objDBAccess.executeQuery(cmdCart);
                cmdCart.Dispose();
                Refresh();

            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Tovar yetarli emas!", "Xabar", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return;
            }
            dataGridBasket.SelectedIndex = CurrentItem;
            dataGridBasket.Focus();
        }

        private void btnTozalash_Click(object sender, RoutedEventArgs e)
        {
            if (shopID == 0 || tbBasket.Rows.Count == 0) return;
            int count = tbBasket.Rows.Count;
            string quantityBasket, product_id, quantityProduct, queryProduct;
            DataTable tbProductFill = new DataTable();
            for (int i = 0; i < count; i++)
            {
                quantityBasket = tbBasket.Rows[i]["quantity"].ToString(); // cart jadvalidagilarni miqdorini double ga tekshiramiz
                DoubleToStr(quantityBasket);
                
                product_id = tbBasket.Rows[i]["product_id"].ToString();
                queryProduct = "select * from product where product_id='" + product_id + "'";
                objDBAccess.readDatathroughAdapter(queryProduct, tbProductFill);
                quantityProduct = tbProductFill.Rows[0]["quantity"].ToString(); // product jadvalidagilarni miqdorini double ga tekshiramiz
                quantityProduct = DoubleToStr(quantityProduct);
                double dquantityBasket = double.Parse(quantityBasket, CultureInfo.InvariantCulture);
                double dquantityProduct = double.Parse(quantityProduct, CultureInfo.InvariantCulture);
                double totalQuantity = dquantityBasket + dquantityProduct;//umumiy miqdorni hisoblaymiz.

                cmd = new MySqlCommand("update product set quantity='" + DoubleToStr(totalQuantity.ToString()) + "' where product_id='" + product_id + "'");
                
                objDBAccess.executeQuery(cmd);
                tbProductFill.Clear();
            }

            cmdCart = new MySqlCommand("delete from cart where shop_id='" + shopID + "'"); //Cart dan bekor qilinganlarni o'chiramiz/
            cmdShop = new MySqlCommand("delete from shop where id='" + shopID + "'"); //Shop dan bekor qilinganlarni o'chiramiz.
            objDBAccess.executeQuery(cmdCart);
            objDBAccess.executeQuery(cmdShop);

            string queryShopId = "select * from shopId where password='" + Kirish_ucont.password1 + "'";
            tbShopId = new DataTable();
            objDBAccess.readDatathroughAdapter(queryShopId, tbShopId);
            if (tbShopId.Rows.Count > 0)
            {
                cmdShopId = new MySqlCommand("update shopId set shop_id=0 where password='" + Kirish_ucont.password1 + "'");
                objDBAccess.executeQuery(cmdShopId);
            }
            else
            {
                string queryId = "select * from shopId order by id desc limit 1";
                DataTable tbSId = new DataTable();
                objDBAccess.readDatathroughAdapter(queryId, tbSId);
                if (tbSId.Rows.Count == 0)
                {
                    cmdShopId = new MySqlCommand("insert into shopId values(1,0,'" + Kirish_ucont.mac_address + "','" + Kirish_ucont.password1 + "')");
                    objDBAccess.executeQuery(cmdShopId);
                }
                else
                {
                    cmdShopId = new MySqlCommand("insert into shopId (shop_id,mac_address,password) values(0,'" + Kirish_ucont.mac_address + "','" + Kirish_ucont.password1 + "')");
                    objDBAccess.executeQuery(cmdShopId);
                }
                try
                {
                    tbSId.Clear();
                    tbSId.Dispose();
                }
                catch (Exception) { }
            }
            tbShopId.Clear();
            tbShopId.Dispose();

            System.Windows.Forms.MessageBox.Show("Tovarlar bekor qilindi!", "Xabar", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            tbProductFill.Dispose();
            cmd.Dispose();
            cmdCart.Dispose();
            cmdShop.Dispose();
            shopID = 0;
            shop = false;
            debtor_id = "1";
            Refresh();
        }
        public static int selection = -1;
        private void SomeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Cart selectedCart = dataGridBasket.SelectedItems[0] as Cart;
                int index = dataGridBasket.SelectedIndex;
                string val_ul = selectedCart.val;
                if(val_ul == "uz") { selection = 0; }
                if(val_ul == "$") { selection = 1; }
                ComboBox ele = dataGridBasket.Columns[3].GetCellContent(dataGridBasket.Items[index]) as ComboBox;
                int i = ele.SelectedIndex;
                if (i == 0 && i != selection)
                {
                    selection = i;
                    int id = 0;
                    Cart selectedItem = dataGridBasket.SelectedItems[0] as Cart;
                    id = selectedItem.id;
                    double price_dollar = selectedItem.price;
                    double quantity = selectedItem.quantity;
                    double kurs = double.Parse(txtKurs.Text, CultureInfo.InvariantCulture);
                    double price_som = price_dollar * kurs;
                    double total_som = price_som * quantity;
                    cmd = new MySqlCommand("update cart set price='" + DoubleToStr(price_som.ToString()) + "', val_id=1,total='"+DoubleToStr(total_som.ToString())+"' where id='" + id + "' and shop_id='"+shopID+"'");
                    objDBAccess.executeQuery(cmd);
                    cmd.Dispose();
                    Refresh();
                   // System.Windows.Forms.MessageBox.Show(price_som.ToString());
                }
                if (i == 1 && i!=selection)
                {
                    selection = i;
                    int id = 0;
                    Cart selectedItem = dataGridBasket.SelectedItems[0] as Cart;
                    id = selectedItem.id;
                    double price_som = selectedItem.price;
                    double quantity = selectedItem.quantity;
                    double kurs = double.Parse(txtKurs.Text, CultureInfo.InvariantCulture);
                    double price_dollar = price_som / kurs;
                    price_dollar = Math.Round(price_dollar, 3);
                    double total_dollar = price_dollar * quantity;
                    cmd = new MySqlCommand("update cart set price='" + DoubleToStr(price_dollar.ToString()) + "', val_id=2, total='"+DoubleToStr(total_dollar.ToString())+"' where id='" + id + "' and shop_id='"+shopID+"'");
                    objDBAccess.executeQuery(cmd);
                    cmd.Dispose();
                    Refresh();
                    //System.Windows.Forms.MessageBox.Show(price_dollar.ToString());
                }

            }
            catch (Exception ex) { System.Windows.Forms.MessageBox.Show(ex.Message); return; }
        }

        public static MySqlCommand cmdShopId;
        public static string product_idEdit = "", priceEdit = "", quantityEdit = "", totalEdit = "";
        public static bool nasiya = false;

        DBAccess objDBAccess = new DBAccess();




        private void BtnAsosiy_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = (MainWindow)Application.Current.MainWindow;
            main.TabMenu.SelectedIndex = 1;
        }

        PrintDialog printDialog1 = new PrintDialog();
       
        private void btnTulov_Click(object sender, RoutedEventArgs e)
        {
            
            if (shopID == 0 || tbBasket.Rows.Count == 0)
            {
                return;
            }
            
            //PrintDialog printDlg = new PrintDialog();
            //printDlg.PrintVisual(dataGridBasket, "Grid Printing.");
            //System.Windows.Forms.DataGridView dvg = new System.Windows.Forms.DataGridView();
            //dvg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            //dvg.Name = "dvg";
            //dvg.AllowUserToAddRows = false;
            //dvg.RowHeadersVisible = false;
            //dvg.Location = new System.Drawing.Point(100, 200);
            //dvg.Visible = true;
            //dvg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect; 

            //DGVPrinter printer = new DGVPrinter();
            //printer.Title = "накладная" + '\n' + "Tel: +998 - 78 - 11 - 31 - 888" + '\n' + "E-mail: frap2020@mail.ru site: www.frap.uz" + '\n';
            //printer.SubTitle = string.Format("Date: {0}", DateTime.Now.Date);
            //printer.SubTitleFormatFlags = System.Drawing.StringFormatFlags.LineLimit | System.Drawing.StringFormatFlags.NoClip;
            //printer.PageNumbers = true;
            //printer.PageNumberInHeader = false;
            //printer.PorportionalColumns = true;
            //printer.HeaderCellAlignment = System.Drawing.StringAlignment.Near;
            //printer.Footer = "Внимание: Заказ в резерве хранится не более 4 дней при точном ответе";
            //printer.FooterSpacing = 15;
            //printer.PrintPreviewDataGridView(dvg);

            string print1 = header;
            print1 += "\n\nChek : " + shopID.ToString() + "\n";
            print1 += "Sotuvchi : " + Kirish_ucont.sellar + "\n";
            string name = "", price = "", quantity = "";
            int i = 1;
            foreach(DataRow item in tbBasket.Rows)
            {
                name = item["name"].ToString();
                price = item["price"].ToString();
                price = DoubleToStr(price);
                quantity = item["quantity"].ToString();
                quantity = DoubleToStr(quantity);
                print1 += i.ToString() + "." + name + "\n";
                double Dquantity = double.Parse(quantity, CultureInfo.InvariantCulture);
                double Dprice = double.Parse(price, CultureInfo.InvariantCulture);
                double total_each = Dquantity * Dprice;
                string str_total_each = DoubleToStr(total_each.ToString());
                print1 += " " + quantity + " x " + price + " = " + str_total_each + "\n";
                i++;

            }
            print1 += "\n" + "Jami summa : " +  "\n";
            print1 += "So'm : " + DoubleToStr(txtTulovSom.Text.ToString()) + "\n";
            print1 += "Dollar : " + DoubleToStr(txtTulovDollar.Text.ToString()) + "\n";

            tulov_click.debtor_id = debtor_id;
            tulov_click tulov = new tulov_click();
            if (txtTulovSom.Text != "") { tulov.tulov_som = txtTulovSom.Text.ToString(); }
            if (txtTulovDollar.Text != "") { tulov.tulov_dollar = txtTulovDollar.Text.ToString(); }
            tulov.shopId = shopID.ToString();
            tulov.FillDebtor = false;
            tulov.print = print1;
            tulov.footer = footer;
            tulov.d_id = debtor_id;
            tulov.naqd_tulov = true; tulov.qarz_tulov = false;
            tulov.ShowDialog();
        }

        private void btnNavbat_Click(object sender, RoutedEventArgs e)
        {
            if (tbBasket.Rows.Count == 0) return;
            if (debtor_id == "1")
            {
                NavbatClient navbatClient = new NavbatClient();
                navbatClient.total_som = txtTulovSom.Text;
                navbatClient.total_dollar = txtTulovDollar.Text;
                navbat = false;
                navbatClient.ShowDialog();
                if (navbat)
                {
                    Refresh();
                }
            }
            else
            {
                if (shopID == 0 || tbBasket.Rows.Count == 0)
                {
                    return;
                }
                string total_som = "0", total_dollar = "0";
                if (txtTulovSom.Text != "")
                {
                    total_som = txtTulovSom.Text;
                    total_som = DoubleToStr(total_som);
                }
                if (txtTulovDollar.Text != "")
                {
                    total_dollar = txtTulovDollar.Text;
                    total_dollar = DoubleToStr(total_dollar);
                }
                cmd = new MySqlCommand("update shop set total_som='" + total_som + "', total_dollar='" + total_dollar + "', queue=1 where id='" + shopID + "'");
                objDBAccess.executeQuery(cmd);
                cmd.Dispose();

                string queryShopId = "select * from shopId where password='" + Kirish_ucont.password1 + "'";
                tbShopId = new DataTable();
                objDBAccess.readDatathroughAdapter(queryShopId, tbShopId);
                if (tbShopId.Rows.Count > 0)
                {
                    cmdShopId = new MySqlCommand("update shopId set shop_id=0 where password='" + Kirish_ucont.password1 + "'");
                    objDBAccess.executeQuery(cmdShopId);
                }
                else
                {
                    string queryId = "select * from shopId order by id desc limit 1";
                    DataTable tbSId = new DataTable();
                    objDBAccess.readDatathroughAdapter(queryId, tbSId);
                    if (tbSId.Rows.Count == 0)
                    {
                        cmdShopId = new MySqlCommand("insert into shopId values(1,0,'" + Kirish_ucont.mac_address + "','" + Kirish_ucont.password1 + "')");
                        objDBAccess.executeQuery(cmdShopId);
                    }
                    else
                    {
                        cmdShopId = new MySqlCommand("insert into shopId (shop_id,mac_address,password) values(0,'" + Kirish_ucont.mac_address + "','" + Kirish_ucont.password1 + "')");
                        objDBAccess.executeQuery(cmdShopId);
                    }
                    try
                    {
                        tbSId.Clear();
                        tbSId.Dispose();
                    }
                    catch (Exception) { }
                }
                tbShopId.Clear();
                tbShopId.Dispose();

                System.Windows.Forms.MessageBox.Show("Tovarlar kutishga o'tkazildi!", "Xabar", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                shopID = 0;
                shop = false;
                debtor_id = "1";
                Refresh();
            }
        }
        public static bool can_refresh = false;
        public void btnCart_Click(object sender, RoutedEventArgs e)
        {
            if (TbProduct.Visibility == Visibility.Collapsed) { return; }
            if (tbProducts.Rows.Count == 0) { return; }
            //insertion new record to shop which has not been there before
            if (shop == false)
            {
                string queryShopID = "select id from shop order by id desc limit 1";
                tbID = new DataTable();
                objDBAccess.readDatathroughAdapter(queryShopID, tbID);
                shopID = 0;
                if (tbID.Rows.Count == 1)
                {
                    shopID = int.Parse(tbID.Rows[0]["id"].ToString());
                    shopID = shopID + 1;
                }
                else { shopID = 1; }
                shop = true;
                DateTime dt = DateTime.Now;
                cmdShop = new MySqlCommand("insert into shop values('" + shopID + "',0,0,0,0,0,0,0,0,'"+DoubleToStr(txtKurs.Text.ToString())+"', '" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "',0,0,0,0,0,0, '" + Kirish_ucont.sellar_id + "',1)");
                objDBAccess.executeQuery(cmdShop);
                //Yaratilgan yangi shopId ni shopId jadvaliga yozamiz
                string queryShopId = "select * from shopId where password='" + Kirish_ucont.password1 + "'";
                tbShopId = new DataTable();
                objDBAccess.readDatathroughAdapter(queryShopId, tbShopId);
                if (tbShopId.Rows.Count > 0)
                {
                    cmdShopId = new MySqlCommand("update shopId set shop_id='" + shopID + "' where password='" + Kirish_ucont.password1 + "'");
                    objDBAccess.executeQuery(cmdShopId);
                }
                else
                {
                    string queryId = "select * from shopId order by id desc limit 1";
                    DataTable tbSId = new DataTable();
                    objDBAccess.readDatathroughAdapter(queryId, tbSId);
                    if (tbSId.Rows.Count == 0)
                    {
                        cmdShopId = new MySqlCommand("insert into shopId values(1,'" + shopID + "','" + Kirish_ucont.mac_address + "','" + Kirish_ucont.password1 + "')");
                        objDBAccess.executeQuery(cmdShopId);
                    }
                    else
                    {
                        cmdShopId = new MySqlCommand("insert into shopId (shop_id,mac_address,password) values('" + shopID + "','" + Kirish_ucont.mac_address + "','" + Kirish_ucont.password1 + "')");
                        objDBAccess.executeQuery(cmdShopId);
                    }
                    try
                    {
                        tbSId.Clear();
                        tbSId.Dispose();
                    }
                    catch (Exception) { }
                }
                tbShopId.Clear();
                tbShopId.Dispose();
                tbID.Clear();
                cmdShop.Dispose();
                tbID.Dispose();
            }

            //Getting properties from selected product\
            Product selectedProduct = dataGridProduct.SelectedItems[0] as Product;
            price = selectedProduct.price.ToString();
            barcode = selectedProduct.barcode;
            oldquantity = selectedProduct.quantity.ToString();
            if (oldquantity == "0")
            {
                System.Windows.Forms.MessageBox.Show("Ushbu maxsulot tugagan!", "Xabar", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                TbProduct.Visibility = Visibility.Collapsed;
                Basket.Visibility = Visibility.Visible;
                //Refresh();
                return;
            }
            product_id = selectedProduct.product_id.ToString();
            name = selectedProduct.name;
            val_ul = selectedProduct.val_ul;
            kurs = txtKurs.Text;
            quantity = selectedProduct.quantity.ToString();
            can_refresh = false;
            Korzinka f3 = new Korzinka();
            f3.barcode = barcode;
            f3.price = price;
            f3.oldquantity = oldquantity;
            f3.product_id = product_id;
            f3.name = name;
            f3.val_ul = val_ul;
            f3.kurs = kurs;
            f3.quantity = quantity;
            f3.ShowDialog();
            if(can_refresh)
            {
                Refresh();

            }
            txtSearch.Clear();
            TbProduct.Visibility = Visibility.Collapsed;
            Basket.Visibility = Visibility.Visible;
            txtSearch.Focus();
        }
    }
}
