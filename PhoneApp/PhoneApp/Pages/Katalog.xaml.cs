using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;

namespace PhoneApp {
    public partial class Katalog : PhoneApplicationPage {
        static List<string> slike;
        static List<string> opisi;
        static int stranica;
        int index;

        public Katalog() {
            InitializeComponent();

            if (stranica == 0) {
                slike = new List<string>();
                opisi = new List<string>();
                index = 0;
                ucitaj();
                btn_prethodna.Visibility = Visibility.Collapsed;
                btn_sljedeca.Margin = new Thickness(0, 0, 0, 0);
                //MessageBox.Show("loadiranje");
            }
            else {
                btn_prethodna.Visibility = Visibility.Visible;
                btn_sljedeca.Margin = new Thickness(200, 0, 0, 0);
            }
            pobrisi(); 
            popuni();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);
            string msg = "";
            if (NavigationContext.QueryString.TryGetValue("msg", out msg))
                stranica = int.Parse(msg);
            //MessageBox.Show("pozvana-" + msg + "-" + "itema:" + slike.Count.ToString() + "-index:" + index.ToString() + "-");
        }

        private void ucitaj() {
            slike.Add("/Resources/001.jpg");
            slike.Add("/Resources/002.jpg");
            slike.Add("/Resources/003.png");
            slike.Add("/Resources/001.jpg");
            slike.Add("/Resources/002.jpg");
            slike.Add("/Resources/003.png");
            slike.Add("/Resources/001.jpg");
            slike.Add("/Resources/002.jpg");
            slike.Add("/Resources/003.png");
            slike.Add("/Resources/001.jpg");
            slike.Add("/Resources/002.jpg");
            slike.Add("/Resources/003.png");
            slike.Add("/Resources/001.jpg");
            slike.Add("/Resources/002.jpg");
            slike.Add("/Resources/003.png");
            slike.Add("/Resources/001.jpg");
            slike.Add("/Resources/002.jpg");
            slike.Add("/Resources/003.png");
            slike.Add("/Resources/001.jpg");
            slike.Add("/Resources/003.png");
            slike.Add("/Resources/001.jpg");
            opisi.Add("RAM: 1.5 GB");
            opisi.Add("OS: SuSE Linux; CPU: Intel Core i5-4210U, 1.7/2.7GHz; Display: 15.6'' HD LED; RAM: 4GB; HDD: 1TB; VGA: AMD Radeon R5 M255 2GB;");
            opisi.Add("Dijagonala 32incha/81cm. Rezolucija 1366x768. HyperReal Engine. Mega dinamički kontrast");
            opisi.Add("OS: Android OS, v4.4.2 (KitKat); CPU: Quad-core 1.2 GHz; Display: 800 x 1280 pixels, 10.1''; RAM: 1.5 GB");
            opisi.Add("OS: SuSE Linux; CPU: Intel Core i5-4210U, 1.7/2.7GHz; Display: 15.6'' HD LED; RAM: 4GB; HDD: 1TB; VGA: AMD Radeon R5 M255 2GB;");
            opisi.Add("Dijagonala 32incha/81cm. Rezolucija 1366x768. HyperReal Engine. Mega dinamički kontrast");
            opisi.Add("OS: Android OS, v4.4.2 (KitKat); CPU: Quad-core 1.2 GHz; Display: 800 x 1280 pixels, 10.1''; RAM: 1.5 GB");
            opisi.Add("OS: SuSE Linux; CPU: Intel Core i5-4210U, 1.7/2.7GHz; Display: 15.6'' HD LED; RAM: 4GB; HDD: 1TB; VGA: AMD Radeon R5 M255 2GB;");
            opisi.Add("Dijagonala 32incha/81cm. Rezolucija 1366x768. HyperReal Engine. Mega dinamički kontrast");
            opisi.Add("OS: ");
            opisi.Add("OS: HDD: 1TB; VGA: AMD Radeon R5 M255 2GB;");
            opisi.Add("Dijagonala 32incha/81cm. Rezolucija 1366x768. HyperReal Engine. Mega dinamički kontrast");
            opisi.Add("OS: Android OS, v4.4.2 (KitKat); CPU: Quad-core 1.2 GHz; Display: 800 x 1280 pixels, 10.1''; RAM: 1.5 GB");
            opisi.Add("OS: SuSE Linux; CPU: Intel Core i5-4210U, 1.7/2.7GHz; Display: 15.6'' HD LED; RAM: 4GB; HDD: 1TB; VGA: AMD Radeon R5 M255 2GB;");
            opisi.Add("Dijagonala 32incha/81cm. Rezolucija 1366x768. HyperReal Engine. Mega dinamički kontrast");
            opisi.Add("OS: Android OS, v4.4.2 (KitKat); CPU: Quad-core 1.2 GHz; Display: 800 x 1280 pixels, 10.1''; RAM: 1.5 GB");
            opisi.Add("OS: SuSE Linux; CPU: Intel Core i5-4210U, 1.7/2.7GHz; Display: 15.6'' HD LED; RAM: 4GB; HDD: 1TB; VGA: AMD Radeon R5 M255 2GB;");
            opisi.Add("Dijagonala 32incha/81cm. Rezolucija 1366x768. HyperReal Engine. Mega dinamički kontrast");
            opisi.Add("OS: Android OS, v4.4.2 (KitKat); CPU: Quad-core 1.2 GHz; Display: 800 x 1280 pixels, 10.1''; RAM: 1.5 GB");
            opisi.Add("Dijagonala 32incha/81cm. Rezolucija 1366x768. HyperReal Engine. Mega dinamički kontrast");
            opisi.Add("OS: Android OS, v4.4.2 (KitKat); CPU: Quad-core 1.2 GHz; Display: 800 x 1280 pixels, 10.1''; RAM: 1.5 GB");
        }

