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
using System.Windows.Media.Imaging;
using System.IO;


namespace it_shop.ViewModel
{
    public class DirektorViewModel : INotifyPropertyChanged
    {

        public DirektorViewModel()
        {
            UcitajZahtjeve = new RelayCommand(new Action(UcitajZahjeveZaNabavkomIzBaze));
            ObrisiUposlenika = new RelayCommand(new Action(ObrisiUposlenikaIzBaze));
            AzuzirajInfoUposlenika = new RelayCommand(new Action(AzurirajInformacijeKorisnika));
            UnosUposlenika = new RelayCommand(new Action(UnesiNovogUposlenikaUBazu));
            PonistiUnosUposlenika = new RelayCommand(new Action(OcistiFormuZaUnosKorisnika));
            UcitajSlikuBinding = UcitajSliku(putanja);
            IzaberiSliku = new RelayCommand(new Action(IzaberiSliku1));
            OdobriZahtjev = new RelayCommand(new Action(OdobriZahtjevZaNabavkom));
            ObrisiZahtjev = new RelayCommand(new Action(ObrisiZahtjevZaNabavkom));
            hardcodedTipoviUposlenika.AddRange(new string[] {"DIREKTOR", "SUPERVIZOR", "PRODAVAC", "SERVISER", "MONTER"});
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

        private ZahtjevZaNabavkom odabraniZahtjev = null;
        private ObservableCollection<ZahtjevZaNabavkom> listaZahtjeva;
        private ObservableCollection<Artikal> listaArtikalaZahtjeva = new ObservableCollection<Artikal>();

        private ICommand obrisiZahtjev;
        private ICommand odobriZahtjev;
        private ICommand ucitajZahtjeve;

        #endregion


        #region Properties

        public ObservableCollection<Artikal> ListaArtikalaZahtjeva
        {
            get { return listaArtikalaZahtjeva; }
            set { listaArtikalaZahtjeva = value; OnPropertyChanged("ListaArtikalaZahtjeva"); }
        }
        public ObservableCollection<ZahtjevZaNabavkom> ListaZahtjeva
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
            get { return odabraniZahtjev; }
            set
            {
                odabraniZahtjev = value;
                PrikaziDetaljeZahtjevaZaNabavkom();
                OnPropertyChanged("OdabraniZahtjev");

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


        #region Metode


        private void UcitajZahjeveZaNabavkomIzBaze()
        {
            MySqlConnection connectionBaza = new MySqlConnection("server=192.168.1.11; user=root; pwd=root; database=it_shop");

            try
            {

                string upitBaza = "SELECT * FROM zahtjevi_nabavke zn, artikli a WHERE a.id_nabavka = zn.id;";
                string datum;
                string odobren;
                bool odobrenZah;
                string idZahtjeva;
                string _naziv, _kategoija, _opis, _proizvodjac, _dodatnaOprema, _serijskiBroj, _barkod;
                int _godina, _mjeseciGarancije, _kolicina;
                double _cijena;
                List<Artikal> lista = new List<Artikal>();
                MySqlDataReader r = UpitNaBazu(upitBaza, connectionBaza);
                while (r.Read())
                {
                    //Zahtjev
                    datum = r.GetString("datum_zahtjeva");
                    odobren = r.GetString("odobren");
                    idZahtjeva = r.GetString("id");

                    //Proizvod
                    _naziv = r.GetString("naziv");
                    _kategoija = r.GetString("kategorija");
                    _godina = r.GetInt32("godina_proizvodnje");
                    _cijena = r.GetDouble("cijena");
                    _opis = r.GetString("opis");
                    _mjeseciGarancije = r.GetInt32("mjeseci_garancije");
                    _proizvodjac = r.GetString("proizvodjac");
                    _dodatnaOprema = r.GetString("dodatna_oprema");
                    _serijskiBroj = r.GetString("serijski_broj");
                    _barkod = r.GetString("barkod");
                    _kolicina = r.GetInt32("kolicina");

                    Artikal artikal = new Artikal(_naziv, _kategoija, _godina, _cijena, _opis, _mjeseciGarancije, _proizvodjac, _dodatnaOprema, _kolicina, _serijskiBroj, _barkod);
                    lista.Add(artikal);
                    if (odobren == "1")
                        odobrenZah = true;
                    else
                        odobrenZah = false;
                    ZahtjevZaNabavkom tmp = new ZahtjevZaNabavkom(lista, odobrenZah);
                    tmp.Id = idZahtjeva;
                    //listaUposlenika.Add(naziv + " " + spol + " " + adresa + " " + telefon);
                    //ListaUposlenika.Add(tmp);
                    ListaZahtjeva.Add(tmp);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        private void PrikaziDetaljeZahtjevaZaNabavkom()
        {
            ObservableCollection<Artikal> tmpLista = new ObservableCollection<Artikal>(OdabraniZahtjev.ListaArtikala);
            ListaArtikalaZahtjeva = tmpLista;

        }
        private void OdobriZahtjevZaNabavkom()
        {
            if (OdabraniZahtjev != null)
            {
                try
                {
                    MySqlConnection connectionBaza = new MySqlConnection("server=192.168.1.11; user=root; pwd=root; database=it_shop");
                    string upitBaza = "UPDATE zahtjevi_nabavke SET odobren = true WHERE id = " + OdabraniZahtjev.Id + ";";

                    DMLUpitiNaBazu(upitBaza, connectionBaza);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Nije odabran zahtjev!\nOdaberite zahtjev za brisanje iz Liste zahtjeva.");
            }
        }
        private void ObrisiZahtjevZaNabavkom()
        {
            if (OdabraniZahtjev != null)
            {
                try
                {
                    MySqlConnection connectionBaza = new MySqlConnection("server=192.168.1.11; user=root; pwd=root; database=it_shop");
                    string upitBaza = "DELETE FROM zahtjevi_nabavke WHERE id = " + OdabraniZahtjev.Id + ";";
                    DMLUpitiNaBazu(upitBaza, connectionBaza);
                    ListaZahtjeva.Remove(OdabraniZahtjev);
                    ListaArtikalaZahtjeva.Clear();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Nije odabran zahtjev!\nOdaberite zahtjev za brisanje iz Liste zahtjeva.");
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
        private string plataAzuriraj;
        private string dodatakNaPlatuAzuriraj;
        private string daniGodisnjegAzuriraj;
        private string usernameAzuriraj;
        private string passwordAzuriraj;
        private BitmapImage slikaAzuriraj;
        private List<string> hardcodedTipoviUposlenika = new List<string>();




        #endregion


        #region Properties Azuriranje Uposlenika

        public BitmapImage SlikaAzuriraj
        {
            get { return slikaAzuriraj; }
            set { slikaAzuriraj = value; OnPropertyChanged("SlikaAzuriraj"); }
        }
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
                UInt32 velicinaSlike;
                byte[] rawData;
                FileStream fs;


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
                        velicinaSlike = r.GetUInt32(r.GetOrdinal("velicina_slike"));
                        rawData = new byte[velicinaSlike];
                        r.GetBytes(r.GetOrdinal("slika"), 0, rawData, 0, (int)velicinaSlike);
                        fs = new FileStream(@"../../Resources/tmp/" + naziv + telefon + ".png", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                        fs.Write(rawData, 0, (int)velicinaSlike);
                        fs.Close();

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
            string upit = "DELETE FROM uposlenici WHERE ime_i_prezime = '" + uposlenik.PunoIme + "' AND broj_telefona = '" + uposlenik.BrojTelefona + "';";
            DMLUpitiNaBazu(upit, connectionBaza);
            ListaUposlenika.Remove(uposlenik);
        }

        private void UcitajInformacijeZaposlenika()
        {
            ImeAzuriraj = OdabraniUposlenik.PunoIme;
            PrezimeAzuriraj = OdabraniUposlenik.PunoIme;
            AdresaAzuriraj = OdabraniUposlenik.Adresa;
            BrojTelefonaAzuriraj = OdabraniUposlenik.BrojTelefona;
            SpolAzuriraj = OdabraniUposlenik.Spol;
            TipUposlenikaAzuriraj = "DIREKTOR";
            PlataAzuriraj = OdabraniUposlenik.Plata.ToString();
            DodatakNaPlatuAzuriraj = OdabraniUposlenik.DodatakNaPlatu.ToString();
            DaniGodisnjegAzuriraj = OdabraniUposlenik.DaniGodisnjegOdmora.ToString();
            SlikaAzuriraj = UcitajSliku(@"../../Resources/tmp/" + OdabraniUposlenik.PunoIme + OdabraniUposlenik.BrojTelefona + ".png");

            MySqlConnection connectionBaza = new MySqlConnection("server=192.168.1.11; user=root; pwd=root; database=it_shop");
            string upitBaza = "SELECT username, password FROM uposlenici WHERE ime_i_prezime='" + ImeAzuriraj + " " + PrezimeAzuriraj 
                            + "' and broj_telefona='" + BrojTelefonaAzuriraj + "';";
            MySqlDataReader reader = UpitNaBazu(upitBaza, connectionBaza);

            UsernameAzuriraj = reader.GetString("username");
            PasswordAzuriraj = reader.GetString("password");
        }

        private void ValidacijaPodataka(string tip, string s1, string s2, string s3, string s4, string s5, string s6, string s7, string s8, string s9, string s10, string s11)
        {
            if (tip != "azuriranje" && tip != "unos")
                return;
            string poruka = string.Empty;
            if (!Regex.IsMatch(s1, @"^[a-zA-Z]+$"))
                poruka = "Ime sadrzi ilegalne znakove!";
            else if (!Regex.IsMatch(s2, @"^[a-zA-Z]+$"))
                poruka = "Prezime sadrzi ilegalne znakove!";
            else if (tip == "azuriranje" && !Regex.IsMatch(s3, @"^[MZ]+$"))
                poruka = "Spol sadrzi ilegalne znakove!";
            else if (!Regex.IsMatch(s4, @"^[0-9]+$"))
                poruka = "Broj telefona sadrzi ilegalne znakove!";
            else if (!Regex.IsMatch(s5, @"^[a-zA-Z0-9]+$"))
                poruka = "Adresa sadrzi ilegalne znakove!";
            else if (tip == "azuriranje") {
                if (string.IsNullOrEmpty(s6))
                    poruka = "Tip uposlenika nije odabran!";
                else if (hardcodedTipoviUposlenika.IndexOf(s6) == -1)
                    poruka = "Tip uposlenika ne postoji!";
            }
            else if (!Regex.IsMatch(s7, @"^[0-9]+$"))
                poruka = "Plata sadrzi ilegalne znakove";
            else if (!Regex.IsMatch(s8, @"(?<=^| )\d+(\.\d+)?(?=$| )"))
                poruka = "Dodatak na platu sadrzi ilegalne znakove";
            else if (tip == "azuriranje" && !Regex.IsMatch(s9, @"^[0-9]+$"))
                poruka = "Dani godisnjeg odmora sadrzi ilegalne znakove";
            else if (!Regex.IsMatch(s10, @"^[a-zA-Z0-9]+$"))
                poruka = "Username sadrzi ilegalne znakove!";
            else if (string.IsNullOrEmpty(s11))
                poruka = "Password ne smije biti prazan!";
            else {
                MySqlConnection connectionBaza = new MySqlConnection("server=192.168.1.11; user=root; pwd=root; database=it_shop");
                string upitBaza = "SELECT 1 FROM uposlenici WHERE USERNAME='" + UsernameAzuriraj + "';";
                MySqlDataReader reader = UpitNaBazu(upitBaza, connectionBaza);
                if (reader.HasRows)
                    poruka = "Vec postoji korisnik sa unesenim username-om!";
            }
        }

        private void AzurirajInformacijeKorisnika()
        {
            ValidacijaPodataka("azuriranje", ImeAzuriraj, PrezimeAzuriraj, SpolAzuriraj, BrojTelefonaAzuriraj, AdresaAzuriraj, TipUposlenikaAzuriraj, PlataAzuriraj,
                                  DodatakNaPlatuAzuriraj, DaniGodisnjegAzuriraj, UsernameAzuriraj, PasswordAzuriraj);
            try
            {
                string upit = "UPDATE uposlenici SET ime_i_prezime = '" + ImeAzuriraj + " " + PrezimeAzuriraj + "', spol = '" + SpolAzuriraj + "', adresa = '" +
                                AdresaAzuriraj + "', broj_telefona = '" + BrojTelefonaAzuriraj + "', tip_uposlenika = '" + TipUposlenikaAzuriraj + "', plata = " + PlataAzuriraj + ", dodatak_na_platu = " + DodatakNaPlatuAzuriraj + ", dani_godisnjeg_odmora = " +
                                DaniGodisnjegAzuriraj + ", username = '" + UsernameAzuriraj + "', password = '" + PasswordAzuriraj + "' WHERE ime_i_prezime = '" +
                                OdabraniUposlenik.PunoIme + "' AND broj_telefona = '" + OdabraniUposlenik.BrojTelefona + "';";
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
            PlataAzuriraj = String.Empty;
            DodatakNaPlatuAzuriraj = String.Empty;
            DaniGodisnjegAzuriraj = String.Empty;
            UsernameAzuriraj = String.Empty;
            PasswordAzuriraj = String.Empty;
        }

        #endregion


        #endregion


        #region Unos Uposlenika - Tab 3

        #region Atributi

        private string imeUposlenika;
        private string prezimeUposlenika;
        private string adresaUposlenika;
        private string brojTelefonaUposlenika;
        private string spolUposlenika;
        private string tipUposlenika;
        private string datumZaposlenjaUposlenika;
        private string plataUposlenika;
        private string dodatakNaPlatuUposlenika;
        private string daniGodisnjeUposlenika;
        private string usernameUposlenika;
        private string passwordUposlenik;
        private string putanja = @"../../Resources/no_image.png";
        private ICommand izaberiSliku;
        private ICommand unosUposlenika;
        private ICommand ponistiUnosUposlenika;
        private BitmapImage ucitajSlikuBinding;


        #endregion

        #region Properties
        public ICommand IzaberiSliku
        {
            get { return izaberiSliku; }
            set { izaberiSliku = value; }
        }
        public ICommand PonistiUnosUposlenika
        {
            get { return ponistiUnosUposlenika; }
            set { ponistiUnosUposlenika = value; }
        }

        public ICommand UnosUposlenika
        {
            get { return unosUposlenika; }
            set { unosUposlenika = value; }
        }

        public string PasswordUposlenika
        {
            get { return passwordUposlenik; }
            set { passwordUposlenik = value; OnPropertyChanged("PasswordUposlenika"); }
        }

        public string UsernameUposlenika
        {
            get { return usernameUposlenika; }
            set { usernameUposlenika = value; OnPropertyChanged("UsernameUposlenika"); }
        }

        public string DaniGodisnjegUposlenika
        {
            get { return daniGodisnjeUposlenika; }
            set { daniGodisnjeUposlenika = value; OnPropertyChanged("DaniGodisnjegUposlenika"); }
        }

        public string DodatakNaPlatuUposlenika
        {
            get { return dodatakNaPlatuUposlenika; }
            set { dodatakNaPlatuUposlenika = value; OnPropertyChanged("DodatakNaPlatuUposlenika"); }
        }

        public string PlataUposlenika
        {
            get { return plataUposlenika; }
            set { plataUposlenika = value; OnPropertyChanged("PlataUposlenika"); }
        }

        public string DatumZaposlenjaUposlenika
        {
            get { return datumZaposlenjaUposlenika; }
            set { datumZaposlenjaUposlenika = value; OnPropertyChanged("DatumZaposlenjaUposlenika"); }
        }

        public string TipUposlenika
        {
            get { return tipUposlenika; }
            set { tipUposlenika = value; OnPropertyChanged("TipUposlenika"); }
        }

        public string SpolUposlenika
        {
            get { return spolUposlenika; }
            set { spolUposlenika = value; OnPropertyChanged("SpolUposlenika"); }
        }

        public string BrojTelefonaUposlenika
        {
            get { return brojTelefonaUposlenika; }
            set { brojTelefonaUposlenika = value; OnPropertyChanged("BrojTelefonaUposlenika"); }
        }

        public string AdresaUposlenika
        {
            get { return adresaUposlenika; }
            set { adresaUposlenika = value; OnPropertyChanged("AdresaUposlenika"); }
        }

        public string PrezimeUposlenika
        {
            get { return prezimeUposlenika; }
            set { prezimeUposlenika = value; OnPropertyChanged("PrezimeUposlenika"); }
        }

        public string ImeUposlenika
        {
            get { return imeUposlenika; }
            set { imeUposlenika = value; OnPropertyChanged("ImeUposlenika"); }
        }


        #endregion

        #region Metode
        private void IzaberiSliku1()
        {
            Microsoft.Win32.OpenFileDialog openFileDialog1 = new Microsoft.Win32.OpenFileDialog();
            openFileDialog1.Filter = "JPEG Files(*.png)|*.jpg|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = false;
            openFileDialog1.ShowDialog();
            putanja = openFileDialog1.FileName;
            if (putanja == string.Empty)
            {
                MessageBox.Show("Niste odabrali datoteku.");
            }
            else
            {
                UcitajSlikuBinding = UcitajSliku(putanja);
            }
        }
        private BitmapImage UcitajSliku(string _putanja)
        {
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri(System.IO.Path.GetFullPath(_putanja), UriKind.RelativeOrAbsolute);
            b.EndInit();
            return b;
        }
        public BitmapImage UcitajSlikuBinding
        {
            get
            {
                return ucitajSlikuBinding;
            }
            set
            {
                ucitajSlikuBinding = value;
                OnPropertyChanged("UcitajSlikuBinding");
            }
        }
        private void UnesiNovogUposlenikaUBazu()
        {
            
            try
            {

                SpolUposlenika = SpolUposlenika.Substring(38);
                imeUposlenika = TipUposlenika.Substring(38);
                DaniGodisnjegUposlenika = DaniGodisnjegUposlenika.Substring(38);

                ValidacijaPodataka("unos", ImeUposlenika, PrezimeUposlenika, SpolUposlenika, BrojTelefonaUposlenika, AdresaUposlenika, TipUposlenika, PlataUposlenika,
                                  DodatakNaPlatuUposlenika, DaniGodisnjegUposlenika, UsernameUposlenika, PasswordUposlenika);

                MySqlConnection connectionBaza = new MySqlConnection("server=192.168.1.11; user=root; pwd=root; database=it_shop");
                MySqlCommand cmd = new MySqlCommand();
                int FileSize;
                byte[] rawData;
                FileStream fs;

                string upit = "INSERT INTO uposlenici (ime_i_prezime, spol, broj_telefona, adresa, tip_uposlenika, datum_zaposlenja, plata, dodatak_na_platu, dani_godisnjeg_odmora, username, password, slika, velicina_slike) VALUES ('" +
                ImeUposlenika + " " + PrezimeUposlenika + "','" + SpolUposlenika + "','" + BrojTelefonaUposlenika + "','" + AdresaUposlenika + "','" + TipUposlenika +
                "', STR_TO_DATE('" + DateTime.Now.ToShortDateString() + "','%d.%m.%Y'), " + PlataUposlenika + "," + DodatakNaPlatuUposlenika + "," + DaniGodisnjegUposlenika + ",'" + UsernameUposlenika + "','" + PasswordUposlenika + "', ";

                fs = new FileStream(putanja, FileMode.Open, FileAccess.Read);
                FileSize = Convert.ToInt32(fs.Length);
                rawData = new byte[FileSize];
                fs.Read(rawData, 0, FileSize);
                fs.Close();
                cmd.Parameters.AddWithValue("@FileSize", FileSize);
                cmd.Parameters.AddWithValue("@File", rawData);

                upit += "@File, @FileSize);";
                connectionBaza.Open();
                cmd.Connection = connectionBaza;
                cmd.CommandText = upit;

                cmd.ExecuteNonQuery();

                MessageBox.Show(upit);
                //DMLUpitiNaBazu(upit, connectionBaza);
                OcistiFormuZaUnosKorisnika();
                connectionBaza.Close();
            }
            catch (System.AggregateException)
            {
                MessageBox.Show("Neuspjela konekcija sa bazom podataka!\nPokušajte ponovo.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show("Došlo je do greške!\nMolimo pokušajte ponovo ili kontaktirajte administratora!");
            }
        }

        private void OcistiFormuZaUnosKorisnika()
        {
            ImeUposlenika = String.Empty;
            PrezimeUposlenika = String.Empty;
            SpolUposlenika = String.Empty;
            BrojTelefonaUposlenika = String.Empty;
            AdresaUposlenika = String.Empty;
            TipUposlenika = String.Empty;
            DatumZaposlenjaUposlenika = String.Empty;
            PlataUposlenika = String.Empty;
            DodatakNaPlatuUposlenika = String.Empty;
            DaniGodisnjegUposlenika = String.Empty;
            UsernameUposlenika = String.Empty;
            PasswordUposlenika = String.Empty;
            UcitajSlikuBinding = UcitajSliku(@"../../Resources/no_image.png");
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
