using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace it_shop.Model
{
    public abstract class Racun
    {
        private static int idRacuna;
        private Kupac kupac;
        private double ukupnaCijena;

        public Racun(Kupac kup)
        {
            Kupac = kup;
        }


        #region Properties
        public double UkupnaCijena
        {
            get { return ukupnaCijena; }
            set { ukupnaCijena = value; }
        }
        
        public Kupac Kupac
        {
            get { return kupac; }
            set { kupac = value; }
        }
        
        public static int IdRacuna
        {
            get { return idRacuna; }
            set { idRacuna = value; }
        }
        #endregion

    }
}
