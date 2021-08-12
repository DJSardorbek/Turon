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
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Media.Effects;

namespace SotuvPlatformasi
{
    /// <summary>
    /// Interaction logic for Kirish_ucont.xaml
    /// </summary>
    public partial class Kirish_ucont : UserControl
    {
        public Kirish_ucont()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The Global variables
        /// </summary>
        public static string sellar_id, staff_id;// filial_id;
        public static string sellar = "";
        public static string mac_address = "";
        public static string password1 = "";
        DBAccess objDBAccess = new DBAccess();
        DataTable tbPassword;
        

        /// <summary>
        /// For getting the device mac_adress
        /// </summary>
        public void GetMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty) // only return MAC Address from first card
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            mac_address = sMacAddress;
        }


        /// <summary>
        ///  The enter pressed on passwordbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                BtnLogin_Click(sender, e);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtPassword.Focus();
        }

        /// <summary>
        /// BtnLogin clickked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        void Simulator()
        {
            for (int i = 0; i < 80; i++)
                Thread.Sleep(5);
        }
        BlurEffect myEffect = new BlurEffect();
        public void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            myEffect.Radius = 10;
            Effect = myEffect;
            using(LoadingWindow lw = new LoadingWindow(Simulator))
            {
                lw.ShowDialog();
            }
            myEffect.Radius = 0;
            Effect = myEffect;
            if (txtPassword.Password == "")
            {
                MessageBox.Show("Parolni kiriting!", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            string password = txtPassword.Password;
            string query;
            try
            {
                
                while(password.IndexOf('\"') > -1)
                {
                    int index = password.IndexOf('\"');
                    string first = password.Substring(0, index);
                    string last = password.Substring(index + 1);
                    password = first + "\'" + last;
                }
                query = "select * from userprofile where password=\""+password+"\"";
                objDBAccess.createConn();
                tbPassword = new DataTable();
                objDBAccess.readDatathroughAdapter(query, tbPassword);
                objDBAccess.closeConn();
            }
            catch(Exception exc) { MessageBox.Show(exc.Message); }
            
            if (tbPassword.Rows.Count == 1)
            {
                sellar_id = tbPassword.Rows[0]["id"].ToString();
                staff_id = tbPassword.Rows[0]["staff_id"].ToString();
                sellar = tbPassword.Rows[0]["first_name"].ToString();
                password1 = tbPassword.Rows[0]["password"].ToString();
                GetMACAddress();

                //Asosiy f2 = new Asosiy();
                //f2.Show();
                MainWindow main = (MainWindow)Application.Current.MainWindow;
                main.SetShop();
                main.TabMenu.SelectedIndex = 1;
                tbPassword.Clear();
                tbPassword.Dispose();
                txtPassword.Clear();
                main.startTimer();

                string queryFilial_id = "select * from filial";
                MainWindow.tbFilial_id = new DataTable();
                objDBAccess.readDatathroughAdapter(queryFilial_id, MainWindow.tbFilial_id);
                MainWindow.filial_id = MainWindow.tbFilial_id.Rows[0]["id"].ToString();
                MainWindow.filial = MainWindow.tbFilial_id.Rows[0]["name"].ToString();
                MainWindow.tbFilial_id.Dispose();
            }
            else
            {
                MessageBox.Show("Parol noto'g'ri kiritildi, qaytadan urinib ko'ring!", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
    }
}
