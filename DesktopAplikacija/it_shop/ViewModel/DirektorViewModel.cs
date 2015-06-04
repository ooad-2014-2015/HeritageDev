using it_shop.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace it_shop.ViewModel
{
    public class DirektorViewModel : INotifyPropertyChanged
    {
        private ICommand obrisiZahtjev;
        private ICommand odobriZahtjev;
        private ICommand ucitajZahtjeve;

        private Uposlenik odabraniUposlenik;
        private ObservableCollection<Uposlenik> listaUposlenika;
        private ZahtjevZaNabavkom odabranizahtjev;
        private ObservableCollection<ZahtjevZaNabavkom> listaZahtjeva;


        #region Properties
        public List<ZahtjevZaNabavkom> ListaZahtjeva
	    {
		    get { return listaZahtjeva;}
		    set 
            { 
                listaZahtjeva = value;
                OnPropertyChanged("ListaZahtjeva");
            }
	    }
	
        public ZahtjevZaNabavkom OdabraniZahtjev
        {
            get { return odabranizahtjev; }
            set 
            { 
                odabranizahtjev = value; 
                OnPropertyChanged("OdabraniZahtjev")

            }
        }
        public ObservableCollection<Uposlenik> ListaUposlenika
        {
            get { return listaUposlenika; }
            set
            {
                listaUposlenika = value;
                OnPropertyChanged("ListaUposlenika");

            }
        }
        public Uposlenik OdabraniUposlenik
        {
            get { return odabraniUposlenik; }
            set
            {
                odabraniUposlenik = value;
                OnPropertyChanged("OdabraniUposlenik");
            }
        }
        public ICommand UcitajZahtjeve
        {
            get { return ucitajZahtjeve; }
            set { ucitajZahtjeve = value; }
        }

        public ICommand OdobriZahtjev
        {
            get { return odobriZahtjev; }
            set { odobriZahtjev = value; }
        }


        public ICommand ObrisiZahtjev
        {
            get { return obrisiZahtjev; }
            set { obrisiZahtjev = value; }
        }


        #endregion

        #region INotify Implementation
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
        #endregion


        public DirektorViewModel()
        {
            ucitajZahtjeve = new RelayCommand(new Action(UcitajZahjeveZaNabavkomIzBaze));
        }

        private void UcitajZahjeveZaNabavkomIzBaze()
        {
            MySqlConnection connectionBaza = new MySqlConnection("server=192.168.1.11; user=root; pwd=root; database=it_shop");
            try
            {
                string upitBaza = "SELECT * FROM uposlenici;";
                string naziv, spol, telefon, adresa, datumZaposlenja;
                double plata, dodatak;
                int godisnji;
                DateTime zaposlenjeDatum;

                connectionBaza.Open();
                MySqlCommand sqlCommand = new MySqlCommand(upitBaza, connectionBaza);
            
                MySqlDataReader r = sqlCommand.ExecuteReader();
                while(r.Read())
                {
                    naziv = r.GetString("ime_i_prezime");
                    spol = r.GetString("spol");
                    adresa = r.GetString("adresa");
                    telefon = r.GetString("broj_telefona");
                    datumZaposlenja = r.GetString("datum_zaposlenja");
                    plata = double.Parse(r.GetString("plata"), System.Globalization.CultureInfo.InvariantCulture);
                    dodatak = double.Parse(r.GetString("dodatak_na_platu"), System.Globalization.CultureInfo.InvariantCulture);
                    zaposlenjeDatum = DateTime.ParseExact("datumZapolenja", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    godisnji = Int32.Parse(r.GetString("dani_godisnjeg_odmora"));
                    
                    Uposlenik tmp = new Uposlenik(naziv, adresa, telefon, zaposlenjeDatum, spol, plata, dodatak, godisnji);
                    listaUposlenika.Add(tmp);


                }
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
            connectionBaza.Close();
        }
        }


       
    }
}
