using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace it_shop.Model
{
    public class ZahtjevZaDostavu
    {
        private string adresaDostave;

        public ZahtjevZaDostavu(string adresa)
        {
            AdresaDostave = adresa;
        }

        #region Properties
        public string AdresaDostave
        {
            get { return adresaDostave; }
            set { adresaDostave = value; }
        }
        #endregion

    }
}
