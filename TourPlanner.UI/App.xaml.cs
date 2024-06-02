using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Windows;
using TourPlanner.BL.Services;
using TourPlanner.DAL.DbContexts;
using TourPlanner.DAL.Services;
using TourPlanner.UI.Services;
using TourPlanner.UI.Stores;
using TourPlanner.UI.ViewModels;
using TourPlanner.Utility.Logging;

namespace TourPlanner.UI
{
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;
        private readonly SharedDataService _sharedDataService;
        private readonly TourPlannerRepository _tourPlannerManager;
        private readonly TourPlannerDbContextFactory _tourPlannerDbContextFactory;
        
        private readonly TourReportService _tourReportService;

        private readonly ImageService _imageService;
        private static readonly ILoggerWrapper logger = Utility.Logging.LoggerFactory.GetLogger();

        public App()
        {
            logger.Info("App initialization started.");

            try
            {
                _imageService = new ImageService(getImageDirectoryPathFromConfigFile());
                logger.Info("ImageService initialized successfully.");

                _navigationStore = new NavigationStore();
                logger.Info("NavigationStore initialized successfully.");

                _sharedDataService = new SharedDataService();
                logger.Info("SharedDataService initialized successfully.");

                _tourPlannerDbContextFactory = new TourPlannerDbContextFactory(getDbStringFromConfigFile());
                logger.Info("TourPlannerDbContextFactory initialized successfully.");

            _tourReportService = new TourReportService();

            ITour tourHandler = new DatabaseTour(_tourPlannerDbContextFactory);
            ITourLog tourLogHandler = new DatabaseTourLog(_tourPlannerDbContextFactory);
            IRouteService routeService = new RouteService(getApiStringFromConfigFile());
            IMapService mapService = new MapService();

                _tourPlannerManager = new TourPlannerRepository(tourHandler, tourLogHandler, routeService, mapService, _imageService);
                logger.Info("TourPlannerRepository initialized successfully.");
            }
            catch (Exception ex)
            {
                logger.Fatal($"Exception during App initialization: {ex.Message}");
                throw;
            }

            logger.Info("App initialization finished");
        }

        private string getImageDirectoryPathFromConfigFile()
        {
            try
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                IConfiguration configuration = builder.Build();

                string imagePath = configuration.GetSection("ImageDirectoryPath")["DefaultPath"];
                logger.Info("Image directory path retrieved successfully from configuration.");
                return imagePath;
            }
            catch (Exception ex)
            {
                logger.Error($"Error retrieving image directory path from configuration: {ex.Message}");
                throw;
            }
        }

        private string getDbStringFromConfigFile()
        {
            try
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                IConfiguration configuration = builder.Build();

                string dbConnectionString = configuration.GetConnectionString("DefaultConnection");
                logger.Info("Database connection string retrieved successfully from configuration.");
                return dbConnectionString;
            }
            catch (Exception ex)
            {
                logger.Error($"Error retrieving database connection string from configuration: {ex.Message}");
                throw;
            }
        }

        private string getApiStringFromConfigFile()
        {
            try
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                IConfiguration configuration = builder.Build();

                string apiKey = configuration.GetSection("ApiKeys")["OpenRouteService"];
                logger.Info("API key retrieved successfully from configuration.");
                return apiKey;
            }
            catch (Exception ex)
            {
                logger.Error($"Error retrieving API key from configuration: {ex.Message}");
                throw;
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            logger.Info("OnStartup begin.");

            try
            {
                using (TourPlannerDbContext dbContext = _tourPlannerDbContextFactory.CreateDbContext())
                {
                    logger.Info("Database context created successfully.");
                    dbContext.Database.Migrate();
                    logger.Info("Database migration completed successfully.");
                }

                _navigationStore.CurrentViewModel = CreateMainMenuViewModel();
                logger.Info("MainMenuViewModel created successfully.");

                MainWindow = new MainWindow()
                {
                    DataContext = new MainViewModel(_navigationStore)
                };
                logger.Info("MainWindow created successfully.");

                MainWindow.Show();
                logger.Info("MainWindow shown successfully.");
            }
            catch (Exception ex)
            {
                logger.Fatal($"Exception during OnStartup: {ex.Message}");
                throw;
            }

            base.OnStartup(e);

            logger.Info("App started.");
        }

        private MainMenuViewModel CreateMainMenuViewModel()
        {
            logger.Info("Creating MainMenuViewModel.");
            return new MainMenuViewModel(_tourPlannerManager,
                new NavigationService(_navigationStore, CreateAddTourViewModel),
                new NavigationService(_navigationStore, CreateEditTourViewModel),
                new NavigationService(_navigationStore, CreateAddTourLogViewModel),
                new NavigationService(_navigationStore, CreateEditTourLogViewModel),
                _sharedDataService);
        }

        private EditTourViewModel CreateEditTourViewModel()
        {
            logger.Info("Creating EditTourViewModel.");
            return new EditTourViewModel(_tourPlannerManager, new NavigationService(_navigationStore, CreateMainMenuViewModel), _sharedDataService);
        }

        private AddTourViewModel CreateAddTourViewModel()
        {
            logger.Info("Creating AddTourViewModel.");
            return new AddTourViewModel(_tourPlannerManager, new NavigationService(_navigationStore, CreateMainMenuViewModel));
        }

        private AddTourLogViewModel CreateAddTourLogViewModel()
        {
            logger.Info("Creating AddTourLogViewModel.");
            return new AddTourLogViewModel(_tourPlannerManager, new NavigationService(_navigationStore, CreateMainMenuViewModel), _sharedDataService);
        }

        private EditTourLogViewModel CreateEditTourLogViewModel()
        {
            logger.Info("Creating EditTourLogViewModel.");
            return new EditTourLogViewModel(_tourPlannerManager, new NavigationService(_navigationStore, CreateMainMenuViewModel), _sharedDataService);
        }
    }
}
