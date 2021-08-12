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

namespace SotuvPlatformasi
{
    /// <summary>
    /// Interaction logic for Navbat_ucont.xaml
    /// </summary>
    public partial class Navbat_ucont : UserControl
    {
        public Navbat_ucont()
        {
            InitializeComponent();
        }
        MainWindow main = (MainWindow)Application.Current.MainWindow;
        DBAccess objDBAccess = new DBAccess();
        public static MySqlCommand cmdWaiting, cmdCart, cmdShop, cmd;
        public static DataTable tbWaiting, tbWaitingCart, tbBasket;
        public static DataTable tbShopId;
        public static MySqlCommand cmdShopId;
        string shop_id = "";

        private void btnKorzinkaOlish_Click(object sender, RoutedEventArgs e)
        {
            if (Sotuv_ucont.shopID != 0)
            {
                System.Windows.Forms.MessageBox.Show("Korzinka bo'sh emas, iltimos avval korzinkani tekshiring!", "Xatolik", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return;
            }
            try
            {
                sold_product existingSoldProduct = dataGridNavbat.SelectedItems[0] as sold_product;
                string id = existingSoldProduct.id.ToString();
                string kurs = existingSoldProduct.kurs.ToString();
                string debtor = existingSoldProduct.debtor_id.ToString();
                DateTime dt = DateTime.Now;
                cmdWaiting = new MySqlCommand("update shop set queue=0,date='" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "' where id='" + id + "'");
                objDBAccess.executeQuery(cmdWaiting);
                Sotuv_ucont.shopID = int.Parse(id);
                Sotuv_ucont.shop = true;
                Sotuv_ucont.debtor_id = debtor;
                
                string queryShopId = "select * from shopId where password='" + Kirish_ucont.password1 + "'";
                tbShopId = new DataTable();
                objDBAccess.readDatathroughAdapter(queryShopId, tbShopId);
                if (tbShopId.Rows.Count > 0)
                {
                    cmdShopId = new MySqlCommand("update shopId set shop_id='" + id + "' where password='" + Kirish_ucont.password1 + "'");
                    objDBAccess.executeQuery(cmdShopId);
                }
                else
                {
                    string queryId = "select * from shopId order by id desc limit 1";
                    DataTable tbSId = new DataTable();
                    objDBAccess.readDatathroughAdapter(queryId, tbSId);
                    if (tbSId.Rows.Count == 0)
                    {
                        cmdShopId = new MySqlCommand("insert into shopId values(1,'" + id + "','" + Kirish_ucont.mac_address + "','" + Kirish_ucont.password1 + "')");
                        objDBAccess.executeQuery(cmdShopId);
                    }
                    else
                    {
                        cmdShopId = new MySqlCommand("insert into shopId (shop_id,mac_address,password) values('" + id + "','" + Kirish_ucont.mac_address + "','" + Kirish_ucont.password1 + "')");
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

                cmd = new MySqlCommand("update valyuta set kurs='"+DoubleToStr(kurs)+"'");
                objDBAccess.executeQuery(cmd);
                cmd.Dispose();

                System.Windows.Forms.MessageBox.Show("Tovarlar korzinkaga o'tkazildi!", "Xabar", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                main.TabMenu.SelectedIndex = 2;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Xabar", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        public string DoubleToStr(string s)
        {
            if(s.IndexOf(',') > -1)
            {
                int index = s.IndexOf(',');
                string first = s.Substring(0, index);
                string last = s.Substring(index + 1);
            }
            return s;
        }

        private void btnBekorQilish_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                objDBAccess.createConn();
                sold_product existingSoldProduct = dataGridNavbat.SelectedItems[0] as sold_product;
                string shopID = existingSoldProduct.id.ToString();
                string queryBasket = "select * from cart where shop_id='" + shopID + "'";
                tbBasket = new DataTable();
                objDBAccess.readDatathroughAdapter(queryBasket, tbBasket);
                int count = tbBasket.Rows.Count;
                string quantityBasket, product_id, quantityProduct, queryProduct;
                DataTable tbProductFill = new DataTable();
                for (int i = 0; i < count; i++)
                {
                    quantityBasket = tbBasket.Rows[i]["quantity"].ToString(); // cart jadvalidagilarni miqdorini double ga tekshiramiz
                    
                    quantityBasket = DoubleToStr(quantityBasket);
                    
                    product_id = tbBasket.Rows[i]["product_id"].ToString();
                    queryProduct = "select * from product where product_id='" + product_id + "'";
                    objDBAccess.readDatathroughAdapter(queryProduct, tbProductFill);
                    
                    quantityProduct = tbProductFill.Rows[0]["quantity"].ToString();  // product jadvalidagilarni miqdorini double ga tekshiramiz

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
                System.Windows.Forms.MessageBox.Show("Tovarlar bazaga qaytarildi!", "Xabar", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                Refresh();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Xatolik", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        public void Refresh()
        {
            string queryWaiting = "select shop.id,debtor.id as debtor_id,debtor.mijoz_fish,shop.total_som ,shop.total_dollar, shop.kurs, shop.date , shop.queue , userprofile.first_name  from shop " +
                "inner join userprofile on shop.sellar_id = userprofile.id " +
                "inner join debtor on shop.debtor_id = debtor.id " +
                "where shop.queue=1";
            tbWaiting.Clear();
            objDBAccess.readDatathroughAdapter(queryWaiting, tbWaiting);
            dataGridNavbat.ItemsSource = GetSoldProductList();
            objDBAccess.closeConn();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearch.Text != "")
            {
                string queryWaiting = "select shop.id,debtor.id as debtor_id,debtor.mijoz_fish,shop.total_som ,shop.total_dollar, shop.kurs, shop.date , shop.queue , userprofile.first_name  from shop " +
                    "inner join userprofile on shop.sellar_id = userprofile.id " +
                    "inner join debtor on shop.debtor_id = debtor.id " +
                    "where shop.queue=1 and debtor.mijoz_fish like '%"+txtSearch.Text+"%'";
                tbWaiting.Clear();
                objDBAccess.readDatathroughAdapter(queryWaiting, tbWaiting);
                dataGridNavbat.ItemsSource = GetSoldProductList();
            }
            else
            {
                string queryWaiting = "select shop.id,debtor.id as debtor_id,debtor.mijoz_fish,shop.total_som ,shop.total_dollar, shop.kurs, shop.date , shop.queue , userprofile.first_name  from shop " +
                    "inner join userprofile on shop.sellar_id = userprofile.id " +
                    "inner join debtor on shop.debtor_id = debtor.id " +
                    "where shop.queue=1";
                tbWaiting.Clear();
                objDBAccess.readDatathroughAdapter(queryWaiting, tbWaiting);
                dataGridNavbat.ItemsSource = GetSoldProductList();
            }
        }

        public class sold_product
        {
            public int id { set; get; }
            public int debtor_id { set; get; }
            public string mijoz_fish { set; get; }
            public double total_som { set; get; }
            public double total_dollar { set; get; }
            public double kurs { set; get; }
            public string date { set; get; }
            public string first_name { set; get; }
        }

        public List<sold_product> sold_productList = new List<sold_product>();

        public List<sold_product> GetSoldProductList()
        {
            sold_productList = (from DataRow dr in tbWaiting.Rows
                                select new sold_product()
                                {
                                    id = Convert.ToInt32(dr["id"]),
                                    debtor_id = Convert.ToInt32(dr["debtor_id"]),
                                    mijoz_fish = dr["mijoz_fish"].ToString(),
                                    total_som = Convert.ToDouble(dr["total_som"]),
                                    total_dollar = Convert.ToDouble(dr["total_dollar"]),
                                    kurs = Convert.ToDouble(dr["kurs"]),
                                    date = dr["date"].ToString(),
                                    first_name = dr["first_name"].ToString()
                                }).ToList();

            return sold_productList;
        }


        private void BtnAsosiy_Click(object sender, RoutedEventArgs e)
        {
            main.TabMenu.SelectedIndex = 1;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            objDBAccess.createConn();
            string queryWaiting = "select shop.id,debtor.id as debtor_id,debtor.mijoz_fish, shop.total_som,shop.total_dollar,shop.kurs, shop.date, shop.queue, userprofile.first_name from shop " +
                "inner join userprofile on shop.sellar_id = userprofile.id " +
                "inner join debtor on shop.debtor_id=debtor.id " +
                "where shop.queue=1";
            tbWaiting = new DataTable();
            objDBAccess.readDatathroughAdapter(queryWaiting, tbWaiting);
            dataGridNavbat.ItemsSource = GetSoldProductList();
           
        }
    }
}
