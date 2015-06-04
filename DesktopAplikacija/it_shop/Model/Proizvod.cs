using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace it_shop.Model
{
    public abstract class Proizvod
    {
        private string naziv;
        private int godinaProizvodnje;
        private string proizvodjac;
        private string serijskiBroj;

        public Proizvod(string naziv, int godinaPro, string proizvodjac, string serijski)
        {
            this.naziv = naziv;
            this.godinaProizvodnje = godinaPro;
            this.proizvodjac = proizvodjac;
            this.serijskiBroj = serijski;
        }

        #region Properties

        public string Naziv
        {
            get { return naziv; }
            set { naziv = value; }
        }
        public int GodinaProizvodnje
        {
            get { return godinaProizvodnje; }
            set { godinaProizvodnje = value; }
        }

        public string Proizvodjac
        {
            get { return proizvodjac; }
            set { proizvodjac = value; }
        }

        public string SerijskiBroj
        {
            get { return serijskiBroj; }
            set { serijskiBroj = value; }
        }
        #endregion



    }
}

            