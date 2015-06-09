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
        private bool status;

        public ServisniPredracun(Kupac kupac, ServisniUredjaj serUredjaj, DateTime datumPredaje, double cijena, bool status) 
            : base(kupac, cijena)
        {
            ServisniUredjaj = serUredjaj;
            DatumPredaje = datumPredaje;
            Status = status;
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

        public bool Status 
        {
            get { return status; }
            set { status = value; }
        }
        #endregion

    }
}
