using System;
using System.Windows;
using TestFromDeeplayComp.Commands.Base_cmd;
using TestFromDeeplayComp.Views.Windows;

namespace TestFromDeeplayComp.Commands
{
    class EditPersonalDataCmd : Command
    {
        private EditPersonalData_Window _Window;
        public override bool CanExecute(object parameter) => _Window == null;
        public override void Execute(object parameter)
        {
            EditPersonalData_Window window = new EditPersonalData_Window
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
