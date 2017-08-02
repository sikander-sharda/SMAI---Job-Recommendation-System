using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyProj.Command
{
    class RelayCommand : ICommand
    {
        Action<object> executeMethod;
        Func<object, bool> canexecuteMethod;
        bool canExecuteCache;


        public RelayCommand(Action<object> executeMethod, Func<object, bool> canexecuteMethod, bool canExecuteCache)
        {
            this.executeMethod = executeMethod;
            this.canexecuteMethod = canexecuteMethod;
            this.canExecuteCache = canExecuteCache;
        }

        public bool CanExecute(object parameter)
        {
            if (canexecuteMethod == null)
                return true;
            else
                return canexecuteMethod(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                //if(!canExecuteCache)
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                //if (!canExecuteCache)
                CommandManager.RequerySuggested -= value;
            }
        }

        public void Execute(object parameter)
        {
            executeMethod(parameter);
        }
    }
}
