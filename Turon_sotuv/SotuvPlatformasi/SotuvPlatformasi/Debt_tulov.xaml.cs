using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for Debt_tulov.xaml
    /// </summary>
    public partial class Debt_tulov : Window
    {
        public Debt_tulov()
        {
            InitializeComponent();


        }
        public string debtor_id = "", qarz_som= "", qarz_dollar="", debtor = "";
        public static MySqlCommand cmdDebtor, cmdPayhistory, cmdCart;
        DBAccess objDBAccess = new DBAccess();
        public static DataTable tbValyuta;

        private void ckNaqd_Click(object sender, RoutedEventArgs e)
        {
            if (ckNaqd.IsChecked == true)
            {
                StackNaqd.Visibility = Visibility.Visible;
                txtNaqd.Focus();
            }
            if (ckNaqd.IsChecked == false)
            {
                StackNaqd.Visibility = Visibility.Collapsed;
            }
        }

        private void chkKarta_Click(object sender, RoutedEventArgs e)
        {
            if (chkKarta.IsChecked == true)
            {
                StackKarta.Visibility = Visibility.Visible;
                txtKarta.Focus();
            }
            if (chkKarta.IsChecked == false)
            {
                StackKarta.Visibility = Visibility.Collapsed;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape)
            {
                Qarzdorlar_ucont.payment = false;
                this.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           txtQarzSom.Text = qarz_som;
            if (qarz_dollar != "0") { txtQarzDollar.Text = qarz_dollar; }
           string queryValyuta = "select * from valyuta";
           tbValyuta = new DataTable();
           objDBAccess.readDatathroughAdapter(queryValyuta, tbValyuta);
           tbValyuta.Dispose();
        }

        private void ckCurreny_Click(object sender, RoutedEventArgs e)
        {
            if (ckCurreny.IsChecked == true)
            {
                string queryValyuta = "select * from valyuta";
                tbValyuta.Clear();
                objDBAccess.readDatathroughAdapter(queryValyuta, tbValyuta);
                txtKurs.Text = tbValyuta.Rows[0]["kurs"].ToString();
                StackCurrency.Visibility = Visibility.Visible;
                txtCurrency.Focus();
            }
            if (ckCurreny.IsChecked == false)
            {
                StackCurrency.Visibility = Visibility.Collapsed;
                txtValSum.Text = "";
                txtCurrency.Text = "";
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtCurrency_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtCurrency.Text != "" && txtKurs.Text != "")
            {
                try
                {
                    double Dcurrency = double.Parse(DoubleToStr(txtCurrency.Text), CultureInfo.InvariantCulture);
                    double Dkurs = double.Parse(DoubleToStr(txtKurs.Text), CultureInfo.InvariantCulture);
                    double DSumda = Dcurrency * Dkurs;
                    txtValSum.Text = DoubleToStr(DSumda.ToString());
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message, "Xatolik", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            else
            {
                txtValSum.Text = "0";
            }
        }

        private void ckTransfer_Click(object sender, RoutedEventArgs e)
        {
            if (ckTransfer.IsChecked == true)
            {
                StackTransfer.Visibility = Visibility.Visible;
                txtPulutkazish.Focus();
            }
            if (ckTransfer.IsChecked == false)
            {
                StackTransfer.Visibility = Visibility.Collapsed;
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

        private void BtnTulov_Click(object sender, RoutedEventArgs e)
        {
            if (txtNaqd.Text.IndexOf(',') > -1 || txtKarta.Text.IndexOf(',') > -1 || txtPulutkazish.Text.IndexOf(',') > -1)
            {
                System.Windows.Forms.MessageBox.Show("To'lovni tiyinini nuqtadan keyin kiriting!", "Xatolik", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }
            string naqd = "0", plastik = "0", transfer = "0", valsum="0", currency="0";
            if(StackNaqd.Visibility == Visibility.Visible && txtNaqd.Text!="")
            {
                naqd = txtNaqd.Text;
            }

            if (StackKarta.Visibility == Visibility.Visible && txtKarta.Text != "")
            {
                plastik = txtKarta.Text;
            }

            if (StackTransfer.Visibility == Visibility.Visible && txtPulutkazish.Text != "")
            {
                transfer = txtPulutkazish.Text;
            }
            if (StackCurrency.Visibility == Visibility.Visible && txtValSum.Text != "" && txtKurs.Text != "" && txtCurrency.Text != "")
            {
                currency = txtCurrency.Text;
                currency = DoubleToStr(currency);
                valsum = txtValSum.Text;
                valsum = DoubleToStr(valsum);
                cmdCart = new MySqlCommand("update valyuta set kurs='" + DoubleToStr(txtKurs.Text) + "'");
                objDBAccess.executeQuery(cmdCart);
                cmdCart.Dispose();
            }
            double Dtulov_dollar = 0;
            double Dqolgan_dollar = 0;
            double Dqolgan_som = 0;
            double Dtulov_som = 0;

            //Agar valyuta $ da to'lanayotgan bo'lsa
            if (StackCurrency.Visibility == Visibility.Visible)
            {
                if (txtQarzDollar.Text != "0.0")
                {
                    Dqolgan_dollar = double.Parse(DoubleToStr(qarz_dollar), CultureInfo.InvariantCulture);
                    if (txtCurrency.Text != "") { Dtulov_dollar = double.Parse(DoubleToStr(txtCurrency.Text), CultureInfo.InvariantCulture); }

                    if (Dtulov_dollar > Dqolgan_dollar)
                    {
                        System.Windows.Forms.MessageBox.Show("To'lov summasi ortiqcha kiritildi!", "Xatolik", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }

                    Dqolgan_som = double.Parse(DoubleToStr(qarz_som), CultureInfo.InvariantCulture);
                    Dtulov_som = double.Parse(naqd, CultureInfo.InvariantCulture) + double.Parse(plastik, CultureInfo.InvariantCulture) + double.Parse(transfer, CultureInfo.InvariantCulture);

                    if (Dtulov_som > Dqolgan_som)
                    {
                        System.Windows.Forms.MessageBox.Show("To'lov summasi ortiqcha kiritildi!", "Xatolik", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    Dqolgan_som = double.Parse(DoubleToStr(qarz_som), CultureInfo.InvariantCulture);
                    Dtulov_som = double.Parse(naqd, CultureInfo.InvariantCulture) + double.Parse(plastik, CultureInfo.InvariantCulture) + double.Parse(transfer, CultureInfo.InvariantCulture) + double.Parse(valsum, CultureInfo.InvariantCulture);

                    if (Dtulov_som > Dqolgan_som)
                    {
                        System.Windows.Forms.MessageBox.Show("To'lov summasi ortiqcha kiritildi!", "Xatolik", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                Dqolgan_som = double.Parse(DoubleToStr(qarz_som), CultureInfo.InvariantCulture);
                Dtulov_som = double.Parse(naqd, CultureInfo.InvariantCulture) + double.Parse(plastik, CultureInfo.InvariantCulture) + double.Parse(transfer, CultureInfo.InvariantCulture);

                if (Dtulov_som > Dqolgan_som)
                {
                    System.Windows.Forms.MessageBox.Show("To'lov summasi ortiqcha kiritildi!", "Xatolik", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return;
                }
            }
            double result_qarz_dollar = Dqolgan_dollar - Dtulov_dollar;
            double result_qarz_som = Dqolgan_som - Dtulov_som;
            //debtor jadvalidan debtorni umumiy_qarzini kamaytiramiz
            cmdDebtor = new MySqlCommand("update debtor set qarz_som='" + DoubleToStr(result_qarz_som.ToString()) + "', qarz_dollar='"+DoubleToStr(result_qarz_dollar.ToString())+"' where id='" + debtor_id + "'");
            objDBAccess.executeQuery(cmdDebtor);
            cmdDebtor.Dispose();

            //payhistory jadvaliga to'langan summani kiritib qo'yamiz
            string pay_id = "select * from payhistory order by id desc limit 1";
            DataTable tbPay_id = new DataTable();
            objDBAccess.readDatathroughAdapter(pay_id, tbPay_id);
            DateTime dt_now = DateTime.Now;
            string kurs = "0";
            if(StackCurrency.Visibility == Visibility.Visible)
            {
                kurs = txtKurs.Text;
                kurs = DoubleToStr(kurs);
            }
            else
            {
                kurs = tbValyuta.Rows[0]["kurs"].ToString();
                kurs = DoubleToStr(kurs);
            }
            if (tbPay_id.Rows.Count == 0) // Agar payhistory jadvali bo'sh bo'lsa id = 1 bo'ladi
            {
                cmdPayhistory = new MySqlCommand("insert into payhistory (id,debtor_id, given_som,given_dollar,kurs, date, status_server) values(1,'" + debtor_id + "','" + DoubleToStr(Dtulov_som.ToString()) + "','"+DoubleToStr(Dtulov_dollar.ToString())+"','"+kurs+"','" + dt_now.ToString("yyyy-MM-dd hh:mm:ss") + "',0)");
                objDBAccess.executeQuery(cmdPayhistory);
                cmdPayhistory.Dispose();
            }
            else
            {
                cmdPayhistory = new MySqlCommand("insert into payhistory (debtor_id, given_som,given_dollar,kurs, date, status_server) values('" + debtor_id + "','" + DoubleToStr(Dtulov_som.ToString()) + "','"+DoubleToStr(Dtulov_dollar.ToString())+"','"+kurs+"','" + dt_now.ToString("yyyy-MM-dd hh:mm:ss") + "',0)");
                objDBAccess.executeQuery(cmdPayhistory);
                cmdPayhistory.Dispose();
            }
            tbPay_id.Dispose();

            System.Windows.Forms.MessageBox.Show("To'lov muvaffaqiyatli amalga oshirildi!", "Xabar", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

            Qarzdorlar_ucont.payment = true;
            Close();

        }
    }
}
