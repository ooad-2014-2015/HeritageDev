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

namespace it_shop_app
{
    public partial class Detailed : PhoneApplicationPage
    {
        public Detailed()
        {
            InitializeComponent();
            Image img = new Image();
            img.Source = new BitmapImage(new Uri("/Assets/001.jpg", UriKind.Relative));
            //img.Height = 200;
            //img.Width = 200;
            img.Stretch = System.Windows.Media.Stretch.Fill;
            ContentPanel.Children.Add(img);
            img.Margin = new Thickness(10, 10, 0, 0);

            TextBlock txt = new TextBlock();
            txt.Text = "Samsung Galaxy Tab 4 \nOS: Android OS, v4.4.2 (KitKat); \nCPU: Quad-core 1.2 GHz; \nDisplay: 800 x 1280 pixels, 10.1''; \nRAM: 1.5 GB; Storage: 16 GB, microSD, up to 64 GB; \nCamera: 3.15 MP, 2048 x 1536 pixels/1.3 MP; \nWi-Fi 802.11 b/g/n; Wi-Fi Direct, Wi-Fi hotspot, Bluetooth v4.0, infrared, microUSB v2.0; \nBaterija: Non-removable Li-Po 6000 mAh do 10h(multimedia); \nBoja: crna; Garancija: 24 mjeseca. \n Cijena: 649,00 KM";
            //txt.Width = 200;
            txt.TextWrapping = TextWrapping.Wrap;
            ContentPanel.Children.Add(txt);
            txt.Margin = new Thickness(10, 10, 0, 0);

            Grid.SetRow(img, 0);
            Grid.SetRow(txt, 1);
        }
    }
}