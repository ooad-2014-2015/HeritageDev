using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace it_shop.Model
{
    public class ZahtjevZaNabavkom
    {
        
        private List<Artikal> listaArtikala;
        private bool zahtjevOdobren;
        private string  datumZahtjeva;
        private string id;

       

        public ZahtjevZaNabavkom(List<Artikal> lista, bool odobren, string datum)
        {
            ZahtjevOdobren = odobren;
            ListaArtikala = lista;
            DatumZahtjeva = datum;

        }


        #region Properties
        public string Id 
        {
            get { return id; }
            set { id = value; }
        }
        public string DatumZahtjeva
        {
            get { return datumZahtjeva; }
            set { datumZahtjeva = value; }
        }
        
        public bool ZahtjevOdobren
        {
            get { return zahtjevOdobren; }
            set { zahtjevOdobren = value; }
        }
        
        public List<Artikal> ListaArtikala
        {
            get { return listaArtikala; }
            set { listaArtikala = value; }
        }

        #endregion

    }
}
