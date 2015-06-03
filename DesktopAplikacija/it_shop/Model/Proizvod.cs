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
        private DateTime godinaProizvodnje;
        private string proizvodjac;
        private string serijskiBroj;
        public Proizvod(string naziv, DateTime godinaPro, string proizvodjac, string serijski)
        {
            this.naziv = naziv;
            this.godinaProizvodnje = godinaPro;
            this.proizvodjac = proizvodjac;
            this.serijskiBroj = serijski;

        }


      
    }
}

            