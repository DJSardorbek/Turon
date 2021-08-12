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

namespace SotuvPlatformasi
{
    /// <summary>
    /// Interaction logic for YFakturaAdd.xaml
    /// </summary>
    public partial class YFakturaAdd : Window
    {
        public YFakturaAdd()
        {
            InitializeComponent();
        }

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

        public string pr_name = "", barcode = "", quantity = "";

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                Button_Click(sender, e);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtProduct.Text = pr_name;
            txtBarcode.Text = barcode;
            txtQuantityOmbor.Text = DoubleToStr(quantity);
            txtQuantity.Focus();
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            YuqlamaFaktura.pr_name = pr_name;
            YuqlamaFaktura.quantity = DoubleToStr(txtQuantity.Text);
            YuqlamaFaktura.barcode = barcode;
            YuqlamaFaktura.Yadd = true;
            Close();
        }
    }
}
