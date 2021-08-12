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

namespace SotuvPlatformasi
{
    /// <summary>
    /// Interaction logic for Sozlamalar_ucont.xaml
    /// </summary>
    public partial class Sozlamalar_ucont : UserControl
    {
        public Sozlamalar_ucont()
        {
            InitializeComponent();
        }
        private void BtnAsosiy_Click(object sender, RoutedEventArgs e)
        {
            //Asosiy main = new Asosiy();
            //main.Show();
            //this.Close();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Check_sozlamalar();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            hodim_add hodim_Add = new hodim_add();
            hodim_Add.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Main.Content = new Staff_list();
        }
    }
}
