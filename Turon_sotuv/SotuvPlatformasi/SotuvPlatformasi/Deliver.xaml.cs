using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Windows.Media.Effects;
using System.Threading;
using System.Globalization;

namespace SotuvPlatformasi
{
    /// <summary>
    /// Interaction logic for Deliver.xaml
    /// </summary>
    public partial class Deliver : Window
    {
        public Deliver()
        {
            InitializeComponent();
        }

        public static DataTable tbDeliver, tbDel;
        public static bool dv = false;
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public static async Task<string> GetObject(string restCallURL)
        {
            HttpClient apiCallClient = new HttpClient();
            string authToken = "token d0347b90933d3d4b4fbd2d30fb2dd79d824091bc";
            HttpRequestMessage apirequest = new HttpRequestMessage(HttpMethod.Get, restCallURL);
            apirequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            apirequest.Headers.Add("Authorization", authToken);
            HttpResponseMessage apiCallResponse = await apiCallClient.SendAsync(apirequest);

            string requestresponse = await apiCallResponse.Content.ReadAsStringAsync();
            return requestresponse;
        }

        public class Delivers
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        public List<Delivers> delivers = new List<Delivers>();

        public List<Delivers> GetDeliverList()
        {
            delivers = (from DataRow dr in tbDeliver.Rows
                        select new Delivers()
                        {
                            id = Convert.ToInt32(dr["id"]),
                            name = dr["name"].ToString()
                        }).ToList();
            return delivers;
        }

        public List<Delivers> GetDeliverListSearch()
        {
            delivers = (from DataRow dr in tbDel.Rows
                        select new Delivers()
                        {
                            id = Convert.ToInt32(dr["id"]),
                            name = dr["name"].ToString()
                        }).ToList();
            return delivers;
        }

        void Simulator()
        {
            for (int i = 0; i < 200; i++)
                Thread.Sleep(5);
        }

        BlurEffect myEffect = new BlurEffect();

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                tbDeliver = new DataTable();
                string responceDeliver = await GetObject("http://turonsavdo.backoffice.uz/api/deliver/");
                myEffect.Radius = 10;
                Effect = myEffect;
                using (LoadingWindow lw = new LoadingWindow(Simulator))
                {
                    lw.ShowDialog();
                }
                myEffect.Radius = 0;
                Effect = myEffect;
                JArray arrayDeliver = JArray.Parse(responceDeliver);
                if (arrayDeliver != null)
                {
                    tbDeliver = JsonConvert.DeserializeObject<DataTable>(responceDeliver);
                    dataGridClient.ItemsSource = GetDeliverList();
                }
                tbDeliver.Dispose();
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

            }
        }

        private void btnChoose_Click(object sender, RoutedEventArgs e)
        {
            Delivers selectedDeliver = dataGridClient.SelectedItems[0] as Delivers;

            ReturnDeliver_ucont.deliver_id = selectedDeliver.id;
            ReturnDeliver_ucont.deliver_name = selectedDeliver.name;
            this.Close();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbDeliver.Rows.Count > 0 && txtSearch.Text.Length > 2)
            {
                DataRow[] rows;
                rows = tbDeliver.Select("name like '%" + txtSearch.Text + "%'");
                
                tbDel = new DataTable();
                tbDel.Columns.Add("id", typeof(int));
                tbDel.Columns.Add("name");
                foreach (var item in rows)
                {
                    DataRow row = tbDel.NewRow();
                    row["id"] = item["id"];
                    row["name"] = item["name"];
                    tbDel.Rows.Add(row);
                }
                dataGridClient.ItemsSource = GetDeliverListSearch();
                tbDel.Dispose();
                dv = true;
            }
            else
            {
                dataGridClient.ItemsSource = GetDeliverList();
                dv = false;
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                txtSearch.Focus();
            }
        }
    }
}
