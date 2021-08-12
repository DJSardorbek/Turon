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

namespace SotuvPlatformasi
{
    /// <summary>
    /// Interaction logic for Staff_list.xaml
    /// </summary>
    public partial class Staff_list : Page
    {
        public Staff_list()
        {
            InitializeComponent();
        }
        public static DataTable tbUsers;
        DBAccess objDBAccess = new DBAccess();


        public class Staff
        {
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string staff { get; set; }
            public string password { get; set; }

        }

        public List<Staff> staffs = new List<Staff>();
        
        public List<Staff> GetStaff()
        {
            staffs = (from DataRow dt in tbUsers.Rows
                      select new Staff()
                      {
                          first_name = dt["first_name"].ToString(),
                          last_name = dt["last_name"].ToString(),
                          staff = dt["staff"].ToString(),
                          password = dt["password"].ToString()
                      }).ToList();
            
            return staffs;
        }

        private void BtnAsosiy_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = (MainWindow)Application.Current.MainWindow;
            main.TabMenu.SelectedIndex = 1;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            string queryUsers = "select first_name, last_name, staff.staff, username, password from userprofile inner join staff where userprofile.staff_id = staff.id";
            tbUsers = new DataTable();
            objDBAccess.readDatathroughAdapter(queryUsers, tbUsers);
            tbUsers.Dispose();

            dataGridStaffs.ItemsSource = GetStaff();

        }
    }
}
