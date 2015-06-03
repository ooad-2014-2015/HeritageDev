using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace it_shop.Model
{
    public class Direktor : Uposlenik
    {
        public Direktor(string ime, string adresa, string telefon, DateTime datumZasposlenja, string spol, double plata, double dodatak, int godisnji)
            : base(ime, adresa, telefon, datumZasposlenja, spol, plata, dodatak, godisnji)
        {
        }
    }
}
