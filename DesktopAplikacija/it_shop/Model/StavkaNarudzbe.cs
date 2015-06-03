using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace it_shop.Model
{
    public class StavkaNarudzbe
    {
        private static int redniBroj;
        private Artikal proizvod;
        private int kolicina;
        private double popust;
        private double ukupnaCijena;


        public StavkaNarudzbe(Artikal proizvod, int kolicina, double popust)
        {
            Proizvod = proizvod;
            Kolicina = kolicina;
            Popust = popust;

        }


        #region Properties
        public double UkupnaCijena
        {
            get { return ukupnaCijena; }
            set { ukupnaCijena = value; }
        }
        
        public double Popust
        {
            get { return popust; }
            set { popust = value; }
        }
        
        public int Kolicina
        {
            get { return kolicina; }
            set { kolicina = value; }
        }
        
        public Artikal Proizvod
        {
            get { return proizvod; }
            set { proizvod = value; }
        }
        

        public static int RedniBroj
        {
            get { return redniBroj; }
            set { redniBroj = value; }
        }
        #endregion
    }
}
