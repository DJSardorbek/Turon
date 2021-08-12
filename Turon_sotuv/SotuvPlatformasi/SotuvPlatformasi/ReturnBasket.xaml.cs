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
using System.Globalization;

namespace SotuvPlatformasi
{
    /// <summary>
    /// Interaction logic for ReturnBasket.xaml
    /// </summary>
    public partial class ReturnBasket : Window
    {
        public ReturnBasket()
        {
            InitializeComponent();
        }

        public string name = "", zavod = "", ulchov = "", tan_narx = "", sot_narx = "", val_ul = "", retsum_id = "", barcode="", quantity="";

        private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                Button_Click(sender, e);
            }
        }

        public static MySqlCommand cmd;
        DBAccess objDBAccess = new DBAccess();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtProduct.Text = name;
            txtZavod.Text = zavod;
            txtUlchov.Text = ulchov;
            txtTanNarx.Text = tan_narx;
            txtSotishNarx.Text = sot_narx;
            txtValUl.Text = val_ul;
            txtQuantity.Focus();

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public string DoubleToStr(string s)
        {
            if(s.IndexOf(',')> -1)
            {
                int index = s.IndexOf(',');
                string first = s.Substring(0, index);
                string last = s.Substring(index + 1);
                s = first + "." + last;
            }
            return s;
        }

        public int GetId(string SqlTable)
        {
            int id = 0;
            string query = "select id from " + SqlTable + " order by id desc limit 1";
            DataTable tb = new DataTable();
            objDBAccess.readDatathroughAdapter(query, tb);
            if (tb.Rows.Count > 0)
            {
                id = Convert.ToInt32(tb.Rows[0]["id"]) + 1;
            }
            else
            {
                id = 1;
            }
            tb.Clear();
            tb.Dispose();
            return id;
        }


        public void Button_Click(object sender, RoutedEventArgs e)
        {
            if(txtQuantity.Text=="")
            {
                return;
            }
            if(double.Parse(txtQuantity.Text, CultureInfo.InvariantCulture) > double.Parse(quantity))
            {
                System.Windows.Forms.MessageBox.Show("Qaytarish miqdor ko'p kiritildi!\nOmbordagi miqdor '" + quantity + "'", "Xatolik", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }
            try
            {
                string val_id = "0";
                if (txtValUl.Text == "uz") { val_id = "1"; }
                else { val_id = "2"; }
                int id = GetId("retdelproduct");
                cmd = new MySqlCommand("insert into retdelproduct (id,retsum_id,pr_name,preparer,measurement,tan_narx,sotish_narx,val_id,barcode,qayt_miqdor) " +
                    "values('" + id + "','" + retsum_id + "','" + name + "','" + zavod + "','" + ulchov + "','" + DoubleToStr(tan_narx) + "','" + DoubleToStr(sot_narx) + "','" + val_id + "','" + barcode + "','" + DoubleToStr(txtQuantity.Text) + "')");
                objDBAccess.executeQuery(cmd);
                cmd.Dispose();
                ReturnDeliver_ucont.ReturnBasket = true;
                this.Close();
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
    }
}
