﻿using it_shop.ViewModel;
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
using System.Windows.Shapes;

namespace it_shop.View {
    /// <summary>
    /// Interaction logic for MonterView.xaml
    /// </summary>
    public partial class MonterView : Window {
        public MonterView() {
            InitializeComponent();
            this.DataContext = new MonterViewModel();
        }
    }
}
