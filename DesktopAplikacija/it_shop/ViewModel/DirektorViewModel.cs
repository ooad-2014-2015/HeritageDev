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
using System.Windows;
using System.Windows.Input;

namespace it_shop.ViewModel
{
    public class DirektorViewModel : INotifyPropertyChanged
    {
        private ICommand obrisiZahtjev;
        private ICommand odobriZahtjev;
        private ICommand ucitajZahtjeve;
        //private ICommand ucitajUposlenike;

       
        

        private Uposlenik odabraniUposlenik;
        private List<Uposlenik> uposleniciArhiva = new List<Uposlenik>();
        private ObservableCollection<string> listaUposlenika = new ObservableCollection<string>();
        private ZahtjevZaNabavkom odabranizahtjev;
        private ObservableCollection<string> listaZahtjeva;


        #region Properties

        public ObservableCollection<string> ListaZahtjeva
        {
            get { return listaZahtjeva; }
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
                OnPropertyChanged("OdabraniZahtjev");

            }
        }

        public ObservableCollection<string> ListaUposlenika
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
            UcitajZahtjeve = new RelayCommand(new Action(UcitajZahjeveZaNabavkomIzBaze));

        }


        
        private void UcitajZahjeveZaNabavkomIzBaze()
        {
            MessageBox.Show("Test");
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
                while (r.Read())
                {
                    naziv = r.GetString("ime_i_prezime");
                    spol = r.GetString("spol");
                    adresa = r.GetString("adresa");
                    telefon = r.GetString("broj_telefona");
                    //datumZaposlenja = r.GetString("datum_zaposlenja");
                    plata = double.Parse(r.GetString("plata"), System.Globalization.CultureInfo.InvariantCulture);
                    dodatak = double.Parse(r.GetString("dodatak_na_platu"), System.Globalization.CultureInfo.InvariantCulture);
                   // zaposlenjeDatum = DateTime.ParseExact("datumZapolenja", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    godisnji = Int32.Parse(r.GetString("dani_godisnjeg_odmora"));

                    Uposlenik tmp = new Uposlenik(naziv, adresa, telefon, DateTime.Now, spol, plata, dodatak, godisnji);
                    listaUposlenika.Add(naziv + " " + spol + " " + adresa + " " + telefon);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
}
