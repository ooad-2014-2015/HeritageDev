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

namespace it_shop.ViewModel {
    public class ProdavacViewModel : INotifyPropertyChanged {

        private string idProizvoda;
        private string nazivProizvoda;
        private string kategorijaProizvoda;
        private string opis;
        private string cijena;
        private string mjeseciGarancije;
        private string proizvodjac;
        private string dodatnaOprema;
        private string kolicina;
        private ICommand unesiButton;
        private ICommand ponistiButton;
        private ICommand izaberiSlikuButton;
        private BitmapImage ucitajSliku;
        private string putanja;

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
        public string IdProizvoda {
            get { return idProizvoda; }
            set { 
                idProizvoda = value;
                OnPropertyChanged("IdProizvoda");
            }
        }
        #endregion Properties

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

        public ProdavacViewModel ( ) {
            UnesiButton = new RelayCommand(new Action(UnosNovogArtikla));
            PonistiButton = new RelayCommand(new Action(PonistiIzmjene));
            IzaberiSlikuButton = new RelayCommand(new Action(IzaberiSliku));
            UcitajSlikuBinding = UcitajSliku(@"/it_shop;component/Resources/no_image.png");
        }
        private BitmapImage UcitajSliku( string _putanja ) {
            BitmapImage b = new BitmapImage();
            b.BeginInit();
            //b.UriSource = new Uri(System.IO.Path.GetFullPath(@"../../Resources/no_image.png"), UriKind.RelativeOrAbsolute);
            b.UriSource = new Uri(_putanja, UriKind.RelativeOrAbsolute);
            b.EndInit();
            return b;
        }
        private void UpisiSliku ( ) {
            MySqlConnection conn;
            MySqlCommand cmd;

            conn = new MySqlConnection();
            cmd = new MySqlCommand();

            string SQL;
            int FileSize;
            byte[] rawData;
            FileStream fs;

            conn.ConnectionString = "server=192.168.1.11;uid=root;pwd=root;database=it_shop;";

            try {
                fs = new FileStream(putanja, FileMode.Open, FileAccess.Read);
                FileSize = Convert.ToInt32(fs.Length);

                rawData = new byte[FileSize];
                fs.Read(rawData, 0, FileSize);
                fs.Close();

                conn.Open();

                SQL = "INSERT INTO slika VALUES(@FileName, @FileSize, @File)";

                cmd.Connection = conn;
                cmd.CommandText = SQL;
                cmd.Parameters.AddWithValue("@FileName", "test");
                cmd.Parameters.AddWithValue("@FileSize", FileSize);
                cmd.Parameters.AddWithValue("@File", rawData);

                cmd.ExecuteNonQuery();

                MessageBox.Show("File Inserted into database successfully!",
                    "Success!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                conn.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex) {
                MessageBox.Show("Error " + ex.Number + " has occurred: " + ex.Message,
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void IzaberiSliku ( ) {
            Microsoft.Win32.OpenFileDialog openFileDialog1 = new Microsoft.Win32.OpenFileDialog();
            openFileDialog1.Filter = "JPEG Files(.png)|*.jpg|All Files (*.*)|*.*";
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
                string _mjeseciGarancije = MjeseciGarancije.Substring(37);
                string _kategorijaProizvoda = KategorijaProizvoda.Substring(37);
                string upit = "INSERT INTO artikli VALUES (" + IdProizvoda + ", '" + NazivProizvoda + "', '" + _kategorijaProizvoda + "', " + Cijena + ", '" + Opis + "', " + _mjeseciGarancije + ", '" + Proizvodjac + "', '" + DodatnaOprema + "', " + Kolicina + ", ";

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
            IdProizvoda = string.Empty;
            NazivProizvoda = string.Empty;
            KategorijaProizvoda = string.Empty;
            Cijena = string.Empty;
            Opis = string.Empty;
            MjeseciGarancije = string.Empty;
            Proizvodjac = string.Empty;
            DodatnaOprema = string.Empty;
            Kolicina = string.Empty;
            UcitajSlikuBinding = UcitajSliku(@"/it_shop;component/Resources/no_image.png");
        }


    }
}
