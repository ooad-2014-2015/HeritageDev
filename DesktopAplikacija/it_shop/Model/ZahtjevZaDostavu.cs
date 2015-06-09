using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace it_shop.Model
{
    public class ZahtjevZaDostavu
    {
        
        private string adresaDostave;
        private int idZahtjeva;

      
        
        public ZahtjevZaDostavu(string adresa, int id)
        {
            AdresaDostave = adresa;
            IdZahtjeva = id;
        }

        #region Properties
        
        public string AdresaDostave
        {
            get { return adresaDostave; }
            set { adresaDostave = value; }
        }
        
        public int IdZahtjeva
        {
            get { return idZahtjeva; }
            set { idZahtjeva = value; }
        }
        
        #endregion

    }
}
