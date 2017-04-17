using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace YorgiControls.Utils
{
    public class Command<T> : ICommand
    {
        public Predicate<T> CanExecuteDelegate { get; set; }
        public Action<T> ExecuteDelegate { get; set; }

        public Command(Action<T> execute)
        {
            this.ExecuteDelegate = execute;
        }

        public Command(Action<T> execute, Predicate<T> canExecute)
        {
            this.ExecuteDelegate = execute;
            this.CanExecuteDelegate = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return CanExecuteDelegate == null || CanExecuteDelegate((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            if (ExecuteDelegate != null) ExecuteDelegate((T)parameter);
        }
    }

    public class Command : ICommand
    {
        public Func<bool> CanExecuteDelegate { get; set; }
        public Action ExecuteDelegate { get; set; }

        public Command(Action execute)
        {
            this.ExecuteDelegate = execute;
        }

        public Command(Action execute, Func<bool> canExecute)
        {
            this.ExecuteDelegate = execute;
            this.CanExecuteDelegate = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (CanExecuteDelegate != null) return CanExecuteDelegate();
            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            if (ExecuteDelegate != null) ExecuteDelegate();
        }
    }
}
