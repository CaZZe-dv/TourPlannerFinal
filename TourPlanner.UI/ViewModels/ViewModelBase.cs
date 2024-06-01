using System;
using System.ComponentModel;
using TourPlanner.Utility.Logging;

namespace TourPlanner.UI.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        private static readonly ILoggerWrapper logger = Utility.Logging.LoggerFactory.GetLogger();

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            logger.Info($"Property '{propertyName}' changed.");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
