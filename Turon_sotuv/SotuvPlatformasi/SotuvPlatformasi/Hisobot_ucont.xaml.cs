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
using System.Globalization;

namespace SotuvPlatformasi
{
    /// <summary>
    /// Interaction logic for Hisobot_ucont.xaml
    /// </summary>
    public partial class Hisobot_ucont : UserControl
    {
        public Hisobot_ucont()
        {
            InitializeComponent();
        }

        DBAccess objDBAccess = new DBAccess();
        public static DataTable tbHisobot, tbHalfHisobot, tbReturnProduct;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            string queryHisobot = "select sum(shop.naqd) as naqd, sum(shop.plastik) as plastik, sum(shop.nasiya_som) as nasiya_som , sum(shop.nasiya_dollar) as nasiya_dollar , sum(shop.transfer) as transfer, sum(shop.currency) as currency, sum(shop.difference_som) as skidka_som, sum(shop.difference_dollar) as skidka_dollar, " +
                "sum(case when returnproduct.val_id = 1 then returnproduct.summa else 0 end) as qaytuv_som, " +
                "sum(case when returnproduct.val_id = 2 then returnproduct.summa else 0 end) as qaytuv_dollar, userprofile.first_name as employee, sum(shop.total_som) as total_som, sum(shop.total_dollar) as total_dollar  from shop " +
                "inner join userprofile on shop.sellar_id = userprofile.id " +
                "left join returnproduct on shop.id = returnproduct.shop_id " +
                "where shop.status_tulov=1 group by(userprofile.id)";

