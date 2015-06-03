using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace it_shop.Model
{
    public class ZahtjevZaPlacanjeNaRate
    {
        private int brojRata;
        private double iznosRate;

        public ZahtjevZaPlacanjeNaRate(int brojRata)
        {
            BrojRata = brojRata;
        }

        #region Properties
        public double IznosRate
        {
            get { return iznosRate; }
            set { iznosRate = value; }
        }
        
        public int BrojRata
        {
            get { return brojRata; }
            set { brojRata = value; }
        }
        #endregion

    }
}
