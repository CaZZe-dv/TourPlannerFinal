// HeaderBarView.xaml.cs
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TourPlanner.UI.Views
{
    public partial class HeaderBarView : UserControl
    {
        public static readonly DependencyProperty FileCommandProperty =
            DependencyProperty.Register("FileCommand", typeof(ICommand), typeof(HeaderBarView));

        public ICommand FileCommand
        {
            get { return (ICommand)GetValue(FileCommandProperty); }
            set { SetValue(FileCommandProperty, value); }
        }

        public static readonly DependencyProperty EditCommandProperty =
            DependencyProperty.Register("EditCommand", typeof(ICommand), typeof(HeaderBarView));

        public ICommand EditCommand
        {
            get { return (ICommand)GetValue(EditCommandProperty); }
            set { SetValue(EditCommandProperty, value); }
        }

        public static readonly DependencyProperty OptionsCommandProperty =
            DependencyProperty.Register("OptionsCommand", typeof(ICommand), typeof(HeaderBarView));

        public ICommand OptionsCommand
        {
            get { return (ICommand)GetValue(OptionsCommandProperty); }
            set { SetValue(OptionsCommandProperty, value); }
        }

        public static readonly DependencyProperty HelpCommandProperty =
            DependencyProperty.Register("HelpCommand", typeof(ICommand), typeof(HeaderBarView));

        public ICommand HelpCommand
        {
            get { return (ICommand)GetValue(HelpCommandProperty); }
            set { SetValue(HelpCommandProperty, value); }
        }

        public HeaderBarView()
        {
            InitializeComponent();
        }
    }
}
