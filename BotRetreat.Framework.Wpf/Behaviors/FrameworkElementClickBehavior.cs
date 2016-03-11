using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace BotRetreat.Framework.Wpf.Behaviors
{
    public class FrameworkElementClickBehavior : Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            AssociatedObject.PreviewMouseDown += (sender, args) => { Command?.Execute(null); };
        }

        public ICommand Command
        {
            get
            {
                return (ICommand)GetValue(CommandProperty);
            }
            set
            {
                SetValue(CommandProperty, value);
            }
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(FrameworkElementClickBehavior), new PropertyMetadata(null));
    }
}