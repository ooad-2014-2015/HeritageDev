using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace it_shop {
    /// <summary>
    /// Interaction logic for test.xaml
    /// </summary>
    public partial class test : Window {
        public test() {
            InitializeComponent();
        }

        
        private void saveImage(string filePath) {
            MySqlConnection conn;
            MySqlCommand cmd;

            conn = new MySqlConnection();
            cmd = new MySqlCommand();

            string SQL;
            int FileSize;
            byte[] rawData;
            FileStream fs;

            conn.ConnectionString = "server=192.168.1.11;uid=root;" +
                "pwd=root;database=it_shop;";

            try {
                fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                FileSize = Convert.ToInt32(fs.Length);

                rawData = new byte[FileSize];
                fs.Read(rawData, 0, FileSize);
                fs.Close();

                conn.Open();

                SQL = "INSERT INTO slika VALUES(@FileName, @FileSize, @File)";

                cmd.Connection = conn;
                cmd.CommandText = SQL;
                cmd.Parameters.AddWithValue("@FileName", "test");
                cmd.Parameters.AddWithValue("@FileSize", FileSize);
                cmd.Parameters.AddWithValue("@File", rawData);

                cmd.ExecuteNonQuery();

                MessageBox.Show("File Inserted into database successfully!",
                    "Success!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                conn.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex) {
                MessageBox.Show("Error " + ex.Number + " has occurred: " + ex.Message,
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void b_test_Click(object sender, RoutedEventArgs e) {
            saveImage(@"../../Resources/tmp/1.png");
        }

        private void loadImage(string filePath) {
            MySqlConnection conn;
            MySqlCommand cmd;
            MySqlDataReader myData;

            conn = new MySqlConnection();
            cmd = new MySqlCommand();

            string SQL;
            UInt32 FileSize;
            byte[] rawData;
            FileStream fs;

            conn.ConnectionString = "server=192.168.1.11;uid=root;" +
                "pwd=root;database=it_shop;";

            SQL = "SELECT ime, velicina, slika FROM slika where ime like 'test'";

            try {
                conn.Open();

                cmd.Connection = conn;
                cmd.CommandText = SQL;

                myData = cmd.ExecuteReader();

                if (!myData.HasRows)
                    throw new Exception("There are no BLOBs to save");

                myData.Read();

                FileSize = myData.GetUInt32(myData.GetOrdinal("velicina"));
                rawData = new byte[FileSize];

                myData.GetBytes(myData.GetOrdinal("slika"), 0, rawData, 0, (int)FileSize);

                fs = new FileStream(filePath + myData.GetString("ime") + ".png", FileMode.OpenOrCreate, FileAccess.Write);
                fs.Write(rawData, 0, (int)FileSize);
                fs.Close();

                //MessageBox.Show("File successfully written to disk!",
                //    "Success!", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                #region test
                BitmapImage b = new BitmapImage();
                b.BeginInit();
                string tmpPath = System.IO.Path.GetFullPath(filePath) + myData.GetString("ime") + ".png";
                MessageBox.Show(System.IO.Path.GetFullPath(filePath));
                //b.UriSource = new Uri("C:/Users/ado/Documents/GitHub/HeritageDev/DesktopAplikacija/it_shop/Resources/1.png", UriKind.Absolute);
                b.UriSource = new Uri(tmpPath, UriKind.RelativeOrAbsolute);
                
                b.EndInit();
                slika.Source = b;
                #endregion
                
                myData.Close();
                conn.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex) {
                MessageBox.Show("Error " + ex.Number + " has occurred: " + ex.Message,
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void b_test2_Click(object sender, RoutedEventArgs e) {
            loadImage(@"../../Resources/tmp/");
        }
    }
}
