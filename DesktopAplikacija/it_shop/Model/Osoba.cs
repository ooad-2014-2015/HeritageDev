using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace it_shop.Model
{
    public abstract class Osoba
    {
        private string punoIme;
        private string adresa;
        private string brojTelefona;

        #region Properties

        public string PunoIme
        {
            get { return punoIme; }
            set { punoIme = value; }
        }
        public string Adresa
        {
            get { return adresa; }
            set { adresa = value; }
        }
        public string BrojTelefona
        {
            get { return brojTelefona; }
            set { brojTelefona = value; }
        }

        #endregion

        public Osoba(string ime, string adr, string brojTel)
        {
            PunoIme = ime;
            Adresa = adr;
            BrojTelefona = brojTel;
        }

      
    }
}
