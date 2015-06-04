using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace it_shop.Model
{
    public class ServisniPredracun : Racun
    {
        private ServisniUredjaj servisniUredjaj;
        private DateTime datumPredaje;

        public ServisniPredracun(Kupac kupac, ServisniUredjaj serUredjaj, DateTime datumPredaje) 
            : base(kupac)
        {
            ServisniUredjaj = serUredjaj;
            DatumPredaje = datumPredaje;
        }

        #region Properties
        public DateTime DatumPredaje
        {
            get { return datumPredaje; }
            set { datumPredaje = value; }
        }

        public ServisniUredjaj ServisniUredjaj
        {
            get { return servisniUredjaj; }
            set { servisniUredjaj = value; }
        }
        #endregion

    }
}
