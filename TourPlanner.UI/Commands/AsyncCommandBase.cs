using System;
using System.Threading.Tasks;
using TourPlanner.Utility.Logging;

namespace TourPlanner.UI.Commands
{
    public abstract class AsyncCommandBase : CommandBase
    {
        private static readonly ILoggerWrapper logger = Utility.Logging.LoggerFactory.GetLogger();

        private bool _isExecuting;
        private bool IsExecuting
        {
            get => _isExecuting;
            set
            {
                _isExecuting = value;
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            bool canExecute = base.CanExecute(parameter) && !IsExecuting;
            logger.Debug($"CanExecute: {canExecute}");
            return canExecute;
        }

        public override async void Execute(object parameter)
        {
            logger.Info("Executing AsyncCommandBase.");
            IsExecuting = true;

            try
            {
                await ExecuteAsync(parameter);
            }
            catch (Exception ex)
            {
                logger.Error($"Error executing async command: {ex.Message}");
            }
            finally
            {
                IsExecuting = false;
            }
        }

        public abstract Task ExecuteAsync(object parameter);
    }
}
