using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Windows.Media.Effects;
using System.Threading;

namespace SotuvPlatformasi
{
    /// <summary>
    /// Interaction logic for faktura_kurish_ucont.xaml
    /// </summary>
    public partial class faktura_kurish_ucont : UserControl
    {
        public faktura_kurish_ucont()
        {
            InitializeComponent();
        }
        MainWindow main = (MainWindow)Application.Current.MainWindow;
        public static string recieve_id = "";
        public static List<FakturaItem> fakturaItems;
        public static List<RecieveItem> recieveItems;

        #region faktura class
        /// <summary>
        /// Classes for faktureItem
        /// </summary>
        public class Product
        {
            public int id { get; set; }
            public string name { get; set; }
            public string measurement { get; set; }
            public string preparer { get; set; }
            public double min_count { get; set; }
        }

        public class FakturaItem
        {
            public int id { get; set; }
            public Product product { get; set; }
            public string name { get; set; }
            public double som { get; set; }
            public double dollar { get; set; }
            public double quantity { get; set; }
            public string barcode { get; set; }
            public int faktura { get; set; }
            public int group { get; set; }
        }

        #endregion

        #region recieve class

        /// <summary>
        /// Recieve item classses
        /// </summary>
        public class Filial
        {
            public int id { get; set; }
            public string name { get; set; }
            public string address { get; set; }
            public double qarz_som { get; set; }
            public double qarz_dol { get; set; }
            public double savdo_puli_som { get; set; }
            public double savdo_puli_dol { get; set; }
        }

        public class Products
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

        public class RecieveItem
        {
            public int id { get; set; }
            public int recieve { get; set; }
            public Products product { get; set; }
            public double som { get; set; }
            public double sotish_som { get; set; }
            public double dollar { get; set; }
            public double sotish_dollar { get; set; }
            public double kurs { get; set; }
            public double quantity { get; set; }
        }

        #endregion

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            main.TabMenu.SelectedIndex = 4;
        }

        private void BtnAsosiy_Click(object sender, RoutedEventArgs e)
        {
            main.TabMenu.SelectedIndex = 1;
        }

        public static async Task<string> GetObject(string restCallURL)
        {
            HttpClient apiCallClient = new HttpClient();
            string authToken = "token b4e829ee7f3616338ec69381da368634759394f4";
            HttpRequestMessage apirequest = new HttpRequestMessage(HttpMethod.Get, restCallURL);
            apirequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            apirequest.Headers.Add("Authorization", authToken);
            HttpResponseMessage apiCallResponse = await apiCallClient.SendAsync(apirequest);

            string requestresponse = await apiCallResponse.Content.ReadAsStringAsync();
            return requestresponse;
        }
        void Simulator()
        {
            for (int i = 0; i < 300; i++)
                Thread.Sleep(5);
        }
        BlurEffect myEffect = new BlurEffect();
        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (MainWindow.fakt_qab_click)
            {
                try
                {
                    string urlFakturaItem = "http://turonsavdo.backoffice.uz/api/recieveitem/rv1/?rec=" + recieve_id;
                    string RecieveItemContent = await GetObject(urlFakturaItem);
                    myEffect.Radius = 10;
                    Effect = myEffect;
                    using (LoadingWindow lw = new LoadingWindow(Simulator))
                    {
                        lw.ShowDialog();
                    }
                    myEffect.Radius = 0;
                    Effect = myEffect;
                    recieveItems = JsonConvert.DeserializeObject<List<RecieveItem>>(RecieveItemContent);
                    dataGridFaktura.ItemsSource = recieveItems;
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message, "Сообщение", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return;
                }
            }
        }
    }
}
