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
using System.Net.Http;
using System.Globalization;
using System.Windows.Media.Effects;
using System.Threading;

namespace SotuvPlatformasi
{
    /// <summary>
    /// Interaction logic for EditPrice.xaml
    /// </summary>
    public partial class EditPrice : Window
    {
        public EditPrice()
        {
            InitializeComponent();
        }
        public string product = "", old_price = "", barcode = "", edit_id = "", val_ul="";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtProduct.Text = product;
            txtOldPrice.Text = old_price;
            txtVal_ul.Text = val_ul;
            txtNewPrice.Focus();
        }

        DBAccess objDBAccess = new DBAccess();

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        void Simulator()
        {
            for (int i = 0; i < 80; i++)
                Thread.Sleep(5);
        }
        BlurEffect myEffect = new BlurEffect();

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (txtNewPrice.Text == "") return;
            if (txtNewPrice.Text.IndexOf(',') > -1) { txtNewPrice.Text = ""; System.Windows.Forms.MessageBox.Show("Nuqta bilan kiriting!", "Xabar", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error); return; }
            string new_price = txtNewPrice.Text;
            string som = "0", dollar = "0";
            if(txtVal_ul.Text == "uz") { som = new_price; }
            if(txtVal_ul.Text == "$") { dollar = new_price; }
            Uri u = new Uri("http://turonsavdo.backoffice.uz/api/changepriceitem/add/");

