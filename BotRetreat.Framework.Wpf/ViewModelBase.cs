using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BotRetreat.Framework.Wpf
{
    public class ViewModelBase : ObservableBase
    {
        private String _lastExceptionMessage;

        public String LastExceptionMessage
        {
            get { return _lastExceptionMessage; }
            set
            {
                _lastExceptionMessage = value;
                this.NotifyPropertyChanged(x => x.LastExceptionMessage);
            }
        }

        public ICommand ClearLastExceptionMessageCommand { get; }

        public ViewModelBase()
        {
            ClearLastExceptionMessageCommand = new RelayCommand(OnClearLastExceptionMessage);
        }

        private void OnClearLastExceptionMessage()
        {
            LastExceptionMessage = null;
        }

        protected async Task ExceptionHandling(Func<Task> action)
        {
            try
            {
                await action();
            }
            catch (Exception ex)
            {
                LastExceptionMessage = ex.Message;
            }
        }

        protected async Task IgnoreExceptions(Func<Task> action)
        {
            try
            {
                await action();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}