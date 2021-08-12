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
    /// Interaction logic for ReturnDeliverReport_ucont.xaml
    /// </summary>
    public partial class ReturnDeliverReport_ucont : UserControl
    {
        public ReturnDeliverReport_ucont()
        {
            InitializeComponent();
        }

        public static DataTable tbRetDelProduct;
        public static string total_som = "0", total_dollar = "0";
        DBAccess objDBAccess = new DBAccess();
        public class ReturnDeliverProduct
        {
            public int id { get; set; }
            public int retsum_id { get; set; }
            public string deliver { get; set; }
            public string pr_name { get; set; }
            public string preparer { get; set; }
            public string measurement { get; set; }
            public double tan_narx { get; set; }
            public double sotish_narx { get; set; }
            public string val_ul { get; set; }
            public string barcode { get; set; }
            public double qayt_miqdor { get; set; }
        }
        List<ReturnDeliverProduct> retdelProductList = new List<ReturnDeliverProduct>();

        public List<ReturnDeliverProduct> GetRetDelProductList()
        {
            retdelProductList = (from DataRow dr in tbRetDelProduct.Rows
                                 select new ReturnDeliverProduct()
                                 {
                                     id = Convert.ToInt32(dr["id"]),
                                     retsum_id = Convert.ToInt32(dr["retsum_id"]),
                                     deliver = dr["deliver"].ToString(),
                                     pr_name = dr["pr_name"].ToString(),
                                     preparer = dr["preparer"].ToString(),
                                     measurement = dr["measurement"].ToString(),
                                     tan_narx = Convert.ToDouble(dr["tan_narx"]),
                                     sotish_narx = Convert.ToDouble(dr["sotish_narx"]),
                                     val_ul = dr["val_ul"].ToString(),
                                     barcode = dr["barcode"].ToString(),
                                     qayt_miqdor = Convert.ToDouble(dr["qayt_miqdor"])
                                 }).ToList();

            return retdelProductList;
        }

        private void BtnAsosiy_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = (MainWindow)Application.Current.MainWindow;
            main.TabMenu.SelectedIndex = 1;
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

        public void SetSumma()
        {
            DateTime dateDan = DatePickerDan.SelectedDate.Value.Date;
            DateTime dateGacha = DatePickerGacha.SelectedDate.Value.Date;
            string dt_dan = dateDan.Date.ToString("yyyy-MM-dd");
            string dt_gacha = dateGacha.Date.ToString("yyyy-MM-dd");

            string querySumSom = "select sum(tan_narx*qayt_miqdor) from retdelproduct " +
                "inner join retdelsumma on retdelproduct.retsum_id=retdelsumma.id " +
                "where retdelsumma.date between '" + dt_dan + "' and '"+dt_gacha+"' and retdelproduct.val_id=1 and retdelsumma.status=1";
            DataTable tbSumSom = new DataTable();
            objDBAccess.readDatathroughAdapter(querySumSom, tbSumSom);
            if (tbSumSom.Rows.Count > 0)
            {
                total_som = tbSumSom.Rows[0]["sum(tan_narx*qayt_miqdor)"].ToString();
                total_som = DoubleToStr(total_som);
                if (total_som == "" || total_som == null)
                {
                    total_som = "0";
                }
            }
            else
            {
                total_som = "0";
            }
            tbSumSom.Clear();
            tbSumSom.Dispose();

            string querySumDollar = "select sum(tan_narx*qayt_miqdor) from retdelproduct " +
                "inner join retdelsumma on retdelproduct.retsum_id=retdelsumma.id " +
                "where retdelsumma.date between '" + dt_dan + "' and '"+dt_gacha+"' and retdelproduct.val_id=2 and retdelsumma.status=1";
            DataTable tbSumDollar = new DataTable();
            objDBAccess.readDatathroughAdapter(querySumDollar, tbSumDollar);
            if (tbSumDollar.Rows.Count > 0)
            {
                total_dollar = tbSumDollar.Rows[0]["sum(tan_narx*qayt_miqdor)"].ToString();
                total_dollar = DoubleToStr(total_dollar);
                if (total_dollar == "" || total_dollar == null)
                {
                    total_dollar = "0";
                }
            }
            else if (tbSumDollar.Rows.Count == 0)
            {
                total_dollar = "0";
            }
            tbSumDollar.Clear();
            tbSumDollar.Dispose();
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

                    string queryRetSumProduct = "select retdelproduct.id, retdelproduct.retsum_id, retdelsumma.deliver,retdelsumma.kurs, retdelproduct.pr_name, retdelproduct.preparer, retdelproduct.measurement, retdelproduct.tan_narx, retdelproduct.sotish_narx, val.name as val_ul, retdelproduct.barcode, retdelproduct.qayt_miqdor from retdelproduct " +
                        "inner join val on val.id = retdelproduct.val_id " +
                        "inner join retdelsumma on retdelproduct.retsum_id = retdelsumma.id " +
                        "where retdelsumma.date between '" + dt_dan + "' and '"+dt_gacha+"' and retdelsumma.status=1";
                    tbRetDelProduct = new DataTable();
                    objDBAccess.readDatathroughAdapter(queryRetSumProduct, tbRetDelProduct);
                    dataGridReport.ItemsSource = GetRetDelProductList();
                    if (tbRetDelProduct.Rows.Count > 0)
                    {
                        txtKurs.Text = tbRetDelProduct.Rows[0]["kurs"].ToString();
                    }
                    tbRetDelProduct.Dispose();

                    SetSumma();
                    txtSumma_dol.Text = total_dollar;
                    txtSumma_uz.Text = total_som;
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

                    string queryRetSumProduct = "select retdelproduct.id, retdelproduct.retsum_id, retdelsumma.deliver,retdelsumma.kurs, retdelproduct.pr_name, retdelproduct.preparer, retdelproduct.measurement, retdelproduct.tan_narx, retdelproduct.sotish_narx, val.name as val_ul, retdelproduct.barcode, retdelproduct.qayt_miqdor from retdelproduct " +
                        "inner join val on val.id = retdelproduct.val_id " +
                        "inner join retdelsumma on retdelproduct.retsum_id = retdelsumma.id " +
                        "where retdelsumma.date between '" + dt_dan + "' and '" + dt_gacha + "' and retdelsumma.status=1";
                    tbRetDelProduct = new DataTable();
                    objDBAccess.readDatathroughAdapter(queryRetSumProduct, tbRetDelProduct);
                    dataGridReport.ItemsSource = GetRetDelProductList();
                    if (tbRetDelProduct.Rows.Count > 0)
                    {
                        txtKurs.Text = tbRetDelProduct.Rows[0]["kurs"].ToString();
                    }
                    tbRetDelProduct.Dispose();

                    SetSumma();
                    txtSumma_dol.Text = total_dollar;
                    txtSumma_uz.Text = total_som;
                }
            }
        }

        private void dateTimePicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
                        
        }
    }
}
