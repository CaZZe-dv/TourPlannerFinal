using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Windows;
using System.Windows.Markup;
using TourPlanner.BL.Services;
using TourPlanner.DAL.DbContexts;
using TourPlanner.DAL.Services;
using TourPlanner.UI.Services;
using TourPlanner.UI.Stores;
using TourPlanner.UI.ViewModels;
using TourPlanner.Utility.Logging;

namespace TourPlanner.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;
        private readonly SharedDataService _sharedDataService;
        private readonly TourPlannerRepository _tourPlannerManager;
        private readonly TourPlannerDbContextFactory _tourPlannerDbContextFactory;
        private readonly ImageService _imageService;
        private static readonly ILoggerWrapper logger = Utility.Logging.LoggerFactory.GetLogger();

        public App()
        {
            logger.Info("App initialization started.");

            _imageService = new ImageService(getImageDirectoryPathFromConfigFile());

            _navigationStore = new NavigationStore();

            _sharedDataService = new SharedDataService();

            _tourPlannerDbContextFactory = new TourPlannerDbContextFactory(getDbStringFromConfigFile());

            ITour tourHandler = new DatabaseTour(_tourPlannerDbContextFactory);
            ITourLog tourLogHandler = new DatabaseTourLog(_tourPlannerDbContextFactory);
            IRouteService routeService = new RouteService(getApiStringFromConfigFile());
            IMapService mapService = new MapService();

            _tourPlannerManager = new TourPlannerRepository(tourHandler, tourLogHandler, routeService, mapService, _imageService);

            logger.Info("App initialization finished");
        }

        private string getImageDirectoryPathFromConfigFile()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration configuration = builder.Build();

            return configuration.GetSection("ImageDirectoryPath")["DefaultPath"];
        }

        private string getDbStringFromConfigFile()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration configuration = builder.Build();

            return configuration.GetConnectionString("DefaultConnection");
        }

        private string getApiStringFromConfigFile()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration configuration = builder.Build();

            return configuration.GetSection("ApiKeys")["OpenRouteService"];
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

            logger.Info("App started.");
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
