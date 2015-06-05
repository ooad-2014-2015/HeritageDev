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
        public ProdavacViewModel ( ) {

            UnesiButton = new RelayCommand(new Action(UnosNovogArtikla));
            PonistiButton = new RelayCommand(new Action(PonistiIzmjene));
            IzaberiSlikuButton = new RelayCommand(new Action(IzaberiSliku));
            UcitajSlikuBinding = UcitajSliku(@"../../Resources/no_image.png");
            IzvrsiPretragu = new RelayCommand(new Action(UcitajArtikleIzBazePretraga));
            DodajUKorpuPretraga = new RelayCommand(new Action(DodajArtikalUKorpuPretraga));
            DodajUKorpuKatalog = new RelayCommand(new Action(DodajArtikalUKorpuKatalog));
            //ZahtjevajProizvodKatalog = new RelayCommand(new Action());
            Printaj = new RelayCommand(new Action(PrintajRacun));
            KategorijeUKatalogu.Add(new KategorijeKatalog("LAPTOP"));
            KategorijeUKatalogu.Add(new KategorijeKatalog("RACUNAR"));
            KategorijeUKatalogu.Add(new KategorijeKatalog("MOBITEL"));
            KategorijeUKatalogu.Add(new KategorijeKatalog("MREZNA OPREMA"));
            KategorijeUKatalogu.Add(new KategorijeKatalog("SOFTVER"));
        }
        private MySqlDataReader UpitNaBazu ( string upit, MySqlConnection con ) {
            con.Open();
            MySqlCommand u = new MySqlCommand(upit, con);
            return u.ExecuteReader();
        }

        #region Funkcije za slike
        private BitmapImage UcitajSliku ( string _putanja ) {
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri(System.IO.Path.GetFullPath(_putanja), UriKind.RelativeOrAbsolute);
            b.EndInit();
            return b;
        }
        private void IzaberiSliku ( ) {
            Microsoft.Win32.OpenFileDialog openFileDialog1 = new Microsoft.Win32.OpenFileDialog();
            openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png|All Files (*.*)|*.*";
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
        #endregion

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
        private string kolicinaKatalog;
        private string prodavacStatusBar;
        private string kolicinaProizvoda;
        private BitmapImage slikaKatalog;
        private ICommand dodajUKorpuKatalog;
        private ICommand zahtjevajProizvodKatalog;
        private Artikal odabraniArtikalKatalog;
        private KategorijeKatalog odabranaKategorija;
        private ObservableCollection<KategorijeKatalog> kategorijeUKatalogu = new ObservableCollection<KategorijeKatalog>();
        private ObservableCollection<Artikal> listaArtikalaKatalog = new ObservableCollection<Artikal>();

        #endregion

        #region Preperties
        public string KolicinaProizvoda {
            get { return kolicinaProizvoda; }
            set {
                kolicinaProizvoda = value;
                OnPropertyChanged("KolicinaProizvoda");
            }
        }
        public ICommand ZahtjevajProizvodKatalog {
            get { return zahtjevajProizvodKatalog; }
            set { zahtjevajProizvodKatalog = value; }
        }
        public string ProdavacStatusBar {
            get { return prodavacStatusBar; }
            set {
                prodavacStatusBar = value;
                OnPropertyChanged("ProdavacStatusBar");
            }
        }
        public ICommand DodajUKorpuKatalog {
            get { return dodajUKorpuKatalog; }
            set { dodajUKorpuKatalog = value; }
        }

        public Artikal OdabraniArtikalKatalog {
            get { return odabraniArtikalKatalog; }
            set {
                odabraniArtikalKatalog = value;
                PrikaziArtikal();
                OnPropertyChanged("OdabraniArtikalKatalog");
            }
        }
        public KategorijeKatalog OdabranaKategorija {
            get { return odabranaKategorija; }
            set {
                odabranaKategorija = value;
                UcitajArtikleizBazeKategorija();
                OnPropertyChanged("OdabranaKategorija");
            }
        }
        public ObservableCollection<KategorijeKatalog> KategorijeUKatalogu {
            get { return kategorijeUKatalogu; }
            set {
                kategorijeUKatalogu = value;
                OnPropertyChanged("KategorijeUKatalogu");
            }
        }
        public BitmapImage SlikaKatalog {
            get { return slikaKatalog; }
            set {
                slikaKatalog = value;
                OnPropertyChanged("SlikaKatalog");
            }
        }
        public ObservableCollection<Artikal> ListaArtikalaKatalog {
            get {
                return listaArtikalaKatalog;
            }
            set {
                listaArtikalaKatalog = value;
                OnPropertyChanged("ListaArtikalaKatalog");
            }
        }
        public string KolicinaKatalog {
            get { return kolicinaKatalog; }
            set {
                kolicinaKatalog = value;
                OnPropertyChanged("KolicinaKatalog");
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
            set {
                godinaProizvodnjeKatalog = value;
                OnPropertyChanged("GodinaProizvodnjeKatalog");
            }
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
        private void DodajArtikalUKorpuKatalog ( ) {
            ListaArtikalaKorpa.Add(OdabraniArtikalKatalog);
            cijenaArtikala += ListaArtikalaKorpa.First(U => U == OdabraniArtikalKatalog).Cijena;
            brojacArtikala++;
            UkupnaCijena = cijenaArtikala.ToString();
            BrojArtikala = brojacArtikala.ToString();
        }
        private void PrikaziArtikal ( ) {
            if (OdabraniArtikalKatalog != null) {
                NazivKatalog = OdabraniArtikalKatalog.Naziv;
                KategorijaKatalog = OdabraniArtikalKatalog.Kategorija;
                GodinaProizvodnjeKatalog = OdabraniArtikalKatalog.GodinaProizvodnje.ToString();
                CijenaKatalog = OdabraniArtikalKatalog.Cijena.ToString();
                OpisKatalog = OdabraniArtikalKatalog.Opis;
                MjeseciGarancijeKatalog = OdabraniArtikalKatalog.MjeseciGarancije.ToString();
                ProizvodjacKatalog = OdabraniArtikalKatalog.Proizvodjac;
                DodatnaOpremaKatalog = OdabraniArtikalKatalog.DodatnaOprema;
                SerijskiBrojKatalog = OdabraniArtikalKatalog.SerijskiBroj;
                KolicinaKatalog = OdabraniArtikalKatalog.Kolicina.ToString();
                string putanjaSlike = @"../../Resources/tmp/artikal_" + SerijskiBrojKatalog + ".png";

                if (!File.Exists(putanjaSlike)) {
                    putanjaSlike = @"../../Resources/no_image.png";
                } 

                SlikaKatalog = UcitajSliku(putanjaSlike);
            }

        }
        private void UcitajArtikleizBazeKategorija ( ) {
            if (listaArtikalaKatalog.Count != 0) {
                listaArtikalaKatalog.Clear();
            }
            try {
                MySqlConnection connectionBaza = new MySqlConnection("server=192.168.1.11; user=root; pwd=root; database=it_shop");
                string _naziv, _kategorija, _opis = null, _proizvodjac = null, _dodatnaOprema = null, _serijskiBroj;
                int _godina, _mjeseciGarancije, _kolicina;
                double _cijena;
                string upitBaza = "SELECT * FROM artikli, stanje_proizvoda WHERE artikli.kategorija = '" + OdabranaKategorija.Kategorija + "' AND artikli.artikal_id = stanje_proizvoda.artikal_id;";

                UInt32 velicinaSlike;
                byte[] rawData;
                FileStream fs;

                MySqlDataReader r = UpitNaBazu(upitBaza, connectionBaza);
                while (r.Read()) {

                    _serijskiBroj = r.GetString("artikal_id");
                    _naziv = r.GetString("naziv");
                    _kategorija = r.GetString("kategorija");
                    _cijena = r.GetDouble("cijena");
                    _mjeseciGarancije = r.GetInt32("garancija");
                    _kategorija = r.GetString("kategorija");
                    _kolicina = r.GetInt32("kolicina");
                    _godina = r.GetInt32("godina_proizvodnje");
                    if (!r.IsDBNull(8))
                        _opis = r.GetString("opis");
                    if (!r.IsDBNull(2))
                        _proizvodjac = r.GetString("proizvodjac");

                    if (!r.IsDBNull(6))
                        _dodatnaOprema = r.GetString("dodatna_oprema");

                    if (!r.IsDBNull(10)) {
                        velicinaSlike = r.GetUInt32(r.GetOrdinal("velicina_slike"));
                        if (velicinaSlike != 0) {
                            rawData = new byte[velicinaSlike];
                            r.GetBytes(r.GetOrdinal("slika"), 0, rawData, 0, (int)velicinaSlike);
                            fs = new FileStream(@"../../Resources/tmp/artikal_" + _serijskiBroj + ".png", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                            fs.Write(rawData, 0, (int)velicinaSlike);
                            fs.Close();
                        }
                    }
                    Artikal artikal = new Artikal(_naziv, _kategorija, _godina, _cijena, _opis, _mjeseciGarancije, _proizvodjac, _dodatnaOprema, _kolicina, _serijskiBroj);
                    listaArtikalaKatalog.Add(artikal);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }
        private void UnosNovogZahtjeva ( ) {
            MySqlConnection con = new MySqlConnection("server=192.168.1.11; user=root; pwd=root; database=it_shop");
            MySqlCommand cmd = new MySqlCommand();


            try {

                string upit = "INSERT INTO zahtjevi_proizvoda (kolicina, artikal_id) VALUES " + KolicinaProizvoda + ", " + ListaArtikalaKorpa.First(U => U == OdabraniArtikalKorpa).SerijskiBroj + ";";
                MessageBox.Show(upit);
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = upit;
                cmd.ExecuteNonQuery();

                MessageBox.Show("Zahtjev uspjesno unesen.");
                PonistiIzmjene();

            } catch (System.AggregateException) {
                MessageBox.Show("Neuspjela konekcija sa bazom podataka!\nPokušajte ponovo.");
            } catch (Exception ex) {
                MessageBox.Show("Došlo je do greške!\nMolimo pokušajte ponovo ili kontaktirajte administratora!");
            }
            con.Close();

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
        private ICommand dodajUKorpuPretraga;
        private ICommand izvrsiPretragu;
        private ICommand printaj;
        private Artikal odabraniArtikalKorpa;
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


        public Artikal OdabraniArtikalKorpa {
            get { return odabraniArtikalKorpa; }
            set {

                odabraniArtikalKorpa = value;
                OnPropertyChanged("OdabraniArtikalKorpa");
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
        public ICommand DodajUKorpuPretraga {
            get { return dodajUKorpuPretraga; }
            set { dodajUKorpuPretraga = value; }
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
        private void DodajArtikalUKorpuPretraga ( ) {
            if (OdabraniArtikalKorpa != null) {
                ListaArtikalaKorpa.Add(OdabraniArtikalKorpa);
                cijenaArtikala += ListaArtikalaKorpa.First(U => U == OdabraniArtikalKorpa).Cijena;
                brojacArtikala++;
                UkupnaCijena = cijenaArtikala.ToString();
                BrojArtikala = brojacArtikala.ToString();
            }
        }
        private void UcitajArtikleIzBazePretraga ( ) {
            if (ListaArtikalaPretrage.Count != 0) {
                ListaArtikalaPretrage.Clear();
            }
            //try {
            MySqlConnection connectionBaza = new MySqlConnection("server=192.168.1.11; user=root; pwd=root; database=it_shop");
            string _naziv, _kategorija, _opis = null, _proizvodjac = null, _dodatnaOprema = null, _id;
            int _godina, _mjeseciGarancije, _kolicina;
            double _cijena;
            int tip = Int32.Parse(VrstaPretrage);
            string upitBaza = null;
            switch (tip) {
                case 0:
                    upitBaza = "SELECT * FROM artikli, stanje_proizvoda WHERE artikal_id LIKE '" + PojamZaPretragu + "%' AND artikli.artikal_id = stanje_proizvoda.artikal_id;";
                    break;
                case 1:
                    upitBaza = "SELECT * FROM artikli, stanje_proizvoda WHERE naziv LIKE '" + PojamZaPretragu + "%' AND artikli.artikal_id = stanje_proizvoda.artikal_id;";
                    break;
                case 2:
                    upitBaza = "SELECT * FROM artikli, stanje_proizvoda WHERE proizvodjac LIKE '" + PojamZaPretragu + "%' AND artikli.artikal_id = stanje_proizvoda.artikal_id;";
                    break;
            }
            MySqlDataReader r = UpitNaBazu(upitBaza, connectionBaza);
            while (r.Read()) {
                _id = r.GetString("artikal_id");
                _naziv = r.GetString("naziv");
                _kategorija = r.GetString("kategorija");
                _cijena = r.GetDouble("cijena");
                _mjeseciGarancije = r.GetInt32("garancija");
                _kategorija = r.GetString("kategorija");
                _kolicina = r.GetInt32("kolicina");
                _godina = r.GetInt32("godina_proizvodnje");
                if (!r.IsDBNull(8))
                    _opis = r.GetString("opis");
                if (!r.IsDBNull(2))
                    _proizvodjac = r.GetString("proizvodjac");

                if (!r.IsDBNull(6))
                    _dodatnaOprema = r.GetString("dodatna_oprema");

                Artikal artikal = new Artikal(_naziv, _kategorija, _godina, _cijena, _opis, _mjeseciGarancije, _proizvodjac, _dodatnaOprema, _kolicina, _id);
                ListaArtikalaPretrage.Add(artikal);
            }
            //} catch (Exception ex) {
            //    MessageBox.Show(ex.ToString());
            //}
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
        private string putanja = @"../../Resources/no_image.png";
        private string godinaProizvodnje;
        private string serijskiBroj;
        private string barKod;
        private ICommand unesiButton;
        private ICommand ponistiButton;
        private ICommand izaberiSlikuButton;
        private BitmapImage ucitajSlikuBinding;
        #endregion

        #region Properties
        public BitmapImage UcitajSlikuBinding {
            get {
                return ucitajSlikuBinding;
            }
            set {
                ucitajSlikuBinding = value;
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


        private void UnosNovogArtikla ( ) {
            MySqlConnection con = new MySqlConnection("server=192.168.1.11; user=root; pwd=root; database=it_shop");
            MySqlCommand cmd = new MySqlCommand();
            int FileSize;
            byte[] rawData;
            FileStream fs;

            try {
                string _mjeseciGarancije = MjeseciGarancije.Substring(38);
                string _kategorijaProizvoda = KategorijaProizvoda.Substring(38);
                string upit = "INSERT INTO artikli (naziv, proizvodjac, godina_proizvodnje, cijena, garancija, dodatna_oprema, kategorija, opis, slika, velicina_slike)" +
                 " VALUES ('" + NazivProizvoda + "', '" + Proizvodjac + "', " + GodinaProizvodnje + ", " + Cijena + ", " + _mjeseciGarancije + ", '" + DodatnaOprema + "', '" + KategorijaProizvoda + "', '" + Opis + "',";

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
