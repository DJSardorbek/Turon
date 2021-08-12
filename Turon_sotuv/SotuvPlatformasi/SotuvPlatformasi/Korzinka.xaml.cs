using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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

namespace SotuvPlatformasi
{
    /// <summary>
    /// Interaction logic for Korzinka.xaml
    /// </summary>
    public partial class Korzinka : Window
    {
        public Korzinka()
        {
            InitializeComponent();
        }

        public string barcode = "";
        public string price = "";
        public string oldquantity = "";
        public string product_id = "";
        public string name = "",val_ul="",kurs="", quantity="";
        MySqlCommand cmdProduct, cmdCart;
        DBAccess objDBAccess = new DBAccess();


        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Sotuv_ucont.can_refresh = false;
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtProduct.Text = name;
            txtBarcode.Text = barcode;
            txtPrice.Text = price;
            txtKurs.Text = kurs;
            txtValUl.Text = val_ul;
            txtQuantityOmbor.Text = quantity;
            txtQuantity.Focus();
            
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_Click(sender, e);
            }
            if (e.Key == Key.Escape)
            {
                Sotuv_ucont.can_refresh = false;
                this.Close();
            }
        }

        private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            
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


        public void Button_Click(object sender, RoutedEventArgs e)
        {

            if (txtQuantity.Text == "") return;
            if (txtQuantity.Text.IndexOf(',') > -1)
            {
                System.Windows.Forms.MessageBox.Show("Nuqta bilan kiriting!", "Сообщение", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                txtQuantity.Clear();
                return;
            }
            string TOTAL = "0", val_id="";
            if (txtQuantity.Text != "")
            {
                double quantity = double.Parse(txtQuantity.Text, CultureInfo.InvariantCulture);
                //Kelishilgan narxni miqdorga ko'paytiramiz
                double total = quantity * double.Parse(DoubleToStr(txtPrice.Text), CultureInfo.InvariantCulture);
                if(txtValUl.Text == "$")
                {
                    double Dkurs = double.Parse(DoubleToStr(kurs), CultureInfo.InvariantCulture);
                    total = total * Dkurs;
                }
                TOTAL = DoubleToStr(total.ToString()); 
                //Agar narx kelishilgan bo'lsa farqni hisoblaymiz
            }
            objDBAccess.createConn();
            int ID = Sotuv_ucont.shopID;
            string rq = txtQuantity.Text;
            oldquantity = DoubleToStr(oldquantity);

            double rq1 = double.Parse(oldquantity, CultureInfo.InvariantCulture);
            double rq2 = double.Parse(rq, CultureInfo.InvariantCulture);
            double resultQuantity = rq1 - rq2; //double.Parse(oldquantity, CultureInfo.CurrentCulture) - double.Parse(rq, CultureInfo.CurrentCulture);
            if (resultQuantity < 0)
            {
                System.Windows.Forms.MessageBox.Show("Maxsulot yetarli emas!\n Bizda maxsulot qoldig'i '" + oldquantity + "'", "Ogohlantirish", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                return;
            }

            if(txtValUl.Text == "uz") { val_id = "1"; }
            else 
            { 
                val_id = "1";
                double Dprice = double.Parse(DoubleToStr(price), CultureInfo.InvariantCulture);
                double Dkurs = double.Parse(DoubleToStr(kurs), CultureInfo.InvariantCulture);
                double R_price = Dprice * Dkurs;
                price = DoubleToStr(R_price.ToString());
            }

            // Agar ushbu maxsulotdan avval olinmagan bo'lsa, id=1 bo'lsa
           if (ID == 1)
           {
                //quantity va totalni double yoki int ekanligini tekshiramiz
                string str_result_quantity = txtQuantity.Text;
                string str_result_total = TOTAL;
                str_result_quantity = DoubleToStr(str_result_quantity);
                str_result_total = DoubleToStr(str_result_total);
                    
                cmdCart = new MySqlCommand("insert into cart (id,shop_id, product_id, name, price, val_id, quantity, total) values('" + ID + "', '" + ID + "', '" + product_id + "', '" + name + "', '" + DoubleToStr(price) + "','"+ val_id + "', '" + DoubleToStr(txtQuantity.Text) + "', '" + str_result_total + "')");
                    
                objDBAccess.executeQuery(cmdCart);

                cmdCart.Dispose();
           }

           else // id si 1 ga teng bo'lmasa
           {
                //quantity va totalni double yoki int ekanligini tekshiramiz
                string str_result_quantity = txtQuantity.Text;
                string str_result_total = TOTAL;
                str_result_quantity = DoubleToStr(str_result_quantity);
                str_result_total = DoubleToStr(str_result_total);
                
                cmdCart = new MySqlCommand("insert into cart (shop_id,product_id,name,price ,val_id,quantity,total) values('" + ID + "', '" + product_id + "', '" + name + "', '" + DoubleToStr(price) + "','"+val_id+"', '" + DoubleToStr(txtQuantity.Text) + "', '" + TOTAL + "')");
                
                objDBAccess.executeQuery(cmdCart);

                cmdCart.Dispose();
           }

            string str_resultQuantity = resultQuantity.ToString();
            str_resultQuantity = DoubleToStr(str_resultQuantity);
            cmdProduct = new MySqlCommand("update product set quantity='" + str_resultQuantity + "' where product_id='" + product_id + "' and barcode='" + barcode + "'");

            objDBAccess.executeQuery(cmdProduct);

            cmdProduct.Dispose();
            cmdCart.Dispose();
            Sotuv_ucont.can_refresh = true;
            Close();
        }
    }
}
