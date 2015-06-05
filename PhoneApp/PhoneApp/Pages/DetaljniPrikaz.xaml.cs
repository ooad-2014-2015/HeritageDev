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

namespace PhoneApp.Pages {
    public partial class DetaljniPrikaz : PhoneApplicationPage {
        private List<string> slike;
        private List<string> opisi;
        public DetaljniPrikaz() {
            InitializeComponent();
            slike = new List<string>();
            opisi = new List<string>();
            ucitaj();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);
            string msg = "";
            int i = 0;
            if (NavigationContext.QueryString.TryGetValue("msg", out msg))
                i = int.Parse(msg);
            loadiraj(i);
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
            slike.Add("/Resources/002.jpg");
            slike.Add("/Resources/003.png");
            opisi.Add("OS: Android OS, v4.4.2 (KitKat); CPU: Quad-core 1.2 GHz; Display: 800 x 1280 pixels, 10.1''; RAM: 1.5 GB");
            opisi.Add("Dijagonala 32incha/81cm. Rezolucija 1366x768. HyperReal Engine. Mega dinamički kontrast");
            opisi.Add("OS: SuSE Linux; CPU: Intel Core i5-4210U, 1.7/2.7GHz; Display: 15.6'' HD LED; RAM: 4GB; HDD: 1TB; VGA: AMD Radeon R5 M255 2GB;");
            opisi.Add("OS: Android OS, v4.4.2 (KitKat); CPU: Quad-core 1.2 GHz; Display: 800 x 1280 pixels, 10.1''; RAM: 1.5 GB");
            opisi.Add("Dijagonala 32incha/81cm. Rezolucija 1366x768. HyperReal Engine. Mega dinamički kontrast");
            opisi.Add("OS: SuSE Linux; CPU: Intel Core i5-4210U, 1.7/2.7GHz; Display: 15.6'' HD LED; RAM: 4GB; HDD: 1TB; VGA: AMD Radeon R5 M255 2GB;");
            opisi.Add("OS: Android OS, v4.4.2 (KitKat); CPU: Quad-core 1.2 GHz; Display: 800 x 1280 pixels, 10.1''; RAM: 1.5 GB");
            opisi.Add("Dijagonala 32incha/81cm. Rezolucija 1366x768. HyperReal Engine. Mega dinamički kontrast");
            opisi.Add("OS: SuSE Linux; CPU: Intel Core i5-4210U, 1.7/2.7GHz; Display: 15.6'' HD LED; RAM: 4GB; HDD: 1TB; VGA: AMD Radeon R5 M255 2GB;");
            opisi.Add("OS: Android OS, v4.4.2 (KitKat); CPU: Quad-core 1.2 GHz; Display: 800 x 1280 pixels, 10.1''; RAM: 1.5 GB");
            opisi.Add("Dijagonala 32incha/81cm. Rezolucija 1366x768. HyperReal Engine. Mega dinamički kontrast");
            opisi.Add("OS: SuSE Linux; CPU: Intel Core i5-4210U, 1.7/2.7GHz; Display: 15.6'' HD LED; RAM: 4GB; HDD: 1TB; VGA: AMD Radeon R5 M255 2GB;");
            opisi.Add("OS: Android OS, v4.4.2 (KitKat); CPU: Quad-core 1.2 GHz; Display: 800 x 1280 pixels, 10.1''; RAM: 1.5 GB");
            opisi.Add("Dijagonala 32incha/81cm. Rezolucija 1366x768. HyperReal Engine. Mega dinamički kontrast");
            opisi.Add("OS: SuSE Linux; CPU: Intel Core i5-4210U, 1.7/2.7GHz; Display: 15.6'' HD LED; RAM: 4GB; HDD: 1TB; VGA: AMD Radeon R5 M255 2GB;");
            opisi.Add("OS: Android OS, v4.4.2 (KitKat); CPU: Quad-core 1.2 GHz; Display: 800 x 1280 pixels, 10.1''; RAM: 1.5 GB");
            opisi.Add("Dijagonala 32incha/81cm. Rezolucija 1366x768. HyperReal Engine. Mega dinamički kontrast");
            opisi.Add("OS: SuSE Linux; CPU: Intel Core i5-4210U, 1.7/2.7GHz; Display: 15.6'' HD LED; RAM: 4GB; HDD: 1TB; VGA: AMD Radeon R5 M255 2GB;");
            opisi.Add("OS: Android OS, v4.4.2 (KitKat); CPU: Quad-core 1.2 GHz; Display: 800 x 1280 pixels, 10.1''; RAM: 1.5 GB");
            opisi.Add("Dijagonala 32incha/81cm. Rezolucija 1366x768. HyperReal Engine. Mega dinamički kontrast");
            opisi.Add("OS: SuSE Linux; CPU: Intel Core i5-4210U, 1.7/2.7GHz; Display: 15.6'' HD LED; RAM: 4GB; HDD: 1TB; VGA: AMD Radeon R5 M255 2GB;");
        }

        private void loadiraj(int i) {
            slika.Source = new BitmapImage(new Uri(slike[i], UriKind.Relative));
            slika.Stretch = System.Windows.Media.Stretch.Fill;
            txt.Text = opisi[i];
        }
    }
}