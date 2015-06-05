using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace it_shop.ViewModel
{
    public class ServiserViewModel : INotifyPropertyChanged
    {
        private string nazivProizvoda;
        private string godinaProizvodnjeProizvoda;
        private string proizvodjacProizvoda;
        private string serijskiBrojProizvoda;
        private string opisKvaraProizvoda;
        private string cijenaPopravkeProizvoda;
        private string datumPredajeProizvoda;
        private ICommand unesiPredracunBtn;
        private ICommand ponistiUnosPredracunaBtn;
        private string nazivKupca;
        private string brojTelefonaKupca;
        private string adresaKupca;

         private void UnesiPredracunUBazu()
        {
            try
            {
                string unosServisnogUredjaja = "INSERT INTO servisni_uredjaj (naziv, godina_proizvodnje, proizvodjac, serijski_broj, opis_kvara, cijena) values ('" + NazivProizvoda + "0'," + GodinaProizvodnjeProizvoda + ",'" + ProizvodjacProizvoda + "'," + SerijskiBrojProizvoda + ",'" + OpisKvaraProizvoda + "'," + CijenaPopravkeProizvoda + ");";
                string dajIDServisnogUredjaja = "SELECT servisni_uredjaj_id FROM servisni_uredjaji WHERE serijski_broj = " + SerijskiBrojProizvoda + ";";

                MySqlConnection connectionBaza = new MySqlConnection("server=192.168.1.11; user=root; pwd=root; database=it_shop");
                DMLUpitiNaBazu(unosServisnogUredjaja, connectionBaza);
                MySqlDataReader r = UpitNaBazu(dajIDServisnogUredjaja, connectionBaza);
                string servisniUredjID = String.Empty;
                while (r.Read())
                {
                    servisniUredjID = r.GetString("servisni_uredjaj_id");
                }
                connectionBaza.Close();


                string upit = "INSERT INTO kupci (ime_prezime, adresa, broj_telefon) VALUES('" + NazivKupca + "','" + AdresaKupca + "','" + BrojTelefonaKupca + "');";
                string dajIDKupca = "SELECT kupac_id FROM kupaci WHERE ime_prezime = '" + NazivKupca + "' AND adresa = '" + AdresaKupca + "' AND broj_telefon = '" + BrojTelefonaKupca + "'";
                DMLUpitiNaBazu(upit, connectionBaza);
                MySqlDataReader r2 = UpitNaBazu(dajIDKupca, connectionBaza);
                string kupacID = String.Empty;
                while (r2.Read())
                {
                    kupacID = r2.GetString("kupac_id");
                }
                connectionBaza.Close();


                string Status = "0";
                string upitGlavni = "INSERT INTO servisni_predracuni (datum, ukupna_cijena, status, kupac_id, servisni_uredjaj_id) + "
                                        + " VALUES(STR_TO_DATE('" + DateTime.Now.ToShortDateString() + "','%d.%m.%Y')," + CijenaPopravkeProizvoda + "," + Status + "," + kupacID + "," + servisniUredjID + ");";

                DMLUpitiNaBazu(upitGlavni, connectionBaza);
                PonistiUnosPredracuna();
            }
            catch(Exception)
            {
                MessageBox.Show("Unos podataka u bazu nije uspio!");

            }
            
            }
         private void PonistiUnosPredracuna()
         {
             NazivProizvoda = string.Empty;
             GodinaProizvodnjeProizvoda = string.Empty;
             ProizvodjacProizvoda = string.Empty;
             SerijskiBrojProizvoda = string.Empty;
             OpisKvaraProizvoda = string.Empty;
             CijenaPopravkeProizvoda = string.Empty;
             NazivKupca = String.Empty;
             AdresaKupca = String.Empty;
             BrojTelefonaKupca = String.Empty;
         }



        public ServiserViewModel()
        {
            UnesiPredracunBtn = new RelayCommand(new Action(UnesiPredracunUBazu));
            PonistiUnosPredracunaBtn = new RelayCommand(new Action(PonistiUnosPredracuna));
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
            con.Close();
        }


        #region Properties

        public string AdresaKupca
        {
            get { return adresaKupca; }
            set { adresaKupca = value; OnPropertyChanged("AdresaKupca"); }
        }
        
                        
        public string BrojTelefonaKupca
        {
            get { return brojTelefonaKupca; }
            set { brojTelefonaKupca = value; OnPropertyChanged("BrojTelefonaKupca"); }
        }
        

        public ICommand PonistiUnosPredracunaBtn
        {
            get { return ponistiUnosPredracunaBtn; }
            set { ponistiUnosPredracunaBtn = value; OnPropertyChanged("PonistiUnosPredracunaBtn"); }
        }
        
        public ICommand UnesiPredracunBtn
        {
            get { return unesiPredracunBtn; }
            set { unesiPredracunBtn = value; OnPropertyChanged("UnesiPredracunBtn"); }
        }
        
        public string DatumPredajeProizvoda
        {
            get { return datumPredajeProizvoda; }
            set { datumPredajeProizvoda = value; OnPropertyChanged("DatumPredajeProizvoda"); }
        }
        
        public string CijenaPopravkeProizvoda
        {
            get { return cijenaPopravkeProizvoda; }
            set { cijenaPopravkeProizvoda = value; OnPropertyChanged("CijenaPopravkeProizvoda"); }
        }
        
        public string OpisKvaraProizvoda
        {
            get { return opisKvaraProizvoda; }
            set { opisKvaraProizvoda = value; OnPropertyChanged("OpisKvaraProizvoda"); }
        }
        
        public string SerijskiBrojProizvoda
        {
            get { return serijskiBrojProizvoda; }
            set { serijskiBrojProizvoda = value; OnPropertyChanged("SerijskiBrojProizvoda"); }
        }
        
        
        public string ProizvodjacProizvoda
        {
            get { return proizvodjacProizvoda; }
            set { proizvodjacProizvoda = value; OnPropertyChanged("ProizvodjacProizvoda"); }
        }
        
        public string GodinaProizvodnjeProizvoda
        {
            get { return godinaProizvodnjeProizvoda; }
            set { godinaProizvodnjeProizvoda = value; OnPropertyChanged("GodinaProizvodnjeProizvoda"); }
        }
        
        public string NazivProizvoda
        {
            get { return nazivProizvoda; }
            set { nazivProizvoda = value; OnPropertyChanged("NazivProizvoda"); }
        }
        
        public string NazivKupca
        {
            get { return nazivKupca; }
            set { nazivKupca = value; OnPropertyChanged("NazivKupca"); }
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
    }
}
