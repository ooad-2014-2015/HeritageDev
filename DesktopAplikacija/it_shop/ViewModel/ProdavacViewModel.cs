using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using it_shop.ViewModel;
using System.IO;
using it_shop.Model;
using System.Collections.ObjectModel;

namespace it_shop.ViewModel {
    public class ProdavacViewModel : INotifyPropertyChanged {
        //tab1
        #region Katalog
        #region Atributi

        private string nazivKatalog;
        private string kategorijaKatalog;
        private string godinaProizvodnjeKatalog;
        private string opisKatalog;
        private string cijenaKatalog;
        private string mjeseciGarancijeKatalog;
        private string proizvodjacKatalog;
        private string dodatnaOpremaKatalog;
        private string serijskiBrojKatalog;
        private string barKodKatalog;
        private ObservableCollection<Artikal> listaArtikalaKatalog = new ObservableCollection<Artikal>();
        private BitmapImage slikaKatalog;
        #endregion

        #region Preperties
        public BitmapImage SlikaKatalog {
            get { return slikaKatalog; }
            set {
                slikaKatalog = value;
                OnPropertyChanged("SlikaKatalog");
            }
        }
        public ObservableCollection<Artikal> ListaArtikalaKatalog {
            get {
                return listaArtikalaPretrage;
            }
            set {
                listaArtikalaPretrage = value;
                OnPropertyChanged("ListaArtikalaKatalog");
            }
        }
        public string BarKodKatalog {
            get { return barKodKatalog; }
            set {
                barKodKatalog = value;
                OnPropertyChanged("BarKodKatalog");
            }
        }
        public string SerijskiBrojKatalog {
            get { return serijskiBrojKatalog; }
            set {
                serijskiBrojKatalog = value;
                OnPropertyChanged("SerijskiBrojKatalog");
            }
        }
        public string DodatnaOpremaKatalog {
            get { return dodatnaOpremaKatalog; }
            set {
                dodatnaOpremaKatalog = value;
                OnPropertyChanged("DodatnaOpremaKatalog");
            }
        }
        public string ProizvodjacKatalog {
            get { return proizvodjacKatalog; }
            set {
                proizvodjacKatalog = value;
                OnPropertyChanged("ProizvodjacKatalog");
            }
        }
        public string MjeseciGarancijeKatalog {
            get { return mjeseciGarancijeKatalog; }
            set {
                mjeseciGarancijeKatalog = value;
                OnPropertyChanged("MjeseciGarancijeKatalog");
            }
        }
        public string CijenaKatalog {
            get { return cijenaKatalog; }
            set {
                cijenaKatalog = value;
                OnPropertyChanged("CijenaKatalog");
            }
        }
        public string OpisKatalog {
            get { return opisKatalog; }
            set {
                opisKatalog = value;
                OnPropertyChanged("OpisKatalog");
            }
        }
        public string GodinaProizvodnjeKatalog {
            get { return godinaProizvodnjeKatalog; }
            set { godinaProizvodnjeKatalog = value; }
        }
        public string KategorijaKatalog {
            get { return kategorijaKatalog; }
            set {
                kategorijaKatalog = value;
                OnPropertyChanged("KategorijaKatalog");
            }
        }
        public string NazivKatalog {
            get { return nazivKatalog; }
            set {
                nazivKatalog = value;
                OnPropertyChanged("NazivKatalog");
            }
        }
        #endregion

