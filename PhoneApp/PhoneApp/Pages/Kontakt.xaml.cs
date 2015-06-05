using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace PhoneApp.Pages {
    public partial class Kontakt : PhoneApplicationPage {
        public Kontakt() {
            InitializeComponent();
            txt_kontakt.Items.Add("Aplikaciju radili:\nAdnan Hrnjic\nAdemir Havic\nSalem Suljkanovic\n\n");
            txt_kontakt.Items.Add("HeritageDev Team");
            txt_kontakt.Items.Add("heritagedev@gmail.com");
        }
    }
}