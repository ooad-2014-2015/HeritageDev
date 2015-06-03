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
    /// Interaction logic for Prodavac.xaml
    /// </summary>
    public partial class ProdavacView : Window {
        public ProdavacView() {
            InitializeComponent();
            popuni();
            for (int i = 0; i < 100; i++) {
                txt_kategorije.Items.Add("kategorija" + (i + 1).ToString());
            }
        }

        private void popuni() {
            for (int i = 0; i < 10; i++) {
                txt_kategorije.Items.Add("kategorija" + (i + 1).ToString());
            }
            for (int i = 0; i < 200; i += 2) {
                //loadirajProizvod(i, i);
                string tempPath = System.IO.Path.GetFullPath(@"../../Resources/tmp/") + "1.png";

                //loadiraj sliku
                Image img = new Image();
                //img.Source = new BitmapImage(new Uri("C:/Users/ado/Documents/GitHub/HeritageDev/DesktopAplikacija/it_shop/Resources/tmp/1.png", UriKind.Absolute));
                img.Source = new BitmapImage(new Uri(tempPath, UriKind.RelativeOrAbsolute));
                img.Stretch = System.Windows.Media.Stretch.Fill;
                Grid.SetRow(img, i / 4);
                Grid.SetColumn(img, i % 4);
                grid_proizvodi.Children.Add(img);
                img.Margin = new Thickness(10, 10, 0, 0);

                //loadiraj opis
                TextBlock txt = new TextBlock();
                txt.Text = "OS: SuSE Linux; CPU: Intel Core i5-4210U, 1.7/2.7GHz; Display: 15.6'' HD LED; RAM: 4GB; HDD: 1TB; VGA: AMD Radeon R5 M255 2GB;";
                txt.TextWrapping = TextWrapping.Wrap;
                Grid.SetRow(txt, i / 4);
                Grid.SetColumn(txt, (i % 4) + 1);
                grid_proizvodi.Children.Add(txt);
                txt.Margin = new Thickness(10, 10, 0, 25);
                txt.FontStyle = FontStyles.Normal;


                Button btn_korpa = new Button();
                btn_korpa.Content = "Dodaj u korpu";
                //btn_korpa += dodajUKorpu(Artikal a);
                btn_korpa.Width = 100;
                btn_korpa.Height = 20;
                btn_korpa.Margin = new Thickness(0, 5, 0, 5);
                btn_korpa.HorizontalAlignment = HorizontalAlignment.Center;
                btn_korpa.VerticalAlignment = VerticalAlignment.Bottom;
                Grid.SetRow(btn_korpa, i / 4);
                Grid.SetColumn(btn_korpa, (i % 4) + 1);
                grid_proizvodi.Children.Add(btn_korpa);
            }

        }

        private void loadirajProizvod(int id_proizvoda, int i) {
            
            //downloaduje sliku
            loadImage(@"../../Resources/tmp/", id_proizvoda);

            //full path do slike
            string tempPath = System.IO.Path.GetFullPath(@"../../Resources/tmp/") + id_proizvoda.ToString() + ".png";

            //loadiraj sliku
            Image img = new Image();
            //img.Source = new BitmapImage(new Uri("C:/Users/ado/Documents/GitHub/HeritageDev/DesktopAplikacija/it_shop/Resources/tmp/1.png", UriKind.Absolute));
            img.Source = new BitmapImage(new Uri(tempPath, UriKind.RelativeOrAbsolute));
            img.Stretch = System.Windows.Media.Stretch.Fill;
            Grid.SetRow(img, i / 4);
            Grid.SetColumn(img, i % 4);
            grid_proizvodi.Children.Add(img);
            img.Margin = new Thickness(10, 10, 0, 0);

            //loadiraj opis
            TextBlock txt = new TextBlock();
            txt.Text = "";
            txt.TextWrapping = TextWrapping.Wrap;
            Grid.SetRow(txt, i / 4);
            Grid.SetColumn(txt, (i % 4) + 1);
            grid_proizvodi.Children.Add(txt);
            txt.Margin = new Thickness(10, 10, 0, 0);
            txt.FontStyle = FontStyles.Normal;

            
            Button btn_korpa = new Button();
            //btn_korpa += dodajUKorpu(Artikal a);
            btn_korpa.Width = 100;
            btn_korpa.Height = 50;
            Grid.SetRow(btn_korpa, i/4);
            Grid.SetColumn(btn_korpa, (i % 4) + 1);
            btn_korpa.Margin = new Thickness(0,0,5,5);
            btn_korpa.HorizontalAlignment = HorizontalAlignment.Right;
            btn_korpa.VerticalAlignment = VerticalAlignment.Bottom;
        }

        private void loadImage(string filePath, int id_proizvoda) {
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

            SQL = "SELECT id_proizvoda, velicina, slika FROM artikli where id_prozivoda=" + id_proizvoda.ToString() + ";";

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

                fs = new FileStream(filePath + myData.GetString("id_proizvoda") + ".png", FileMode.OpenOrCreate, FileAccess.Write);
                fs.Write(rawData, 0, (int)FileSize);
                fs.Close();

                //MessageBox.Show("File successfully written to disk!",
                //    "Success!", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                //#region test
                //BitmapImage b = new BitmapImage();
                //b.BeginInit();
                //string tmpPath = System.IO.Path.GetFullPath(filePath) + myData.GetString("ime") + ".png";
                ////MessageBox.Show(System.IO.Path.GetFullPath(filePath));
                ////b.UriSource = new Uri("C:/Users/ado/Documents/GitHub/HeritageDev/DesktopAplikacija/it_shop/Resources/1.png", UriKind.Absolute);
                //b.UriSource = new Uri(tmpPath, UriKind.RelativeOrAbsolute);

                //b.EndInit();
                //slika.Source = b;
                //#endregion

                myData.Close();
                conn.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex) {
                MessageBox.Show("Error " + ex.Number + " has occurred: " + ex.Message,
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        
    }
}
