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
            MySqlConnection con = new MySqlConnection("server=192.168.1.11; user=root; pwd=root; database=it_shop");
            try {
                con.Open();
                //login --> zaposlenici
                MySqlCommand upit = new MySqlCommand("select * from login where username = '" + txt_username.Text + "' and password = '" + txt_password.Password + "';", con);
                MySqlDataReader r = upit.ExecuteReader();

                if (r.HasRows && r.Read()) {
                    string tipuposlenika = r.GetString("tipuposlenika");
                    switch (tipuposlenika) {
                        case "DIREKTOR":
                        //neki drugi uposlenik 
                        case "PRODAVAC":
                        case "SERVISER":
                        case "MONTER":
                            MessageBox.Show("Tip uposlenika: " + tipuposlenika);
                            break;
                        default:
                            MessageBox.Show("Nema uposlenog sa tim tipom.");
                            break;
                    }
                } else {
                    MessageBox.Show("Pogresni podaci.");
                    txt_username.Text = "";
                    txt_password.Password = "";
                }

            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
            con.Close();
        }

        private void OnKeyDownHandler ( object sender, KeyEventArgs e ) {
            if (e.Key == Key.Return) {
                btn_login_Click(this, e);
            }
        }
    }
}
