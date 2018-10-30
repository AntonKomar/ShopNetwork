﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShopNetwork.UI
{
    public class RelayCommand : ICommand 
    {
        readonly Predicate<object> _canExecute;
        readonly Action<object> _execute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute ?? throw new NullReferenceException();
            _canExecute = canExecute;
        }

        public RelayCommand(Action<object> execute):this(execute, null)
        {

        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) => _canExecute == null ? true : _canExecute(parameter);

        public void Execute(object parameter) => _execute.Invoke(parameter);
    }
}
