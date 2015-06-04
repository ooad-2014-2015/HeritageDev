using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace it_shop.Model
{
    public class ServisniUredjaj : Proizvod
    {
        private string opisKvara;
        private double cijenaPopravke;

        public ServisniUredjaj(string naziv, int godinaProizvodnje, string proizvodjac, string serijskiBroj, string opisKvara, double cijena) 
            : base(naziv, godinaProizvodnje, proizvodjac, serijskiBroj)
        {
            CijenaPopravke = cijena;
            OpisKvara = opisKvara;
        }

        #region Properties
        public double CijenaPopravke
        {
            get { return cijenaPopravke; }
            set { cijenaPopravke = value; }
        }
        
        public string OpisKvara
        {
            get { return opisKvara; }
            set { opisKvara = value; }
        }

        #endregion

    }
}
