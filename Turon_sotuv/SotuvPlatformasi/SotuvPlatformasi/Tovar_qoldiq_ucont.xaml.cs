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
using System.Windows.Media.Effects;

namespace SotuvPlatformasi
{
    /// <summary>
    /// Interaction logic for Tovar_qoldiq_ucont.xaml
    /// </summary>
    public partial class Tovar_qoldiq_ucont : UserControl
    {
        public Tovar_qoldiq_ucont()
        {
            InitializeComponent();
            comboFilial.Items.Add("Tugagan");
            comboFilial.Items.Add("Kam qolgan");
            comboFilial.Items.Add("Yetarli");
        }

        DBAccess objDBAccess = new DBAccess();
        public static DataTable tbProductKam, tbProductUrta, tbProductMeyor, tbNavigateKam, tbNavigateUrta, tbNavigateMeyor;
        public static int index =-1;
        private void BtnAsosiy_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = (MainWindow)Application
                .Current.MainWindow;
            main.TabMenu.SelectedIndex = 1;
        }
        public class Product
        {
            public int product_id { get; set; }
            public string name { get; set; }
            public double price { get; set; }
            public string val_ul { get; set; }
            public double quantity { get; set; }
            public string barcode { get; set; }
            public string measurement { get; set; }
            public string preparer { get; set; }
            public double min_count { get; set; }
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

        List<Product> productListKam = new List<Product>();
        List<Product> productListUrta = new List<Product>();
        List<Product> productListMeyor = new List<Product>();

        public List<Product> GetProductKamList()
        {
            productListKam = (from DataRow dr in tbProductKam.Rows
                           select new Product()
                           {
                               product_id = Convert.ToInt32(dr["product_id"]),
                               name = dr["name"].ToString(),
                               price = Convert.ToDouble(dr["price"]),
                               val_ul = dr["val_ul"].ToString(),
                               quantity = Convert.ToDouble(dr["quantity"]),
                               barcode = dr["barcode"].ToString(),
                               measurement = dr["measurement"].ToString(),
                               preparer = dr["preparer"].ToString(),
                               min_count = Convert.ToDouble(dr["min_count"])
                           }).ToList();

            return productListKam;
        }


        public List<Product> GetProductUrtaList()
        {
            productListUrta = (from DataRow dr in tbProductUrta.Rows
                           select new Product()
                           {
                               product_id = Convert.ToInt32(dr["product_id"]),
                               name = dr["name"].ToString(),
                               price = Convert.ToDouble(dr["price"]),
                               val_ul = dr["val_ul"].ToString(),
                               quantity = Convert.ToDouble(dr["quantity"]),
                               barcode = dr["barcode"].ToString(),
                               measurement = dr["measurement"].ToString(),
                               preparer = dr["preparer"].ToString(),
                               min_count = Convert.ToDouble(dr["min_count"])
                           }).ToList();

            return productListUrta;
        }

        public List<Product> GetProductMeyorList()
        {
            productListMeyor = (from DataRow dr in tbProductMeyor.Rows
                           select new Product()
                           {
                               product_id = Convert.ToInt32(dr["product_id"]),
                               name = dr["name"].ToString(),
                               price = Convert.ToDouble(dr["price"]),
                               val_ul = dr["val_ul"].ToString(),
                               quantity = Convert.ToDouble(dr["quantity"]),
                               barcode = dr["barcode"].ToString(),
                               measurement = dr["measurement"].ToString(),
                               preparer = dr["preparer"].ToString(),
                               min_count = Convert.ToDouble(dr["min_count"])
                           }).ToList();

            return productListMeyor;
        }

        BlurEffect myEffect = new BlurEffect();

