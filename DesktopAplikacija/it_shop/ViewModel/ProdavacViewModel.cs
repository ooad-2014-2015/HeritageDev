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
        private Image ucitajSliku;

        private string putanja;
        public Image UcitajSlikuBinding {
            get {
                if (putanja != "") {
                    BitmapImage bitmap = new BitmapImage(new Uri(putanja, UriKind.RelativeOrAbsolute));
                    ucitajSliku.Source = bitmap;
                } 
                return ucitajSliku; 
            }
            set { 
                ucitajSliku = value;
                OnPropertyChanged("UcitajSlikuBinding");
            }
        }
       
        #region Properties
        public ICommand IzaberiSlikuButton {
            get { return ucitajsliku; }
            set { ucitajsliku = value; }
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

        public ProdavacViewModel ( ) {
            UnesiButton = new RelayCommand(new Action(UnosArtikla));
            PonistiButton = new RelayCommand(new Action(PonistiIzmjene));
            IzaberiSlikuButton = new RelayCommand(new Action(IzaberiSliku));
            //UcitajSlikuBinding = new RelayCommand(new Action(UcitajSliku));
            putanja = "";
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
            }
            //slika.Source = new BitmapImage(new Uri(putanja, UriKind.RelativeOrAbsolute));
            //MessageBox.Show(putanja);
        }

        private void UnosArtikla ( ) {
            
            MySqlConnection con = new MySqlConnection("server=192.168.1.11; user=root; pwd=root; database=it_shop");
            try {
                string _mjeseciGarancije = MjeseciGarancije.Substring(37);

                string _kategorijaProizvoda = KategorijaProizvoda.Substring(37);
                MessageBox.Show(_kategorijaProizvoda);
                MessageBox.Show(_mjeseciGarancije);


                string upit = "INSERT INTO artikli VALUES (" + IdProizvoda + ", '" + NazivProizvoda + "', '" + _kategorijaProizvoda + "', " + Cijena + ", '" + Opis + "', " + _mjeseciGarancije + ", '" + Proizvodjac + "', '" + DodatnaOprema + "', " + Kolicina + ", null)";

                MessageBox.Show(upit);

                //Task<MySqlDataReader> nit = Task<MySqlDataReader>.Factory.StartNew(( ) => UpitNaBazu(upit, con));
                con.Open();
                MySqlCommand cmd = new MySqlCommand(upit, con);
                cmd.ExecuteNonQuery();


            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            //} catch (System.AggregateException) {
            //    MessageBox.Show("Neuspjela konekcija sa bazom podataka!\nPokušajte ponovo.");
            //} catch (Exception ex) {
            //    MessageBox.Show("Došlo je do greške!\nMolimo pokušajte ponovo ili kontaktirajte administratora!");
            }
            con.Close();

        }

        //public void KreirajNit ( ) {
        //    Thread nit = new Thread(( ) => ValidirajLogin()) { IsBackground = true };
        //    nit.Join();
        //}

        private MySqlDataReader UpitNaBazu ( string upit, MySqlConnection con ) {
            con.Open();
            MySqlCommand u = new MySqlCommand(upit, con);
            return u.ExecuteReader();
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

        public ICommand ucitajsliku { get; set; }
    }
}
