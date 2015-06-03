using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace it_shop.Model
{
    public class ProdajniRacun : Racun
    {
        private List<StavkaNarudzbe> listaStavkiNarudzbe;
        private double popust;
        private ZahtjevZaPlacanjeNaRate zahtjevRata;
        private ZahtjevZaDostavu dostavaProizvoda;


        public ProdajniRacun(Kupac kupac, StavkaNarudzbe stavka, double popust) 
            : base(kupac)
        {
            ListaStavkiNarudzbe.Add(stavka);
            Popust = popust;
        }


        #region Properties
        public ZahtjevZaDostavu DostavaProizvoda
        {
            get { return dostavaProizvoda; }
            set { dostavaProizvoda = value; }
        }
        

        public ZahtjevZaPlacanjeNaRate ZahtjevRata
        {
            get { return zahtjevRata; }
            set { zahtjevRata = value; }
        }
        
        public double Popust
        {
            get { return popust; }
            set { popust = value; }
        }
        
        public List<StavkaNarudzbe> ListaStavkiNarudzbe
        {
            get { return listaStavkiNarudzbe; }
            set { listaStavkiNarudzbe = value; }
        }
        #endregion

    }
}
