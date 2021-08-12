using System;
using System.Collections.Generic;
using System.Drawing.Printing;
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
namespace SotuvPlatformasi
{
    /// <summary>
    /// Interaction logic for Tul_qab_ucont.xaml
    /// </summary>
    public partial class Tul_qab_ucont : UserControl
    {
        PrintDocument printDocument1 = new PrintDocument();
        public Tul_qab_ucont()
        {
            InitializeComponent();
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
        }
        public static int shopId = 0;
        public static string print="";
        public static string p_Date = "", pOmbor = "", pKlient = "", pTel = "", pQarz_som = "", pQarz_dollar = "";
        public static string pResult_Qarz_Som = "", pResult_Qarz_Dollar = "", pJami_Som = "", pJami_Dollar = "";
        public static string pSkidka_som = "", pSkidka_dollar = "", pTulov_Som = "", pTulov_Dollar = "", pSkidka_foiz = "";
        public static DataTable tbSaleDetail;
        DBAccess objDBAccess = new DBAccess();

        private void BtnAsosiy_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = (MainWindow)Application.Current.MainWindow;
            main.TabMenu.SelectedIndex = 2;
        }
        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(print, new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, new System.Drawing.Point(10, 10));
        }

        public List<SaleDetail> saleDetail = new List<SaleDetail>();
        public List<SaleDetail> GetDebtor()
        {
            saleDetail = (from DataRow dr in tbSaleDetail.Rows
                       select new SaleDetail()
                       {
                           product_id = Convert.ToInt32(dr["product_id"]),
                           name = dr["name"].ToString(),
                           measurement = dr["measurement"].ToString(),
                           quantity = Convert.ToDouble(dr["quantity"]),
                           val_ul = dr["val_ul"].ToString(),
                           price = Convert.ToDouble(dr["price"]),
                           total = Convert.ToDouble(dr["total"])
                       }).ToList();
            return saleDetail;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           // MessageBox.Show(print);
            printDocument1.Print();

            #region Printing check via crytall report
            //string querySaleDetail = "select cart.product_id, cart.name, product.measurement, cart.quantity, val.name as val_ul, cart.price, cart.total from cart " +
            //    "inner join product on cart.product_id = product.product_id " +
            //    "inner join val on cart.val_id=val.id " +
            //    "where cart.shop_id='" + shopId + "'";
            //tbSaleDetail = new DataTable();
            //objDBAccess.readDatathroughAdapter(querySaleDetail, tbSaleDetail);

            //string queryOmbor = "select name from filial";
            //DataTable tbFilial = new DataTable();
            //objDBAccess.readDatathroughAdapter(queryOmbor, tbFilial);
            //pOmbor = tbFilial.Rows[0]["name"].ToString();
            //GetDebtor();
            //using(frmPrint frm = new frmPrint(saleDetail))
            //{
            //    frm.ShowDialog();
            //}

            #endregion

            #region Printing check from windows form 
            //BasketForm basketForm = new BasketForm();
            //basketForm.shop_id = shopId.ToString();
            //basketForm.ShowDialog();
            #endregion
        }
    }
}
