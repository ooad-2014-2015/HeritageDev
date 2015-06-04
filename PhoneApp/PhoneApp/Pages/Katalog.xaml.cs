﻿using System;
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
        List<string> slike;
        List<string> opisi;

        public Katalog() {
            InitializeComponent();
            slike = new List<string>();
            opisi = new List<string>();
            popuni();
        }

        private void popuni() {
            List<string> slike = new List<string>();
            List<string> opisi = new List<string>();
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
            opisi.Add("OS: Android OS, v4.4.2 (KitKat); CPU: Quad-core 1.2 GHz; Display: 800 x 1280 pixels, 10.1''; RAM: 1.5 GB");
            opisi.Add("OS: SuSE Linux; CPU: Intel Core i5-4210U, 1.7/2.7GHz; Display: 15.6'' HD LED; RAM: 4GB; HDD: 1TB; VGA: AMD Radeon R5 M255 2GB;");
            opisi.Add("Dijagonala 32incha/81cm. Rezolucija 1366x768. HyperReal Engine. Mega dinamički kontrast");
            opisi.Add("OS: Android OS, v4.4.2 (KitKat); CPU: Quad-core 1.2 GHz; Display: 800 x 1280 pixels, 10.1''; RAM: 1.5 GB");
            opisi.Add("OS: SuSE Linux; CPU: Intel Core i5-4210U, 1.7/2.7GHz; Display: 15.6'' HD LED; RAM: 4GB; HDD: 1TB; VGA: AMD Radeon R5 M255 2GB;");
            opisi.Add("Dijagonala 32incha/81cm. Rezolucija 1366x768. HyperReal Engine. Mega dinamički kontrast");
            opisi.Add("OS: Android OS, v4.4.2 (KitKat); CPU: Quad-core 1.2 GHz; Display: 800 x 1280 pixels, 10.1''; RAM: 1.5 GB");
            opisi.Add("OS: SuSE Linux; CPU: Intel Core i5-4210U, 1.7/2.7GHz; Display: 15.6'' HD LED; RAM: 4GB; HDD: 1TB; VGA: AMD Radeon R5 M255 2GB;");
            opisi.Add("Dijagonala 32incha/81cm. Rezolucija 1366x768. HyperReal Engine. Mega dinamički kontrast");
            opisi.Add("OS: Android OS, v4.4.2 (KitKat); CPU: Quad-core 1.2 GHz; Display: 800 x 1280 pixels, 10.1''; RAM: 1.5 GB");

            for (int i = 0; i < 10; i++) {
                Image img = new Image();
                img.Source = new BitmapImage(new Uri(slike[i], UriKind.Relative));
                img.Height = 150;
                img.Width = 150;
                img.HorizontalAlignment = HorizontalAlignment.Left;
                img.Stretch = System.Windows.Media.Stretch.Fill;
                Grid.SetRow(img, i);
                grid_katalog.Children.Add(img);
                img.Margin = new Thickness(25, 0, 0, 0);

                TextBlock txt = new TextBlock();
                txt.Text = opisi[i];
                txt.HorizontalAlignment = HorizontalAlignment.Stretch;
                txt.TextWrapping = TextWrapping.Wrap;
                Grid.SetRow(txt, i);
                grid_katalog.Children.Add(txt);
                txt.Margin = new Thickness(185, 25, 25, 25);
            }

        }
    }
}