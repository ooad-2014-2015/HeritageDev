using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace it_shop.Model
{
    public abstract class Osoba
    {
        private string PunoIme;
        private string adresa;
        private string brojTelefona;

        public Osoba(string ime, string adr, string brojTel)
        {
            PunoIme = ime;
            adresa = adr;
            brojTelefona = brojTel;
        }
    }
}
