using it_shop.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace it_shop.ViewModel
{
    public class ServiserViewModel : INotifyPropertyChanged
    {
       
        public ServiserViewModel()
        {
            UnesiPredracunBtn = new RelayCommand(new Action(UnesiPredracunUBazu));
            PonistiUnosPredracunaBtn = new RelayCommand(new Action(PonistiUnosPredracuna));
            PromijeniStatusUredjaja = new RelayCommand(new Action(PromijeniStatusServisnogUredjaja));
            ObrisiUredjaj = new RelayCommand(new Action(ObrisiUredjajIzBaze));
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


        #region DodajNovi Servisni Predracun - Tab 1

        #region Atributi

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
        private string statusBarError;

       

        #endregion


        #region Properties

        public string StatusBarError
        {
            get { return statusBarError; }
            set { statusBarError = value; OnPropertyChanged("StatusBarError"); }
        }
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


        #region Metode
        //DOdat threading za bazu
        private void UnesiPredracunUBazu()
        {
            StatusBarError = String.Empty;
            try
            {
                string unosServisnogUredjaja = "INSERT INTO servisni_uredjaji (naziv, godina_proizvodnje, proizvodjac, serijski_broj, opis_kvara, cijena) values ('" + NazivProizvoda + "0'," + GodinaProizvodnjeProizvoda + ",'" + ProizvodjacProizvoda + "'," + SerijskiBrojProizvoda + ",'" + OpisKvaraProizvoda + "'," + CijenaPopravkeProizvoda + ");";
                string dajIDServisnogUredjaja = "SELECT servisni_uredjaj_id FROM servisni_uredjaji WHERE serijski_broj = " + SerijskiBrojProizvoda + ";";

                MySqlConnection connectionBaza = new MySqlConnection("server=192.168.1.127; user=root; pwd=root; database=it_shop");

                DMLUpitiNaBazu(unosServisnogUredjaja, connectionBaza);
                MySqlDataReader r = UpitNaBazu(dajIDServisnogUredjaja, connectionBaza);
                string servisniUredjID = String.Empty;
                while (r.Read())
                {
                    servisniUredjID = r.GetString("servisni_uredjaj_id");
                }
                connectionBaza.Close();

                
                string upit = "INSERT INTO kupci (ime_prezime, adresa, broj_telefon) VALUES('" + NazivKupca + "','" + AdresaKupca + "','" + BrojTelefonaKupca + "');";
                string dajIDKupca = "SELECT kupac_id FROM kupci WHERE ime_prezime = '" + NazivKupca + "' AND adresa = '" + AdresaKupca + "' AND broj_telefon = '" + BrojTelefonaKupca + "'";
               
                DMLUpitiNaBazu(upit, connectionBaza);
                MySqlDataReader r2 = UpitNaBazu(dajIDKupca, connectionBaza);
                string kupacID = String.Empty;
                while (r2.Read())
                {
                    kupacID = r2.GetString("kupac_id");
                }
                connectionBaza.Close();


                string Status = "0";
                string upitGlavni = "INSERT INTO servisni_predracuni (datum, ukupna_cijena, status, kupac_id, servisni_uredjaj_id) "
                                        + " VALUES(STR_TO_DATE('" + DateTime.Now.ToShortDateString() + "','%d.%m.%Y')," + CijenaPopravkeProizvoda + "," + Status + "," + kupacID + "," + servisniUredjID + ");";
                MessageBox.Show(upitGlavni);
                DMLUpitiNaBazu(upitGlavni, connectionBaza);
                PonistiUnosPredracuna();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                StatusBarError = "Došlo je do pogreške u sistemu!";

            }

        }
        private void PonistiUnosPredracuna()
        {
            NazivProizvoda = String.Empty;
            GodinaProizvodnjeProizvoda = String.Empty;
            ProizvodjacProizvoda = String.Empty;
            SerijskiBrojProizvoda = String.Empty;
            OpisKvaraProizvoda = String.Empty;
            CijenaPopravkeProizvoda = String.Empty;
            NazivKupca = String.Empty;
            AdresaKupca = String.Empty;
            BrojTelefonaKupca = String.Empty;

        }

        #endregion
        
        
        #endregion


        #region Pregled Uredjaja za Servisiranje - Tab 2


        #region Atributi

        private ObservableCollection<ServisniPredracun> listaServisnihUredjaja = new ObservableCollection<ServisniPredracun>();
        private ServisniPredracun odabraniUredjaj = null;
        private ICommand promijeniStatusUredjaja;
        private ICommand obrisiUredjaj;
        private int odabraniTab;
        private delegate void KreirajNitDelegat();

        #endregion


        #region Properties
       
        public int OdabraniTab
        {
            get { return odabraniTab; }
            set
            {
                odabraniTab = value;
                StatusBarError = String.Empty;
                if (OdabraniTab == 1)
                   UcitajPregledUredjajZaServisiranje();
            }
        }
        public ICommand ObrisiUredjaj
        {
            get { return obrisiUredjaj; }
            set { obrisiUredjaj = value; }
        }
        
        public ICommand PromijeniStatusUredjaja
        {
            get { return promijeniStatusUredjaja; }
            set { promijeniStatusUredjaja = value; OnPropertyChanged("PromijeniStatusUredjaja"); }
        }

        public ServisniPredracun OdabraniUredjaj
        {
            get { return odabraniUredjaj; }
            set { odabraniUredjaj = value; OnPropertyChanged("OdabraniUredjaj"); }
        }

        public ObservableCollection<ServisniPredracun> ListaServisnihUredjaja
        {
            get { return listaServisnihUredjaja; }
            set { listaServisnihUredjaja = value; OnPropertyChanged("ListaServisnihUredjaja"); }
        }

        #endregion

       
        #region Metode

        //private void KreirajNit(KreirajNitDelegat metoda)
        //{

        //    Thread nit = new Thread(()=> metoda()) {IsBackground=true};
        //    nit.Start();
        //}
       
        private void UcitajPregledUredjajZaServisiranje()
        {
            StatusBarError = String.Empty;
            ListaServisnihUredjaja.Clear();
            try
            {
                MySqlConnection connectionBaza = new MySqlConnection("server=192.168.1.127; user=root; pwd=root; database=it_shop");
                string upitBaza = "SELECT sp.datum, sp.ukupna_cijena, sp.status, k.ime_prezime, k.adresa, k.broj_telefon, su.naziv,"
                                  + " su.godina_proizvodnje, su.proizvodjac, su.serijski_broj, su.opis_kvara, su.cijena "
                                  + "FROM servisni_predracuni sp, kupci k, servisni_uredjaji su "
                                  + "WHERE sp.kupac_id = k.kupac_id AND sp.servisni_uredjaj_id = su.servisni_uredjaj_id;";
               // MessageBox.Show(upitBaza);
                MySqlDataReader r = UpitNaBazu(upitBaza, connectionBaza);
                while (r.Read())
                {
                    DateTime datum;
                    double ukupnaCijena, cijena;
                    int status, godinaProizvodnje;
                    string imePrezimeKupca, adresaKupca, brojTelefonaKupca, nazivUredjaja, proizvodjac, opisKvara, serijskiBroj;
                    bool statusUredjaja;


                    imePrezimeKupca = r.GetString("ime_prezime");
                    adresaKupca = r.GetString("adresa");
                    brojTelefonaKupca = r.GetString("broj_telefon");

                    Kupac kupac = new Kupac(imePrezimeKupca, adresaKupca, brojTelefonaKupca);

                    nazivUredjaja = r.GetString("naziv");
                    proizvodjac = r.GetString("proizvodjac");
                    opisKvara = r.GetString("opis_kvara");
                    cijena = double.Parse(r.GetString("cijena"), System.Globalization.CultureInfo.InvariantCulture);
                    godinaProizvodnje = Int32.Parse(r.GetString("godina_proizvodnje"));
                    serijskiBroj = r.GetString("serijski_broj");

                    ServisniUredjaj uredjaj = new ServisniUredjaj(nazivUredjaja, godinaProizvodnje, proizvodjac, serijskiBroj, opisKvara, cijena);

                    ukupnaCijena = double.Parse(r.GetString("ukupna_cijena"), System.Globalization.CultureInfo.InvariantCulture);
                    datum = DateTime.Parse(r.GetString("datum"), new CultureInfo("en-CA"));
                    status = Int32.Parse(r.GetString("status"));

                    if (status == 1)
                        statusUredjaja = true;
                    else
                        statusUredjaja = false;

                    ServisniPredracun predracun = new ServisniPredracun(kupac, uredjaj, datum, cijena, statusUredjaja);

                    ListaServisnihUredjaja.Add(predracun);

                }
            }
            catch (Exception ex)
            {
                
               // MessageBox.Show(ex.ToString());
                StatusBarError = "Došlo je do pogreške u sistemu!";
            }

        }

        private void PromijeniStatusServisnogUredjaja()
        {
            StatusBarError = String.Empty;
            try
            {
                MySqlConnection connectionBaza = new MySqlConnection("server=192.168.1.127; user=root; pwd=root; database=it_shop");

                int i = ListaServisnihUredjaja.IndexOf(OdabraniUredjaj);
                bool stat = false;
                
                if (!ListaServisnihUredjaja[i].Status)
                    stat = true;
                int statusBaza = 0;
                if (!ListaServisnihUredjaja[i].Status)
                    statusBaza = 1;
                
                string upitBaza = "UPDATE servisni_predracuni SET status = " + statusBaza + " WHERE servisni_uredjaj_id = "
                                   + "(SELECT s.servisni_uredjaj_id FROM servisni_uredjaji s WHERE s.serijski_broj = " + OdabraniUredjaj.ServisniUredjaj.SerijskiBroj + ");";
                //MessageBox.Show(upitBaza);
                ListaServisnihUredjaja[i].Status = stat;
                DMLUpitiNaBazu(upitBaza, connectionBaza);
                ListaServisnihUredjaja.Clear();
                UcitajPregledUredjajZaServisiranje();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                StatusBarError = "Došlo je do pogreške u sistemu!";
            }
        }

        private void ObrisiUredjajIzBaze()
        {
            StatusBarError = String.Empty;
            try
            {
                
                MySqlConnection connectionBaza = new MySqlConnection("server=192.168.1.127; user=root; pwd=root; database=it_shop");

                string upitBaza = "DELETE FROM servisni_predracuni WHERE  "
                                   + "servisni_uredjaj_id = (SELECT s.servisni_uredjaj_id FROM servisni_uredjaji s WHERE s.serijski_broj = " + OdabraniUredjaj.ServisniUredjaj.SerijskiBroj + ");";
                MessageBox.Show(upitBaza);
                ListaServisnihUredjaja.Remove(OdabraniUredjaj);
                DMLUpitiNaBazu(upitBaza, connectionBaza);
            }
            catch (Exception ex)
            {
                //StatusBarError = "Došlo je do pogreške u sistemu!";
                MessageBox.Show(ex.ToString());
            }
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
