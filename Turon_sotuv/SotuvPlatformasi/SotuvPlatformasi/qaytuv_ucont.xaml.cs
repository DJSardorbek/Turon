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
    /// Interaction logic for qaytuv_ucont.xaml
    /// </summary>
    public partial class qaytuv_ucont : UserControl
    {
        public qaytuv_ucont()
        {
            InitializeComponent();
        }

        DBAccess objDBAccess = new DBAccess();
        DataTable tbQaytuv;
        public class Qaytuv
        {
            public int chek { get; set; }
            public int product_id { get; set; }
            public string name { get; set; }
            public string preparer { get; set; }
            public string measurement { get; set; }
            public double quan_pr { get; set; }
            public double price { get; set; }
            public string val_ul { get; set; }
            public double quantity { get; set; }
            public double total { get; set; }
            public string first_name { get; set; }
            public DateTime date { get; set; }
        }
        public List<Qaytuv> qaytuv = new List<Qaytuv>();
        public List<Qaytuv> Get_qaytuv()
        {
            qaytuv = (from DataRow dr in tbQaytuv.Rows
                      select new Qaytuv()
                      {
                          chek = Convert.ToInt32(dr["shop_id"]),
                          product_id = Convert.ToInt32(dr["product_id"]),
                          name = dr["name"].ToString(),
                          preparer = dr["preparer"].ToString(),
                          measurement = dr["measurement"].ToString(),
                          quan_pr = Convert.ToDouble(dr["quan_pr"]),
                          price = Convert.ToDouble(dr["price"]),
                          val_ul =dr["val_ul"].ToString(),
                          quantity = Convert.ToDouble(dr["quantity"]),
                          total = Convert.ToDouble(dr["total"]),
                          first_name = dr["first_name"].ToString(),
                          date = Convert.ToDateTime(dr["date"])

                      }).ToList();
            return qaytuv;
        }

        public void dateTimePicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime dt = dateTimePicker1.SelectedDate.Value.Date;
            string st_now = dt.ToString("yyyy-MM-dd"); 
            string queryQaytuv = "select cart.shop_id,cart.product_id, cart.name,product.preparer, product.measurement,product.quantity as quan_pr, cart.price, val.name as val_ul, cart.quantity, cart.total, userprofile.first_name, shop.date " +
                "from cart " +
                "inner join shop on cart.shop_id = shop.id " +
                "inner join userprofile on shop.sellar_id = userprofile.id " +
                "inner join product on cart.product_id=product.id inner join val on cart.val_id = val.id "+
                "where shop.date like '" + st_now + "%' and shop.debt = 0 and shop.status_tulov=1";
            tbQaytuv = new DataTable();
            objDBAccess.readDatathroughAdapter(queryQaytuv, tbQaytuv);
            dataGridQaytuv.ItemsSource = Get_qaytuv();
            tbQaytuv.Dispose();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (dateTimePicker1.Text != "")
            {
                if (txtSearch.Text != "")
                {
                    try
                    {
                        int number = int.Parse(txtSearch.Text, CultureInfo.InvariantCulture);
                        DateTime dt = dateTimePicker1.SelectedDate.Value.Date;
                        string st_now = dt.ToString("yyyy-MM-dd");
                        string queryQaytuv = "select cart.shop_id ,cart.product_id, cart.name ,product.preparer, product.measurement,product.quantity as quan_pr, cart.price, val.name as val_ul, cart.quantity, cart.total, userprofile.first_name, shop.date " +
                            "from cart " +
                            "inner join shop on cart.shop_id = shop.id " +
                            "inner join userprofile on shop.sellar_id = userprofile.id " +
                            "inner join product on cart.product_id=product.id inner join val on cart.val_id = val.id " +
                            "where shop.date like '" + st_now + "%' and shop.debt = 0 and shop.status_tulov=1 and cart.shop_id like '" + txtSearch.Text + "%'";
                        tbQaytuv.Clear();
                        objDBAccess.readDatathroughAdapter(queryQaytuv, tbQaytuv);
                        dataGridQaytuv.ItemsSource = Get_qaytuv();
                    }
                    catch (Exception)
                    {
                        DateTime dt = dateTimePicker1.SelectedDate.Value.Date;
                        string st_now = dt.ToString("yyyy-MM-dd");
                        string queryQaytuv = "select cart.shop_id ,cart.product_id, cart.name ,product.preparer, product.measurement,product.quantity as quan_pr, cart.price, val.name as val_ul, cart.quantity, cart.total, userprofile.first_name, shop.date " +
                            "from cart " +
                            "inner join shop on cart.shop_id = shop.id " +
                            "inner join userprofile on shop.sellar_id = userprofile.id " +
                            "inner join product on cart.product_id=product.id inner join val on cart.val_id = val.id " +
                            "where shop.date like '" + st_now + "%' and shop.debt = 0 and shop.status_tulov=1 and cart.name like '%" + txtSearch.Text + "%'";
                        tbQaytuv.Clear();
                        objDBAccess.readDatathroughAdapter(queryQaytuv, tbQaytuv);
                        dataGridQaytuv.ItemsSource = Get_qaytuv();
                    }
                }
                else
                {
                    DateTime dt = dateTimePicker1.SelectedDate.Value.Date;
                    string st_now = dt.ToString("yyyy-MM-dd");
                    string queryQaytuv = "select cart.shop_id,cart.product_id, cart.name,product.preparer, product.measurement,product.quantity as quan_pr, cart.price, val.name as val_ul, cart.quantity, cart.total, userprofile.first_name, shop.date " +
                        "from cart " +
                        "inner join shop on cart.shop_id = shop.id " +
                        "inner join userprofile on shop.sellar_id = userprofile.id " +
                        "inner join product on cart.product_id=product.id inner join val on cart.val_id = val.id " +
                        "where shop.date like '" + st_now + "%' and shop.debt = 0 and shop.status_tulov=1";
                    tbQaytuv = new DataTable();
                    objDBAccess.readDatathroughAdapter(queryQaytuv, tbQaytuv);
                    dataGridQaytuv.ItemsSource = Get_qaytuv();
                    tbQaytuv.Dispose();
                }
            }
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                dataGridQaytuv.SelectedIndex = 0;
                var u = e.OriginalSource as UIElement;
                e.Handled = true;
                u.MoveFocus(new TraversalRequest(FocusNavigationDirection.Down));
            }
        }

        private void UserControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                txtSearch.Focus();
            }
        }

        private void BtnAsosiy_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = (MainWindow)Application.Current.MainWindow;
            main.TabMenu.SelectedIndex = 1;
        }

        private void btnQaytuv_Click(object sender, RoutedEventArgs e)
        {
            if (tbQaytuv.Rows.Count == 0) return;
            Qaytuv qaytuv1 = dataGridQaytuv.SelectedItems[0] as Qaytuv;
            qaytuv Qayt = new qaytuv();
            Qayt.shop_id = qaytuv1.chek.ToString();
            Qayt.product_id = qaytuv1.product_id.ToString();
            Qayt.product = qaytuv1.name;
            Qayt.measurement = qaytuv1.measurement;
            Qayt.preparer = qaytuv1.preparer;
            Qayt.quan_ombor = qaytuv1.quan_pr.ToString();
            Qayt.sold_price = qaytuv1.price.ToString();
            Qayt.sold_quan = qaytuv1.quantity.ToString();
            Qayt.val_ul = qaytuv1.val_ul;
            Qayt.sold = true; Qayt.debt = false;
            Qayt.ShowDialog();
        }
    }
}