        void Simulator()
        {
            DataTable tb = new DataTable();
            string query = "";
            if(index == 0)
            {
                query = "select product.product_id, product.name, product.price, val.name as val_ul,product.quantity, product.barcode, product.measurement, product.preparer, product.min_count from product " +
                "inner join val on val.id = product.val_id " +
                "where quantity = 0 order by quantity limit 100";
                objDBAccess.readDatathroughAdapter(query, tb);
                //dataGridProductKam.ItemsSource = GetProductKamList();
            }
            if (index == 1)
            {
                query = "select product.product_id, product.name, product.price, val.name as val_ul,product.quantity, product.barcode, product.measurement, product.preparer, product.min_count from product " +
                "inner join val on val.id = product.val_id " +
                "where quantity <=min_count order by quantity limit 100";
                objDBAccess.readDatathroughAdapter(query, tb);
                //dataGridProductUrta.ItemsSource = GetProductUrtaList();
            }
            if (index == 2)
            {
                query = "select product.product_id, product.name, product.price, val.name as val_ul,product.quantity, product.barcode, product.measurement, product.preparer, product.min_count from product " +
                "inner join val on val.id = product.val_id " +
                "where quantity > min_count order by quantity limit 100";
                objDBAccess.readDatathroughAdapter(query, tb);
                //dataGridProductMeyor.ItemsSource = GetProductMeyorList();
            }
            tb.Dispose();
            //if(dbgrid == "DataGridProductKam")
            //{
            //    DBgrid.ItemsSource = GetProductKamList();
            //}
            //if (dbgrid == "DataGridProductUrta")
            //{
            //    DBgrid.ItemsSource = GetProductUrtaList();
            //}
            //if (dbgrid == "DataGridProductMeyor")
            //{
            //    DBgrid.ItemsSource = GetProductMeyorList();
            //}
        }
        public static int firstKam = 0, firstUrta = 0, firstMeyor = 0;

        private void btnPreviusUrta_Click(object sender, RoutedEventArgs e)
        {

        }

        public static int LastKam = 0, LastUrta = 0, LastMeyor = 0;
        public int RowKam = -1, RowUrta = -1, RowMeyor = -1;
        private void comboFilial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Tugagan maxsulot uchun
            if(comboFilial.SelectedIndex == 0)
            {
                index = 0;
                string queryProductKam = "select product.product_id, product.name, product.price, val.name as val_ul,product.quantity, product.barcode, product.measurement, product.preparer, product.min_count from product " +
                "inner join val on val.id = product.val_id " +
                "where product.quantity = 0 order by product.product_id limit 100";
                tbProductKam = new DataTable();
                objDBAccess.readDatathroughAdapter(queryProductKam, tbProductKam);
                if (tbProductKam.Rows.Count >= 100)
                {
                    tbNavigateKam = new DataTable();
                    tbNavigateKam.Columns.Add("first", typeof(int));
                    tbNavigateKam.Columns.Add("last" , typeof(int));
                    firstKam = Convert.ToInt32(tbProductKam.Rows[0]["product_id"]);
                    LastKam = Convert.ToInt32(tbProductKam.Rows[99]["product_id"]);
                    tbNavigateKam.Rows.Add(firstKam, LastKam);
                    RowKam = 0;
                    GridPage.Visibility = Visibility.Visible;
                    tbNavigateKam.Dispose();
                }
                dataGridProductKam.ItemsSource = GetProductKamList();
                myEffect.Radius = 10;
                Effect = myEffect;
                using (LoadingWindow lw = new LoadingWindow(Simulator))
                {
                    lw.ShowDialog();
                }
                myEffect.Radius = 0;
                Effect = myEffect;

                StackKam.Visibility = Visibility.Visible;
                StackUrta.Visibility = Visibility.Collapsed;
                StackMeyor.Visibility = Visibility.Collapsed;
                tbProductKam.Dispose();

            }

