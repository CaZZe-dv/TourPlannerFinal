namespace TourPlanner.UI.Commands
{
    public abstract class AsyncCommandBase : CommandBase
    {

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
            return base.CanExecute(parameter) && !IsExecuting;
        }

        public override async void Execute(object paramter)
        {
            IsExecuting = true;

            await ExecuteAsync(paramter);

            IsExecuting = false;
        }

        public abstract Task ExecuteAsync(object parameter);
    }
}
