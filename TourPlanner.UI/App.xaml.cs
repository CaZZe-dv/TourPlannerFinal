using Microsoft.EntityFrameworkCore;
using System.Windows;
using TourPlanner.DAL.DbContexts;
using TourPlanner.DAL.Services;
using TourPlanner.UI.Services;
using TourPlanner.UI.Stores;
using TourPlanner.UI.ViewModels;

namespace TourPlanner.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;
        private readonly SharedDataService _sharedDataService;
        private readonly TourPlannerManager _tourPlannerManager;
        private readonly TourPlannerDbContextFactory _tourPlannerDbContextFactory;

        private const string CONNECTION_STRING = "Host=localhost;Username=postgres;Password=12345;Database=TourPlannerDatabase";

        public App()
        {
            _navigationStore = new NavigationStore();

            _sharedDataService = new SharedDataService();

            _tourPlannerDbContextFactory = new TourPlannerDbContextFactory(CONNECTION_STRING);

            ITour tourHandler = new DatabaseTour(_tourPlannerDbContextFactory);
            ITourLog tourLogHandler = new DatabaseTourLog(_tourPlannerDbContextFactory);

            _tourPlannerManager = new TourPlannerManager(tourHandler, tourLogHandler);
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            using (TourPlannerDbContext dbContext = _tourPlannerDbContextFactory.CreateDbContext())
                dbContext.Database.Migrate();

            _navigationStore.CurrentViewModel = CreateMainMenuViewModel();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

        private MainMenuViewModel CreateMainMenuViewModel()
        {
            return new MainMenuViewModel(_tourPlannerManager,
                new NavigationService(_navigationStore, CreateAddTourViewModel),
                new NavigationService(_navigationStore, CreateEditTourViewModel),
                new NavigationService(_navigationStore, CreateAddTourLogViewModel),
                new NavigationService(_navigationStore, CreateEditTourLogViewModel),
                _sharedDataService);
        }

        private EditTourViewModel CreateEditTourViewModel()
        {
            return new EditTourViewModel(_tourPlannerManager, new NavigationService(_navigationStore, CreateMainMenuViewModel), _sharedDataService);
        }

        private AddTourViewModel CreateAddTourViewModel()
        {
            return new AddTourViewModel(_tourPlannerManager, new NavigationService(_navigationStore, CreateMainMenuViewModel));
        }

        private AddTourLogViewModel CreateAddTourLogViewModel()
        {
            return new AddTourLogViewModel(_tourPlannerManager, new NavigationService(_navigationStore, CreateMainMenuViewModel), _sharedDataService);
        }

        private EditTourLogViewModel CreateEditTourLogViewModel()
        {
            return new EditTourLogViewModel(_tourPlannerManager, new NavigationService(_navigationStore, CreateMainMenuViewModel), _sharedDataService);
        }
    }

}