            // O'rta maxsulot uchun
            if (comboFilial.SelectedIndex == 1)
            {
                index = 1;
                try
                {
                    string queryProductUrta = "select product.product_id, product.name, product.price, val.name as val_ul,product.quantity, product.barcode, product.measurement, product.preparer, product.min_count from product " +
                    "inner join val on val.id = product.val_id " +
                    "where product.quantity <=product.min_count and product.quantity > 0 order by product.product_id limit 100";
                    tbProductUrta = new DataTable();
                    objDBAccess.readDatathroughAdapter(queryProductUrta, tbProductUrta);
                    if (tbProductUrta.Rows.Count >= 100)
                    {
                        tbNavigateUrta = new DataTable();
                        tbNavigateUrta.Columns.Add("first", typeof(int));
                        tbNavigateUrta.Columns.Add("last", typeof(int));
                        firstUrta = Convert.ToInt32(tbProductUrta.Rows[0]["product_id"]);
                        LastUrta = Convert.ToInt32(tbProductUrta.Rows[99]["product_id"]);
                        tbNavigateUrta.Rows.Add(firstUrta, LastUrta);
                        RowUrta = 0;
                        GridPageUrta.Visibility = Visibility.Visible;
                        tbNavigateUrta.Dispose();
                    }
                    dataGridProductUrta.ItemsSource = GetProductUrtaList();
                    myEffect.Radius = 10;
                    Effect = myEffect;
                    using (LoadingWindow lw = new LoadingWindow(Simulator))
                    {
                        lw.ShowDialog();
                    }
                    myEffect.Radius = 0;
                    Effect = myEffect;
                    StackKam.Visibility = Visibility.Collapsed;
                    StackUrta.Visibility = Visibility.Visible;
                    StackMeyor.Visibility = Visibility.Collapsed;
                    tbProductUrta.Dispose();
                }catch(Exception ex) { MessageBox.Show(ex.Message); }
            }

            // Yetarli maxsulot uchun
            if (comboFilial.SelectedIndex == 2)
            {
                index = 2;
                string queryProductMeyor = "select product.product_id, product.name, product.price, val.name as val_ul,product.quantity, product.barcode, product.measurement, product.preparer, product.min_count from product " +
                "inner join val on val.id = product.val_id " +
                "where product.quantity > product.min_count order by product.product_id limit 100";
                tbProductMeyor = new DataTable();
                objDBAccess.readDatathroughAdapter(queryProductMeyor, tbProductMeyor);
                if (tbProductMeyor.Rows.Count >= 100)
                {
                    tbNavigateMeyor = new DataTable();
                    tbNavigateMeyor.Columns.Add("first", typeof(int));
                    tbNavigateMeyor.Columns.Add("last", typeof(int));
                    firstMeyor = Convert.ToInt32(tbProductMeyor.Rows[0]["product_id"]);
                    LastMeyor = Convert.ToInt32(tbProductMeyor.Rows[99]["product_id"]);
                    tbNavigateMeyor.Rows.Add(firstMeyor, LastMeyor);
                    RowMeyor = 0;
                    GridPageMeyor.Visibility = Visibility.Visible;
                    tbNavigateMeyor.Dispose();
                }
                dataGridProductMeyor.ItemsSource = GetProductMeyorList();
                myEffect.Radius = 10;
                Effect = myEffect;
                using (LoadingWindow lw = new LoadingWindow(Simulator))
                {
                    lw.ShowDialog();
                }
                myEffect.Radius = 0;
                Effect = myEffect;
                StackKam.Visibility = Visibility.Collapsed;
                StackUrta.Visibility = Visibility.Collapsed;
                StackMeyor.Visibility = Visibility.Visible;
                tbProductMeyor.Dispose();

            }
        }

