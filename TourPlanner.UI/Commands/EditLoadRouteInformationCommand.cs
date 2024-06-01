﻿using System;
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
    public class EditLoadRouteInformationCommand : AsyncCommandBase
    {
        private readonly EditTourViewModel _editTourViewModel;
        private readonly TourPlannerRepository _tourPlannerRepository;
        public EditLoadRouteInformationCommand(EditTourViewModel editTourViewModel, TourPlannerRepository tourPlannerRepository)
        {
            _editTourViewModel = editTourViewModel;
            _tourPlannerRepository = tourPlannerRepository;
            _editTourViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_editTourViewModel.EditTourFrom) ||
                e.PropertyName == nameof(_editTourViewModel.EditTourTo) ||
                e.PropertyName == nameof(_editTourViewModel.EditTourTransportType))
            {
                OnCanExecuteChanged();
                _editTourViewModel.IsRouteInformationFetched = false;
                ResetTourInformationDisplay();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter) && !string.IsNullOrEmpty(_editTourViewModel.EditTourFrom)
                && !string.IsNullOrEmpty(_editTourViewModel.EditTourTo)
                && !string.IsNullOrEmpty(_editTourViewModel.EditTourTransportType);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            RouteResponse? routeResponse = await _tourPlannerRepository.GetRouteInformation(_editTourViewModel.EditTourTransportType,
                _editTourViewModel.EditTourFrom, _editTourViewModel.EditTourTo);
            if (routeResponse != null)
            {
                BitmapSource? image = await _tourPlannerRepository.GetRouteImage(routeResponse.Start, routeResponse.End);
                if (image != null)
                {
                    _editTourViewModel.EditTourDistance = routeResponse.Distance.ToString();
                    _editTourViewModel.EditTourEstimatedTime = TimeSpan.FromMinutes(routeResponse.Duration).ToString();
                    _editTourViewModel.EditTourImage = image;
                    _editTourViewModel.IsRouteInformationFetched = true;
                    return;
                }
            }
            ResetTourInformationDisplay();
        }

        private void ResetTourInformationDisplay()
        {
            _editTourViewModel.EditTourDistance = string.Empty;
            _editTourViewModel.EditTourEstimatedTime = string.Empty;
            _editTourViewModel.EditTourImage = null;
        }
    }
}
