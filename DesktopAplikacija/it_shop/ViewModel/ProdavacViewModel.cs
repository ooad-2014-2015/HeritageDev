using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Threading;
using System.Threading.Tasks;

namespace it_shop.ViewModel {
    class ProdavacViewModel : INotifyPropertyChanged {

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
        //dodati sliku

        #region Properties
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
            set { kolicina = value; }
        }
        public string DodatnaOprema {
            get { return dodatnaOprema; }
            set { dodatnaOprema = value; }
        }
        public string Proizvodjac {
            get { return proizvodjac; }
            set { proizvodjac = value; }
        }
        public string MjeseciGarancije {
            get { return mjeseciGarancije; }
            set { mjeseciGarancije = value; }
        }
        public string Cijena {
            get { return cijena; }
            set { cijena = value; }
        }
        public string Opis {
            get { return opis; }
            set { opis = value; }
        }
        public string KategorijaPRoizvoda {
            get { return kategorijaProizvoda; }
            set { kategorijaProizvoda = value; }
        }
        public string NazivProizvoda {
            get { return nazivProizvoda; }
            set { nazivProizvoda = value; }
        }
        public string IdProizvoda {
            get { return idProizvoda; }
            set { idProizvoda = value; }
        }
        #endregion Properties

        public ProdavacViewModel ( ) {
            UnesiButton = new RelayCommand(new Action(UnosArtikla));
            PonistiButton = new RelayCommand(new Action(PonistiIzmjene));
        }

        private void UnosArtikla ( ) {
            MessageBox.Show(MjeseciGarancije.ToString());
        }

        private void PonistiIzmjene ( ) {
            IdProizvoda = string.Empty;
            NazivProizvoda = string.Empty;
            KategorijaPRoizvoda = string.Empty;
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
    }
}
