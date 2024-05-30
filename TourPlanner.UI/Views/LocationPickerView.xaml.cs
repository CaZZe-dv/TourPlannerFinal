using GMap.NET.MapProviders;
using GMap.NET;
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
    /// Interaktionslogik für LocationPickerView.xaml
    /// </summary>
    public partial class LocationPickerView : UserControl
    {
        private enum LocationType
        {
            From,
            To,
            None
        }

        private LocationType currentLocationType = LocationType.None;

        public static readonly DependencyProperty FromLocationProperty =
            DependencyProperty.Register("FromLocation", typeof(string), typeof(LocationPickerView), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty ToLocationProperty =
            DependencyProperty.Register("ToLocation", typeof(string), typeof(LocationPickerView), new PropertyMetadata(string.Empty));

        public string FromLocation
        {
            get => (string)GetValue(FromLocationProperty);
            set => SetValue(FromLocationProperty, value);
        }

        public string ToLocation
        {
            get => (string)GetValue(ToLocationProperty);
            set => SetValue(ToLocationProperty, value);
        }

        public LocationPickerView()
        {
            InitializeComponent();

            mapControl.MapProvider = GMapProviders.OpenStreetMap;
            mapControl.Position = new PointLatLng(48.210033, 16.363449); // Vienna coordinates
            mapControl.MinZoom = 2;
            mapControl.MaxZoom = 17;
            mapControl.Zoom = 10;
            mapControl.ShowCenter = false;
        }

        private void FromTextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            currentLocationType = LocationType.From;
        }

        private void ToTextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            currentLocationType = LocationType.To;
        }

        private void MapControl_OnMapClick(object sender, MouseButtonEventArgs e)
        {
            var point = e.GetPosition(mapControl);
            var latLng = mapControl.FromLocalToLatLng((int)point.X, (int)point.Y);

            if (currentLocationType == LocationType.From)
            {
                FromLocation = $"{latLng.Lat}, {latLng.Lng}";
                FromTextBox.Text = FromLocation;
                currentLocationType = LocationType.None;
            }
            else if (currentLocationType == LocationType.To)
            {
                ToLocation = $"{latLng.Lat}, {latLng.Lng}";
                ToTextBox.Text = ToLocation;
                currentLocationType = LocationType.None;
            }
        }
    }
}
