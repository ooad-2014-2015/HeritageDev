using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace it_shop.ViewModel
{
    public class ServiserViewModel : INotifyPropertyChanged
    {
        private string nazivKupca;
        private string nazivProizvoda;
        private string godinaProizvodnjeProizvoda;
        private string proizvodjacProizvoda;
        private string serijskiBrojProizvoda;
        private string opisKvaraProizvoda;
        private string cijenaPopravkeProizvoda;
        private string datumPredajeProizvoda;
        private ICommand unesiPredracunBtn;
        private ICommand ponistiUnosPredracunaBtn;

        public ICommand PonistiUnosPredracunaBtn
        {
            get { return ponistiUnosPredracunaBtn; }
            set { ponistiUnosPredracunaBtn = value; OnPropertyChanged("PonistiUnosPredracunaBtn"); }
        }
        
        public ICommand UnesiPredracunBtn
        {
            get { return unesiPredracunBtn; }
            set { unesiPredracunBtn = value; OnPropertyChanged("UnesiPredracunBtn"); }
        }
        

       
        

        public string DatumPredajeProizvoda
        {
            get { return datumPredajeProizvoda; }
            set { datumPredajeProizvoda = value; OnPropertyChanged("DatumPredajeProizvoda"); }
        }
        
        public string CijenaPopravkeProizvoda
        {
            get { return cijenaPopravkeProizvoda; }
            set { cijenaPopravkeProizvoda = value; OnPropertyChanged("CijenaPopravkeProizvoda"); }
        }
        
        public string OpisKvaraProizvoda
        {
            get { return opisKvaraProizvoda; }
            set { opisKvaraProizvoda = value; OnPropertyChanged("OpisKvaraProizvoda"); }
        }
        
        public string SerijskiBrojProizvoda
        {
            get { return serijskiBrojProizvoda; }
            set { serijskiBrojProizvoda = value; OnPropertyChanged("SerijskiBrojProizvoda"); }
        }
        
        
        public string ProizvodjacProizvoda
        {
            get { return proizvodjacProizvoda; }
            set { proizvodjacProizvoda = value; OnPropertyChanged("ProizvodjacProizvoda"); }
        }
        
        public string GodinaProizvodnjeProizvoda
        {
            get { return godinaProizvodnjeProizvoda; }
            set { godinaProizvodnjeProizvoda = value; OnPropertyChanged("GodinaProizvodnjeProizvoda"); }
        }
        
        public string NazivProizvoda
        {
            get { return nazivProizvoda; }
            set { nazivProizvoda = value; OnPropertyChanged("NazivProizvoda"); }
        }
        
        public string NazivKupca
        {
            get { return nazivKupca; }
            set { nazivKupca = value; OnPropertyChanged("NazivKupca"); }
        }

        #region INotify Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
