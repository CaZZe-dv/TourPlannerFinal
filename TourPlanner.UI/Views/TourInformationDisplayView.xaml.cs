﻿using System;
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
    /// Interaktionslogik für TourInformationDisplayView.xaml
    /// </summary>
    public partial class TourInformationDisplayView : UserControl
    {
        public static readonly DependencyProperty DistanceProperty =
            DependencyProperty.Register("Distance", typeof(string), typeof(TourInformationDisplayView), new PropertyMetadata(string.Empty));

        public string Distance
        {
            get { return (string)GetValue(DistanceProperty); }
            set { SetValue(DistanceProperty, value); }
        }

        public static readonly DependencyProperty EstimatedTimeProperty =
            DependencyProperty.Register("EstimatedTime", typeof(string), typeof(TourInformationDisplayView), new PropertyMetadata(string.Empty));

        public string EstimatedTime
        {
            get { return (string)GetValue(EstimatedTimeProperty); }
            set { SetValue(EstimatedTimeProperty, value); }
        }

        public static readonly DependencyProperty ImagePathProperty =
            DependencyProperty.Register("ImagePath", typeof(string), typeof(TourInformationDisplayView), new PropertyMetadata(string.Empty));

        public string ImagePath
        {
            get { return (string)GetValue(ImagePathProperty); }
            set { SetValue(ImagePathProperty, value); }
        }
        public TourInformationDisplayView()
        {
            InitializeComponent();
        }
    }
}
