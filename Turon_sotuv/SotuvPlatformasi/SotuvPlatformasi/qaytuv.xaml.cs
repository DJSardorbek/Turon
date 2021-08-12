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
using System.Data;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace SotuvPlatformasi
{
    /// <summary>
    /// Interaction logic for qaytuv.xaml
    /// </summary>
    public partial class qaytuv : Window
    {
        public qaytuv()
        {
            InitializeComponent();
        }
        DBAccess objDBAccess = new DBAccess();
        public static MySqlCommand cmdProduct, cmdReturnProduct;
        public string product = "", preparer="", measurement="",
        sold_price = "",current_price="",barcode="",sold_quan="",
        quan_ombor = "", shop_id = "", product_id = "", val_ul="";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtProduct.Text = product;
            txtPreparer.Text = preparer;
            txtMeasurement.Text = measurement;
            txtQuanOmbor.Text = quan_ombor;
            txtSoldQuan.Text = sold_quan;
            txtPrice.Text = sold_price;
            txtVal.Text = val_ul;
            txtQuantity.Focus();

        }

        public bool sold = false, debt = false;

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
        {

        }
        public string DoubleToStr(string s)
        {
            if(s.IndexOf(',')> -1)
            {
                int index = s.IndexOf(',');
                string first = s.Substring(0, index);
                string last = s.Substring(index + 1);
                s = first + "." + last;
            }
            return s;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtQuantity.Text == "") return;


            //Sotilgan tovarlar qaytib kelgan bo'lsa
            if (sold == true && debt == false)
            {
                if (txtQuantity.Text.IndexOf(',') > -1)
                {
                    System.Windows.Forms.MessageBox.Show("Miqdorni qoldiq qismini nuqta bilan kiriting!", "Xatolik", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    txtQuantity.Clear();
                    return;
                }
                string str_sotilganQuantity = sold_quan;
                str_sotilganQuantity = DoubleToStr(str_sotilganQuantity);
                if (double.Parse(txtQuantity.Text, CultureInfo.InvariantCulture) > double.Parse(str_sotilganQuantity, CultureInfo.InvariantCulture))
                {
                    System.Windows.Forms.MessageBox.Show("Qaytarish miqdori ko'p kiritildi!\nSotilgan miqdor '" + sold_quan + "'", "Сообщение", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return;
                }

                double quantityQaytarish = double.Parse(txtQuantity.Text, CultureInfo.InvariantCulture);
                double dOld_price = double.Parse(DoubleToStr(sold_price), CultureInfo.InvariantCulture);
                double total = quantityQaytarish * dOld_price;
                string txtQaytarishSumma = total.ToString();
                string summa = txtQaytarishSumma; //
                summa = DoubleToStr(summa);
                //Product jadvalidan qaytarilayotgan maxsulot ma'lumotlarini olamiz
                DataTable tbProduct = new DataTable();
                string queryProduct = "select * from product where product_id='" + product_id + "'";
                objDBAccess.readDatathroughAdapter(queryProduct, tbProduct);
                string quantityProduct = tbProduct.Rows[0]["quantity"].ToString(); //
                quantityProduct = DoubleToStr(quantityProduct);
                current_price = tbProduct.Rows[0]["price"].ToString();
                barcode = tbProduct.Rows[0]["barcode"].ToString();
                tbProduct.Clear();
                tbProduct.Dispose();
                double DquantityProduct = double.Parse(quantityProduct, CultureInfo.InvariantCulture);
                double DQaytarishQuantity = double.Parse(txtQuantity.Text, CultureInfo.InvariantCulture);
                double result_quantityProduct = DquantityProduct + DQaytarishQuantity;
                string str_result_quantityProduct = result_quantityProduct.ToString(); //
                str_result_quantityProduct = DoubleToStr(str_result_quantityProduct);

                //returnproduct jadvali uchun difference
                double Dcurrent_price = double.Parse(DoubleToStr(current_price), CultureInfo.InvariantCulture);
                double Dold_price = double.Parse(DoubleToStr(sold_price), CultureInfo.InvariantCulture);
                double difference = (Dcurrent_price - Dold_price) * quantityQaytarish;
                string str_difference = difference.ToString(); //
                str_difference = DoubleToStr(str_difference);

                //Returnproduct jadvali bo'sh yoki bo'shmasligini tekshirib olamiz
                DataTable tbReturnProduct_idIs_Null = new DataTable();
                string queryReturnProduct_idIs_Null = "select id from returnproduct order by id desc limit 1";
                objDBAccess.readDatathroughAdapter(queryReturnProduct_idIs_Null, tbReturnProduct_idIs_Null);
                int returnProduct_id = 0;
                if (tbReturnProduct_idIs_Null.Rows.Count > 0) // Agar bo'sh bo'lmasa returnProduct_id +=1 bo'ladi
                { returnProduct_id = int.Parse(tbReturnProduct_idIs_Null.Rows[0]["id"].ToString()) + 1; }
                else { returnProduct_id = 1; }  // Agar bo'sh bo'lsa returnProduct_id = 1 bo'ladi
                tbReturnProduct_idIs_Null.Clear();
                tbReturnProduct_idIs_Null.Dispose();

                // Productdan quantity miqdoriga qo'shib qo'yamiz
                cmdProduct = new MySqlCommand("update product set quantity='" + str_result_quantityProduct + "' where product_id='" + product_id + "'");
                objDBAccess.executeQuery(cmdProduct);

                // return productga (id, shop_id, product_id, return_quantity, summa, date, difference) yoziladi
                DateTime dt_now = DateTime.Now;
                string val_id = "0";
                if(txtVal.Text=="uz")
                {
                    val_id = "1";
                }
                if(txtVal.Text =="$")
                {
                    val_id = "2";
                }
                cmdReturnProduct = new MySqlCommand("insert into returnproduct (id,shop_id,product_id, return_quantity, summa, val_id, sold, debt, date, difference,status_server,barcode) " +
                    "values('" + returnProduct_id + "', '" + shop_id + "', '" + product_id + "', '" + txtQuantity.Text + "', '" + summa + "','"+val_id+"',1,0, '" + dt_now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + str_difference + "',0,'" + barcode + "')");
                objDBAccess.executeQuery(cmdReturnProduct);

                
                cmdProduct.Dispose();
                cmdReturnProduct.Dispose();

                System.Windows.Forms.MessageBox.Show("Tovar muvaffaqiyatli qaytarib olindi!", "Xabar", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                Close();
            }
        }
    }
}
