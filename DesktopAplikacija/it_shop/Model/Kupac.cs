using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace it_shop.Model
{
    public class Kupac : Osoba
    {
        public Kupac(string ime, string adresa, string telefon) : base(ime, adresa, telefon)
        {
                
        }
    }
}
