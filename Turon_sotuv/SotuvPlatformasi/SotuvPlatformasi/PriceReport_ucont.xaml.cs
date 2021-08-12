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
using System.Globalization;

namespace SotuvPlatformasi
{
    /// <summary>
    /// Interaction logic for PriceReport_ucont.xaml
    /// </summary>
    public partial class PriceReport_ucont : UserControl
    {
        public PriceReport_ucont()
        {
            InitializeComponent();
        }
        
        DBAccess objDBAccess = new DBAccess();
        public static DataTable tbChangedprice, tbPricecart;

        public void SetDifference()
        {
            txtFarq_uz.Text = tbChangedprice.Rows[0]["difference_som"].ToString();
            txtFarq_dol.Text = tbChangedprice.Rows[0]["difference_dollar"].ToString();
        }
        public class PriceCart
        {
            public int id { get; set; }
            public int ch_id { get; set; }
            public string pr_name { get; set; }
            public double old_price { get; set; }
            public double new_price { get; set; }
            public double residue { get; set; }
            public double difference { get; set; }
            public double total_diff { get; set; }
            public string val_ul { get; set; }
        }
        List<PriceCart> priceCartList = new List<PriceCart>();
        public List<PriceCart> GetPriceCartList()
        {
            priceCartList = (from DataRow dr in tbPricecart.Rows
                             select new PriceCart()
                             {
                                 id = Convert.ToInt32(dr["id"]),
                                 ch_id = Convert.ToInt32(dr["ch_id"]),
                                 pr_name = dr["pr_name"].ToString(),
                                 old_price = Convert.ToDouble(dr["old_price"]),
                                 new_price = Convert.ToDouble(dr["new_price"]),
                                 residue = Convert.ToDouble(dr["residue"]),
                                 difference = Convert.ToDouble(dr["difference"]),
                                 total_diff = Convert.ToDouble(dr["total_diff"]),
                                 val_ul = dr["val_ul"].ToString()
                             }).ToList();
            return priceCartList;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = (MainWindow)Application.Current.MainWindow;
            main.TabMenu.SelectedIndex = 15;
        }

        private void DatePickerDan_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatePickerGacha.Text== "")
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
                    try
                    {
                        string querySearch = "select id, date, difference_som, difference_dollar  from changedprice where date between '" + dt_dan + "' and '" + dt_gacha + "'";
                        try
                        {
                            tbChangedprice.Clear();
                            tbPricecart.Clear();
                        }
                        catch (Exception ex) { tbChangedprice = new DataTable(); tbPricecart = new DataTable(); }
                        objDBAccess.readDatathroughAdapter(querySearch, tbChangedprice);
                        if (tbChangedprice.Rows.Count > 0)
                        {
                            try { SetDifference(); } catch (Exception) { }
                            string queryPriceCart = "select pricecart.id, pricecart.ch_id, pricecart.pr_name, pricecart.old_price, pricecart.new_price, pricecart.residue, pricecart.difference, pricecart.total_diff, val.name as val_ul from pricecart " +
                            "inner join changedprice on pricecart.ch_id = changedprice.id " +
                            "inner join val on pricecart.val_id = val.id " +
                            "where changedprice.id='" + tbChangedprice.Rows[0]["id"] + "'";
                            objDBAccess.readDatathroughAdapter(queryPriceCart, tbPricecart);
                            dataGridBasket.ItemsSource = GetPriceCartList();
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void DatePickerGacha_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatePickerDan.Text== "")
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
                    try
                    {
                        string querySearch = "select id, date, difference_som, difference_dollar  from changedprice where date between '" + dt_dan + "' and '" + dt_gacha + "'";
                        try
                        {
                            tbChangedprice.Clear();
                            tbPricecart.Clear();
                        }
                        catch (Exception ex) { tbChangedprice = new DataTable(); tbPricecart = new DataTable(); }
                        objDBAccess.readDatathroughAdapter(querySearch, tbChangedprice);
                        if (tbChangedprice.Rows.Count > 0)
                        {
                            try { SetDifference(); } catch (Exception) { }
                            string queryPriceCart = "select pricecart.id, pricecart.ch_id, pricecart.pr_name, pricecart.old_price, pricecart.new_price, pricecart.residue, pricecart.difference, pricecart.total_diff, val.name as val_ul from pricecart " +
                            "inner join changedprice on pricecart.ch_id = changedprice.id " +
                            "inner join val on pricecart.val_id = val.id " +
                            "where changedprice.id='" + tbChangedprice.Rows[0]["id"] + "'";
                            objDBAccess.readDatathroughAdapter(queryPriceCart, tbPricecart);
                            dataGridBasket.ItemsSource = GetPriceCartList();
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string queryChangedPrice = "select id, date, difference_som, difference_dollar  from changedprice where date like '" + date + "%'";
            tbChangedprice = new DataTable();
            objDBAccess.readDatathroughAdapter(queryChangedPrice, tbChangedprice);
            
            if (tbChangedprice.Rows.Count > 0)
            {
                SetDifference();
                string s_id = tbChangedprice.Rows[0]["id"].ToString();
                string queryPriceCart = "select pricecart.id, pricecart.ch_id, pricecart.pr_name, pricecart.old_price, pricecart.new_price, pricecart.residue, pricecart.difference, pricecart.total_diff, val.name as val_ul from pricecart " +
                "inner join changedprice on pricecart.ch_id = changedprice.id " +
                "inner join val on pricecart.val_id = val.id " +
                "where changedprice.id='"+s_id+"'"; 
                tbPricecart = new DataTable();
                objDBAccess.readDatathroughAdapter(queryPriceCart, tbPricecart);
                dataGridBasket.ItemsSource = GetPriceCartList();
            }
            else
            {
                string queryPriceCart = "select pricecart.id, pricecart.ch_id, pricecart.pr_name, pricecart.old_price, pricecart.new_price, pricecart.residue, pricecart.difference, pricecart.total_diff, val.name as val_ul from pricecart " +
                "inner join changedprice on pricecart.ch_id = changedprice.id " +
                "inner join val on pricecart.val_id = val.id " +
                "where changedprice.id=0"; 
                tbPricecart = new DataTable();
                objDBAccess.readDatathroughAdapter(queryPriceCart, tbPricecart);
                dataGridBasket.ItemsSource = GetPriceCartList();
            }
            tbChangedprice.Dispose();
            tbPricecart.Dispose();
            DatePickerDan.SelectedDate = DateTime.Now.Date;
            DatePickerGacha.SelectedDate = DateTime.Now.Date;

        }
    }
}
