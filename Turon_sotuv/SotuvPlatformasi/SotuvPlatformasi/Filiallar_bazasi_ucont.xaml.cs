using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Effects;

namespace SotuvPlatformasi
{
    /// <summary>
    /// Interaction logic for Filiallar_bazasi_ucont.xaml
    /// </summary>
    public partial class Filiallar_bazasi_ucont : UserControl
    {
        public Filiallar_bazasi_ucont()
        {
            InitializeComponent();
            //comboFilial.Items.Add("Filial1");
            //comboFilial.Items.Add("Filial2");
            //comboFilial.Items.Add("Filial3");
        }

        public static bool filial = false;
        public class Filial
        {
            public int id { get; set; }
            public string name { get; set; }
            public string address { get; set; }
            public int qarz_som { get; set; }
            public int qarz_dol { get; set; }
            public int savdo_puli_som { get; set; }
            public int savdo_puli_dol { get; set; }
        }

        public class Result
        {
            public int id { get; set; }
            public Filial filial { get; set; }
            public string name { get; set; }
            public double som { get; set; }
            public double sotish_som { get; set; }
            public double dollar { get; set; }
            public double sotish_dollar { get; set; }
            public double kurs { get; set; }
            public double quantity { get; set; }
            public string preparer { get; set; }
            public double min_count { get; set; }
            public string measurement { get; set; }
            public string barcode { get; set; }
            public int group { get; set; }
        }

        public class ProductFilial
        {
            public int count { get; set; }
            public object next { get; set; }
            public object previous { get; set; }
            public IList<Result> results { get; set; }
        }


        public class _Filial
        {
            public int id { get; set; }
            public string name { get; set; }
            public string address { get; set; }
        }

        public static async Task<string> GetObject(string restCallURL)
        {
            HttpClient apiCallClient = new HttpClient();
            string authToken = "token 249d4a8aa9ecf75844d87926b7b7ee4e1cd8b1da";
            HttpRequestMessage apirequest = new HttpRequestMessage(HttpMethod.Get, restCallURL);
            apirequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            apirequest.Headers.Add("Authorization", authToken);
            HttpResponseMessage apiCallResponse = await apiCallClient.SendAsync(apirequest);

            string requestresponse = await apiCallResponse.Content.ReadAsStringAsync();
            return requestresponse;
        }

        private void BtnAsosiy_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = (MainWindow)Application.Current.MainWindow;
            main.TabMenu.SelectedIndex = 1;
        }

        public string ProductFilialContent = "", combo_sel_index = "", ContentFilial = "";
        void Simulator()
        {
            for(int i = 0; i<200; i++)
            Thread.Sleep(5);
        }
        List<_Filial> filialArray;
        ProductFilial productFilial;

        BlurEffect myEffect = new BlurEffect();

        private async void comboFilial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                _Filial selectedFilial = comboFilial.SelectedItem as _Filial;
                combo_sel_index = selectedFilial.id.ToString();
                string urlProductfilial = "http://turonsavdo.backoffice.uz/api/productfilial/by_filial/?f=" + combo_sel_index;

                ProductFilialContent = await GetObject(urlProductfilial);
                myEffect.Radius = 10;
                Effect = myEffect;
                using (LoadingWindow lw = new LoadingWindow(Simulator))
                {
                    lw.ShowDialog();
                }
                myEffect.Radius = 0;
                Effect = myEffect;
                productFilial = JsonConvert.DeserializeObject<ProductFilial>(ProductFilialContent);
                dataGridFilillar.ItemsSource = productFilial.results.ToList();
                if (dataGridFilillar.Items.Count != 0)
                    GridPage.Visibility = Visibility.Visible;
                else
                    GridPage.Visibility = Visibility.Collapsed;
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Exception", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                
            }
        }

        private async void btnPrevius_Click(object sender, RoutedEventArgs e)
        {
            if (productFilial.previous != null)
            {
                try
                {
                    string urlProductfilial = productFilial.previous.ToString();
                    string ProductFilialContent = await GetObject(urlProductfilial);
                    myEffect.Radius = 10;
                    Effect = myEffect;
                    using (LoadingWindow lw = new LoadingWindow(Simulator))
                    {
                        lw.ShowDialog();
                    }
                    myEffect.Radius = 0;
                    Effect = myEffect;
                    productFilial = JsonConvert.DeserializeObject<ProductFilial>(ProductFilialContent);
                    dataGridFilillar.ItemsSource = productFilial.results.ToList();
                    if(productFilial.previous == null)
                    {
                        btnPrevius.IsEnabled = false;
                        btnPrevius.Opacity = 0.5;
                    }
                    if (btnNext.IsEnabled == false)
                    {
                        btnNext.IsEnabled = true;
                        btnNext.Opacity = 1;
                    }
                }
                catch(Exception ex)
                {
                     System.Windows.Forms.MessageBox.Show(ex.Message, "Exception", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            else
            {
                btnPrevius.IsEnabled = false;
                btnPrevius.Opacity = 0.5;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var tbx = sender as TextBox;
                if (productFilial.count != 0)
                {
                    if (!string.IsNullOrEmpty(tbx.Text))
                    {
                        //var filterList = productFilial.results.ToList().Where(x => x.name.Contains(tbx.Text));
                        var filterList = from pr in productFilial.results
                                         where pr.name.Contains(tbx.Text)
                                         select pr; 
                        dataGridFilillar.ItemsSource = null;
                        dataGridFilillar.ItemsSource = filterList;
                    }
                    else
                    {
                        dataGridFilillar.ItemsSource = productFilial.results.ToList();
                    }
                }
            }
            catch(Exception) { }
        }

        MainWindow main = (MainWindow)Application.Current.MainWindow;

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (filial == false && main.TabMenu.SelectedIndex == 8)
            {
                string urlFilial = "http://turonsavdo.backoffice.uz/api/filial/";
                ContentFilial = await GetObject(urlFilial);
                myEffect.Radius = 10;
                Effect = myEffect;
                using (LoadingWindow lw = new LoadingWindow(Simulator))
                {
                    lw.ShowDialog();
                }
                myEffect.Radius = 0;
                Effect = myEffect;
                filialArray = JsonConvert.DeserializeObject<List<_Filial>>(ContentFilial);
                comboFilial.ItemsSource = filialArray.ToList();
                comboFilial.DisplayMemberPath = "name";
                filial = true;
            }
        }

        private async void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if(productFilial.next != null)
            {
                try
                {
                    string urlProductfilial = productFilial.next.ToString();
                    string ProductFilialContent = await GetObject(urlProductfilial);
                    myEffect.Radius = 10;
                    Effect = myEffect;
                    using (LoadingWindow lw = new LoadingWindow(Simulator))
                    {
                        lw.ShowDialog();
                    }
                    myEffect.Radius = 0;
                    Effect = myEffect;
                    productFilial = JsonConvert.DeserializeObject<ProductFilial>(ProductFilialContent);
                    dataGridFilillar.ItemsSource = productFilial.results.ToList();
                    if(productFilial.next == null)
                    {
                        btnNext.IsEnabled = false;
                        btnNext.Opacity = 0.5;
                    }
                    if (btnPrevius.IsEnabled == false)
                    {
                        btnPrevius.IsEnabled = true;
                        btnPrevius.Opacity = 1;
                    }
                }
                catch(Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message, "Exception", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            else
            {
                btnNext.IsEnabled = false;
                btnNext.Opacity = 0.5;
            }
        }
    }
}
