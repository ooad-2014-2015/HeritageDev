using it_shop.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace it_shop.ViewModel
{
    public class DirektorViewModel : INotifyPropertyChanged
    {
       
        public DirektorViewModel()
        {
            UcitajZahtjeve = new RelayCommand(new Action(UcitajZahjeveZaNabavkomIzBaze));
            ObrisiUposlenika = new RelayCommand(new Action(ObrisiUposlenikaIzBaze));
            ObrisiUposlenika = new RelayCommand(new Action(UnosNovogKorisnikaUBazu));
            AzuzirajInfoUposlenika = new RelayCommand(new Action(AzurirajInformacijeKorisnika));
        }

        private MySqlDataReader UpitNaBazu(string upit, MySqlConnection con)
        {
            con.Open();
            MySqlCommand u = new MySqlCommand(upit, con);
            return u.ExecuteReader();
        }

        private void DMLUpitiNaBazu(string upit, MySqlConnection con)
        {
            con.Open();
            MySqlCommand u = new MySqlCommand(upit, con);
            u.ExecuteNonQuery();
        }

        #region Zahtjevi Za Nabavke - Tab 1

        #region Atributi
      
        private ZahtjevZaNabavkom odabranizahtjev;
        private ObservableCollection<string> listaZahtjeva;
        private ICommand obrisiZahtjev;
        private ICommand odobriZahtjev;
        private ICommand ucitajZahtjeve;

        #endregion

      
        #region Properties
       
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

        
        #region Metode
       
        //Zahtjeve ucitava trenutno ucitava uposlenike
        private void UcitajZahjeveZaNabavkomIzBaze()
        {
            MySqlConnection connectionBaza = new MySqlConnection("server=192.168.1.11; user=root; pwd=root; database=it_shop");

            try
            {

                string upitBaza = "SELECT * FROM uposlenici;";
                string naziv, spol, telefon, adresa, datumZaposlenja;
                double plata, dodatak;
                int godisnji;
                // DateTime zaposlenjeDatum;

                MySqlDataReader r = UpitNaBazu(upitBaza, connectionBaza);
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
                    //listaUposlenika.Add(naziv + " " + spol + " " + adresa + " " + telefon);
                    //ListaUposlenika.Add(tmp);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        
        #endregion
       
        #endregion

        #region Pregled Zaposlenika - Tab 2

        #region Atributi

        private int odabraniTab;
        private Uposlenik odabraniUposlenik;
        private List<Uposlenik> uposleniciArhiva = new List<Uposlenik>();
        private ObservableCollection<Uposlenik> listaUposlenika = new ObservableCollection<Uposlenik>();
        private ICommand obrisiUposlenika;
        private ICommand azuzirajInfoUposlenika;
        private string imeAzuriraj;
        private string prezimeAzuriraj;
        private string spolAzuriraj;
        private string brojTelefonaAzuriraj;
        private string adresaAzuriraj;
        private string tipUposlenikaAzuriraj;
        private string datumZaposlenjaAzuriraj;
        private string plataAzuriraj;
        private string dodatakNaPlatuAzuriraj;
        private string daniGodisnjegAzuriraj;
        private string usernameAzuriraj;
        private string passwordAzuriraj;

        #endregion

     
        #region Properties Azuriranje Uposlenika

        public ICommand AzuzirajInfoUposlenika
        {
            get { return azuzirajInfoUposlenika; }
            set { azuzirajInfoUposlenika = value; }
        }

        public ICommand ObrisiUposlenika
        {
            get { return obrisiUposlenika; }
            set { obrisiUposlenika = value; }
        }
       
        public int OdabraniTab
        {
            get { return odabraniTab; }
            set
            {
                odabraniTab = value;
                if (OdabraniTab == 1)
                    UcitajUposlenikeIzBaze();


            }
        }

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
                UcitajInformacijeZaposlenika();
                OnPropertyChanged("OdabraniUposlenik");
            }
        }
       
        public string PasswordAzuriraj
        {
            get { return passwordAzuriraj; }
            set { passwordAzuriraj = value; OnPropertyChanged("PasswordAzuriraj"); }
        }

        public string UsernameAzuriraj
        {
            get { return usernameAzuriraj; }
            set { usernameAzuriraj = value; OnPropertyChanged("UsernameAzuriraj"); }
        }

        public string DaniGodisnjegAzuriraj
        {
            get { return daniGodisnjegAzuriraj; }
            set { daniGodisnjegAzuriraj = value; OnPropertyChanged("DaniGodisnjegAzuriraj"); }
        }

        public string DodatakNaPlatuAzuriraj
        {
            get { return dodatakNaPlatuAzuriraj; }
            set { dodatakNaPlatuAzuriraj = value; OnPropertyChanged("DodatakNaPlatuAzuriraj"); }
        }

        public string PlataAzuriraj
        {
            get { return plataAzuriraj; }
            set { plataAzuriraj = value; OnPropertyChanged("PlataAzuriraj"); }
        }

        public string DatumZaposlenjaAzuriraj
        {
            get { return datumZaposlenjaAzuriraj; }
            set { datumZaposlenjaAzuriraj = value; OnPropertyChanged("DatumZaposlenjaAzuriraj"); }
        }

        public string TipUposlenikaAzuriraj
        {
            get { return tipUposlenikaAzuriraj; }
            set { tipUposlenikaAzuriraj = value; OnPropertyChanged("TipUposlenikaAzuriraj"); }
        }

        public string AdresaAzuriraj
        {
            get { return adresaAzuriraj; }
            set { adresaAzuriraj = value; OnPropertyChanged("AdresaAzuriraj"); }
        }


        public string BrojTelefonaAzuriraj
        {
            get { return brojTelefonaAzuriraj; }
            set { brojTelefonaAzuriraj = value; OnPropertyChanged("BrojTelefonaAzuriraj"); }
        }

        public string SpolAzuriraj
        {
            get { return spolAzuriraj; }
            set { spolAzuriraj = value; OnPropertyChanged("SpolAzuriraj"); }
        }

        public string PrezimeAzuriraj
        {
            get { return prezimeAzuriraj; }
            set { prezimeAzuriraj = value; OnPropertyChanged("PrezimeAzuriraj"); }
        }

        public string ImeAzuriraj
        {
            get { return imeAzuriraj; }
            set { imeAzuriraj = value; OnPropertyChanged("ImeAzuriraj"); }
        }

        #endregion


        #region Metode
        private void UcitajUposlenikeIzBaze()
        {
            if (ListaUposlenika.Count == 0)
            {
                MySqlConnection connectionBaza = new MySqlConnection("server=192.168.1.11; user=root; pwd=root; database=it_shop");

                string upitBaza = "SELECT * FROM uposlenici;";
                string naziv, spol, telefon, adresa, datumZaposlenja;
                double plata, dodatak;
                int godisnji;
                DateTime zaposlenjeDatum;


                try
                {
                    MySqlDataReader r = UpitNaBazu(upitBaza, connectionBaza);
                    while (r.Read())
                    {
                        naziv = r.GetString("ime_i_prezime");
                        spol = r.GetString("spol");
                        adresa = r.GetString("adresa");
                        telefon = r.GetString("broj_telefona");
                        datumZaposlenja = r.GetString("datum_zaposlenja");
                        plata = double.Parse(r.GetString("plata"), System.Globalization.CultureInfo.InvariantCulture);
                        dodatak = double.Parse(r.GetString("dodatak_na_platu"), System.Globalization.CultureInfo.InvariantCulture);
                        zaposlenjeDatum = DateTime.Parse(datumZaposlenja, new CultureInfo("en-CA"));
                        godisnji = Int32.Parse(r.GetString("dani_godisnjeg_odmora"));

                        //Tip Resolve
                        Uposlenik tmp = new Uposlenik(naziv, adresa, telefon, zaposlenjeDatum, spol, plata, dodatak, godisnji);
                        ListaUposlenika.Add(tmp);


                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }


            }


        }

        private void ObrisiUposlenikaIzBaze()
        {
            MySqlConnection connectionBaza = new MySqlConnection("server=192.168.1.11; user=root; pwd=root; database=it_shop");
            Uposlenik uposlenik = OdabraniUposlenik;
            string upit = "DELETE FROM uposlenici WHERE ime_i_prezime = '" + uposlenik.PunoIme + "';";
            DMLUpitiNaBazu(upit, connectionBaza);
            //ListaUposlenika.Remove(uposlenik);
        }

        private void UnosNovogKorisnikaUBazu()
        {

        }

        private void UcitajInformacijeZaposlenika()
        {
            ImeAzuriraj = OdabraniUposlenik.PunoIme;
            PrezimeAzuriraj = OdabraniUposlenik.PunoIme;
            AdresaAzuriraj = OdabraniUposlenik.Adresa;
            BrojTelefonaAzuriraj = OdabraniUposlenik.BrojTelefona;
            SpolAzuriraj = OdabraniUposlenik.Spol;
            TipUposlenikaAzuriraj = "DIREKTOR";
            DatumZaposlenjaAzuriraj = OdabraniUposlenik.DatumZaposlenja.ToShortDateString();
            PlataAzuriraj = OdabraniUposlenik.Plata.ToString();
            DodatakNaPlatuAzuriraj = OdabraniUposlenik.DodatakNaPlatu.ToString();
            DaniGodisnjegAzuriraj = OdabraniUposlenik.DaniGodisnjegOdmora.ToString();
            UsernameAzuriraj = "Neko";
            PasswordAzuriraj = "Neko";
        }

        private void AzurirajInformacijeKorisnika()
        {
            try
            {
                string upit = "UPDATE uposlenici SET ime_i_prezime = '" + ImeAzuriraj + " " + PrezimeAzuriraj + "', spol = '" + SpolAzuriraj + "', adresa = '" +
                                AdresaAzuriraj + "', broj_telefona = '" + BrojTelefonaAzuriraj + "', tip_uposlenika = '" + TipUposlenikaAzuriraj + "', plata = " + PlataAzuriraj + ", dodatak_na_platu = " + DodatakNaPlatuAzuriraj + ", dani_godisnjeg_odmora = " +
                                DaniGodisnjegAzuriraj + ", username = '" + UsernameAzuriraj + "', password = '" + PasswordAzuriraj + "' WHERE id = 1;";
                MessageBox.Show(upit);

                MySqlConnection connectionBaza = new MySqlConnection("server=192.168.1.11; user=root; pwd=root; database=it_shop");
                DMLUpitiNaBazu(upit, connectionBaza);
                OcistiFormuZaAzuriranjeInformacijaUposlenih();
                ListaUposlenika.Clear();
                UcitajUposlenikeIzBaze();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }
       
        private void OcistiFormuZaAzuriranjeInformacijaUposlenih()
        {
            ImeAzuriraj = String.Empty;
            PrezimeAzuriraj = String.Empty;
            AdresaAzuriraj = String.Empty;
            BrojTelefonaAzuriraj = String.Empty;
            SpolAzuriraj = String.Empty;
            TipUposlenikaAzuriraj = String.Empty;
            DatumZaposlenjaAzuriraj = String.Empty;
            PlataAzuriraj = String.Empty;
            DodatakNaPlatuAzuriraj = String.Empty;
            DaniGodisnjegAzuriraj = String.Empty;
            UsernameAzuriraj = String.Empty;
            PasswordAzuriraj = String.Empty;
        }
        
        #endregion
       
        
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

    }
}
