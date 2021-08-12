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
    /// Interaction logic for DebtorReport.xaml
    /// </summary>
    public partial class DebtorReport : UserControl
    {
        public DebtorReport()
        {
            InitializeComponent();
        }

        DBAccess objDBAccess = new DBAccess();

        public static string debtor_id = "";
        public static DataTable tbDebtorReport;

        public class debtorReport
        {
            public int id { get; set; }
            public string date { get; set; }
            public string mijoz_fish { get; set; }
            public double kurs { get; set; }
            public double qarz_som { get; set; }
            public double qarz_dollar { get; set; }
        }

        public static List<debtorReport> debtorReports = new List<debtorReport>();

        public List<debtorReport> GetDebtorReports()
        {
            debtorReports = (from DataRow dr in tbDebtorReport.Rows
                             select new debtorReport()
                             {
                                 id = Convert.ToInt32(dr["id"]),
                                 date = dr["date"].ToString(),
                                 mijoz_fish = dr["mijoz_fish"].ToString(),
                                 kurs = Convert.ToDouble(dr["kurs"]),
                                 qarz_som = Convert.ToDouble(dr["qarz_som"]),
                                 qarz_dollar = Convert.ToDouble(dr["qarz_dollar"])

                             }).ToList();
            return debtorReports;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = (MainWindow)Application.Current.MainWindow;

            main.TabMenu.SelectedIndex = 9;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            string queryDebtorReport = "select shop.id, shop.date, debtor.mijoz_fish, shop.kurs, debtor.qarz_som, debtor.qarz_dollar from shop " +
                "inner join debtor on debtor.id = shop.debtor_id " +
                "where debtor.id='"+debtor_id+"'";
            tbDebtorReport = new DataTable();
            objDBAccess.readDatathroughAdapter(queryDebtorReport, tbDebtorReport);
            dataGridDebtorReport.ItemsSource = GetDebtorReports();
            
        }

        private void DatePickerGacha_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatePickerDan.SelectedDate.ToString() == "")
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
                    string queryDebtorReport = "select shop.id, shop.date, debtor.mijoz_fish, shop.kurs, debtor.qarz_som, debtor.qarz_dollar from shop " +
                    "inner join debtor on debtor.id = shop.debtor_id " +
                    "where debtor.id='" + debtor_id + "' and shop.date>='" + dt_dan + "' and shop.date<='" + dt_gacha + "'";
                    tbDebtorReport.Clear();
                    objDBAccess.readDatathroughAdapter(queryDebtorReport, tbDebtorReport);
                    dataGridDebtorReport.ItemsSource = GetDebtorReports();
                }
                return;
            }
            else
            {
                return;
            }
        }

        private void DatePickerDan_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatePickerGacha.SelectedDate.ToString() == "")
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
                    string queryDebtorReport = "select shop.id, shop.date, debtor.mijoz_fish, shop.kurs, debtor.qarz_som, debtor.qarz_dollar from shop " +
                    "inner join debtor on debtor.id = shop.debtor_id " +
                    "where debtor.id='" + debtor_id + "' and shop.date>='" + dt_dan + "' and shop.date<='" + dt_gacha + "'";
                    tbDebtorReport.Clear();
                    objDBAccess.readDatathroughAdapter(queryDebtorReport, tbDebtorReport);
                    dataGridDebtorReport.ItemsSource = GetDebtorReports();
                }
                return;
            }
            else
            {
                return;
            }
        }
    }
}
