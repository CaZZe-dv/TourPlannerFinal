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
using System.Windows.Shapes;

namespace TourPlanner.UI.Views
{
    /// <summary>
    /// Interaction logic for CustomDialogReport.xaml
    /// </summary>
    public partial class CustomDialogReport : Window
    {
        public CustomDialogReport()
        {
            InitializeComponent();
        }
        private void TourReport_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Tag = "TourReport";
            this.Close();
        }

        private void SummarizeReport_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Tag = "SummarizeReport";
            this.Close();
        }
    }
}
