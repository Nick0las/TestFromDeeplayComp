using System;
using System.Windows;
using TestFromDeeplayComp.Commands.Base_cmd;
using TestFromDeeplayComp.Views.Windows;

namespace TestFromDeeplayComp.Commands
{
    internal class AddNewEmployeeCmd : Command
    {
        private AddNewEmployee_Window _Window;
        public override bool CanExecute(object parameter) => _Window == null;
        public override void Execute(object parameter)
        {
            AddNewEmployee_Window window = new AddNewEmployee_Window
            {
                Owner = Application.Current.MainWindow
            };
            _Window = window;
            window.Closed += OnWindowClosed;
            window.ShowDialog();
        }
        private void OnWindowClosed(object Sender, EventArgs E)
        {
            ((Window)Sender).Closed -= OnWindowClosed;
            _Window = null;
        }
    }
}
