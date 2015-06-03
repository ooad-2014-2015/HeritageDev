using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace it_shop.Model
{
    public class ServisniPredracun : Racun
    {
        private List<ServisniUredjaj> listaServisnihUredjaj;
        private DateTime datumPredaje;

        public ServisniPredracun(Kupac kupac, List<ServisniUredjaj> lista, DateTime datumPredaje) 
            : base(kupac)
        {
            ListaServisnihUredjaj = listaServisnihUredjaj;
            DatumPredaje = datumPredaje;
        }

        #region Properties
        public DateTime DatumPredaje
        {
            get { return datumPredaje; }
            set { datumPredaje = value; }
        }
        
        public List<ServisniUredjaj> ListaServisnihUredjaj
        {
            get { return listaServisnihUredjaj; }
            set { listaServisnihUredjaj = value; }
        }
        #endregion

    }
}
