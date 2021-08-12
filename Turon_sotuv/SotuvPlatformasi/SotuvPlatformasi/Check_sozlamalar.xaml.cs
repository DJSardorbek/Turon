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
    /// Interaction logic for Check_sozlamalar.xaml
    /// </summary>
    public partial class Check_sozlamalar : Page
    {
        public Check_sozlamalar()
        {
            InitializeComponent();
        }
        DBAccess objDBAccess = new DBAccess();
        public static MySqlCommand cmd;
        MainWindow main = (MainWindow)Application.Current.MainWindow;
        private void BtnAsosiy_Click(object sender, RoutedEventArgs e)
        {
            main.TabMenu.SelectedIndex = 1;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string queryChek = "select * from chek";
            DataTable tbChek = new DataTable();
            objDBAccess.readDatathroughAdapter(queryChek, tbChek);
            if(tbChek.Rows.Count> 0)
            {
                cmd = new MySqlCommand("update chek set header='" + txtHeader.Text + "', footer='" + txtFooter.Text + "'");
                objDBAccess.executeQuery(cmd);
                cmd.Dispose();
                System.Windows.Forms.MessageBox.Show("Chek muvaffaqiyatli saqlab olindi!", "Xabar", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            else
            {
                cmd = new MySqlCommand("insert into chek (id,header,footer) values(1,'" + txtHeader.Text + "', '" + txtFooter.Text + "')");
                objDBAccess.executeQuery(cmd);
                cmd.Dispose();
                System.Windows.Forms.MessageBox.Show("Chek muvaffaqiyatli saqlab olindi!", "Xabar", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            string queryChek = "select * from chek";
            DataTable tbChek = new DataTable();
            objDBAccess.readDatathroughAdapter(queryChek, tbChek);
            txtHeader.Text = tbChek.Rows[0]["header"].ToString();
            txtFooter.Text = tbChek.Rows[0]["footer"].ToString();
            tbChek.Dispose();
        }
    }
}
