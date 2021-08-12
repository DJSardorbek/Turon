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
using System.Windows.Shapes;
using System.Data;
using MySql.Data.MySqlClient;

namespace SotuvPlatformasi
{
    /// <summary>
    /// Interaction logic for NavbatClient.xaml
    /// </summary>
    public partial class NavbatClient : Window
    {
        public NavbatClient()
        {
            InitializeComponent();
        }

        DBAccess objDBAccess = new DBAccess();
        public static MySqlCommand cmd, cmdShopId;
        public static DataTable tbShopId, tbClient;
        public string total_som = "0", total_dollar = "0";
        public static bool can_refresh = false;

        public class Client
        {
            public int id { get; set; }
            public string mijoz_fish { get; set; }
            public double qarz_som { get; set; }
            public double qarz_dollar { get; set; }

        }
        public List<Client> clients = new List<Client>();
        public List<Client> GetClientList()
        {
            clients = (from DataRow dr in tbClient.Rows
                       select new Client()
                       {
                           id = Convert.ToInt32(dr["id"]),
                           mijoz_fish = dr["mijoz_fish"].ToString(),
                           qarz_som = Convert.ToDouble(dr["qarz_som"]),
                           qarz_dollar = Convert.ToDouble(dr["qarz_dollar"])
                       }).ToList();
            return clients;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string queryClient = "select * from debtor";
            tbClient = new DataTable();
            objDBAccess.readDatathroughAdapter(queryClient, tbClient);
            dataGridClient.ItemsSource = GetClientList();
            tbClient.Dispose();
            txtSearch.Focus();
        }

        public void Refresh(bool c_refresh)
        {
            if (c_refresh)
            {
                string queryClient = "select * from debtor";
                tbClient.Clear();
                objDBAccess.readDatathroughAdapter(queryClient, tbClient);
                dataGridClient.ItemsSource = GetClientList();
                txtSearch.Focus();
            }
            else
            {
                return;
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            NewClient newClient = new NewClient();
            can_refresh = false;
            newClient.ShowDialog();
            Refresh(can_refresh);
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearch.Text != "")
            {
                string queryClient = "select * from debtor where mijoz_fish LIKE '%" + txtSearch.Text + "%'";
                tbClient.Clear();
                objDBAccess.readDatathroughAdapter(queryClient, tbClient);
                dataGridClient.ItemsSource = GetClientList();
            }
            else
            {
                string queryQaytuv = "select * from debtor";
                tbClient.Clear();
                objDBAccess.readDatathroughAdapter(queryQaytuv, tbClient);
                dataGridClient.ItemsSource = GetClientList();
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

        private void btnChoose_Click(object sender, RoutedEventArgs e)
        {
            if (Sotuv_ucont.shopID == 0 || Sotuv_ucont.tbBasket.Rows.Count == 0)
            {
                return;
            }
            Client selectedClient = dataGridClient.SelectedItems[0] as Client;
            string debtor_id = selectedClient.id.ToString();
            string total_s = "0", total_d = "0";
            if (total_som != "")
            {
                total_s = total_som;
                total_s = DoubleToStr(total_s);
            }
            if (total_dollar != "")
            {
                total_d = total_dollar;
                total_d = DoubleToStr(total_d);
            }
            cmd = new MySqlCommand("update shop set total_som='" + total_s + "', total_dollar='" + total_d + "',debtor_id='"+debtor_id+"', queue=1 where id='" + Sotuv_ucont.shopID + "'");
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
            Sotuv_ucont.shopID = 0;
            Sotuv_ucont.shop = false;
            Sotuv_ucont.navbat = true;
            Sotuv_ucont.debtor_id = "1";
            this.Close();
        }
    }
}
