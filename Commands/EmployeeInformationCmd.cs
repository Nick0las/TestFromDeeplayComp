using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using TestFromDeeplayComp.Commands.Base_cmd;
using TestFromDeeplayComp.Views.Windows;

namespace TestFromDeeplayComp.Commands
{
    internal class EmployeeInformationCmd : Command
    {
        private EmplooyeeInformation_Window _Window;
        public override bool CanExecute(object parameter) => _Window == null;
        public override void Execute(object parameter)
        {
            EmplooyeeInformation_Window window = new EmplooyeeInformation_Window
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