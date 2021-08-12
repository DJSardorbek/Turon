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
    /// Interaction logic for NewClient.xaml
    /// </summary>
    public partial class NewClient : Window
    {
        public NewClient()
        {
            InitializeComponent();
        }
        
        public static MySqlCommand cmd;
        DBAccess objDBAccess = new DBAccess();

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(txtMijozFish.Text =="" || txtPhone1.Text=="")
            {
                System.Windows.Forms.MessageBox.Show("Ma'lumotlar to'liq emas,\nIltimos tekshirib qaytadan urinib ko'ring!");
                return;
            }
            else
            {
                try
                {
                    int id = 1;
                    string queryDebtorId = "select id from debtor order by id desc limit 1";
                    DataTable tbDebtorId = new DataTable();
                    objDBAccess.readDatathroughAdapter(queryDebtorId, tbDebtorId);
                    if(tbDebtorId.Rows.Count > 0)
                    {
                        id = int.Parse(tbDebtorId.Rows[0]["id"].ToString(), System.Globalization.CultureInfo.InvariantCulture) + 1;
                    }
                    cmd = new MySqlCommand("insert into debtor (id,mijoz_fish,tel_1,tel_2,qarz_som,qarz_dollar,difference,status_difference) " +
                        "values('"+id+"','"+txtMijozFish.Text+"','"+txtPhone1.Text+"','"+txtPhone2.Text+"',0,0,0,0)");
                    objDBAccess.executeQuery(cmd);
                    cmd.Dispose();
                    System.Windows.Forms.MessageBox.Show("Yangi mijoz muvaffaqiyatli qo'shildi!","Xabar", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    ChooseClient.can_refresh = true;
                    this.Close();
                }
                catch(Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message, "Xatolik", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
        }
    }
}
