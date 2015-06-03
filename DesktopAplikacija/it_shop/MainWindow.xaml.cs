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
using System.IO;
using System.Data.Linq;
using System.Data;


namespace it_shop {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

        }

        public void readFromDB() {
            #region stari db
            // poruka.Content = "admin";
            // string username = "root";
            // string password = "root";
            // string db = "IT_SHOP";
            // //Konekcija na bazu
            // string connectionString = "server=192.168.1.11;user=" + username + ";pwd=" + password
            //+ ";database=" + db;
            // MySqlConnection con = new MySqlConnection(connectionString);
            // //con.Open();
            // MySqlCommand upit = new MySqlCommand();
            // upit.Connection = con;
            // upit.CommandText = "SELECT * FROM ZAPOSLENICI;";
            // txt.Text = null;
            // try
            // {
            //     //open the connection
            //     con.Open();
            //     //use a DataReader to process each record
            //     MySqlDataReader msqlReader = upit.ExecuteReader();
            //     while (msqlReader.Read())
            //     {
            //         txt.Text += msqlReader.GetString(0) + '\t' + msqlReader.GetString(1) + '\t' + msqlReader.GetString(2) + '\t' + msqlReader.GetString(3) + '\t' + msqlReader.GetString(4) + '\n';
            //     }
            // }
            // catch (Exception er)
            // {
            //     txt.Text = er.ToString();
            // }
            // finally
            // {
            //     //always close the connection
            //     con.Close();
            // }
            #endregion

            #region db connect
            //define the connection reference and initialize it
            MySqlConnection msqlCon = null;
            msqlCon = new MySqlConnection("server=192.168.1.11;user=root;pwd=root;database=it_shop");
            //define the command reference
            MySqlCommand upit = new MySqlCommand();
            //define the connection used by the command object
            upit.Connection = msqlCon;
            //define the command text
            upit.CommandText = "SELECT * FROM zaposlenici;";
            txt.Text = null;
            try {
                //open the connection
                msqlCon.Open();
                //use a DataReader to process each record
                MySqlDataReader msqlReader = upit.ExecuteReader();
                while (msqlReader.Read()) {
                    for (int i = 0; i < msqlReader.FieldCount; i++)
                        txt.Text += msqlReader.GetValue(i).ToString() + " , ";
                    txt.Text += '\n';
                }
            }
            catch (Exception er) {
                //do something with the exception
            }
            finally {
                //always close the connection
                msqlCon.Close();
            }
            #endregion
        }

        public static byte[] ImageToBinary(string imagePath) {
            FileStream fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[fileStream.Length];
            fileStream.Read(buffer, 0, (int)fileStream.Length);
            fileStream.Close();
            return buffer;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            MySqlConnection msqlCon = null;
            msqlCon = new MySqlConnection("server=192.168.1.11;user=root;pwd=root;database=it_shop");
            MySqlCommand upit = new MySqlCommand();
            upit.Connection = msqlCon;
            txt.Text = null;
            try {
                msqlCon.Open();
                byte[] s = ImageToBinary("C:/Users/ado/Desktop/0.jpg");
                string slika = null;
                for (int i = 0; i < s.Length; i++) {
                    slika += s[i];
                }
                txt.Text = slika;
                poruka.Content = slika.Length;
                upit.CommandText = "UPDATE ZAPOSLENICI set IDBINARY=" + slika + " where jib = 1;";
                upit.ExecuteNonQuery();
            }
            catch (Exception er) {
                poruka.Content = er.Message.ToString();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            MySqlConnection msqlCon = null;
            msqlCon = new MySqlConnection("server=192.168.1.11;user=root;pwd=root;database=it_shop");
            MySqlCommand upit = new MySqlCommand();
            try {
                msqlCon.Open();
                upit.CommandText = "SELECT IDBinary FROM ZAPOSLENICI WHERE JIB=1";
                upit.Connection = msqlCon;
                MySqlDataReader msqlReader = upit.ExecuteReader();
                while (msqlReader.Read()) {
                    Byte[] slikaTEMP = new Byte[65536];
                    msqlReader.GetBytes(0, 0, slikaTEMP, 0, 65536);
                    BitmapImage slika = byteArrayToImage(slikaTEMP);
                    slicica.Source = slika;
                }
            }
            catch (Exception er) {
                txt.Text = er.ToString();
            }
            
        }

        public BitmapImage byteArrayToImage(byte[] byteArrayIn) {
            if (byteArrayIn == null || byteArrayIn.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(byteArrayIn)) {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
        //public static System.Drawing.Image BinaryToImage(System.Data.Linq.Binary binaryData) {
        //    if (binaryData == null) return null;

        //    byte[] buffer = binaryData.ToArray();
        //    MemoryStream memStream = new MemoryStream();
        //    memStream.Write(buffer, 0, buffer.Length);
        //    return System.Drawing.Image.FromStream(memStream);
        //}

        


    }
}
