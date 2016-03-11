using System;
using System.Windows.Input;

namespace BotRetreat.Framework.Wpf
{
    public interface IRelayCommand : ICommand
    {
        void RaiseCanExecuteChanged();
    }

    public class RelayCommand : IRelayCommand
    {
        private readonly Action _command;
        private readonly Func<Boolean> _canExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action command, Func<Boolean> canExecute = null)
        {
            _command = command;
            _canExecute = canExecute;
        }

        public Boolean CanExecute(Object parameter)
        {
            return _canExecute?.Invoke() ?? true;
        }

        public void Execute(Object parameter)
        {
            _command();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public class RelayCommand<T> : ICommand where T : class
    {
        private readonly Action<T> _command;
        private readonly Func<Boolean> _canExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<T> command, Func<Boolean> canExecute = null)
        {
            _command = command;
            _canExecute = canExecute;
        }

        public Boolean CanExecute(Object parameter)
        {
            return _canExecute?.Invoke() ?? true;
        }

        public void Execute(Object parameter)
        {
            _command(parameter as T);
        }
    }
}