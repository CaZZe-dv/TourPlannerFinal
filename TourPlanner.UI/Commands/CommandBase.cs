using System.Windows.Input;
using TourPlanner.Utility.Logging;

namespace TourPlanner.UI.Commands
{
    public abstract class CommandBase : ICommand
    {
        private static readonly ILoggerWrapper logger = Utility.Logging.LoggerFactory.GetLogger();

        public event EventHandler? CanExecuteChanged;

        public virtual bool CanExecute(object? parameter)
        {
            return true;
        }

        public abstract void Execute(object? parameter);

        protected void OnCanExecuteChanged()
        {
            logger.Debug("CanExecuteChanged event invoked.");
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
