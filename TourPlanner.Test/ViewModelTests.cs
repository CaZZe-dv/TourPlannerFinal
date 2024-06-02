using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DAL.Services;
using TourPlanner.UI.Commands;
using TourPlanner.UI.Services;
using TourPlanner.UI.ViewModels;

namespace TourPlanner.Test
{
    public class ViewModelTests
    {
        private AddTourViewModel _addTourViewModel;
        private TourPlannerRepository _tourPlannerRepository;
        private NavigationService _navigationService;

        [SetUp]
        public void SetUp()
        {
            _tourPlannerRepository = new TourPlannerRepository(null, null, null, null, null);

            // Create a Func<ViewModelBase> that returns null for testing purposes
            Func<ViewModelBase> nullFunc = () => null;
            _navigationService = new NavigationService(new UI.Stores.NavigationStore(), nullFunc);

            _addTourViewModel = new AddTourViewModel(_tourPlannerRepository, _navigationService);
        }

        [Test]
        public void AddTourName_PropertyChanged_FiresPropertyChangedEvent()
        {
            bool propertyChanged = false;
            _addTourViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(_addTourViewModel.AddTourName))
                {
                    propertyChanged = true;
                }
            };

            _addTourViewModel.AddTourName = "New Tour Name";
            Assert.IsTrue(propertyChanged);
        }

        [Test]
        public void AddTourDescription_PropertyChanged_FiresPropertyChangedEvent()
        {
            bool propertyChanged = false;
            _addTourViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(_addTourViewModel.AddTourDescription))
                {
                    propertyChanged = true;
                }
            };

            _addTourViewModel.AddTourDescription = "New Description";
            Assert.IsTrue(propertyChanged);
        }

        [Test]
        public void AddTourFrom_PropertyChanged_FiresPropertyChangedEvent()
        {
            bool propertyChanged = false;
            _addTourViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(_addTourViewModel.AddTourFrom))
                {
                    propertyChanged = true;
                }
            };

            _addTourViewModel.AddTourFrom = "New Starting Point";
            Assert.IsTrue(propertyChanged);
        }

        [Test]
        public void AddTourTo_PropertyChanged_FiresPropertyChangedEvent()
        {
            bool propertyChanged = false;
            _addTourViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(_addTourViewModel.AddTourTo))
                {
                    propertyChanged = true;
                }
            };

            _addTourViewModel.AddTourTo = "New Destination";
            Assert.IsTrue(propertyChanged);
        }

        [Test]
        public void AddTourTransportType_PropertyChanged_FiresPropertyChangedEvent()
        {
            bool propertyChanged = false;
            _addTourViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(_addTourViewModel.AddTourTransportType))
                {
                    propertyChanged = true;
                }
            };

            _addTourViewModel.AddTourTransportType = "Driving";
            Assert.IsTrue(propertyChanged);
        }

        [Test]
        public void AddTourDistance_PropertyChanged_FiresPropertyChangedEvent()
        {
            bool propertyChanged = false;
            _addTourViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(_addTourViewModel.AddTourDistance))
                {
                    propertyChanged = true;
                }
            };

            _addTourViewModel.AddTourDistance = "100 km";
            Assert.IsTrue(propertyChanged);
        }
    }
}
