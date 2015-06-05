using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace it_shop.Model {
    public class KategorijeKatalog {
        private string kategorija;

        public KategorijeKatalog ( string kategorija ) {
            Kategorija = kategorija;
        }

        public string Kategorija {
            get { return kategorija; }
            set { kategorija = value; }
        }
    }
}
