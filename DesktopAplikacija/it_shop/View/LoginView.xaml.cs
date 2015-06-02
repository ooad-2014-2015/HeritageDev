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

namespace it_shop.View {
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window {
        public LoginView ( ) {
            InitializeComponent();
        }

        private void btn_login_Click ( object sender, RoutedEventArgs e ) {
            {
                MySqlConnection con = new MySqlConnection("server=192.168.1.11; user=root; pwd=root; database=it_shop");
                con.Open();
                //login --> zaposlenici
                MySqlCommand upit = new MySqlCommand("select * from login where binary username = '" + txt_username.Text + "' and binary password = '" + txt_password.Text + "';", con);
                MySqlDataReader r = upit.ExecuteReader();

                if (r.HasRows) {
                    //MessageBox.Show("Ima");
                    int tipUposlenika;
                    Int32.TryParse(r.GetString("Tip"), out tipUposlenika);
                    switch (tipUposlenika) {
                        case 1:
                        //neki drugi uposlenik 
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            MessageBox.Show("Tip uposlenika: " + tipUposlenika.ToString());
                            break;
                    }


                } else {
                    MessageBox.Show("Pogresni podaci.");
                    txt_username.Text = "";
                    txt_password.Text = "";
                }


                con.Close();

            }
        }

        private void txt_username_GotFocus ( object sender, RoutedEventArgs e ) {
            if (txt_username.Text == "Username")
                txt_username.Text = String.Empty;
        }

        private void txt_password_GotFocus ( object sender, RoutedEventArgs e ) {

            if (txt_password.Text == "Password")
                txt_password.Text = String.Empty;
        }





    }
}
