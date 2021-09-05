using System;
using System.Windows.Input;

namespace Explorer
{
    /// <summary>
    /// Basic command that runs on action
    /// </summary>
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// Action to run
        /// </summary>
        private Action mAction;

        /// <summary>
        /// Event that fired when the <see cref="CanExecute(object)"/> method value has changed
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        /// <summary>
        /// Default Constructor
        /// </summary>
        public RelayCommand(Action action)
        {
            mAction = action;
        }

        /// <summary>
        /// Relay Command can always execute
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Execute the commands action
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            mAction();
        }
    }
}
