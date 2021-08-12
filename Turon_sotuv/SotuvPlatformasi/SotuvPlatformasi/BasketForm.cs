using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using MySql.Data.MySqlClient;
using DGVPrinterHelper;
using System.Globalization;

namespace SotuvPlatformasi
{
    public partial class BasketForm : Form
    {
        public BasketForm()
        {
            InitializeComponent();
        }

        public string shop_id = "";

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
        DBAccess objDBAccess = new DBAccess();
        public static DataTable tbBasket = new DataTable();
        private void BasketForm_Load(object sender, EventArgs e)
        {
            string queryCart = "select cart.product_id as KOD, cart.name as MAXSULOT, cart.price as NARXI, val.name as VAL_ULCHOV, cart.quantity as MIQDOR, cart.total as SUMMA from cart " +
                "inner join shop on cart.shop_id=shop.id " +
                "inner join val on cart.val_id = val.id " +
                "where shop.id='" + shop_id + "'";
            tbBasket = new DataTable();
            objDBAccess.readDatathroughAdapter(queryCart, tbBasket);
            dbGridBasket.DataSource = tbBasket;
            double summa_uz = 0; string str_summa_uz = "0";
            double summa_dol = 0; string str_summa_dol = "0";
            string val_ul = "";
            foreach(DataRow item in tbBasket.Rows)
            {
                val_ul = item["VAL_ULCHOV"].ToString();
                if (val_ul == "uz")
                {
                    str_summa_uz = item["SUMMA"].ToString();
                    str_summa_uz = DoubleToStr(str_summa_uz);
                    summa_uz += double.Parse(str_summa_uz, CultureInfo.InvariantCulture);
                }
                if (val_ul == "$")
                {
                    str_summa_dol = item["SUMMA"].ToString();
                    str_summa_dol = DoubleToStr(str_summa_dol);
                    summa_dol += double.Parse(str_summa_dol, CultureInfo.InvariantCulture);
                }
            }
            DGVPrinter printer = new DGVPrinter();
            printer.Title = "накладная" + '\n' + "Tel: +998 - 78 - 11 - 31 - 888" + '\n' + "E-mail: frap2020@mail.ru site: www.frap.uz" + "\n\n";
            printer.SubTitle = string.Format("Sana: {0}\n\nJami summa uz: {1}       Jami summa $: {2}\n\n", DateTime.Now.Date.ToString("dd.MM.yyyy"), summa_uz.ToString(), summa_dol.ToString());
            printer.SubTitleFormatFlags = System.Drawing.StringFormatFlags.LineLimit | System.Drawing.StringFormatFlags.NoClip;
            printer.PageNumbers = false;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = System.Drawing.StringAlignment.Near;
            printer.Footer = "";//"Jami summa : " + summa.ToString() + "\n\nВнимание: Заказ в резерве хранится не более 4 дней при точном ответе";
            printer.FooterSpacing = 0;
            printer.PrintDataGridView(dbGridBasket);
            this.Close();
        }
    }
}
