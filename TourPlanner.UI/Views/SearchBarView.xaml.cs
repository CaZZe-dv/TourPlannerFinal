using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TourPlanner.UI.Views
{
    public partial class SearchBarView : UserControl
    {
        // Define dependency properties
        public static readonly DependencyProperty SearchBarProperty =
            DependencyProperty.Register("SearchBar", typeof(string), typeof(SearchBarView), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty RouteImageProperty =
            DependencyProperty.Register("RouteImage", typeof(ImageSource), typeof(SearchBarView), new PropertyMetadata(null));

        // Define corresponding properties
        public string SearchBar
        {
            get { return (string)GetValue(SearchBarProperty); }
            set { SetValue(SearchBarProperty, value); }
        }

        public ImageSource RouteImage
        {
            get { return (ImageSource)GetValue(RouteImageProperty); }
            set { SetValue(RouteImageProperty, value); }
        }

        public SearchBarView()
        {
            InitializeComponent();
        }
    }
}
