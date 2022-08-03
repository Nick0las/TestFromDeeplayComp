using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TestFromDeeplayComp.ViewModels;

namespace TestFromDeeplayComp.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для EmplooyeeInformation_Window.xaml
    /// </summary>
    public partial class EmplooyeeInformation_Window : Window
    {
        public EmplooyeeInformation_Window()
        {
            InitializeComponent();
            if(GridManagerPesonal.Visibility == Visibility.Visible)
            {
                CheckBoxManager.IsChecked = true;
            }
        }

        private void CheckBoxWorker_Checked(object sender, RoutedEventArgs e)
        {
            if (CheckBoxWorker.IsChecked == true)
            {
                WorkedDataGrid.Visibility = Visibility.Visible;
                ManagerDataGrid.Visibility = Visibility.Hidden;
                CheckBoxManager.IsChecked = false;
                GridManagerPesonal.Visibility = Visibility.Hidden;
                GridWorkerPesonal.Visibility = Visibility.Visible;
                if (GridManagerPesonal.Visibility == Visibility.Hidden)
                {
                    ManagerDataGrid.UnselectAll();
                }
            }
        }

        private void CheckBoxManager_Checked(object sender, RoutedEventArgs e)
        {
            ManagerDataGrid.Visibility = Visibility.Visible;
            WorkedDataGrid.Visibility = Visibility.Hidden;
            CheckBoxWorker.IsChecked = false;
            GridManagerPesonal.Visibility = Visibility.Visible;
            GridWorkerPesonal.Visibility = Visibility.Hidden;
            if (GridWorkerPesonal.Visibility == Visibility.Hidden)
            {
                WorkedDataGrid.UnselectAll();
            }
        }
    }
}