        private void btnPreviusKam_Click(object sender, RoutedEventArgs e)
        {
            if (StackKam.Visibility == Visibility.Visible)
            {
                if ((RowKam - 1) >= 0)
                {
                    string queryProductKam = "select product.product_id, product.name, product.price, val.name as val_ul,product.quantity, product.barcode, product.measurement, product.preparer, product.min_count from product " +
                    "inner join val on val.id = product.val_id " +
                    "where product.quantity = 0 and product.product_id>='" + tbNavigateKam.Rows[RowKam - 1]["first"] + "' and product.product_id<='" + tbNavigateKam.Rows[RowKam - 1]["last"] + "' order by product.product_id limit 100";
                    tbProductKam.Clear();
                    objDBAccess.readDatathroughAdapter(queryProductKam, tbProductKam);

                    DataRow rowItem = tbNavigateKam.Rows[RowKam];
                    rowItem.Delete();
                    tbNavigateKam.AcceptChanges();
                    RowKam -= 1;
                    dataGridProductKam.ItemsSource = GetProductKamList();
                    myEffect.Radius = 10;
                    Effect = myEffect;
                    using (LoadingWindow lw = new LoadingWindow(Simulator))
                    {
                        lw.ShowDialog();
                    }
                    myEffect.Radius = 0;
                    Effect = myEffect;
                }
                else
                {
                    btnPreviusKam.IsEnabled = false;
                    btnPreviusKam.Opacity = 0.5;

                }
                btnNextKam.IsEnabled = true;
                btnNextKam.Opacity = 1;
            }
            
            if(StackUrta.Visibility == Visibility.Visible)
            {
                if ((RowUrta - 1) >= 0)
                {
                    string queryProductUrta = "select product.product_id, product.name, product.price, val.name as val_ul,product.quantity, product.barcode, product.measurement, product.preparer, product.min_count from product " +
                    "inner join val on val.id = product.val_id " +
                    "where product.quantity <=product.min_count and product.quantity > 0 and product.product_id>='" + tbNavigateUrta.Rows[RowUrta - 1]["first"] + "' and product.product_id<='" + tbNavigateUrta.Rows[RowUrta - 1]["last"] + "' order by product.product_id limit 100";
                    tbProductUrta.Clear();
                    objDBAccess.readDatathroughAdapter(queryProductUrta, tbProductUrta);
                    DataRow rowItem = tbNavigateUrta.Rows[RowUrta];
                    rowItem.Delete();
                    tbNavigateUrta.AcceptChanges();
                    RowUrta -= 1;
                    dataGridProductUrta.ItemsSource = GetProductUrtaList();
                    myEffect.Radius = 10;
                    Effect = myEffect;
                    using (LoadingWindow lw = new LoadingWindow(Simulator))
                    {
                        lw.ShowDialog();
                    }
                    myEffect.Radius = 0;
                    Effect = myEffect;
                }
                else
                {
                    btnPreviusUrta.IsEnabled = false;
                    btnPreviusUrta.Opacity = 0.5;

                }
                btnNextUrta.IsEnabled = true;
                btnNextUrta.Opacity = 1;
            }

            if(StackMeyor.Visibility == Visibility.Visible)
            {
                if ((RowMeyor - 1) >= 0)
                {
                    string queryProductMeyor = "select product.product_id, product.name, product.price, val.name as val_ul,product.quantity, product.barcode, product.measurement, product.preparer, product.min_count from product " +
                    "inner join val on val.id = product.val_id " +
                    "where product.quantity > product.min_count and product.product_id>='" + tbNavigateMeyor.Rows[RowMeyor - 1]["first"] + "' and product.product_id<='" + tbNavigateMeyor.Rows[RowMeyor - 1]["last"] + "' order by product.product_id limit 100";
                    tbProductMeyor.Clear();
                    
                    objDBAccess.readDatathroughAdapter(queryProductMeyor, tbProductMeyor);
                    DataRow rowItem = tbNavigateMeyor.Rows[RowMeyor];
                    rowItem.Delete();
                    tbNavigateMeyor.AcceptChanges();

                    RowMeyor -= 1;
                    dataGridProductMeyor.ItemsSource = GetProductMeyorList();
                    myEffect.Radius = 10;
                    Effect = myEffect;
                    using (LoadingWindow lw = new LoadingWindow(Simulator))
                    {
                        lw.ShowDialog();
                    }
                    myEffect.Radius = 0;
                    Effect = myEffect;
                }
                else
                {
                    btnPreviusMeyor.IsEnabled = false;
                    btnPreviusMeyor.Opacity = 0.5;

                }
                btnNextMeyor.IsEnabled = true;
                btnNextMeyor.Opacity = 1;
            }
        }

