using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using it_shop_app.Resources;
using System.Windows.Media.Imaging;

namespace it_shop_app
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();

            // Define the Columns
            ColumnDefinition colDef1 = new ColumnDefinition();
            ColumnDefinition colDef2 = new ColumnDefinition();
            katalog.ColumnDefinitions.Add(colDef1);
            katalog.ColumnDefinitions.Add(colDef2);

            // Define the Rows
            RowDefinition rowDef1 = new RowDefinition();
            RowDefinition rowDef2 = new RowDefinition();
            RowDefinition rowDef3 = new RowDefinition();
            RowDefinition rowDef4 = new RowDefinition();
            RowDefinition rowDef5 = new RowDefinition();
            katalog.RowDefinitions.Add(rowDef1);
            katalog.RowDefinitions.Add(rowDef2);
            katalog.RowDefinitions.Add(rowDef3);
            katalog.RowDefinitions.Add(rowDef4);
            katalog.RowDefinitions.Add(rowDef5);

            List<string> slike = new List<string>();
            List<string> opisi = new List<string>();
            slike.Add("/Assets/001.jpg");
            slike.Add("/Assets/002.jpg");
            slike.Add("/Assets/003.png");
            opisi.Add("OS: Android OS, v4.4.2 (KitKat); CPU: Quad-core 1.2 GHz; Display: 800 x 1280 pixels, 10.1''; RAM: 1.5 GB");
            opisi.Add("OS: SuSE Linux; CPU: Intel Core i5-4210U, 1.7/2.7GHz; Display: 15.6'' HD LED; RAM: 4GB; HDD: 1TB; VGA: AMD Radeon R5 M255 2GB;");
            opisi.Add("Dijagonala 32incha/81cm. Rezolucija 1366x768. HyperReal Engine. Mega dinamički kontrast");
            for (int i = 0; i < 3; i++)
            {

                Image img = new Image();
                img.Source = new BitmapImage(new Uri(slike[i], UriKind.Relative));
                img.Height = 200;
                img.Width = 200;
                img.Stretch = System.Windows.Media.Stretch.Fill;
                Grid.SetRow(img, i);
                Grid.SetColumn(img, 0);
                katalog.Children.Add(img);
                img.Margin = new Thickness(10, 10, 0, 0);

                TextBlock txt = new TextBlock();
                txt.Text = opisi[i];
                txt.Width = 200;
                txt.TextWrapping = TextWrapping.Wrap;
                Grid.SetRow(txt, i);
                Grid.SetColumn(txt, 1);
                katalog.Children.Add(txt);
                txt.Margin = new Thickness(10, 10, 0, 0);
                               
            }
        }

        private void but_kategorije_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Detailed.xaml", UriKind.Relative));
            //Detailed temp = new Detailed();
            //temp.Show();
        }

        //private void Dugme1_Click(object sender, RoutedEventArgs e)
        //{
        //    Dugme1.Content = "novo";
        //}

        //private void slika_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        //{
        //    slika.Height = 20;
        //}

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}