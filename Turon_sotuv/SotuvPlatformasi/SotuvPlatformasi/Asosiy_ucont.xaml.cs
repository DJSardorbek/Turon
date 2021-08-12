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
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SotuvPlatformasi
{
    /// <summary>
    /// Interaction logic for Asosiy_ucont.xaml
    /// </summary>
    public partial class Asosiy_ucont : UserControl
    {
        public Asosiy_ucont()
        {
            InitializeComponent();
        }
        DataTable tbShopId;
        //CurrencyManager managertb;
        public static MySqlCommand cmdShopId;
        public static string filial_id = "", filial = "";
        public static DataTable tbFilial_id;
        public static bool Recieve = false;
        public static string header = "", headerCash = "";
        public static string footer = "", footerCash = "";
        DBAccess objDBAccess = new DBAccess();
        MainWindow main = (MainWindow)Application.Current.MainWindow;
        private void BtnSotuv_Click(object sender, RoutedEventArgs e)
        {
            
            main.TabMenu.SelectedIndex = 2;
        }

        private void BtnLogOut_Click(object sender, RoutedEventArgs e)
        {
            main.TabMenu.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            main.TabMenu.SelectedIndex = 3;
        }

        private void Window_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.F1)
            //{
            //    Sotuv sales = new Sotuv();
            //    sales.Show();
            //    this.Close();
            //}
            //if (e.KeyCode == Keys.F2)
            //{
            //    Navbat navbat = new Navbat();
            //    navbat.Show();
            //    this.Close();
            //}
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            main.TabMenu.SelectedIndex = 6;
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

        }

        private void btnFakturaHisob_Click(object sender, RoutedEventArgs e)
        {
            main.TabMenu.SelectedIndex = 7;
        }

        private void btnQarzdorlar_Click(object sender, RoutedEventArgs e)
        {
            main.TabMenu.SelectedIndex = 9;
        }

        private void btnTovarQoldiq_Click(object sender, RoutedEventArgs e)
        {
            main.TabMenu.SelectedIndex = 10;
        }

        private void btnHisobot_Click(object sender, RoutedEventArgs e)
        {
            main.TabMenu.SelectedIndex = 11;
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void btnQaytuv_Click(object sender, RoutedEventArgs e)
        {
            main.TabMenu.SelectedIndex = 13;
        }

        private void btnChangePrice_Click(object sender, RoutedEventArgs e)
        {
            main.TabMenu.SelectedIndex = 15;
        }

        private void BtnReturnDeliver_Click(object sender, RoutedEventArgs e)
        {
            main.TabMenu.SelectedIndex = 17;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            main.TabMenu.SelectedIndex = 8;
        }

        

        private async void Faktura_qabul_Click(object sender, RoutedEventArgs e)
        {
            main.TabMenu.SelectedIndex = 4;
            MainWindow.fakt_qab_click = true;
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            /*try
            {
                string ShopId = "select * from shopId where password='" + MainWindow.password1 + "'";
                tbShopId = new DataTable();
                objDBAccess.readDatathroughAdapter(ShopId, tbShopId);
                if (tbShopId.Rows.Count > 0)
                {
                    Sotuv.shopID = int.Parse(tbShopId.Rows[0]["shop_id"].ToString());
                    if (int.Parse(tbShopId.Rows[0]["shop_id"].ToString()) != 0)
                    {
                        Sotuv.shop = true;
                        cmdShopId = new MySqlCommand("update shopId set mac_address='" + MainWindow.mac_address + "' where password='" + MainWindow.password1 + "'");
                        objDBAccess.executeQuery(cmdShopId);
                        cmdShopId.Dispose();
                    }
                    else
                    {
                        Sotuv.shop = false;
                    }
                }
                tbShopId.Clear();
                tbShopId.Dispose();
            }
            catch (Exception ex)
            {

            }*/

            string queryFilial_id = "select * from filial";
            tbFilial_id = new DataTable();
            objDBAccess.readDatathroughAdapter(queryFilial_id, tbFilial_id);
            filial_id = tbFilial_id.Rows[0]["id"].ToString();
            filial = tbFilial_id.Rows[0]["name"].ToString();
            tbFilial_id.Dispose();

            string send = "select * from send";
            DataTable tbSend = new DataTable();
            objDBAccess.readDatathroughAdapter(send, tbSend);
            if (tbSend.Rows.Count > 0)
            {
                string password = tbSend.Rows[0]["password"].ToString();
                if (password == Kirish_ucont.password1)
                {
                    //TimerSend.Enabled = true;
                }
            }
            tbSend.Dispose();
        }
    }
}