        private void btnNextKam_Click(object sender, RoutedEventArgs e)
        {
            if (StackKam.Visibility == Visibility.Visible)
            {
                string queryProductKam = "select product.product_id, product.name, product.price, val.name as val_ul,product.quantity, product.barcode, product.measurement, product.preparer, product.min_count from product " +
                    "inner join val on val.id = product.val_id " +
                    "where product.quantity = 0 and product.product_id>'" + tbNavigateKam.Rows[RowKam]["last"] + "' order by product.product_id limit 100";
                tbProductKam.Clear();
                objDBAccess.readDatathroughAdapter(queryProductKam, tbProductKam);
                if (tbProductKam.Rows.Count >= 100)
                {
                    firstKam = Convert.ToInt32(tbProductKam.Rows[0]["product_id"]);
                    LastKam = Convert.ToInt32(tbProductKam.Rows[99]["product_id"]);
                    tbNavigateKam.Rows.Add(firstKam, LastKam);
                    RowKam += 1;
                    GridPage.Visibility = Visibility.Visible;
                    btnPreviusKam.IsEnabled = true;
                    btnPreviusKam.Opacity = 1;
                }
                else if (tbProductKam.Rows.Count > 0)
                {
                    firstKam = Convert.ToInt32(tbProductKam.Rows[0]["product_id"]);
                    LastKam = Convert.ToInt32(tbProductKam.Rows[tbProductKam.Rows.Count - 1]["product_id"]);
                    tbNavigateKam.Rows.Add(firstKam, LastKam);
                    RowKam += 1;
                    GridPage.Visibility = Visibility.Visible;
                    btnPreviusKam.IsEnabled = true;
                    btnPreviusKam.Opacity = 1;
                }
                else
                {
                    btnNextKam.IsEnabled = false;
                    btnNextKam.Opacity = 0.5;
                    btnPreviusKam.IsEnabled = true;
                    btnPreviusKam.Opacity = 1;
                }
                dataGridProductKam.ItemsSource = GetProductKamList();
                myEffect.Radius = 10;
                Effect = myEffect;
                using (LoadingWindow lw = new LoadingWindow(Simulator))
                {
                    lw.ShowDialog();
                }
                myEffect.Radius = 0;
                Effect = myEffect;
            }

            if (StackUrta.Visibility == Visibility.Visible)
            {
                string queryProductUrta = "select product.product_id, product.name, product.price, val.name as val_ul,product.quantity, product.barcode, product.measurement, product.preparer, product.min_count from product " +
                    "inner join val on val.id = product.val_id " +
                    "where product.quantity <= product.min_count and product.quantity > 0 and product.product_id>'" + tbNavigateUrta.Rows[RowUrta]["last"] + "' order by product.product_id limit 100";
                tbProductUrta.Clear();
                objDBAccess.readDatathroughAdapter(queryProductUrta, tbProductUrta);
                if (tbProductUrta.Rows.Count >= 100)
                {
                    firstUrta= Convert.ToInt32(tbProductUrta.Rows[0]["product_id"]);
                    LastUrta = Convert.ToInt32(tbProductUrta.Rows[99]["product_id"]);
                    tbNavigateUrta.Rows.Add(firstUrta, LastUrta);
                    RowUrta += 1;
                    GridPageUrta.Visibility = Visibility.Visible;
                    btnPreviusUrta.IsEnabled = true;
                    btnPreviusUrta.Opacity = 1;
                }
                else if (tbProductUrta.Rows.Count > 0)
                {
                    firstUrta = Convert.ToInt32(tbProductUrta.Rows[0]["product_id"]);
                    LastUrta = Convert.ToInt32(tbProductUrta.Rows[tbProductUrta.Rows.Count - 1]["product_id"]);
                    tbNavigateUrta.Rows.Add(firstUrta, LastUrta);
                    RowUrta += 1;
                    GridPageUrta.Visibility = Visibility.Visible;
                    btnPreviusUrta.IsEnabled = true;
                    btnPreviusUrta.Opacity = 1;
                }
                else
                {
                    btnNextUrta.IsEnabled = false;
                    btnNextUrta.Opacity = 0.5;
                    btnPreviusUrta.IsEnabled = true;
                    btnPreviusUrta.Opacity = 1;
                }
                dataGridProductUrta.ItemsSource = GetProductUrtaList();
                myEffect.Radius = 10;
                Effect = myEffect;
                using (LoadingWindow lw = new LoadingWindow(Simulator))
                {
                    lw.ShowDialog();
                }
                myEffect.Radius = 0;
                Effect = myEffect;
            }

            if (StackMeyor.Visibility == Visibility.Visible)
            {
                string queryProductMeyor = "select product.product_id, product.name, product.price, val.name as val_ul,product.quantity, product.barcode, product.measurement, product.preparer, product.min_count from product " +
                    "inner join val on val.id = product.val_id " +
                    "where product.quantity > product.min_count and product.product_id>'" + tbNavigateMeyor.Rows[RowMeyor]["last"] + "' order by product.product_id limit 100";
                tbProductMeyor.Clear();
                objDBAccess.readDatathroughAdapter(queryProductMeyor, tbProductMeyor);
                if (tbProductMeyor.Rows.Count >= 100)
                {
                    firstMeyor = Convert.ToInt32(tbProductMeyor.Rows[0]["product_id"]);
                    LastMeyor = Convert.ToInt32(tbProductMeyor.Rows[99]["product_id"]);
                    tbNavigateMeyor.Rows.Add(firstMeyor, LastMeyor);
                    RowMeyor += 1;
                    GridPageMeyor.Visibility = Visibility.Visible;
                    btnPreviusMeyor.IsEnabled = true;
                    btnPreviusMeyor.Opacity = 1;
                }
                else if (tbProductMeyor.Rows.Count > 0)
                {
                    firstMeyor = Convert.ToInt32(tbProductMeyor.Rows[0]["product_id"]);
                    LastMeyor = Convert.ToInt32(tbProductMeyor.Rows[tbProductMeyor.Rows.Count - 1]["product_id"]);
                    tbNavigateMeyor.Rows.Add(firstMeyor, LastMeyor);
                    RowMeyor += 1;
                    GridPageMeyor.Visibility = Visibility.Visible;
                    btnPreviusMeyor.IsEnabled = true;
                    btnPreviusMeyor.Opacity = 1;
                }
                else
                {
                    btnNextMeyor.IsEnabled = false;
                    btnNextMeyor.Opacity = 0.5;
                    btnPreviusMeyor.IsEnabled = true;
                    btnPreviusMeyor.Opacity = 1;
                }
                dataGridProductMeyor.ItemsSource = GetProductMeyorList();
                myEffect.Radius = 10;
                Effect = myEffect;
                using (LoadingWindow lw = new LoadingWindow(Simulator))
                {
                    lw.ShowDialog();
                }
                myEffect.Radius = 0;
                Effect = myEffect;
            }
        }



