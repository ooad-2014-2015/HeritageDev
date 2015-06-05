using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using it_shop.ViewModel;
using System.IO;

namespace it_shop {
    /// <summary>
    /// Interaction logic for Prodavac.xaml
    /// </summary>
    public partial class ProdavacView : Window {
        public ProdavacView() {
            InitializeComponent();
            this.DataContext = new ProdavacViewModel();
        }     
    }
}
