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
using System.Drawing;
using System.Globalization;
using System.ComponentModel;

namespace SotuvPlatformasi
{
    /// <summary>
    /// Interaction logic for tulov_click.xaml
    /// </summary>
    public partial class tulov_click : Window
    {
        public tulov_click()
        {
            InitializeComponent();
        }
        public  string tulov_som= "0", tulov_dollar="0";
        public static string debtor = "", tel_1="",xaq_som="0",xaq_dollar="0";
        public string d_id = "1";
        public static string debtor_id = "1";
        public string tul_naqd = "0", tul_karta = "0",tul_som="0",tul_dol="0", tul_utkazma = "0", tul_valtura = "0", tul_nasiya_som = "0", tul_nasiya_dollar = "0", tul_skidka = "0";
        public static bool can_client = false; 
        DBAccess objDBAccess = new DBAccess();
        public string print = "";
        public static MySqlCommand cmdShop, cmdDebtor, cmdCart, cmdDebt, cmdDebtCart, cmdShopId;
        public static DataTable tbDebtor, tbCart, tbShopId;
        public static DataTable tbNasiya, tbValyuta;
        //public static CurrencyManager managerCart, managerDebtor, managerNasiya;

        public bool naqd_tulov = false, qarz_tulov = false;
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Naqd to'lov uchun
            if(naqd_tulov==true && qarz_tulov == false)
            {
                if (tulov_som != "") { txtTulovSumma.Text = tulov_som; }
                if (tulov_dollar != "") { txtTulovDollar.Text = tulov_dollar; }
                
                if (tulov_som != "") { txtTulSom.Text = tulov_som; tul_som = tulov_som; }
                if (tulov_dollar != "") { txtTulDollar.Text = tulov_dollar; tul_dol = tulov_dollar; }

                if(debtor_id!="1")
                {
                    string queryClient = "select mijoz_fish from debtor where id='" + debtor_id + "'";
                    DataTable tbClient = new DataTable();
                    objDBAccess.readDatathroughAdapter(queryClient, tbClient);
                    txtClient.Text = tbClient.Rows[0]["mijoz_fish"].ToString();
                    tbClient.Clear();
                    tbClient.Dispose();
                    StackKlient.Visibility = Visibility.Visible;
                    AutoSumma();
                }
            }
        }

        public string shopId = "";
        public string footer = "";
        public double change_skidka = 0;
        private void txtSSumma_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        public void AutoSumma()
        {
            double Dtul_som = 0, Dtul_naqd = 0, Dtul_plastik = 0, Dtul_transfer = 0, Dtul_valSom = 0, DsSom = 0, Dtotal = 0;
            if (txtTulovDollar.Text == "0.0")
            {
                tul_som = tulov_som;
                Dtul_som = double.Parse(tul_som, CultureInfo.InvariantCulture);
                Dtul_naqd = double.Parse(tul_naqd, CultureInfo.InvariantCulture);
                Dtul_plastik = double.Parse(DoubleToStr(tul_karta), CultureInfo.InvariantCulture);
                Dtul_transfer = double.Parse(DoubleToStr(tul_utkazma), CultureInfo.InvariantCulture);
                DsSom = double.Parse(DoubleToStr(txtSSumma.Text), CultureInfo.InvariantCulture);
                if (StackCurrency.Visibility == Visibility.Visible)
                { Dtul_valSom = double.Parse(DoubleToStr(txtValSum.Text), CultureInfo.InvariantCulture); }

                Dtotal = Dtul_som - Dtul_naqd - Dtul_plastik - Dtul_transfer - DsSom - Dtul_valSom;
                tul_som = DoubleToStr(Dtotal.ToString());
            }
            else
            {
                tul_som = tulov_som;
                Dtul_som = double.Parse(tul_som, CultureInfo.InvariantCulture);
                Dtul_naqd = double.Parse(tul_naqd, CultureInfo.InvariantCulture);
                Dtul_plastik = double.Parse(DoubleToStr(tul_karta), CultureInfo.InvariantCulture);
                Dtul_transfer = double.Parse(DoubleToStr(tul_utkazma), CultureInfo.InvariantCulture);
                DsSom = double.Parse(DoubleToStr(txtSSumma.Text), CultureInfo.InvariantCulture);

                Dtotal = Dtul_som - Dtul_naqd - Dtul_plastik - Dtul_transfer - DsSom - Dtul_valSom;
                tul_som = DoubleToStr(Dtotal.ToString());

                if (StackCurrency.Visibility == Visibility.Visible)
                {
                    Dtotal = Dtul_som - Dtul_naqd - Dtul_plastik - Dtul_transfer - DsSom;
                    tul_som = DoubleToStr(Dtotal.ToString());

                    tul_dol = tulov_dollar;
                    double Dtul_dol = double.Parse(DoubleToStr(tul_dol), CultureInfo.InvariantCulture);
                    double Dtxttul_dol = double.Parse(DoubleToStr(tul_valtura), CultureInfo.InvariantCulture);
                    double DsDol = double.Parse(DoubleToStr(txtSDollar.Text), CultureInfo.InvariantCulture);
                    double Dtdol = Dtul_dol - Dtxttul_dol - DsDol;

                    tul_dol = DoubleToStr(Dtdol.ToString());
                }

            }
            if (debtor_id != "1" && txtTulovDollar.Text != "0")
            {
                txtNasiyaSom.Text = DoubleToStr(tul_som);
                txtNasiyaDollar.Text = DoubleToStr(tul_dol);
            }
            else
            {
                if(!String.IsNullOrEmpty(txtValSum.Text))
                {
                    double DValsum = double.Parse(DoubleToStr(txtValSum.Text));
                    double Dresult = double.Parse(DoubleToStr(tul_som)) - DValsum;
                    txtNasiyaSom.Text = DoubleToStr(Dresult.ToString());
                }
                
            }
        }

