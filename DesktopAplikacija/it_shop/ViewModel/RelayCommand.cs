using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace it_shop.ViewModel {
    public class RelayCommand : ICommand {
        private Action _action;
        public RelayCommand ( Action action ) {
            _action = action;
        }
        public event EventHandler CanExecuteChanged;
        public bool CanExecute ( object parameter ) { 
            return true; 
        }
        public bool CanExecute ( ) { 
            return true; 
        }
        public void Execute ( object parameter ) {
            if (_action != null)
                _action();
        }
    }
}