            var payload = "{\"filial\": \""+MainWindow.filial_id+"\",\"items\": [{\"barcode\": \"" + barcode + "\",\"som\": \"" + som + "\",\"dollar\": \""+dollar+"\"}]}";
            HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");
            var t = Task.Run(() => PostURI(u, content));
            myEffect.Radius = 10;
            Effect = myEffect;
            using (LoadingWindow lw = new LoadingWindow(Simulator))
            {
                lw.ShowDialog();
            }
            myEffect.Radius = 0;
            Effect = myEffect;
            t.Wait();
            if (t.Result == "Error!")
            {
                System.Windows.Forms.MessageBox.Show("Server bilan bog'lanishda xatolik, iltimos tekshirib ko'ring!", "Xatolik", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }
            else if (t.Result != "Error!")
            {
                string str_oldPrice = DoubleToStr(txtOldPrice.Text);
                double Dold_price = double.Parse(str_oldPrice);
                string str_newPrice = DoubleToStr(txtNewPrice.Text);
                double Dnew_Price = double.Parse(str_newPrice);
                double difference = Dnew_Price - Dold_price; // bitta maxsulot farqi

                //Product jadvalidan qoldiqni olamiz
                string queryProductQuan = "select quantity from product where barcode='" + barcode + "'";
                DataTable tbProductQuan = new DataTable();
                objDBAccess.readDatathroughAdapter(queryProductQuan, tbProductQuan);
                string str_quan = tbProductQuan.Rows[0]["quantity"].ToString();
                str_quan = DoubleToStr(str_quan);
                double Dquan = double.Parse(str_quan);
                tbProductQuan.Clear();
                tbProductQuan.Dispose();
                double totalPr_diff = difference * Dquan; // Umumiy farq

                //Pricecart jadvalidan id ni belgilab olamiz
                double prcId = 0;
                string queryPrId = "select id from pricecart order by id desc limit 1";
                DataTable tbPrId = new DataTable();
                objDBAccess.readDatathroughAdapter(queryPrId, tbPrId);
                if (tbPrId.Rows.Count == 0) { prcId = 1; }
                else { prcId = int.Parse(tbPrId.Rows[0]["id"].ToString(), CultureInfo.InvariantCulture) + 1; }
                tbPrId.Clear();
                tbPrId.Dispose();

                //Changedprice jadvalidan ushbu kundagi narx o'zgarish bor yoki yo'qligini tekshiramiz
                string queryDif = "select difference_som, difference_dollar from changedprice where id='" + edit_id + "'";
                DataTable tbDifference = new DataTable();
                objDBAccess.readDatathroughAdapter(queryDif, tbDifference);

                if (tbDifference.Rows.Count > 0) // Agar ushbu kunda o'zgartirilgan bo'lsa difference+=totalPr_diff bo'ladi
                {
                    string str_diff_som = tbDifference.Rows[0]["difference_som"].ToString();
                    str_diff_som = DoubleToStr(str_diff_som);
                    double old_diff_som = double.Parse(str_diff_som);
                    double total_diff_som = old_diff_som ; // umumiy farq

                    string str_diff_dollar = tbDifference.Rows[0]["difference_dollar"].ToString();
                    str_diff_dollar = DoubleToStr(str_diff_dollar);
                    double old_diff_dollar = double.Parse(str_diff_dollar);
                    double total_diff_dollar = old_diff_dollar;

                    if(txtVal_ul.Text == "uz") { total_diff_som += totalPr_diff; }
                    if(txtVal_ul.Text == "$") { total_diff_dollar += totalPr_diff; }

                    cmdChangedPrice = new MySqlCommand("update changedprice set difference_som='"+ DoubleToStr(total_diff_som.ToString()) + "', difference_dollar='"+DoubleToStr(total_diff_dollar.ToString())+"' where id='" + edit_id + "'");
                    objDBAccess.executeQuery(cmdChangedPrice);
                    cmdChangedPrice.Dispose();
                }
                else // Agar ushbu sanada bo'lmasa difference = totalPr_diff ga teng bo'ladi
                {
                    string difference_som = "0", difference_dollar = "0";
                    if(txtVal_ul.Text == "uz") { difference_som = DoubleToStr(totalPr_diff.ToString()); }
                    if(txtVal_ul.Text == "$") { difference_dollar = DoubleToStr(totalPr_diff.ToString()); }
                    cmdChangedPrice = new MySqlCommand("update changedprice set difference_som='"+difference_som+"', difference_dollar='" + difference_dollar + "' where id='" + edit_id + "'");
                    objDBAccess.executeQuery(cmdChangedPrice);
                    cmdChangedPrice.Dispose();
                }
                tbDifference.Clear();
                tbDifference.Dispose();
                string val_id = "0";
                if(txtVal_ul.Text == "uz") { val_id = "1"; }
                if(txtVal_ul.Text == "$") { val_id = "2"; }
                cmdPriceCart = new MySqlCommand("insert into pricecart (id,ch_id,pr_name,old_price,new_price,residue,difference,total_diff, val_id) values('" + prcId + "','" + edit_id.ToString() + "','" + product + "','" + str_oldPrice + "','" + str_newPrice + "','" + str_quan + "','" + DoubleToStr(difference.ToString()) + "','" + DoubleToStr(totalPr_diff.ToString()) + "','"+val_id+"')");
                objDBAccess.executeQuery(cmdPriceCart);
                cmdPriceCart.Dispose();
                
                cmdProduct = new MySqlCommand("update product set price='" + str_newPrice + "' where barcode='" + barcode + "'");
                objDBAccess.executeQuery(cmdProduct);
                cmdProduct.Dispose();
                ChangePrice_ucont.editPrice = true;
                this.Close();
            }
        }

        public static MySqlCommand cmdPriceCart, cmdChangedPrice, cmdProduct;

        static async Task<string> PostURI(Uri u, HttpContent c)
        {
            var response = string.Empty;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "token d0347b90933d3d4b4fbd2d30fb2dd79d824091bc");
                try
                {
                    HttpResponseMessage result = await client.PostAsync(u, c);
                    if (result.IsSuccessStatusCode)
                    {
                        using (HttpContent content = result.Content)
                        {
                            response = await content.ReadAsStringAsync();
                        }
                    }
                    else
                    {
                        response = "Error!";
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }
            return response;
        }

        public string DoubleToStr(string s)
        {
            if (s.IndexOf(',') > -1)
            {
                int index = s.IndexOf(',');
                string first = s.Substring(0, index);
                string last = s.Substring(index + 1);
            }
            return s;
        }


    }
}
