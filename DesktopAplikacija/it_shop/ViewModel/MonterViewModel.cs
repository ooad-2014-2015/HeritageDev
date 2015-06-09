using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using it_shop.Model;
using System.Windows;

namespace it_shop.ViewModel
{
    class MonterViewModel: INotifyPropertyChanged
    {

        private ObservableCollection<ZahtjevZaDostavu> listaZahtjevaZaDostavom = new ObservableCollection<ZahtjevZaDostavu>();
        private ZahtjevZaDostavu odabraniZahtjevDostave = null;
        private ObservableCollection<Artikal> listaZahtjevaArtikala = new ObservableCollection<Artikal>();

        public ObservableCollection<Artikal> ListaZahtjevaArtikala
        {
            get { return listaZahtjevaArtikala; }
            set { listaZahtjevaArtikala = value; OnPropertyChanged("ListaArtikala"); }
        }
        
        public ZahtjevZaDostavu OdabraniZahtjevDostave
        {
            get { return odabraniZahtjevDostave; }
            set 
            { 
                odabraniZahtjevDostave = value; 
                OnPropertyChanged("OdabraniZahtjevDostave");
                UcitajInformacijeZahtjeva();
            }
        }
        
        public ObservableCollection<ZahtjevZaDostavu> ListaZahtjevaZaDostavom
        {
            get { return listaZahtjevaZaDostavom; }
            set { listaZahtjevaZaDostavom = value; OnPropertyChanged("ListaZahtjevaZaDostavom"); }
        }
        

        public MonterViewModel()
        {
            UcitajZahtjeveZaDostavu();
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

        private void UcitajZahtjeveZaDostavu()
        {
            try
            {
                MySqlConnection connectionBaza = new MySqlConnection("server=192.168.1.127; user=root; pwd=root; database=it_shop");
                string upitBaza = "SELECT zd.adresa, zd.zahtjev_dostave_id FROM zahtjevi_dostave zd;";
                // MessageBox.Show(upitBaza);
                MySqlDataReader r = UpitNaBazu(upitBaza, connectionBaza);
                while (r.Read())
                {
                    string adresa = r.GetString("adresa");
                    int id = Int32.Parse(r.GetString("zahtjev_dostave_id"));

                    ZahtjevZaDostavu zah = new ZahtjevZaDostavu(adresa, id);
                    ListaZahtjevaZaDostavom.Add(zah);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void UcitajInformacijeZahtjeva()
        {
            try
            {
                MySqlConnection connectionBaza = new MySqlConnection("server=192.168.1.127; user=root; pwd=root; database=it_shop");
                string upitBaza = "SELECT artikal_id FROM artikli_zahtjevi_dostave WHERE zahtjev_dostave_id = " + OdabraniZahtjevDostave.IdZahtjeva + ";";
                // MessageBox.Show(upitBaza);
                MySqlDataReader r = UpitNaBazu(upitBaza, connectionBaza);
                while (r.Read())
                {
                    MySqlConnection connectionBaza1 = new MySqlConnection("server=192.168.1.127; user=root; pwd=root; database=it_shop");
                    string upitBaza1 = "SELECT naziv, proizvodjac, godina_proizvodnje, cijena, garancija, dodatna_oprema, kategorija, opis" +
                                        " FROM artikli a WHERE artikal_id = " + r.GetString("artikal_id") + ";";

                    MySqlDataReader r1 = UpitNaBazu(upitBaza1, connectionBaza1);
                    while (r1.Read())
                    {
                        Artikal tmp = new Artikal(r1.GetString("naziv"), r1.GetString("kategorija"), Int32.Parse(r1.GetString("godina_proizvodnje")), Double.Parse(r1.GetString("cijena")), r1.GetString("opis"),
                                                    Int32.Parse(r1.GetString("garancija")), r1.GetString("proizvodjac"), r1.GetString("dodatna_oprema"), Int32.Parse(r1.GetString("naziv")), r1.GetString("naziv"));

                        ListaZahtjevaArtikala.Add(tmp);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        


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
