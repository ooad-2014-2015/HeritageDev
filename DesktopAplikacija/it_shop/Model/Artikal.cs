using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace it_shop.Model
{
    public class Artikal : Proizvod
    {
        private readonly int barKod;
        private double cijena;
        private int mjeseciGarancije;
        private string dodatnaOprema;

        public Artikal(string naziv, DateTime godinaProizvodnje, string proizvodjac, string serijskiBroj, double cijena, int barkod, int mjeseciGarancije, string dodatnaOprema)
            : base(naziv, godinaProizvodnje, proizvodjac, serijskiBroj)
        {
            this.barKod = barkod;
            Cijena = cijena;
            MjeseciGarancije = mjeseciGarancije;
            DodatnaOprema = dodatnaOprema;

        }

        #region Properties
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
        
        public int BarKod
        {
            get { return barKod; }

        }
        #endregion

    }
}
