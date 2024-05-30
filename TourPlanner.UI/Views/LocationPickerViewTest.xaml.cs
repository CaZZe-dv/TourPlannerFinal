using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TourPlanner.UI.Views
{
    /// <summary>
    /// Interaktionslogik für LocationPickerViewTest.xaml
    /// </summary>
    public partial class LocationPickerViewTest : UserControl
    {

        public static readonly DependencyProperty FromProperty =
            DependencyProperty.Register("From", typeof(string), typeof(LocationPickerViewTest));

        public static readonly DependencyProperty ToProperty =
            DependencyProperty.Register("To", typeof(string), typeof(LocationPickerViewTest));

        public string From
        {
            get { return (string)GetValue(FromProperty); }
            set { SetValue(FromProperty, value); }
        }

        public string To
        {
            get { return (string)GetValue(ToProperty); }
            set { SetValue(ToProperty, value); }
        }
        public LocationPickerViewTest()
        {
            InitializeComponent();
        }
    }
}
