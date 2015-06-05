using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace it_shop.Model
{
    public class Artikal : Proizvod
    {
        private readonly string barKod;
        private double cijena;
        private int mjeseciGarancije;
        private string dodatnaOprema;
        private string kategorija;
        private string opis;
        private int kolicina;
        public Artikal(string naziv, string kategorija, int godinaProizvodnje, double cijena, string opis, int mjeseciGarancije, string proizvodjac, string dodatnaOprema, int kolicina, string serijskiBroj, string barKod)
            : base(naziv, godinaProizvodnje, proizvodjac, serijskiBroj)
        {
            Kategorija = kategorija;
            Cijena = cijena;
            Opis = opis;
            MjeseciGarancije = mjeseciGarancije;
            DodatnaOprema = dodatnaOprema;
            Kolicina = kolicina;
            SerijskiBroj = serijskiBroj;
        }

        #region Properties
        public int Kolicina {
            get { return kolicina; }
            set { kolicina = value; }
        }
        public string Opis {
            get { return opis; }
            set { opis = value; }
        }
        public string Kategorija {
            get { return kategorija; }
            set { kategorija = value; }
        }
        public string DodatnaOprema
        {
            get { return dodatnaOprema; }
            set { dodatnaOprema = value; }
        }
        public int MjeseciGarancije
        {
            get { return mjeseciGarancije; }
            set { mjeseciGarancije = value; }
        }
        
        public double Cijena
        {
            get { return cijena; }
            set { cijena = value; }
        }
        
        public string BarKod
        {
            get { return barKod; }
        }
        #endregion

    }
}
