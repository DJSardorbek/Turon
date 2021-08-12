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
namespace SotuvPlatformasi
{
    /// <summary>
    /// Interaction logic for Qarzdorlar_ucont.xaml
    /// </summary>
    public partial class Qarzdorlar_ucont : UserControl
    {
        public Qarzdorlar_ucont()
        {
            InitializeComponent();
            
        }

        public List<Qarzdorlar> qarzdorlar;
        DBAccess objDBAccess = new DBAccess();
        public static DataTable tbDebtor;
        public int itemsSource = 0;
        public class Qarzdorlar
        {
            public int id { get; set; }
            public string mijoz_fish { get; set; }
            public string tel_1 { get; set; }
            public string tel_2 { get; set; }
            public double qarz_som { get; set; }
            public double qarz_dollar { get; set; }
            public string return_date { get; set; }

        }

        public List<Qarzdorlar> GetQarzdorList()
        {
            qarzdorlar = (from DataRow dt in tbDebtor.Rows
                          select new Qarzdorlar()
                          {
                              id = Convert.ToInt32(dt["id"].ToString()),
                              mijoz_fish = dt["mijoz_fish"].ToString(),
                              tel_1 = dt["tel_1"].ToString(),
                              tel_2 = dt["tel_2"].ToString(),
                              qarz_som = Convert.ToDouble(dt["qarz_som"]),
                              qarz_dollar = Convert.ToDouble(dt["qarz_dollar"]),
                              return_date = dt["return_date"].ToString()
                          }).ToList();

            return qarzdorlar;
        }

        private void BtnAsosiy_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = (MainWindow)Application.Current.MainWindow;
            main.TabMenu.SelectedIndex = 1;
        }

        //To'lovi o'tib ketganlar ro'yhati strelka <

        public void TulovUtibKetgan()
        {
            DateTime date_now = DateTime.Now;
            string dt_now = date_now.ToString("yyyy-MM-dd");
            string queryDebtor = "select distinct debtor.id, debtor.mijoz_fish, debtor.tel_1, debtor.tel_2, debtor.qarz_som,debtor.qarz_dollar, debt.return_date " +
                "from debtor " +
                "inner join debt " +
                "where return_date = (select distinct max(return_date) " +
                "from debt " +
                "where debtor.id = debt.debtor_id) " +
                "and return_date<'" + dt_now + "' " +
                "order by debt.return_date";

            tbDebtor = new DataTable();
            objDBAccess.readDatathroughAdapter(queryDebtor, tbDebtor);
            tbDebtor.Dispose();
        }

        // To'lovi yaqinlashi kelayotgan
        public void TulovYaqinlashgan()
        {
            DateTime date_now = DateTime.Now;
            string dt_now = date_now.ToString("yyyy-MM-dd");
            string queryDebtor = "select distinct debtor.id, debtor.mijoz_fish, debtor.tel_1, debtor.tel_2, debtor.qarz_som,debtor.qarz_dollar, debt.return_date " +
                "from debtor " +
                "inner join debt " +
                "where return_date = (select distinct max(return_date) " +
                "from debt " +
                "where debtor.id = debt.debtor_id) " +
                "and return_date>'" + dt_now + "' " +
                "order by debt.return_date";

            tbDebtor = new DataTable();
            objDBAccess.readDatathroughAdapter(queryDebtor, tbDebtor);
            tbDebtor.Dispose();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            TulovUtibKetgan();
            dataGridQarzdorlar.ItemsSource = GetQarzdorList();
            itemsSource = 0;
        }

        private void btnTulovUtibKetgan_Click(object sender, RoutedEventArgs e)
        {
            TulovUtibKetgan();
            dataGridQarzdorlar.ItemsSource = GetQarzdorList();
            itemsSource = 0;
        }

        private void btnTulovYaqinlashgan_Click(object sender, RoutedEventArgs e)
        {
            TulovYaqinlashgan();
            dataGridQarzdorlar.ItemsSource = GetQarzdorList();
            itemsSource = 1;
        }
        public static string debtor_id = "", qarz_som = "", qarz_dollar="", debtor = "";
        public static bool payment = false;
        public static int CurrentItem = 0;

        private void btnTarix_Click(object sender, RoutedEventArgs e)
        {
            Qarzdorlar selectedQarzdor = dataGridQarzdorlar.SelectedItems[0] as Qarzdorlar;
            debtor_id = selectedQarzdor.id.ToString();
            DebtorPayHistory_ucont.debtor_id = debtor_id;
            MainWindow main = (MainWindow)Application.Current.MainWindow;
            main.TabMenu.SelectedIndex = 19;
        }

        private void dataGridQarzdorlar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Qarzdorlar selectedQarzdorlar = dataGridQarzdorlar.SelectedItems[0] as Qarzdorlar;
            DebtorReport.debtor_id = selectedQarzdorlar.id.ToString();
            MainWindow main = (MainWindow)Application.Current.MainWindow;
            main.TabMenu.SelectedIndex = 14;
        }

        private void btnTulov_Click(object sender, RoutedEventArgs e)
        {
            Qarzdorlar selectedQarzdor = dataGridQarzdorlar.SelectedItems[0] as Qarzdorlar;
            debtor_id = selectedQarzdor.id.ToString();
            qarz_som = selectedQarzdor.qarz_som.ToString(); 
            qarz_dollar = selectedQarzdor.qarz_dollar.ToString(); 
            debtor = selectedQarzdor.mijoz_fish.ToString();
            CurrentItem = selectedQarzdor.id;
            Debt_tulov debtTulov = new Debt_tulov();
            debtTulov.debtor_id = debtor_id;
            debtTulov.qarz_som = qarz_som;
            debtTulov.qarz_dollar = qarz_dollar;
            debtTulov.debtor = debtor;
            payment = false;
            debtTulov.ShowDialog();
            if(payment)
            {
                Refresh();
            }
        }

        public void Refresh()
        {
            // To'lovi o'tib ketganlar uchun
            if(itemsSource == 0)
            {
                TulovUtibKetgan();
                dataGridQarzdorlar.ItemsSource = GetQarzdorList();
            }
            //To'lovi yaqinlashib qolganlar uchun
            else
            {
                TulovYaqinlashgan();
                dataGridQarzdorlar.ItemsSource = GetQarzdorList();
            }
        }
    }
}