        private void txtNaqd_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtNaqd.Text != "")
            {
                tul_naqd = txtNaqd.Text;
            }
            else
            {
                tul_naqd = "0";
            }
            AutoSumma();
        }

        private void txtKarta_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(txtKarta.Text != "")
            {
                tul_karta = txtKarta.Text;
            }
            else
            {
                tul_karta = "0";
            }
            AutoSumma();
        }

        private void txtPulutkazish_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(txtPulutkazish.Text!="")
            {
                tul_utkazma = txtPulutkazish.Text;
            }
            else
            {
                tul_utkazma = "0";
            }
            AutoSumma();
        }

        public bool FillDebtor = false;

        private void txtSSumma_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void txtSSumma_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                
                if (txtSSumma.Text != "")
                {
                    string s = txtSSumma.Text;
                    txtSkidka.Text = "";
                    txtSSumma.Text = s;

                    tul_som = tulov_som;
                    tul_som = (double.Parse(DoubleToStr(tul_som), CultureInfo.InvariantCulture) - double.Parse(DoubleToStr(txtSSumma.Text), CultureInfo.InvariantCulture)).ToString();
                    txtTulSom.Text = DoubleToStr(tul_som);
                    
                    AutoSumma();
                    return;
                }
                if (txtSSumma.Text == "")
                {
                    txtSSumma.Text = "0";
                    txtSkidka.Text = "";

                    tul_som = tulov_som;
                    tul_som = (double.Parse(DoubleToStr(tul_som), CultureInfo.InvariantCulture) - double.Parse(DoubleToStr(txtSSumma.Text), CultureInfo.InvariantCulture)).ToString();
                    txtTulSom.Text = DoubleToStr(tul_som);

                    AutoSumma();
                    
                    return;
                }

            }
            catch(Exception) { }
        }

        public class Debtor
        {
            public string mijoz_fish { get; set; }
            public string phone1 { get; set; }
            public string phone2 { get; set; }
        }

        public List<Debtor> debtors = new List<Debtor>();
        public List<Debtor> GetDebtor()
        {
            debtors = (from DataRow dr in tbDebtor.Rows
                       select new Debtor()
                       {
                           mijoz_fish = dr["mijoz_fish"].ToString(),
                           phone1 = dr["tel_1"].ToString(),
                           phone2 = dr["tel_2"].ToString()
                       }).ToList();
            return debtors;
        }

        public void ClientRefresh()
        {
            if (debtor_id != "1")
            {
                txtClient.Text = debtor;
                StackKlient.Visibility = Visibility.Visible;
                txtXaqSom.Text = xaq_som;
                txtXaqDollar.Text = xaq_dollar;
            }
            else
            {
                txtClient.Text = debtor;
                txtXaqSom.Text = "";
                txtXaqDollar.Text = "";
                StackKlient.Visibility = Visibility.Collapsed;
            }
            
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void comboMijozFish_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void chSkidka_Click(object sender, RoutedEventArgs e)
        {
            if(chSkidka.IsChecked == true)
            {
                txtSkidka.IsEnabled = true;
                txtSkidka.Focus();
                txtNaqd.Text = "";
                txtKarta.Text = "";
                txtPulutkazish.Text = "";
                txtValSum.Text = "";
                AutoSumma();
            }

            else
            {
                txtSkidka.IsEnabled = false;
                txtSSumma.Text = "0";
                txtSkidka.Text = "";
                AutoSumma();
            }
        }

        private void txtSkidka_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtSkidka.Text != "")
                {
                    if (txtTulovSumma.Text != "0.0")
                    {
                        double DskidkaSom = double.Parse(DoubleToStr(tulov_som.ToString()), CultureInfo.InvariantCulture) * (double.Parse(DoubleToStr(txtSkidka.Text), CultureInfo.InvariantCulture) / 100);
                        txtSSumma.Text = DoubleToStr(DskidkaSom.ToString());
                        tul_som = tulov_som;
                        tul_som = (double.Parse(DoubleToStr(tul_som), CultureInfo.InvariantCulture) - double.Parse(DoubleToStr(txtSSumma.Text), CultureInfo.InvariantCulture)).ToString();
                        txtTulSom.Text = DoubleToStr(tul_som);
                    }
                    if (txtTulovDollar.Text != "0.0" && txtTulDollar.Text!="0")
                    {
                        txtSSumma.IsReadOnly = false;
                        txtSDollar.IsReadOnly = false;
                        //double DskidkaDollar = double.Parse(DoubleToStr(tulov_dollar.ToString()), CultureInfo.InvariantCulture) * (double.Parse(DoubleToStr(txtSkidka.Text), CultureInfo.InvariantCulture) / 100);
                        //txtSDollar.Text = DoubleToStr(DskidkaDollar.ToString());
                        //tul_dol = tulov_dollar;
                        //tul_dol = (double.Parse(DoubleToStr(tul_dol), CultureInfo.InvariantCulture) - double.Parse(DoubleToStr(txtSDollar.Text), CultureInfo.InvariantCulture)).ToString();
                        //txtTulDollar.Text = DoubleToStr(tul_dol);
                    }
                }
                else
                {
                    txtSSumma.Text = "0";
                    txtSDollar.Text = "0";
                    if (tulov_som != "") { tul_som = tulov_som; }
                    if (tulov_dollar != "") { tul_dol = tulov_dollar; }
                    txtTulSom.Text = DoubleToStr(tul_som);
                    txtTulDollar.Text = DoubleToStr(tul_dol);
                }
                AutoSumma();
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return;
            }
        }

        private void btnChoose_Click(object sender, RoutedEventArgs e)
        {
            ChooseClient chooseClient = new ChooseClient();
            chooseClient.ShowDialog();
            if (can_client)
            {
                ClientRefresh();
                AutoSumma();
            }
        }

        private void txtCurrency_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(txtCurrency.Text!="" && txtKurs.Text!="")
            {
                try
                {
                    double Dcurrency = double.Parse(DoubleToStr(txtCurrency.Text), CultureInfo.InvariantCulture);
                    double Dkurs = double.Parse(DoubleToStr(txtKurs.Text), CultureInfo.InvariantCulture);
                    double DSumda = Dcurrency * Dkurs;
                    txtValSum.Text = DoubleToStr(DSumda.ToString());

                    tul_valtura = txtCurrency.Text;
                    AutoSumma();
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message, "Xatolik", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                }
            }
            else
            {
                txtValSum.Text = "0";
                tul_valtura = "0";
                AutoSumma();
            }
        }

        private void comboPhone1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void ckCurreny_Click(object sender, RoutedEventArgs e)
        {
            if (ckCurreny.IsChecked == true)
            {
                string queryValyuta = "select * from valyuta"; 
                tbValyuta = new DataTable();
                objDBAccess.readDatathroughAdapter(queryValyuta, tbValyuta);
                txtKurs.Text = tbValyuta.Rows[0]["kurs"].ToString();
                StackCurrency.Visibility = Visibility.Visible;
                txtCurrency.Focus();
            }
            if (ckCurreny.IsChecked == false)
            {
                StackCurrency.Visibility = Visibility.Collapsed;
                txtValSum.Text = "";
                txtCurrency.Text = "";
            }
        }

        private void comboPhone2_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

        // BarcodeLib.Barcode barCode = new BarcodeLib.Barcode();

        /*public void CreateBarcode(string barcode)
        {
            errorProvider1.Clear();
            int nW = Convert.ToInt32(130);
            int nH = Convert.ToInt32(60);

            barCode.Alignment = BarcodeLib.AlignmentPositions.CENTER;

            BarcodeLib.TYPE type = BarcodeLib.TYPE.UNSPECIFIED;
            type = BarcodeLib.TYPE.CODE128;
            try
            {
                if (type != BarcodeLib.TYPE.UNSPECIFIED)
                {
                    barCode.IncludeLabel = true;
                    barCode.RotateFlipType = (RotateFlipType)Enum.Parse(typeof(RotateFlipType),
                        "RotateNoneFlipNone", true);
                    pictureBarcode.Image = barCode.Encode(type, barcode, Color.Black, Color.White, nW, nH);

                }
                pictureBarcode.Width = pictureBarcode.Image.Width;
                pictureBarcode.Height = pictureBarcode.Image.Height;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }*/

        //Naqd pul cheki
        //MessageBox.Show("Тўлов муваффакиятли амалга оширилди!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        print += "Тўланди :\nНакд : " + str_tulovSumma + "\n";
        //        print += "Скидка : : " + skidka + "\n";
        //        print += footer;
        //        printDocument1.Print();
        //        CreateBarcode(shopId);
        //printBarCode();

        //Plastik cheki
        //MessageBox.Show("Тўлов муваффакиятли амалга оширилди!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        print += "Тўланди :\nПластик : " + str_tulovSumma + "\n";
        //        print += "Скидка : " + skidka + "\n";
        //        print += footer;
        //        printDocument1.Print();
        //        CreateBarcode(shopId);
        //printBarCode();

        //Naasiyasiz to'lov amalga oshirilganda
        //MessageBox.Show("Тўлов муваффакиятли амалга оширилди!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        print += "Тўланди :\nНакд : " + naqd + "\n";
        //        print += "Пластик : " + plastik + "\n";
        //        print += "Pul o'tkazma : " + transfer + "\n";
        //        print += "Скидка : " + skidka + "\n";
        //        print += footer;
        //        printDocument1.Print();
        //        CreateBarcode(shopId);
        //printBarCode();
        MainWindow main = (MainWindow)Application.Current.MainWindow;

        private void BtnTulov_Click(object sender, RoutedEventArgs e)
        {
            /// To'lov nasiyasiz aralash amalga oshirilganda
            if (StackKlient.Visibility == Visibility.Collapsed)
            {
                string naqd = "0", plastik = "0", transfer = "0", skidkaSom = "0", skidkaDollar="0", currency = "0", valsum = "0";
                //Nasiya yo'q bo'lsa

                if (txtNaqd.Text.IndexOf(',') > -1 || txtKarta.Text.IndexOf(',') > -1 || txtPulutkazish.Text.IndexOf(',') > -1)
                {
                    System.Windows.Forms.MessageBox.Show("Summani tiyinini nuqtadan keyin kiriting!", "Xatolik", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    txtNaqd.Text = "";
                    txtKarta.Text = "";
                    txtPulutkazish.Text = "";
                    return;
                }
                if (StackNaqd.Visibility == Visibility.Visible && txtNaqd.Text != "")
                {
                    naqd = txtNaqd.Text;
                }

                if (StackKarta.Visibility == Visibility.Visible && txtKarta.Text != "")
                {
                    plastik = txtKarta.Text;
                }
                if (StackTransfer.Visibility == Visibility.Visible && txtPulutkazish.Text != "")
                {
                    transfer = txtPulutkazish.Text;
                }
                if (StackCurrency.Visibility == Visibility.Visible && txtValSum.Text != "" && txtKurs.Text != "" && txtCurrency.Text != "")
                {
                    currency = txtCurrency.Text; 
                    currency = DoubleToStr(currency);
                    if (!string.IsNullOrEmpty(txtValSum.Text))
                    { valsum = txtValSum.Text; }
                    valsum = DoubleToStr(valsum);
                    cmdCart = new MySqlCommand("update valyuta set kurs='" + DoubleToStr(txtKurs.Text) + "'");
                    objDBAccess.executeQuery(cmdCart);
                    cmdCart.Dispose();
                }

                if (txtSSumma.Text != "0")
                {
                    skidkaSom = txtSSumma.Text;
                    skidkaSom = DoubleToStr(skidkaSom);
                }

                //Agar dollarda maxsulot sotib olingan bo'lsa
                string jamiTulovDollar = "0";
                string jamiTulovSom = txtTulovSumma.Text;
                jamiTulovSom = DoubleToStr(jamiTulovSom);
                if (txtTulovDollar.Text !="0.0")
                {
                    try
                    {
                        skidkaDollar = txtSDollar.Text;
                        skidkaDollar = DoubleToStr(skidkaDollar);

                        if (txtCurrency.Text != "") { currency = txtCurrency.Text; }
                        currency = DoubleToStr(currency);

                        jamiTulovDollar = txtTulovDollar.Text;
                        jamiTulovDollar = DoubleToStr(jamiTulovDollar);

                        //Dollarda qilingan to'lov
                        double DskikdaDollar = double.Parse(skidkaDollar, CultureInfo.InvariantCulture);
                        double Dcurreny = double.Parse(currency, CultureInfo.InvariantCulture);
                        double DtotalDol = Dcurreny + DskikdaDollar;

                        //So'mda qilingan to'lov
                        double Dnaqd = double.Parse(naqd, CultureInfo.InvariantCulture);
                        double Dplastik = double.Parse(plastik, CultureInfo.InvariantCulture);
                        double Dtransfer = double.Parse(transfer, CultureInfo.InvariantCulture);
                        double Dskidka = double.Parse(skidkaSom, CultureInfo.InvariantCulture);
                        double DvalSum = double.Parse(valsum, CultureInfo.InvariantCulture);
                        double Dtotal = Dnaqd + Dplastik + Dtransfer + Dskidka + DvalSum;
                        if (Dtotal != double.Parse(jamiTulovSom, CultureInfo.InvariantCulture))
                        {
                            System.Windows.Forms.MessageBox.Show("To'lov summasi noto'g'ri kiritildi!", "Xatolik2", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            return;
                     
                        }
                        if(txtTulovDollar.Text!="0")
                        {
                            if(DoubleToStr(DtotalDol.ToString()) != DoubleToStr(txtTulovDollar.Text))
                            {
                                System.Windows.Forms.MessageBox.Show("To'lov summasi noto'g'ri kiritildi!", "Xatolik1", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message, "Exception", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }
                }

                // Agar to'lov faqat so'mda qilingan bo'lsa
                else 
                {
                    double Dnaqd = double.Parse(naqd, CultureInfo.InvariantCulture);
                    double Dplastik = double.Parse(plastik, CultureInfo.InvariantCulture);
                    double Dtransfer = double.Parse(transfer, CultureInfo.InvariantCulture);
                    double Dvalsum = double.Parse(valsum, CultureInfo.InvariantCulture);
                    double Dskidka = double.Parse(skidkaSom, CultureInfo.InvariantCulture);
                    double Dtotal = Dnaqd + Dplastik + Dtransfer + Dskidka + Dvalsum;

                    if (Dtotal != double.Parse(jamiTulovSom, CultureInfo.InvariantCulture))
                    {
                        System.Windows.Forms.MessageBox.Show("To'lov summasi no'to'gri kiritildi!", "Xatolik", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    } 
                }
                DateTime dt_now = DateTime.Now;
                if (StackCurrency.Visibility == Visibility.Visible)
                {
                    cmdShop = new MySqlCommand("update shop set naqd='" + naqd + "', plastik='" + plastik + "', transfer='" + transfer + "',currency='" + currency + "', total_som='" + jamiTulovSom + "', total_dollar='" + jamiTulovDollar + "',difference_som='" + DoubleToStr(skidkaSom) + "',difference_dollar='" + DoubleToStr(skidkaDollar) + "',kurs='" + DoubleToStr(txtKurs.Text) + "', status_tulov=1, date='" + dt_now.ToString("yyyy-MM-dd HH:mm:ss") + "',debtor_id='" + debtor_id + "' where id='" + shopId + "'");
                    objDBAccess.executeQuery(cmdShop);
                    cmdShop.Dispose();
                }

                if(StackCurrency.Visibility == Visibility.Collapsed)
                {
                    cmdShop = new MySqlCommand("update shop set naqd='" + naqd + "', plastik='" + plastik + "', transfer='" + transfer + "',currency='" + currency + "', total_som='" + jamiTulovSom + "', total_dollar='" + jamiTulovDollar + "',difference_som='" + DoubleToStr(skidkaSom) + "',difference_dollar='" + DoubleToStr(skidkaDollar) + "', status_tulov=1, date='" + dt_now.ToString("yyyy-MM-dd HH:mm:ss") + "',debtor_id='" + debtor_id + "' where id='" + shopId + "'");
                    objDBAccess.executeQuery(cmdShop);
                    cmdShop.Dispose();
                }
                

                string queryShopId = "select * from shopId where password='" + Kirish_ucont.password1 + "'";
                tbShopId = new DataTable();
                objDBAccess.readDatathroughAdapter(queryShopId, tbShopId);
                if (tbShopId.Rows.Count > 0)
                {
                    cmdShopId = new MySqlCommand("update shopId set shop_id=0 where password='" + Kirish_ucont.password1 + "'");
                    objDBAccess.executeQuery(cmdShopId);
                }
                else
                {
                    string queryId = "select * from shopId order by id desc limit 1";
                    DataTable tbSId = new DataTable();
                    objDBAccess.readDatathroughAdapter(queryId, tbSId);
                    if (tbSId.Rows.Count == 0)
                    {
                        cmdShopId = new MySqlCommand("insert into shopId values(1,0,'" + Kirish_ucont.mac_address + "','" + Kirish_ucont.password1 + "')");
                        objDBAccess.executeQuery(cmdShopId);
                    }
                    else
                    {
                        cmdShopId = new MySqlCommand("insert into shopId (shop_id,mac_address,password) values(0,'" + Kirish_ucont.mac_address + "','" + Kirish_ucont.password1 + "')");
                        objDBAccess.executeQuery(cmdShopId);
                    }
                    try
                    {
                        tbSId.Clear();
                        tbSId.Dispose();
                    }
                    catch (Exception) { }
                }
                tbShopId.Clear();
                tbShopId.Dispose();
                Tul_qab_ucont.shopId = Sotuv_ucont.shopID;
                Sotuv_ucont.shopID = 0;
                Sotuv_ucont.shop = false;
                Sotuv_ucont.debtor_id = "1";
                print += "=============================";
                print += "To'landi : \n";
                if (naqd != "0") { print += "Naqd : " + naqd + "\n"; }
                if (plastik != "0") { print += "Plastik : " + plastik + "\n"; }
                if (transfer != "0") { print += "Pul o'tkazma : " + transfer + "\n"; }
                if (currency != "0") { print += "Dollar : " + currency + "\n"; }
                if (skidkaSom != "0") { print += "Skidka uz: " + skidkaSom + "\n"; }
                if (skidkaDollar != "0") { print += "Skidka $: " + skidkaDollar + "\n"; }
                print += "\n" + footer;
                Tul_qab_ucont.print = print;
                Tul_qab_ucont.p_Date = DateTime.Now.ToString("yyyy-MM-dd");
                Tul_qab_ucont.pKlient = txtClient.Text;
                Tul_qab_ucont.pTel = tel_1;
                Tul_qab_ucont.pQarz_som = xaq_som;
                Tul_qab_ucont.pQarz_dollar = xaq_dollar;
                Tul_qab_ucont.pResult_Qarz_Som = xaq_som;
                Tul_qab_ucont.pResult_Qarz_Dollar = xaq_dollar;
                Tul_qab_ucont.pJami_Som = txtTulovSumma.Text;
                Tul_qab_ucont.pJami_Dollar = txtTulovDollar.Text;
                Tul_qab_ucont.pSkidka_som = txtSSumma.Text;
                Tul_qab_ucont.pSkidka_dollar = txtSDollar.Text;
                Tul_qab_ucont.pTulov_Som = txtTulSom.Text;
                Tul_qab_ucont.pTulov_Dollar = txtTulDollar.Text;
                if (txtSkidka.Text != "") { Tul_qab_ucont.pSkidka_foiz = txtSkidka.Text; }
                else { Tul_qab_ucont.pSkidka_foiz = "0"; }

                this.Close();
                main.TabMenu.SelectedIndex = 12;
            }

            /// To'lov nasiya aralash amalga oshirilganda
            if (StackKlient.Visibility == Visibility.Visible)
            {
                string naqd = "0", plastik = "0", transfer = "0", nasiyaSom = "0", nasiyaDollar="0", skidkaSom = "0", skidkaDollar="0", currency = "0", valsum = "0";
                if (dateTimePicker1.SelectedDate.ToString() == "")
                {
                    System.Windows.Forms.MessageBox.Show("Qaytarish sanasini belgilang!", "Xatolik", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return;
                }
                
                if (txtNaqd.Text.IndexOf(',') > -1 || txtKarta.Text.IndexOf(',') > -1) // Agar naqd pul , bilan kiritilsa
                {
                    System.Windows.Forms.MessageBox.Show("Summani tiyinini nuqtadan keyin kiriting!", "Xatolik", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    txtNaqd.Text = "";
                    return;
                }

                if (txtNaqd.Text != "")
                {
                    naqd = txtNaqd.Text;
                }

                if (txtKarta.Text != "")
                {
                    plastik = txtKarta.Text;
                }

                if (txtPulutkazish.Text != "")
                {
                    transfer = txtPulutkazish.Text;
                }
                if (StackCurrency.Visibility == Visibility.Visible && txtKurs.Text != "" && txtValSum.Text != "" && txtCurrency.Text != "")
                {
                    currency = txtCurrency.Text;
                    currency = DoubleToStr(currency);
                    if (!String.IsNullOrEmpty(txtValSum.Text))
                    { valsum = txtValSum.Text; }
                    valsum = DoubleToStr(valsum);
                    cmdCart = new MySqlCommand("update valyuta set kurs='" + DoubleToStr(txtKurs.Text) + "'");
                    objDBAccess.executeQuery(cmdCart);
                    cmdCart.Dispose();
                }
                if (txtSSumma.Text != "0")
                {
                    skidkaSom = txtSSumma.Text;
                    skidkaSom = DoubleToStr(skidkaSom);
                }

                if (txtNasiyaSom.Text != "")
                {
                    nasiyaSom = txtNasiyaSom.Text;
                    nasiyaSom = DoubleToStr(nasiyaSom);
                }

                if(txtNasiyaDollar.Text !="")
                {
                    nasiyaDollar = txtNasiyaDollar.Text;
                    nasiyaDollar = DoubleToStr(nasiyaDollar);
                }

                string jamiTulovSom = txtTulovSumma.Text;
                jamiTulovSom = DoubleToStr(jamiTulovSom);

                string jamiTulovDollar = "0";
                if(txtTulovDollar.Text != "0.0")
                {
                    try
                    {
                        skidkaDollar = txtSDollar.Text;
                        skidkaDollar = DoubleToStr(skidkaDollar);

                        if (txtCurrency.Text != "") { currency = txtCurrency.Text; }
                        currency = DoubleToStr(currency);

                        jamiTulovDollar = txtTulovDollar.Text;
                        jamiTulovDollar = DoubleToStr(jamiTulovDollar);

                        //Dollarda qilingan to'lov
                        double DskikdaDollar = double.Parse(skidkaDollar, CultureInfo.InvariantCulture);
                        double Dcurreny = double.Parse(currency, CultureInfo.InvariantCulture);
                        double DnasiyaDollar = 0;
                        if (txtNasiyaDollar.Text != "") { DnasiyaDollar = double.Parse(DoubleToStr(txtNasiyaDollar.Text), CultureInfo.InvariantCulture); }
                        double DtotalDol = Dcurreny + DskikdaDollar + DnasiyaDollar;

                        //So'mda qilingan to'lov
                        double Dnaqd = double.Parse(naqd, CultureInfo.InvariantCulture);
                        double Dplastik = double.Parse(plastik, CultureInfo.InvariantCulture);
                        double Dtransfer = double.Parse(transfer, CultureInfo.InvariantCulture);
                        double Dskidka = double.Parse(skidkaSom, CultureInfo.InvariantCulture);
                        double DvalSum = double.Parse(valsum, CultureInfo.InvariantCulture);
                        double DnasiyaSom = 0;
                        if (txtNasiyaSom.Text != "") { DnasiyaSom = double.Parse(DoubleToStr(txtNasiyaSom.Text), CultureInfo.InvariantCulture); }
                        double Dtotal = Dnaqd + Dplastik + Dtransfer + Dskidka + DnasiyaSom + DvalSum;
                        if (Dtotal != double.Parse(jamiTulovSom, CultureInfo.InvariantCulture) )
                        {
                            System.Windows.Forms.MessageBox.Show("To'lov summasi noto'g'ri kiritildi!", "Xatolik", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            return;
                        }

                        if(txtTulovDollar.Text!="0")
                        {
                            if(DtotalDol != double.Parse(jamiTulovDollar, CultureInfo.InvariantCulture))
                            {
                                System.Windows.Forms.MessageBox.Show("To'lov summasi noto'g'ri kiritildi!", "Xatolik", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message, "Exception", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    //So'mda qilingan to'lov
                    double Dnaqd = double.Parse(naqd, CultureInfo.InvariantCulture);
                    double Dplastik = double.Parse(plastik, CultureInfo.InvariantCulture);
                    double Dtransfer = double.Parse(transfer, CultureInfo.InvariantCulture);
                    double DvalSum = double.Parse(valsum, CultureInfo.InvariantCulture);
                    double Dskidka = double.Parse(skidkaSom, CultureInfo.InvariantCulture);
                    double DnasiyaSom = 0;
                    if (txtNasiyaSom.Text != "") { DnasiyaSom = double.Parse(DoubleToStr(txtNasiyaSom.Text), CultureInfo.InvariantCulture); }
                    double Dtotal = Dnaqd + Dplastik + Dtransfer + Dskidka + DvalSum + DnasiyaSom;

                    if (Dtotal!= double.Parse(jamiTulovSom, CultureInfo.InvariantCulture))
                    {
                        System.Windows.Forms.MessageBox.Show("To'lov summasi noto'g'ri kiritildi!", "Xatolik", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return;
                    }
                }
                
                //Agar comboBox larning visibility = visible bo'lsa
                
                //debt jadvali bo'sh yoki bo'shmasligini tekshiramiz
                int Debt_id = 0;
                string queryDebt = "select * from debt order by id desc limit 1";
                DataTable tbDebt = new DataTable();
                objDBAccess.readDatathroughAdapter(queryDebt, tbDebt);
                if (tbDebt.Rows.Count > 0) //Agar bo'sh bo'lmasa debt_id+=1 bo'ladi
                {
                    Debt_id = int.Parse(tbDebt.Rows[0]["id"].ToString()) + 1;
                }
                else { Debt_id = 1; } // Agar bo'sh bo'lsa debt_id = 1 bo'ladi
                tbDebt.Clear();
                tbDebt.Dispose();

                //debt jadvaliga debtor_id, shop_id, return_date larni yozamiz
                string Debtor_id = debtor_id;
                string date_now = "";
                date_now = dateTimePicker1.SelectedDate.Value.Date.ToString("yyyy-MM-dd");
                //if (dateTimePicker1.Text != "")
                //{
                //    string date = dateTimePicker1.SelectedDate.ToString();
                //    int index = date.IndexOf("0:");
                //    date = date.Substring(0, index);

                //    string day = date.Substring(0, 2);
                //    string month = date.Substring(3, 2);
                //    string year = date.Substring(6, 4);

                //    date = year + "-" + month + "-" + day;
                //    date_now = date;
                //}

                cmdDebt = new MySqlCommand("insert into debt (id,debtor_id, shop_id, return_date) values('" + Debt_id + "','" + Debtor_id + "', '" + shopId + "','" + date_now + "' )");
                objDBAccess.executeQuery(cmdDebt);
                cmdDebt.Dispose();



                //shopni naqd, plastik, nasiya, jamisumma, status_tulov, debt ustunlarini update qilamiz
                DateTime dt_now = DateTime.Now;
                if (StackCurrency.Visibility == Visibility.Visible)
                {
                    cmdShop = new MySqlCommand("update shop set naqd='" + naqd + "', plastik='" + plastik + "', transfer='" + transfer + "',currency='" + currency + "',kurs='" + DoubleToStr(txtKurs.Text) + "', nasiya_som='" + nasiyaSom + "',nasiya_dollar='" + nasiyaDollar + "', total_som='" + jamiTulovSom + "', total_dollar='" + jamiTulovDollar + "',difference_som='" + DoubleToStr(skidkaSom) + "',difference_dollar='" + DoubleToStr(skidkaDollar) + "', status_tulov=1, debt=1, date='" + dt_now.ToString("yyyy-MM-dd HH:mm:ss") + "',debtor_id='" + debtor_id + "' where id='" + shopId + "'");
                    objDBAccess.executeQuery(cmdShop);
                    cmdShop.Dispose();
                }

                if(StackCurrency.Visibility == Visibility.Collapsed)
                {
                    cmdShop = new MySqlCommand("update shop set naqd='" + naqd + "', plastik='" + plastik + "', transfer='" + transfer + "',currency='" + currency + "', nasiya_som='" + nasiyaSom + "',nasiya_dollar='" + nasiyaDollar + "', total_som='" + jamiTulovSom + "', total_dollar='" + jamiTulovDollar + "',difference_som='" + DoubleToStr(skidkaSom) + "',difference_dollar='" + DoubleToStr(skidkaDollar) + "', status_tulov=1, debt=1, date='" + dt_now.ToString("yyyy-MM-dd HH:mm:ss") + "',debtor_id='" + debtor_id + "' where id='" + shopId + "'");
                    objDBAccess.executeQuery(cmdShop);
                    cmdShop.Dispose();
                }
                //umumiy_qarz debtor jadvaliga yozilishi kerek  

                string old_somQarz = xaq_som;
                double Dold_somQarz = double.Parse(old_somQarz, CultureInfo.InvariantCulture);
                string str_somNasiya = txtNasiyaSom.Text;
                str_somNasiya = DoubleToStr(str_somNasiya);
                double DsomNasiya = double.Parse(str_somNasiya, CultureInfo.InvariantCulture);

                double result_somQarz = Dold_somQarz + DsomNasiya;
                string str_result_somQarz = result_somQarz.ToString();
                str_result_somQarz = DoubleToStr(str_result_somQarz);

                string old_dolQarz = xaq_dollar;
                double Dold_dolQarz = double.Parse(old_dolQarz, CultureInfo.InvariantCulture);
                string str_dolNasiya = "0";
                if (!String.IsNullOrEmpty(txtNasiyaDollar.Text))
                {
                    str_dolNasiya = txtNasiyaDollar.Text;
                    str_dolNasiya = DoubleToStr(str_dolNasiya);
                }
                double DdolNasiya = double.Parse(str_dolNasiya, CultureInfo.InvariantCulture);

                double result_dolQarz = Dold_dolQarz + DdolNasiya;
                string str_result_dolQarz = result_dolQarz.ToString();
                str_result_dolQarz = DoubleToStr(str_result_dolQarz);
                cmdDebtor = new MySqlCommand("update debtor set qarz_som='" + str_result_somQarz + "', qarz_dollar='"+str_result_dolQarz+"' where id='" + Debtor_id + "'");
                objDBAccess.executeQuery(cmdDebtor);
                cmdDebtor.Dispose();

                
                string queryShopId = "select * from shopId where password='" + Kirish_ucont.password1 + "'";
                tbShopId = new DataTable();
                objDBAccess.readDatathroughAdapter(queryShopId, tbShopId);
                if (tbShopId.Rows.Count > 0)
                {
                    cmdShopId = new MySqlCommand("update shopId set shop_id=0 where password='" + Kirish_ucont.password1 + "'");
                    objDBAccess.executeQuery(cmdShopId);
                }
                else
                {
                    string queryId = "select * from shopId order by id desc limit 1";
                    DataTable tbSId = new DataTable();
                    objDBAccess.readDatathroughAdapter(queryId, tbSId);
                    if (tbSId.Rows.Count == 0)
                    {
                        cmdShopId = new MySqlCommand("insert into shopId values(1,0,'" + Kirish_ucont.mac_address + "','" + Kirish_ucont.password1 + "')");
                        objDBAccess.executeQuery(cmdShopId);
                    }
                    else
                    {
                        cmdShopId = new MySqlCommand("insert into shopId (shop_id,mac_address,password) values(0,'" + Kirish_ucont.mac_address + "','" + Kirish_ucont.password1 + "')");
                        objDBAccess.executeQuery(cmdShopId);
                    }
                    try
                    {
                        tbSId.Clear();
                        tbSId.Dispose();
                    }
                    catch (Exception) { }
                }
                tbShopId.Clear();
                tbShopId.Dispose();
                Tul_qab_ucont.shopId = Sotuv_ucont.shopID;
                Sotuv_ucont.shopID = 0;
                Sotuv_ucont.shop = false;
                Sotuv_ucont.debtor_id = "1";
                print += "To'landi : \n";
                if (naqd != "0") { print += "Naqd : " + naqd + "\n"; }
                if (plastik != "0") { print += "Plastik : " + plastik + "\n"; }
                if (transfer != "0") { print += "Pul o'tkazma : " + transfer + "\n"; }
                if (currency != "0") { print += "Dollar : " + currency + "\n"; }
                if (nasiyaSom != "0") { print += "Nasiya uz: " + nasiyaSom + "\n"; }
                if (nasiyaDollar != "0") { print += "Nasiya $: " + nasiyaDollar + "\n"; }
                if (skidkaSom != "0") { print += "Skidka uz: " + skidkaSom + "\n"; }
                if (skidkaDollar != "0") { print += "Skidka $: " + skidkaDollar + "\n"; }
                print += "\n" + footer;
                Tul_qab_ucont.print = print;
                Tul_qab_ucont.print = print;
                Tul_qab_ucont.p_Date = DateTime.Now.ToString("dd MMMM yyyy");
                Tul_qab_ucont.pKlient = txtClient.Text;
                Tul_qab_ucont.pTel = tel_1;
                Tul_qab_ucont.pQarz_som = xaq_som;
                Tul_qab_ucont.pQarz_dollar = xaq_dollar;

                Tul_qab_ucont.pResult_Qarz_Som = str_result_somQarz;
                Tul_qab_ucont.pResult_Qarz_Dollar = str_result_dolQarz;

                Tul_qab_ucont.pJami_Som = txtTulovSumma.Text;
                Tul_qab_ucont.pJami_Dollar = txtTulovDollar.Text;
                Tul_qab_ucont.pSkidka_som = txtSSumma.Text;
                Tul_qab_ucont.pSkidka_dollar = txtSDollar.Text;
                Tul_qab_ucont.pTulov_Som = txtTulSom.Text;
                Tul_qab_ucont.pTulov_Dollar = txtTulDollar.Text;
                if (txtSkidka.Text != "") { Tul_qab_ucont.pSkidka_foiz = txtSkidka.Text; }
                else { Tul_qab_ucont.pSkidka_foiz = "0"; }
                this.Close();
                main.TabMenu.SelectedIndex = 12;
            }
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape) 
            {
                this.Close();
            }
            
        }

        private void chkKarta_Click(object sender, RoutedEventArgs e)
        {
            if(chkKarta.IsChecked == true)
            {
                StackKarta.Visibility = Visibility.Visible;
                txtKarta.Focus();
            }
            if (chkKarta.IsChecked == false)
            {
                StackKarta.Visibility = Visibility.Collapsed;
                txtKarta.Text = "";
            }

        }

        private void ckTransfer_Click(object sender, RoutedEventArgs e)
        {
            if (ckTransfer.IsChecked == true)
            {
                StackTransfer.Visibility = Visibility.Visible;
                txtPulutkazish.Focus();
            }
            if (ckTransfer.IsChecked == false)
            {
                StackTransfer.Visibility = Visibility.Collapsed;
                txtPulutkazish.Text = "";
            }
        }

        private void ckNaqd_Click(object sender, RoutedEventArgs e)
        {
            if (ckNaqd.IsChecked == true)
            {
                StackNaqd.Visibility = Visibility.Visible;
                txtNaqd.Focus();
            }
            if (ckNaqd.IsChecked == false)
            {
                StackNaqd.Visibility = Visibility.Collapsed;
                txtNaqd.Text = "";
            }
        }
    }
}