        //public List<Product> GetProductLoop(int n)
        //{
        //    for(int i = 0; i<n; i++)
        //    {
        //        Product product = new Product();
        //        product.product_id = Convert.ToInt32(tbProduct.Rows[i]["product_id"]);
        //        product.name = tbProduct.Rows[i]["name"].ToString();
        //        product.price = Convert.ToDouble(tbProduct.Rows[i]["price"].ToString());
        //        product.val_ul = tbProduct.Rows[i]["val_ul"].ToString();
        //        product.quantity = Convert.ToDouble(tbProduct.Rows[i]["quantity"].ToString());
        //        product.barcode = tbProduct.Rows[i]["barcode"].ToString();
        //        product.measurement = tbProduct.Rows[i]["measurement"].ToString();
        //        product.preparer = tbProduct.Rows[i]["preparer"].ToString();

        //        string st_quan = tbProduct.Rows[i]["quantity"].ToString();
        //        double Dquan = double.Parse(st_quan = DoubleToStr(st_quan), CultureInfo.InvariantCulture);

        //        string min_quan = tbProduct.Rows[i]["min_count"].ToString();
        //        double Dmin_quan = double.Parse(DoubleToStr(min_quan), CultureInfo.InvariantCulture);
        //        if (Dquan < Dmin_quan)
        //        {
        //            product.status = "red";
        //        }

        //        if(Dquan == Dmin_quan)
        //        {
        //            product.status = "yellow";
        //        }
        //        if(Dquan > Dmin_quan)
        //        {
        //            product.status = "green";
        //        }
        //        productList.Add(product);
        //    }

        //    return productList;
        //}

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Hello");
            //string queryProduct = "select product.product_id, product.name, product.price, val.name as val_ul,product.quantity, product.barcode, product.measurement, product.preparer, product.min_count from product " +
            //    "inner join val on val.id = product.val_id " +
            //    "where quantity <=min_count order by quantity";
            //tbProduct = new DataTable();
            //objDBAccess.readDatathroughAdapter(queryProduct, tbProduct);
            //if(tbProduct.Rows.Count > 50)
            //{
            //    first = 50;
            //    dataGridProduct.ItemsSource = GetProductLoop(50);
            //    n = tbProduct.Rows.Count - 50;
            //}
            //else
            //{
            //    n = tbProduct.Rows.Count;
            //    dataGridProduct.ItemsSource = GetProductLoop(n);
            //    n = 0;
            //}
            
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            //
        }
    }
}
