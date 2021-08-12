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
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Data;
using System.Net.Http;
using Newtonsoft.Json;

namespace SotuvPlatformasi
{
    /// <summary>
    /// Interaction logic for hodim_add.xaml
    /// </summary>
    public partial class hodim_add : Window
    {
        public hodim_add()
        {
            InitializeComponent();
        }
        public static DataTable tbStaff;
        public static MySqlCommand cmdUser;

        DBAccess objDBAccess = new DBAccess();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string queryStaff = "select * from staff";
            tbStaff = new DataTable();
            objDBAccess.readDatathroughAdapter(queryStaff, tbStaff);
            comboLavozim.ItemsSource = tbStaff.DefaultView;
            comboLavozim.DisplayMemberPath = tbStaff.Columns["staff"].ToString();
            comboLavozim.SelectedValuePath = tbStaff.Columns["id"].ToString();
            objDBAccess.closeConn();
        }

        static async Task<string> PostURI(Uri u, HttpContent c)
        {
            var response = string.Empty;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "token 3b3650ae90df953d29521e55492920b531d7cc6f");
                try
                {
                    HttpResponseMessage result = await client.PostAsync(u, c);
                    if (result.IsSuccessStatusCode)
                    {
                        using (HttpContent content = result.Content)
                        {
                            response = await content.ReadAsStringAsync();
                        }
                    }
                    else
                    {
                        response = "Error!";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return response;
        }

        public class UserObject
        {
            public int id { get; set; }
            public string username { get; set; }
            public string password { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public int staff { get; set; }
            public int filial { get; set; }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnQoshish_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text == "" || txtPass.Password == "" || txtSurname.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("Ma'lumotlar to'liq emas,\niltimos tekshirib qaytadan urinib ko'ring!", "Xatolik", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }
            if (txtPass.Password.Length < 6)
            {
                System.Windows.Forms.MessageBox.Show("Parol eng kamida 6 xonalik bo'lishi kerek!", "Xatolik", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }

            Uri u = new Uri("http://turonsavdo.backoffice.uz/api/userprofile/");
            string password = txtPass.Password;
            string first_name = txtName.Text;
            string last_name = txtSurname.Text;
            string staff = (comboLavozim.SelectedIndex + 1).ToString();
            string filial = MainWindow.filial_id;

            var payload = "{\"username\": \"Santexnika\",\"password\": \"" + password + "\",\"first_name\": \"" + first_name + "\",\"last_name\": \"" + last_name + "\",\"staff\": \"" + staff + "\",\"filial\": \"" + filial + "\"}";
            //MessageBox.Show(payload);
            HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");
            var t = Task.Run(() => PostURI(u, content));
            t.Wait();
            if (t.Result == "Error!")
            {
                System.Windows.Forms.MessageBox.Show("Server bilan bog'lanishda xatolik!", "Сообщение", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            else if (t.Result != "Error!")
            {
                string UserContent = t.Result;
                string userId = "";
                UserObject user = JsonConvert.DeserializeObject<UserObject>(UserContent);
                userId = user.id.ToString();

                cmdUser = new MySqlCommand("insert into userprofile (id,password,first_name,last_name,staff_id) values('" + userId + "','" + password + "','" + first_name + "','" + last_name + "','" + staff + "')");
                objDBAccess.executeQuery(cmdUser);
                cmdUser.Dispose();
                System.Windows.Forms.MessageBox.Show("Yangi xodim muvaffaqiyatli qo'shildi!", "Сообщение", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                txtName.Clear();
                txtPass.Clear();
                txtSurname.Clear();
                this.Close();
                
            }
        }
    }
}
