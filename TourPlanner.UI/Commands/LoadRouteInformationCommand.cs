using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using TourPlanner.BL.Services;
using TourPlanner.DAL.Services;
using TourPlanner.UI.ViewModels;

namespace TourPlanner.UI.Commands
{
    public class LoadRouteInformationCommand : AsyncCommandBase
    {
        private readonly AddTourViewModel _addTourViewModel;
        private readonly TourPlannerRepository _tourPlannerRepository;
        public LoadRouteInformationCommand(AddTourViewModel addTourViewModel, TourPlannerRepository tourPlannerRepository)
        {
            _addTourViewModel = addTourViewModel;
            _tourPlannerRepository = tourPlannerRepository;
            _addTourViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_addTourViewModel.AddTourFrom) ||
                e.PropertyName == nameof(_addTourViewModel.AddTourTo) ||
                e.PropertyName == nameof(_addTourViewModel.AddTourTransportType))
            {
                OnCanExecuteChanged();
                _addTourViewModel.IsRouteInformationFetched = false;
            }
        }

        private bool ValidateLongitudeLatitude(string value)
        {
            var regex = new Regex(@"^[-+]?([1-8]?\d(\.\d+)?|90(\.0+)?),\s*[-+]?(180(\.0+)?|((1[0-7]\d)|([1-9]?\d))(\.\d+)?)$");
            return regex.IsMatch(value);
        }

        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter) && !string.IsNullOrEmpty(_addTourViewModel.AddTourFrom)
                && !string.IsNullOrEmpty(_addTourViewModel.AddTourTo)
                && !string.IsNullOrEmpty(_addTourViewModel.AddTourTransportType)
                && ValidateLongitudeLatitude(_addTourViewModel.AddTourFrom)
                && ValidateLongitudeLatitude(_addTourViewModel.AddTourTo);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            RouteResponse? routeResponse = await _tourPlannerRepository.GetRouteInformation(_addTourViewModel.AddTourTransportType,
                _addTourViewModel.AddTourFrom, _addTourViewModel.AddTourTo);
            //BitmapImage? image = await _tourPlannerRepository.GetRouteImage("1", _addTourViewModel.AddTourFrom, _addTourViewModel.AddTourTo);
            if (routeResponse != null /** && image != null**/)
            {
                _addTourViewModel.AddTourDistance = routeResponse.Distance.ToString();
                _addTourViewModel.AddTourEstimatedTime = TimeSpan.FromMinutes(routeResponse.Duration).ToString();
                //_addTourViewModel.AddTourImage = image;
                _addTourViewModel.IsRouteInformationFetched = true;
                return;
            }
            _addTourViewModel.AddTourFrom = string.Empty;
            _addTourViewModel.AddTourTo = string.Empty;
            //_addTourViewModel.AddTourTransportType = string.Empty;
            _addTourViewModel.AddTourDistance = string.Empty;
            _addTourViewModel.AddTourEstimatedTime = string.Empty;
            //_addTourViewModel.AddTourImage = null;
        }
    }
}
