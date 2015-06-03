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

namespace it_shop.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private LoginView loginForma;
        private ICommand button;
        private string username;
        private string password;


        #region Properties
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        public ICommand Button
        {
            get { return button; }
            set { button = value; }
        }

        #endregion

        public LoginViewModel(LoginView loginForma)
        {
            Button = new RelayCommand(new Action(ValidirajLogin));
            this.loginForma = loginForma;
        }

        public void KreirajNit()
        {
            Thread nit = new Thread(() => ValidirajLogin()) { IsBackground = true };
            nit.Join();
        }

        private MySqlDataReader UpitNaBazu(string upit, MySqlConnection con)
        {
            con.Open();
            MySqlCommand u = new MySqlCommand(upit, con);
            return u.ExecuteReader();
        }
        private void ValidirajLogin()
        {
            MySqlConnection con = new MySqlConnection("server=192.168.1.11; user=root; pwd=root; database=it_shop");
            try
            {
                string upit = "SELECT * FROM uposlenici WHERE username = '" + Username + "' AND password = '" + Password + "';";

                Task<MySqlDataReader> nit = Task<MySqlDataReader>.Factory.StartNew(() => UpitNaBazu(upit, con));

                MySqlDataReader r = nit.Result;
                if (r.HasRows && r.Read())
                {
                    string tipuposlenika = r.GetString("tip_uposlenika");
                    switch (tipuposlenika)
                    {
                        case "PRODAVAC":
                            ProdavacView prodavac = new ProdavacView();
                            prodavac.Show();
                            loginForma.Close();
                            break;
                        case "DIREKTOR":
                            DirectorView direktor = new DirectorView();
                            direktor.Show();
                            loginForma.Close();
                            break;
                        case "SERVISER":
                        case "MONTER":
                            MessageBox.Show("Tip uposlenika: " + tipuposlenika);
                            break;
                        default:
                            MessageBox.Show("Nema uposlenog sa tim tipom.");
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Pogresni podaci.");
                    Username = String.Empty;
                    Password = String.Empty;
                }

            }
            catch (System.AggregateException)
            {
                MessageBox.Show("Neuspjela konekcija sa bazom podataka!\nPokušajte ponovo.");
                Username = String.Empty;
                Password = String.Empty;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Došlo je do greške!\nMolimo pokušajte ponovo ili kontaktirajte administratora!");
                Username = String.Empty;
                Password = String.Empty;
            }
            con.Close();
        }





        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

}
