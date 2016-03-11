using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace BotRetreat.Framework.Wpf
{
    public class ObservableBase : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies a property change.
        /// </summary>
        /// <param name="propertySelector">Expression to a property that has changed.</param>
        public void Notify<TModel, TValue>(Expression<Func<TModel, TValue>> propertySelector)
        {
            if (PropertyChanged != null && propertySelector != null)
            {
                var memberExpression = propertySelector.Body as MemberExpression;
                if (memberExpression != null)
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberExpression.Member.Name));
                }
            }
        }
    }

    public static class ObservableBaseExtension
    {

        public static void NotifyPropertyChanged<TModel, TValue>(this TModel observableBase, Expression<Func<TModel, TValue>> propertySelector) where TModel : ObservableBase
        {
            observableBase.Notify(propertySelector);
        }

        public static void NotifyPropertyChanged<TModel, TValue1, TValue2>(this TModel observableBase, Expression<Func<TModel, TValue1>> propertySelector1, Expression<Func<TModel, TValue2>> propertySelector2) where TModel : ObservableBase
        {
            observableBase.NotifyPropertyChanged(propertySelector1);
            observableBase.NotifyPropertyChanged(propertySelector2);
        }

        public static void NotifyPropertyChanged<TModel, TValue1, TValue2, TValue3>(this TModel observableBase, Expression<Func<TModel, TValue1>> propertySelector1, Expression<Func<TModel, TValue2>> propertySelector2, Expression<Func<TModel, TValue3>> propertySelector3) where TModel : ObservableBase
        {
            observableBase.Notify(propertySelector1);
            observableBase.Notify(propertySelector2);
            observableBase.Notify(propertySelector3);
        }

    }
}