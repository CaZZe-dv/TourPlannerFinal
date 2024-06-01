using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TourPlanner.BL.Models;
using TourPlanner.DAL.Services;
using TourPlanner.UI.Commands;
using TourPlanner.UI.Services;

namespace TourPlanner.UI.ViewModels
{
    public class MainMenuViewModel : ViewModelBase
    {
        public ICommand MainMenuFileCommand { get; }
        public ICommand MainMenuEditCommand { get; }
        public ICommand MainMenuOptionsCommand { get; }
        public ICommand MainMenuHelpCommand { get; }

        private string _searchBar;

        public string SearchBar
        {
            get => _searchBar;
            set
            {
                _searchBar = value;
                OnPropertyChanged(nameof(SearchBar));
            }
        }


        private readonly ObservableCollection<TourViewModel> _tours;
        public IEnumerable<TourViewModel> Tours => _tours;

        private readonly ObservableCollection<TourLogViewModel> _tourLogs;
        public IEnumerable<TourLogViewModel> TourLogs => _tourLogs;

        public ICommand ListControlViewButtonPlus { get; }
        public ICommand ListControlViewButtonMinus { get; }
        public ICommand ListControlViewButtonEdit { get; }

        private TourViewModel _selectedTour;
        public TourViewModel SelectedTour
        {
            get => _selectedTour;
            set
            {
                _selectedTour = value;
                OnPropertyChanged(nameof(SelectedTour));
                _sharedDataService.SelectedTour = _selectedTour != null ? SelectedTour.Tour : null;
                TourIsChoosen = SelectedTour != null;
                if (SelectedTour != null) 
                { 
                    LoadTourLogCommand.Execute(null);
                    LoadImageCommand.Execute(null);
                }

            }
        }

        private TourLogViewModel _selectedTourLog;
        public TourLogViewModel SelectedTourLog
        {
            get => _selectedTourLog;
            set
            {
                _selectedTourLog = value;
                OnPropertyChanged(nameof(SelectedTourLog));
                _sharedDataService.SelectedTourLog = _selectedTourLog != null ? _selectedTourLog.TourLog : null;
            }
        }

        private readonly SharedDataService _sharedDataService;

        public ICommand LoadTourCommand { get; }

        public ICommand LoadTourLogCommand { get; }

        public ICommand MapControlViewButtonPlus { get; }
        public ICommand MapControlViewButtonMinus { get; }
        public ICommand MapControlViewButtonEdit { get; }

        private bool _isLoadingTours;
        public bool IsLoadingTours
        {
            get => _isLoadingTours;
            set
            {
                _isLoadingTours = value;
                OnPropertyChanged(nameof(IsLoadingTours));
            }
        }

        private bool _isLoadingTourLogs;
        public bool IsLoadingTourLogs
        {
            get => _isLoadingTourLogs;
            set
            {
                _isLoadingTourLogs = value;
                OnPropertyChanged(nameof(IsLoadingTourLogs));
            }
        }

        private bool _tourIsChoosen;
        public bool TourIsChoosen
        {
            get => _tourIsChoosen;
            set
            {
                _tourIsChoosen = value;
                OnPropertyChanged(nameof(TourIsChoosen));
            }
        }

        private BitmapSource _mainMenuImage;

        public BitmapSource MainMenuImage
        {
            get => _mainMenuImage;
            set
            {
                _mainMenuImage = value;
                OnPropertyChanged(nameof(MainMenuImage));
            }
        }

        public ICommand LoadImageCommand { get; }

        public MainMenuViewModel(TourPlannerRepository tourPlannerManager, NavigationService addTourNavigationService, NavigationService editTourNavigationService,
            NavigationService addTourLogNavigationService, NavigationService editTourLogNavigationService, SharedDataService sharedDataService)
        {
            _sharedDataService = sharedDataService;

            _tours = new ObservableCollection<TourViewModel>();
            _tourLogs = new ObservableCollection<TourLogViewModel>();

            ListControlViewButtonPlus = new AddTourCommand(addTourNavigationService);
            ListControlViewButtonMinus = new RemoveTourCommand(this, tourPlannerManager);
            ListControlViewButtonEdit = new EditTourCommand(this, editTourNavigationService);

            MapControlViewButtonEdit = new EditTourLogCommand(this, editTourLogNavigationService);
            MapControlViewButtonMinus = new RemoveTourLogCommand(this, tourPlannerManager);
            MapControlViewButtonPlus = new AddTourLogCommand(this, addTourLogNavigationService);

            LoadTourCommand = new LoadTourCommand(tourPlannerManager, this);
            LoadTourCommand.Execute(null);

            LoadTourLogCommand = new LoadTourLogCommand(tourPlannerManager, this);

            LoadImageCommand = new LoadImageCommand(this, tourPlannerManager, sharedDataService);
        }

        public void UpdateTours(IEnumerable<Tour> tours)
        {
            _tours.Clear();

            if (tours != null)
            {
                foreach (Tour t in tours)
                {
                    _tours.Add(new TourViewModel(t));
                }
            }
        }

        public void UpdateTourLogs(IEnumerable<TourLog> tourLogs)
        {
            _tourLogs.Clear();

            if (tourLogs != null)
            {
                foreach (TourLog tl in tourLogs)
                {
                    _tourLogs.Add(new TourLogViewModel(tl));
                }
            }
        }

    }
}
