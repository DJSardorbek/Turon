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
using MySql.Data.MySqlClient;
using System.Data;

namespace SotuvPlatformasi
{
    /// <summary>
    /// Interaction logic for ChooseClient.xaml
    /// </summary>
    public partial class ChooseClient : Window
    {
        public ChooseClient()
        {
            InitializeComponent();
        }

        DBAccess objDBAccess = new DBAccess();
        public static DataTable tbClient;
        public static bool can_refresh = false;
        public class Client
        {
            public int id { get; set; }
            public string mijoz_fish { get; set; }
            public string tel_1 { get; set; }
            public double qarz_som { get; set; }
            public double qarz_dollar { get; set; }

        }
        public List<Client> clients=new List<Client>();
        public List<Client> GetClientList()
        {
            clients = (from DataRow dr in tbClient.Rows
                       select new Client()
                       {
                           id = Convert.ToInt32(dr["id"]),
                           mijoz_fish = dr["mijoz_fish"].ToString(),
                           tel_1 = dr["tel_1"].ToString(),
                           qarz_som = Convert.ToDouble(dr["qarz_som"]),
                           qarz_dollar = Convert.ToDouble(dr["qarz_dollar"])
                       }).ToList();
            return clients;
        }
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearch.Text != "")
            {
                string queryClient = "select * from debtor where mijoz_fish LIKE '%"+txtSearch.Text+"%'";
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

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            NewClient newClient = new NewClient();
            can_refresh = false;
            newClient.ShowDialog();
            Refresh(can_refresh);

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
            if(c_refresh)
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

        private void btnChoose_Click(object sender, RoutedEventArgs e)
        {
            Client selectecClient = dataGridClient.SelectedItems[0] as Client;
            tulov_click.xaq_som = selectecClient.qarz_som.ToString();
            tulov_click.xaq_dollar = selectecClient.qarz_dollar.ToString();
            tulov_click.debtor = selectecClient.mijoz_fish;
            tulov_click.tel_1 = selectecClient.tel_1;
            tulov_click.debtor_id = selectecClient.id.ToString();
            tulov_click.can_client = true;
            this.Close();
        }
    }
}
