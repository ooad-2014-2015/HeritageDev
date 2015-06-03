using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace it_shop.ViewModel {
    public class LoginViewModel : INotifyPropertyChanged {
        private ICommand _button;
        private string username;

	public string Username
	{
		get { return username;}
		set { username = value;}
	}
        private string password;

	public string Password
	{
		get { return password;}
		set { password = value;}
	}
	
	

    public ICommand btn_login {
        get { return _button; }
        set { _button = value; }
    }

    public LoginViewModel ( ) {
        btn_login = new RelayCommand(new Action(ValidirajLogin));
    }

    public void ValidirajLogin ( ) {
        MySqlConnection con = new MySqlConnection("server=192.168.1.11; user=root; pwd=root; database=it_shop");
        try {
            con.Open();
            //login --> zaposlenici
            MySqlCommand upit = new MySqlCommand("select * from login where username = '" + Username+ "' and password = '" + Password + "';", con);
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
                Username = "";
                Password = "";
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
