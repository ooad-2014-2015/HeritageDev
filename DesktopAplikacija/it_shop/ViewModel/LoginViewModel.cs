using it_shop.View;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace it_shop.ViewModel {
    public class LoginViewModel : INotifyPropertyChanged {
        private LoginView loginForma;
        private ICommand _button;
        private string username;
        private string password;
	    
       
        public string Username
	    {
		    get { return username;}
		    set { username = value;}
	    }
	    public string Password
	    {
		    get { return password;}
		    set { password = value;}
	    }
        public ICommand btn_login {
            get { return _button; }
            set { _button = value; }
        }

        public LoginViewModel (LoginView loginForma) {
            btn_login = new RelayCommand(new Action(ValidirajLogin));
            this.loginForma = loginForma;
        }

        public void KreirajNit ( ) {
            Thread nit = new Thread(( ) => ValidirajLogin()) { IsBackground = true };
            nit.Join();
        }

        private MySqlDataReader UpitNaBazu ( string upit, MySqlConnection con ) {
            con.Open();
            MySqlCommand u = new MySqlCommand(upit, con);
            return u.ExecuteReader();
        }
        private void ValidirajLogin ( ) {
            MySqlConnection con = new MySqlConnection("server=192.168.1.11; user=root; pwd=root; database=it_shop");
            try {
                string upit = "SELECT * FROM uposlenici WHERE username = '" + Username + "' AND password = '" + Password + "';";

                Task<MySqlDataReader> nit = Task<MySqlDataReader>.Factory.StartNew(( ) => UpitNaBazu(upit, con));

                MySqlDataReader r = nit.Result;
                if (r.HasRows && r.Read()) {
                    string tipuposlenika = r.GetString("tip_uposlenika");
                    switch (tipuposlenika) {
                        case "PRODAVAC":
                            ProdavacView prodavac = new ProdavacView();
                            prodavac.Show();
                            loginForma.Close();
                        break;
                        case "DIREKTOR":
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
                }

            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
            con.Close();
        }
    

        


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged ( string propertyName ) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        protected void NotifyPropertyChanged ( string propertyName ) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

}