        #region Metode
        private void UcitajSveArtikleIzBaze ( ) {
            if (listaArtikalaKatalog.Count != 0) {
                listaArtikalaKatalog.Clear();
            }

            try {
                MySqlConnection connectionBaza = new MySqlConnection("server=192.168.1.11; user=root; pwd=root; database=it_shop");
                string _naziv, _kategoija, _opis, _proizvodjac, _dodatnaOprema, _serijskiBroj, _barkod;
                int _godina, _mjeseciGarancije, _kolicina;
                double _cijena;
                string upitBaza = "SELECT * FROM artikli WHERE;";
                UInt32 velicinaSlike;
                byte[] rawData;
                FileStream fs;


                MySqlDataReader r = UpitNaBazu(upitBaza, connectionBaza);
                while (r.Read()) {
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
                    velicinaSlike = r.GetUInt32(r.GetOrdinal("velicina_slike"));
                    rawData = new byte[velicinaSlike];
                    r.GetBytes(r.GetOrdinal("slika"), 0, rawData, 0, (int)velicinaSlike);
                    fs = new FileStream(@"../../Resources/tmp/" + _serijskiBroj + ".png", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    fs.Write(rawData, 0, (int)velicinaSlike);
                    fs.Close();
                    Artikal artikal = new Artikal(_naziv, _kategoija, _godina, _cijena, _opis, _mjeseciGarancije, _proizvodjac, _dodatnaOprema, _kolicina, _serijskiBroj, _barkod);
                    listaArtikalaKatalog.Add(artikal);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion
        #endregion

        //tab2
        #region Korpa
        #region Atributi
        private int brojacArtikala = 0;
        private double cijenaArtikala = 0;

        private string vrstaPretrage;
        private string pojamZaPretragu;
        private string ukupnaCijena;
        private string brojArtikala;
        private ICommand dodajUKorpu;
        private ICommand izvrsiPretragu;
        private ICommand printaj;
        private Artikal odabraniArtikal;
        private ObservableCollection<Artikal> listaArtikalaPretrage = new ObservableCollection<Artikal>();
        private ObservableCollection<Artikal> listaArtikalaKorpa = new ObservableCollection<Artikal>();
        #endregion

        #region Properties
        public string BarKod {
            get { return barKod; }
            set {
                barKod = value;
                OnPropertyChanged("BarKod");
            }
        }
        public string SerijskiBroj {
            get { return serijskiBroj; }
            set {
                serijskiBroj = value;
                OnPropertyChanged("SerijskiBroj");
            }
        }
        public string GodinaProizvodnje {
            get { return godinaProizvodnje; }
            set {
                godinaProizvodnje = value;
                OnPropertyChanged("GodinaPRoizvodnje");
            }
        }
        public string BrojArtikala {
            get { return brojArtikala; }
            set {
                brojArtikala = value;
                OnPropertyChanged("BrojArtikala");
            }
        }

        public string UkupnaCijena {
            get { return ukupnaCijena; }
            set {
                ukupnaCijena = value;
                OnPropertyChanged("UkupnaCijena");
            }
        }

        public Artikal OdabraniArtikal {
            get { return odabraniArtikal; }
            set {
                odabraniArtikal = value;
                OnPropertyChanged("OdabraniArtikal");
            }
        }
        public ObservableCollection<Artikal> ListaArtikalaKorpa {
            get {
                return listaArtikalaKorpa;
            }
            set {
                listaArtikalaKorpa = value;
                OnPropertyChanged("ListaArtikalaKorpa");
            }
        }
        public ObservableCollection<Artikal> ListaArtikalaPretrage {
            get {
                return listaArtikalaPretrage;
            }
            set {
                listaArtikalaPretrage = value;
                OnPropertyChanged("ListaArtikalaPretrage");
            }
        }
        public string PojamZaPretragu {
            get { return pojamZaPretragu; }
            set {
                pojamZaPretragu = value;
                OnPropertyChanged("PojamZaPretragu");
            }
        }
        public ICommand Printaj {
            get { return printaj; }
            set { printaj = value; }
        }
        public ICommand IzvrsiPretragu {
            get { return izvrsiPretragu; }
            set { izvrsiPretragu = value; }
        }
        public ICommand DodajUKorpu {
            get { return dodajUKorpu; }
            set { dodajUKorpu = value; }
        }
        public string VrstaPretrage {
            get { return vrstaPretrage; }
            set { vrstaPretrage = value; }
        }
        #endregion

        #region Metode
        private void PrintajRacun ( ) {
            if (ListaArtikalaKorpa.Count != 0) {
                string racunPrint = "\n";
                racunPrint += "======================================\n";
                racunPrint += "                   Racun\n";
                racunPrint += "======================================\n";
                racunPrint += " Naziv \t\t\t Cijena\n";
                racunPrint += "======================================\n";
                foreach (var item in ListaArtikalaKorpa) {
                    string naziv = item.Naziv;
                    string cijena = item.Cijena.ToString("0.00") + " KM";
                    racunPrint += " " + naziv + " \t\t\t " + cijena + "\n";
                }
                racunPrint += "======================================\n";
                racunPrint += "Iznos PDV:\t " + (cijenaArtikala - (cijenaArtikala / Convert.ToDouble(1.17))).ToString("0.00") + " KM\n";
                racunPrint += "Ukupno bez PDV:\t " + (cijenaArtikala / Convert.ToDouble(1.17)).ToString("0.00") + " KM\n";
                racunPrint += "Ukupno sa PDV:\t " + cijenaArtikala.ToString("0.00") + " KM\n";
                racunPrint += "======================================\n";
                racunPrint += "Datum i vrijeme prodaje: \t" + DateTime.Now.ToShortDateString() + "  " + DateTime.Now.ToShortTimeString() + "\n";
                racunPrint += "======================================\n";


                PrintDialog printDialog = new PrintDialog();
                FlowDocument fl = new FlowDocument(new Paragraph(new Run(racunPrint)));
                fl.Name = "printanje";
                IDocumentPaginatorSource idpSource = fl;

                printDialog.PrintDocument(idpSource.DocumentPaginator, "racun");


                MessageBox.Show("Racun isprintan.", "Info");

                ListaArtikalaKorpa.Clear();
                cijenaArtikala = 0;
                brojacArtikala = 0;
                UkupnaCijena = cijenaArtikala.ToString();
                BrojArtikala = brojacArtikala.ToString();
            }
        }
        private void DodajArtikalUKorpu ( ) {
            ListaArtikalaKorpa.Add(OdabraniArtikal);
            cijenaArtikala += ListaArtikalaKorpa.First(U => U == OdabraniArtikal).Cijena;
            brojacArtikala++;
            UkupnaCijena = cijenaArtikala.ToString();
            BrojArtikala = brojacArtikala.ToString();
        }
        private void UcitajArtikleIzBaze ( ) {
            if (ListaArtikalaPretrage.Count != 0) {
                ListaArtikalaPretrage.Clear();
            }

            try {
                MySqlConnection connectionBaza = new MySqlConnection("server=192.168.1.11; user=root; pwd=root; database=it_shop");
                string _naziv, _kategoija, _opis, _proizvodjac, _dodatnaOprema, _serijskiBroj, _barkod;
                int _godina, _mjeseciGarancije, _kolicina;
                double _cijena;
                int tip = Int32.Parse(VrstaPretrage);
                MessageBox.Show(tip.ToString());
                string upitBaza = null;
                switch (tip) {
                    case 0:
                        upitBaza = "SELECT * FROM artikli WHERE id LIKE '" + PojamZaPretragu + "%';";
                        break;
                    case 1:
                        upitBaza = "SELECT * FROM artikli WHERE naziv LIKE '" + PojamZaPretragu + "%';";
                        break;
                    case 2:
                        upitBaza = "SELECT * FROM artikli WHERE proizvodjac LIKE '" + PojamZaPretragu + "%';";
                        break;
                }
                MessageBox.Show(upitBaza, "Upit");
                MySqlDataReader r = UpitNaBazu(upitBaza, connectionBaza);
                while (r.Read()) {
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
                    ListaArtikalaPretrage.Add(artikal);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion
        #endregion

        //tab3
        #region Unos
        #region Atributi
        private string nazivProizvoda;
        private string kategorijaProizvoda;
        private string opis;
        private string cijena;
        private string mjeseciGarancije;
        private string proizvodjac;
        private string dodatnaOprema;
        private string kolicina;
        private string putanja;
        private string godinaProizvodnje;
        private string serijskiBroj;
        private string barKod;
        private ICommand unesiButton;
        private ICommand ponistiButton;
        private ICommand izaberiSlikuButton;
        private BitmapImage ucitajSliku;
        #endregion

        #region Properties
        public BitmapImage UcitajSlikuBinding {
            get {
                return ucitajSliku;
            }
            set {
                ucitajSliku = value;
                OnPropertyChanged("UcitajSlikuBinding");
            }
        }
        public ICommand IzaberiSlikuButton {
            get { return izaberiSlikuButton; }
            set { izaberiSlikuButton = value; }
        }
        public ICommand PonistiButton {
            get { return ponistiButton; }
            set { ponistiButton = value; }
        }
        public ICommand UnesiButton {
            get { return unesiButton; }
            set { unesiButton = value; }
        }
        public string Kolicina {
            get { return kolicina; }
            set {
                kolicina = value;
                OnPropertyChanged("Kolicina");
            }
        }
        public string DodatnaOprema {
            get { return dodatnaOprema; }
            set {
                dodatnaOprema = value;
                OnPropertyChanged("DodatnaOprema");
            }
        }
        public string Proizvodjac {
            get { return proizvodjac; }
            set {
                proizvodjac = value;
                OnPropertyChanged("Proizvodjac");
            }
        }
        public string MjeseciGarancije {
            get { return mjeseciGarancije; }
            set {
                mjeseciGarancije = value;
                OnPropertyChanged("MjeseciGarancije");
            }
        }
        public string Cijena {
            get { return cijena; }
            set {
                cijena = value;
                OnPropertyChanged("Cijena");
            }
        }
        public string Opis {
            get { return opis; }
            set {
                opis = value;
                OnPropertyChanged("Opis");
            }
        }
        public string KategorijaProizvoda {
            get { return kategorijaProizvoda; }
            set {
                kategorijaProizvoda = value;
                OnPropertyChanged("KategorijaProizvoda");
            }
        }
        public string NazivProizvoda {
            get { return nazivProizvoda; }
            set {
                nazivProizvoda = value;
                OnPropertyChanged("NazivProizvoda");
            }
        }
        #endregion Properties

        #region Metode
        private BitmapImage UcitajSliku ( string _putanja ) {
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri(System.IO.Path.GetFullPath(_putanja), UriKind.RelativeOrAbsolute);
            b.EndInit();
            return b;
        }
        private void IzaberiSliku ( ) {
            Microsoft.Win32.OpenFileDialog openFileDialog1 = new Microsoft.Win32.OpenFileDialog();
            openFileDialog1.Filter = "JPEG Files(*.png)|*.jpg|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = false;
            openFileDialog1.ShowDialog();
            putanja = openFileDialog1.FileName;
            if (putanja == string.Empty) {
                MessageBox.Show("Niste odabrali datoteku.");
            } else {
                UcitajSlikuBinding = UcitajSliku(putanja);
            }
        }
        private void UnosNovogArtikla ( ) {
            MySqlConnection con = new MySqlConnection("server=192.168.1.11; user=root; pwd=root; database=it_shop");
            MySqlCommand cmd = new MySqlCommand();
            int FileSize;
            byte[] rawData;
            FileStream fs;

            try {
                string _mjeseciGarancije = MjeseciGarancije.Substring(38);
                string _kategorijaProizvoda = KategorijaProizvoda.Substring(38);
                string upit = "INSERT INTO artikli (naziv, kategorija, godina_proizvodnje, cijena, opis, mjeseci_garancije, proizvodjac, serijski_broj, barkod, dodatna_oprema, kolicina, slika, velicina_slike)" +
                 " VALUES (" + NazivProizvoda + ", '" + _kategorijaProizvoda + "', " + GodinaProizvodnje + ", " + Cijena + ", '" + Opis + "', " + _mjeseciGarancije + ", '" + Proizvodjac + "', '" + SerijskiBroj + "', '" + BarKod + "', '" + DodatnaOprema + "', " + Kolicina + ", ";

                fs = new FileStream(putanja, FileMode.Open, FileAccess.Read);
                FileSize = Convert.ToInt32(fs.Length);
                rawData = new byte[FileSize];
                fs.Read(rawData, 0, FileSize);
                fs.Close();
                cmd.Parameters.AddWithValue("@FileSize", FileSize);
                cmd.Parameters.AddWithValue("@File", rawData);

                upit += "@File, @FileSize);";
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = upit;

                MessageBox.Show(upit);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Artikal uspjesno unesen.");
                PonistiIzmjene();

            } catch (System.AggregateException) {
                MessageBox.Show("Neuspjela konekcija sa bazom podataka!\nPokušajte ponovo.");
            } catch (Exception ex) {
                MessageBox.Show("Došlo je do greške!\nMolimo pokušajte ponovo ili kontaktirajte administratora!");
            }
            con.Close();

        }
        private void PonistiIzmjene ( ) {
            NazivProizvoda = string.Empty;
            KategorijaProizvoda = string.Empty;
            Cijena = string.Empty;
            Opis = string.Empty;
            MjeseciGarancije = string.Empty;
            Proizvodjac = string.Empty;
            DodatnaOprema = string.Empty;
            Kolicina = string.Empty;
            GodinaProizvodnje = string.Empty;
            SerijskiBroj = string.Empty;
            BarKod = string.Empty;
            UcitajSlikuBinding = UcitajSliku(@"../../Resources/no_image.png");
        }
        #endregion
        #endregion

        public ProdavacViewModel ( ) {
            UnesiButton = new RelayCommand(new Action(UnosNovogArtikla));
            PonistiButton = new RelayCommand(new Action(PonistiIzmjene));
            IzaberiSlikuButton = new RelayCommand(new Action(IzaberiSliku));
            UcitajSlikuBinding = UcitajSliku(@"../../Resources/no_image.png");
            IzvrsiPretragu = new RelayCommand(new Action(UcitajArtikleIzBaze));
            DodajUKorpu = new RelayCommand(new Action(DodajArtikalUKorpu));
            Printaj = new RelayCommand(new Action(PrintajRacun));
        }
        private MySqlDataReader UpitNaBazu ( string upit, MySqlConnection con ) {
            con.Open();
            MySqlCommand u = new MySqlCommand(upit, con);
            return u.ExecuteReader();
        }

        #region INotify Implementation
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
        #endregion

    }  
}