        private void popuni() {
            //pobrisi();
            for (int i = stranica * 10; i < stranica * 10 + 10 && i < slike.Count; i++) {
                Grid g = grid_katalog.FindName("g_" + ((i % 10) + 1).ToString()) as Grid;

                //punjenje slike
                Image img = new Image();
                img.Source = new BitmapImage(new Uri(slike[i], UriKind.Relative));
                img.Height = 150;
                img.Width = 150;
                img.HorizontalAlignment = HorizontalAlignment.Left;
                img.Stretch = System.Windows.Media.Stretch.Fill;
                //Grid.SetRow(img, i % 10);
                Grid.SetColumn(img, 0);
                //grid_katalog.Children.Add(img);
                g.Children.Add(img);
                img.Margin = new Thickness(0, 0, 0, 0);

                //punjenje teksta
                TextBlock txt = new TextBlock();
                txt.Text = opisi[i];
                txt.HorizontalAlignment = HorizontalAlignment.Stretch;
                txt.TextWrapping = TextWrapping.Wrap;
                //Grid.SetRow(txt, i % 10);
                Grid.SetColumn(txt, 1);
                //grid_katalog.Children.Add(txt);
                g.Children.Add(txt);
                txt.Margin = new Thickness(0, 0, 0, 0);
                index++;
            }

            //skrivanje preostalih buttona
            if (index < 10) {
                int t_index = index;
                while (t_index < 10) {
                    string imeButtona = "btn_" + (t_index+1).ToString();
                        Button t = grid_katalog.FindName(imeButtona) as Button;
                        t.Visibility = Visibility.Collapsed;
                        t_index++;
                }
                btn_sljedeca.Visibility = Visibility.Collapsed;
                btn_prethodna.Margin = new Thickness(0, 0, 0, 0);
            }
        }

        private void btn_sljedeca_Tap(object sender, System.Windows.Input.GestureEventArgs e) {
            stranica++;
            NavigationService.Navigate(new Uri("/Pages/Katalog.xaml?msg=" + stranica.ToString(), UriKind.Relative));
        }

        private void pobrisi() {
            for (int i = 0; i < grid_katalog.Children.Count; i++) {
                if (grid_katalog.Children[i] is TextBlock || grid_katalog.Children[i] is Image) {
                    grid_katalog.Children.Remove(grid_katalog.Children[i]);
                    i--;
                }
            }
        }

        private void btn_prethodna_Tap(object sender, System.Windows.Input.GestureEventArgs e) {
            NavigationService.GoBack();
            stranica--;
        }

        private void btn_1_Tap(object sender, System.Windows.Input.GestureEventArgs e) {
            NavigationService.Navigate(new Uri("/Pages/DetaljniPrikaz.xaml?msg=" + (stranica * 10).ToString(), UriKind.Relative));
        }
        private void btn_2_Tap(object sender, System.Windows.Input.GestureEventArgs e) {
            NavigationService.Navigate(new Uri("/Pages/DetaljniPrikaz.xaml?msg=" + ((stranica * 10) + 1).ToString(), UriKind.Relative));
        }
        private void btn_3_Tap(object sender, System.Windows.Input.GestureEventArgs e) {
            NavigationService.Navigate(new Uri("/Pages/DetaljniPrikaz.xaml?msg=" + ((stranica * 10) + 2).ToString(), UriKind.Relative));
        }
        private void btn_4_Tap(object sender, System.Windows.Input.GestureEventArgs e) {
            NavigationService.Navigate(new Uri("/Pages/DetaljniPrikaz.xaml?msg=" + ((stranica * 10) + 3).ToString(), UriKind.Relative));
        }
        private void btn_5_Tap(object sender, System.Windows.Input.GestureEventArgs e) {
            NavigationService.Navigate(new Uri("/Pages/DetaljniPrikaz.xaml?msg=" + ((stranica * 10) + 4).ToString(), UriKind.Relative));
        }
        private void btn_6_Tap(object sender, System.Windows.Input.GestureEventArgs e) {
            NavigationService.Navigate(new Uri("/Pages/DetaljniPrikaz.xaml?msg=" + ((stranica * 10) + 5).ToString(), UriKind.Relative));
        }
        private void btn_7_Tap(object sender, System.Windows.Input.GestureEventArgs e) {
            NavigationService.Navigate(new Uri("/Pages/DetaljniPrikaz.xaml?msg=" + ((stranica * 10) + 6).ToString(), UriKind.Relative));
        }
        private void btn_8_Tap(object sender, System.Windows.Input.GestureEventArgs e) {
            NavigationService.Navigate(new Uri("/Pages/DetaljniPrikaz.xaml?msg=" + ((stranica * 10) + 7).ToString(), UriKind.Relative));
        }
        private void btn_9_Tap(object sender, System.Windows.Input.GestureEventArgs e) {
            NavigationService.Navigate(new Uri("/Pages/DetaljniPrikaz.xaml?msg=" + ((stranica * 10) + 8).ToString(), UriKind.Relative));
        }
        private void btn_10_Tap(object sender, System.Windows.Input.GestureEventArgs e) {
            NavigationService.Navigate(new Uri("/Pages/DetaljniPrikaz.xaml?msg=" + ((stranica * 10) + 9).ToString(), UriKind.Relative));
        }
    }
}