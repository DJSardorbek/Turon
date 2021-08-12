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
    /// Interaction logic for DebtorPayHistory_ucont.xaml
    /// </summary>
    public partial class DebtorPayHistory_ucont : UserControl
    {
        public DebtorPayHistory_ucont()
        {
            InitializeComponent();
        }

        DBAccess objDBAccess = new DBAccess();
        public static DataTable tbDebtorPay;
        public static string debtor_id = "";
        public class DebtorPay
        {
            public int id { get; set; }
            public string mijoz_fish { get; set; }
            public double som { get; set; }
            public double dollar { get; set; }
            public double kurs { get; set; }
            public string date { get; set; }
        }

        List<DebtorPay> debtorPayList = new List<DebtorPay>();

        public List<DebtorPay> GetDebtorPayList()
        {
            debtorPayList = (from DataRow dr in tbDebtorPay.Rows
                             select new DebtorPay()
                             {
                                 id = Convert.ToInt32(dr["id"]),
                                 mijoz_fish = dr["mijoz_fish"].ToString(),
                                 som = Convert.ToDouble(dr["given_som"]),
                                 dollar = Convert.ToDouble(dr["given_dollar"]),
                                 kurs = Convert.ToDouble(dr["kurs"]),
                                 date = dr["date"].ToString()
                             }).ToList();
            return debtorPayList;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = (MainWindow)Application.Current.MainWindow;
            main.TabMenu.SelectedIndex = 9;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            string queryPayHistory = "select payhistory.id, debtor.mijoz_fish, payhistory.given_som, payhistory.given_dollar, payhistory.kurs, payhistory.date from payhistory " +
                "inner join debtor on payhistory.debtor_id = debtor.id " +
                "where payhistory.debtor_id='"+debtor_id+"'";
            tbDebtorPay = new DataTable();
            objDBAccess.readDatathroughAdapter(queryPayHistory, tbDebtorPay);
            dataGridDebtorReport.ItemsSource = GetDebtorPayList();
            tbDebtorPay.Dispose();
        }

        private void DatePickerDan_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatePickerGacha.Text == "")
            {
                return;
            }
            if (DatePickerGacha.Text != "" && DatePickerDan.Text != "")
            {
                DateTime dateDan = DatePickerDan.SelectedDate.Value.Date;
                DateTime dateGacha = DatePickerGacha.SelectedDate.Value.Date;
                if (dateGacha > dateDan)
                {
                    string dt_dan = dateDan.Date.ToString("yyyy-MM-dd");
                    string dt_gacha = dateGacha.Date.ToString("yyyy-MM-dd");
                    string queryPayHistory = "select payhistory.id, debtor.mijoz_fish, payhistory.given_som, payhistory.given_dollar, payhistory.kurs, payhistory.date from payhistory " +
                    "inner join debtor on payhistory.debtor_id = debtor.id " +
                    "where payhistory.debtor_id='" + debtor_id + "' and payhistory.date between '"+dt_dan+"' and '"+dt_gacha+"'";
                    tbDebtorPay = new DataTable();
                    objDBAccess.readDatathroughAdapter(queryPayHistory, tbDebtorPay);
                    dataGridDebtorReport.ItemsSource = GetDebtorPayList();

                }
            }
        }

        private void DatePickerGacha_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatePickerDan.Text == "")
            {
                return;
            }
            if (DatePickerGacha.Text != "" && DatePickerDan.Text != "")
            {
                DateTime dateDan = DatePickerDan.SelectedDate.Value.Date;
                DateTime dateGacha = DatePickerGacha.SelectedDate.Value.Date;
                if (dateGacha > dateDan)
                {
                    string dt_dan = dateDan.Date.ToString("yyyy-MM-dd");
                    string dt_gacha = dateGacha.Date.ToString("yyyy-MM-dd");
                    string queryPayHistory = "select payhistory.id, debtor.mijoz_fish, payhistory.given_som, payhistory.given_dollar, payhistory.kurs, payhistory.date from payhistory " +
                    "inner join debtor on payhistory.debtor_id = debtor.id " +
                    "where payhistory.debtor_id='" + debtor_id + "' and payhistory.date between '" + dt_dan + "' and '" + dt_gacha + "'";
                    tbDebtorPay = new DataTable();
                    objDBAccess.readDatathroughAdapter(queryPayHistory, tbDebtorPay);
                    dataGridDebtorReport.ItemsSource = GetDebtorPayList();
                }
            }
        }
    }
}