            tbHisobot = new DataTable();
            objDBAccess.readDatathroughAdapter(queryHisobot, tbHisobot);
            dataGridHisobot.ItemsSource = GetHisobot();
            tbHisobot.Dispose();
            TotalAll_uz = 0;
            TotalAll_dol = 0;
            SetTotalAll();
            DatePickerDan.SelectedDate = null;
            DatePickerGacha.SelectedDate = null;
        }
        private void BtnAsosiy_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = (MainWindow)Application.Current.MainWindow;
            main.TabMenu.SelectedIndex = 1;
        }

        public class Hisobot
        {
            public string naqd { get; set; }
            public string plastik { get; set; }
            public string nasiya_som { get; set; }
            public string nasiya_dollar { get; set; }
            public string transfer { get; set; }
            public string currency { get; set; }
            public string skidka_som { get; set; }
            public string skidka_dollar { get; set; }
            public string qaytuv_som { get; set; }
            public string qaytuv_dollar { get; set; }
            public string employee { get; set; }
            public string total_som { get; set; }
            public string total_dollar { get; set; }
        }

        public List<Hisobot> hisobots = new List<Hisobot>();
        public List<Hisobot> GetHisobot()
        {
            hisobots = (from DataRow dr in tbHisobot.Rows
                           select new Hisobot()
                           {
                               naqd = dr["naqd"].ToString(),
                               plastik = dr["plastik"].ToString(),
                               nasiya_som = dr["nasiya_som"].ToString(),
                               nasiya_dollar = dr["nasiya_dollar"].ToString(),
                               transfer = dr["transfer"].ToString(),
                               currency = dr["currency"].ToString(),
                               skidka_som = dr["skidka_som"].ToString(),
                               skidka_dollar = dr["skidka_dollar"].ToString(),
                               qaytuv_som = dr["qaytuv_som"].ToString(),
                               qaytuv_dollar = dr["qaytuv_dollar"].ToString(),
                               employee = dr["employee"].ToString(),
                               total_som = dr["total_som"].ToString(),
                               total_dollar = dr["total_dollar"].ToString()
                           }).ToList();

            return hisobots;
        }
        public string DoubleToStr(string s)
        {
            if (s.IndexOf(',') > -1)
            {
                int index = s.IndexOf(',');
                string first = s.Substring(0, index);
                string last = s.Substring(index + 1);
                s = first + "." + last;
            }
            return s;
        }
        public double TotalAll_uz = 0;
        public double TotalAll_dol = 0;
        public void SetTotalAll()
        {
            string each_total_uz = ""; double Deach_total_uz;
            foreach(DataRow dtRow in tbHisobot.Rows)
            {
                each_total_uz = dtRow["total_som"].ToString();
                each_total_uz = DoubleToStr(each_total_uz);
                Deach_total_uz = double.Parse(each_total_uz, CultureInfo.InvariantCulture);
                TotalAll_uz += Deach_total_uz;
            }
            txtJamiSavdo_uz.Text = TotalAll_uz.ToString();

            string each_total_dol = ""; double Deach_total_dol;
            foreach (DataRow dtRow in tbHisobot.Rows)
            {
                each_total_dol = dtRow["total_dollar"].ToString();
                each_total_dol = DoubleToStr(each_total_dol);
                Deach_total_dol = double.Parse(each_total_dol, CultureInfo.InvariantCulture);
                TotalAll_dol += Deach_total_dol;
            }

            txtJamiSavdo_dol.Text = TotalAll_dol.ToString();
        }

        public void DatePickerGacha_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(DatePickerDan.SelectedDate.ToString() == "")
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
                    string queryHalfHisobot = "select sum(shop.naqd) as naqd, sum(shop.plastik) as plastik, sum(shop.nasiya_som) as nasiya_som , sum(shop.nasiya_dollar) as nasiya_dollar , sum(shop.transfer) as transfer, sum(shop.currency) as currency, sum(shop.difference_som) as skidka_som, sum(shop.difference_dollar) as skidka_dollar, " +
                     "userprofile.first_name as employee, sum(shop.total_som) as total_som, sum(shop.total_dollar) as total_dollar  from shop " +
                     "inner join userprofile on shop.sellar_id = userprofile.id " +
                     "where shop.status_tulov=1 " +
                     "and date(shop.date)>='" + dt_dan + "' and date(shop.date)<='" + dt_gacha + "' " +
                     "group by(userprofile.id)";
                    tbHalfHisobot = new DataTable();
                    objDBAccess.readDatathroughAdapter(queryHalfHisobot, tbHalfHisobot);

                    string queryReturnproduct = "select sum(case when returnproduct.val_id = 1 then returnproduct.summa else 0 end) as qaytuv_som, " +
                        "sum(case when returnproduct.val_id = 2 then returnproduct.summa else 0 end) as qaytuv_dollar from returnproduct " +
                        "inner join shop on returnproduct.shop_id = shop.id " +
                        "inner join userprofile on shop.sellar_id = userprofile.id " +
                        "where date(returnproduct.date)>='" + dt_dan + "' and date(returnproduct.date)<='" + dt_gacha + "' " +
                        "group by(userprofile.id)";
                    tbReturnProduct = new DataTable();
                    objDBAccess.readDatathroughAdapter(queryReturnproduct, tbReturnProduct);
                    tbHisobot.Clear();
                    int i = 0;
                    DataRow row;
                    foreach (DataRow dt in tbHalfHisobot.Rows)
                    {
                        row = tbHisobot.NewRow();
                        row["naqd"] = dt["naqd"];
                        row["plastik"] = dt["plastik"];
                        row["nasiya_som"] = dt["nasiya_som"];
                        row["nasiya_dollar"] = dt["nasiya_dollar"];
                        row["transfer"] = dt["transfer"];
                        row["currency"] = dt["currency"];
                        row["skidka_som"] = dt["skidka_som"];
                        row["skidka_dollar"] = dt["skidka_dollar"];
                        row["employee"] = dt["employee"];
                        row["total_som"] = dt["total_som"];
                        row["total_dollar"] = dt["total_dollar"];
                        row["qaytuv_som"] = "0";
                        row["qaytuv_dollar"] = "0";
                        tbHisobot.Rows.Add(row);
                    }

                    foreach (DataRow dt in tbReturnProduct.Rows)
                    {
                        tbHisobot.Rows[i]["qaytuv_som"] = dt["qaytuv_som"];
                        tbHisobot.Rows[i]["qaytuv_dollar"] = dt["qaytuv_dollar"];
                        i++;
                    }
                    i = 0;

                    dataGridHisobot.ItemsSource = GetHisobot();
                    TotalAll_uz = 0;
                    TotalAll_dol = 0;
                    SetTotalAll();
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
                    string queryHalfHisobot = "select sum(shop.naqd) as naqd, sum(shop.plastik) as plastik, sum(shop.nasiya_som) as nasiya_som , sum(shop.nasiya_dollar) as nasiya_dollar , sum(shop.transfer) as transfer, sum(shop.currency) as currency, sum(shop.difference_som) as skidka_som, sum(shop.difference_dollar) as skidka_dollar, " +
                     "userprofile.first_name as employee, sum(shop.total_som) as total_som, sum(shop.total_dollar) as total_dollar  from shop " +
                     "inner join userprofile on shop.sellar_id = userprofile.id " +
                     "where shop.status_tulov=1 " +
                     "and date(shop.date)>='"+dt_dan+"' and date(shop.date)<='"+dt_gacha+"' " +
                     "group by(userprofile.id)";
                    tbHalfHisobot = new DataTable();
                    objDBAccess.readDatathroughAdapter(queryHalfHisobot, tbHalfHisobot);

                    string queryReturnproduct = "select sum(case when returnproduct.val_id = 1 then returnproduct.summa else 0 end) as qaytuv_som, " +
                        "sum(case when returnproduct.val_id = 2 then returnproduct.summa else 0 end) as qaytuv_dollar from returnproduct " +
                        "inner join shop on returnproduct.shop_id = shop.id " +
                        "inner join userprofile on shop.sellar_id = userprofile.id " +
                        "where date(returnproduct.date)>='" + dt_dan + "' and date(returnproduct.date)<='" + dt_gacha + "' " +
                        "group by(userprofile.id)";
                    tbReturnProduct = new DataTable();
                    objDBAccess.readDatathroughAdapter(queryReturnproduct, tbReturnProduct);
                    tbHisobot.Clear();
                    int i = 0;
                    DataRow row;
                    foreach(DataRow dt in tbHalfHisobot.Rows)
                    {
                        row = tbHisobot.NewRow();
                        row["naqd"] = dt["naqd"];
                        row["plastik"] = dt["plastik"];
                        row["nasiya_som"] = dt["nasiya_som"];
                        row["nasiya_dollar"] = dt["nasiya_dollar"];
                        row["transfer"] = dt["transfer"];
                        row["currency"] = dt["currency"];
                        row["skidka_som"] = dt["skidka_som"];
                        row["skidka_dollar"] = dt["skidka_dollar"];
                        row["employee"] = dt["employee"];
                        row["total_som"] = dt["total_som"];
                        row["total_dollar"] = dt["total_dollar"];
                        row["qaytuv_som"] = "0";
                        row["qaytuv_dollar"] = "0";
                        tbHisobot.Rows.Add(row);
                    }
                    
                    foreach(DataRow dt in tbReturnProduct.Rows)
                    {
                        tbHisobot.Rows[i]["qaytuv_som"] = dt["qaytuv_som"];
                        tbHisobot.Rows[i]["qaytuv_dollar"] = dt["qaytuv_dollar"];
                        i++;
                    }
                    i = 0;

                    dataGridHisobot.ItemsSource = GetHisobot();
                    TotalAll_uz = 0;
                    TotalAll_dol = 0;
                    SetTotalAll();
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
