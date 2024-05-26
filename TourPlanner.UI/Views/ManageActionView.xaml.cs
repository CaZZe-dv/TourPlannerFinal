using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TourPlanner.UI.Views
{
    /// <summary>
    /// Interaktionslogik für ManageActionView.xaml
    /// </summary>
    public partial class ManageActionView : UserControl
    {
        public static readonly DependencyProperty ManageActionViewTextProperty =
            DependencyProperty.Register("ManageActionViewText", typeof(string), typeof(ManageActionView), new PropertyMetadata(string.Empty));

        public string ManageActionViewText
        {
            get { return (string)GetValue(ManageActionViewTextProperty); }
            set { SetValue(ManageActionViewTextProperty, value); }
        }

        public static readonly DependencyProperty ButtonPlusProperty =
            DependencyProperty.Register("ButtonPlus", typeof(ICommand), typeof(ManageActionView));

        public ICommand ButtonPlus
        {
            get { return (ICommand)GetValue(ButtonPlusProperty); }
            set { SetValue(ButtonPlusProperty, value); }
        }

        // Dependency property for the "ButtonMinus" command
        public static readonly DependencyProperty ButtonMinusProperty =
            DependencyProperty.Register("ButtonMinus", typeof(ICommand), typeof(ManageActionView));

        public ICommand ButtonMinus
        {
            get { return (ICommand)GetValue(ButtonMinusProperty); }
            set { SetValue(ButtonMinusProperty, value); }
        }

        // Dependency property for the "ButtonEdit" command
        public static readonly DependencyProperty ButtonEditProperty =
            DependencyProperty.Register("ButtonEdit", typeof(ICommand), typeof(ManageActionView));

        public ICommand ButtonEdit
        {
            get { return (ICommand)GetValue(ButtonEditProperty); }
            set { SetValue(ButtonEditProperty, value); }
        }

        public ManageActionView()
        {
            InitializeComponent();
        }
    }
}
