using Microsoft.Data.Sqlite;
using System;
using System.Windows;
using System.Windows.Input;
using TestFromDeeplayComp.Commands;
using TestFromDeeplayComp.Models;
using TestFromDeeplayComp.Services;
using TestFromDeeplayComp.Services.Interfaces;
using TestFromDeeplayComp.ViewModels.ViewModel_Base;
using TestFromDeeplayComp.Views.Windows;

namespace TestFromDeeplayComp.ViewModels
{
    internal class EmployeeInformation_VM : Base_VM, IDownloadWorkPersonal_Info, IDownload_ManagerAllInfo, IDeleteEmployee, IDownloadProfileFromDB
    {
        #region заголовок окна
        private string _Tittle = "Информация о сотрудниках";
        /// <summary>Заголовок окна </summary>
        public string Tittle
        {
            get => _Tittle;
            set => Set(ref _Tittle, value);
        }
        #endregion

        #region Свойства, привязанные к окну
        public ManagerPersonal_Info SelectedManager { get; set; }
        public WorkPersonal_Info SelectedWorkPersonal { get; set; }

        #endregion

        #region Команды

        #region Редактирование личных данных персонала (таблица Profile)
        
        /// <summary>Команда редактирования личных данных персонала</summary>
        private ICommand _EditPersonalDataCommand;
        public ICommand EditManagerPersonalCommand =>
            _EditPersonalDataCommand ??= new LamdaCommand(OnEditPersonalDataCommandExecuted, CanEditPersonalDataCommandExecute);
        private bool CanEditPersonalDataCommandExecute(object p) => true;
        private void OnEditPersonalDataCommandExecuted(object p)
        {
            if(SelectedManager != null)
            {
                var employee = SelectedManager;
                Collections.ManagersInfo.Clear();
                Collections.ManagersInfo.Add(employee);
                var dlg = new EditPersonalData_Window 
                {
                    
                };
                if (dlg.ShowDialog() == true)
                {
                    
                }
                else
                {
                    
                }
            }
            if(SelectedWorkPersonal != null)
            {
                //var employee = (WorkPersonal_Info)p;
                var employee = SelectedWorkPersonal;
                Collections.WorkPersonals_Info.Clear();
                Collections.WorkPersonals_Info.Add(employee);
                var dlg = new EditPersonalData_Window
                {

                };
                if (dlg.ShowDialog() == true)
                {
                    MessageBox.Show("Пользователь выполнил редактирование раба");
                }
            }
        }
        #endregion

        #region Редактирование должностной информации персонала
        private ICommand _EditPostInformationCommand;
        public ICommand EditPostInformationCommand =>
            _EditPostInformationCommand ??= new LamdaCommand(OnEditPostInformationCommandExecuted, CanEditPostInformationCommandExecute);
        private bool CanEditPostInformationCommandExecute(object p) => true;
        private void OnEditPostInformationCommandExecuted(object p)
        {
            if (SelectedManager != null)
            {
                var employee = SelectedManager;
                Collections.ManagersInfo.Clear();
                Collections.ManagersInfo.Add(employee);
                var dlg = new EditPostInformation_Window
                {

                };
                if (dlg.ShowDialog() == true)
                {
                    
                }
                else
                {
                    
                }
            }
            if (SelectedWorkPersonal != null)
            {
                var employee = SelectedWorkPersonal;
                Collections.WorkPersonals_Info.Clear();
                Collections.WorkPersonals_Info.Add(employee);
                var dlg = new EditPostInformation_Window
                {

                };
                if (dlg.ShowDialog() == true)
                {
                    
                }
                else
                {
                    
                }
            }
        }


        #endregion

        #region Удаление сотрудника(из должности)
        /// <summary>Команда удаления персонала</summary>
        private ICommand _DeletePersonalCommand;
        public ICommand DeletePersonalCommand =>
            _DeletePersonalCommand ??= new LamdaCommand(OnDeletePersonalCommandExecuted, CanDeletePersonalCommandExecute);
        private bool CanDeletePersonalCommandExecute(object p) => true;
        private void OnDeletePersonalCommandExecuted(object p)
        {
            if (SelectedManager != null)
            {
                SelectedWorkPersonal = null;
                var employee = SelectedManager;
                MessageBoxResult questionDeleteEmployee = MessageBox.Show("Удаление сотрудника. Подтверждаете?", "Удалить выбранного сотрудника?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if(questionDeleteEmployee == MessageBoxResult.Yes)
                {
                    IDeleteEmployee.DeleteManager_personal(employee.IdProfileManager);
                    MessageBoxResult questionDeleteUser = MessageBox.Show("Удалить анкету удаленного сотрудника?", "Удаление анкеты сотрудника", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if(questionDeleteUser == MessageBoxResult.Yes)
                    {
                        IDeleteEmployee.DeleteProfile(employee.IdProfileManager);
                        MessageBox.Show("Анкета сотрудника удалена!", "Удаление прошло успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Удаление профиля отменено!", "Вы отменили удаление!", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            if(SelectedWorkPersonal != null)
            {
                SelectedManager = null;
                var employee = SelectedWorkPersonal;
                MessageBoxResult questionDeleteEmployee = MessageBox.Show("Удаление сотрудника. Подтверждаете?", "Удалить выбранного сотрудника?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if(questionDeleteEmployee == MessageBoxResult.Yes)
                {
                    IDeleteEmployee.DeleteWork_personal(SelectedWorkPersonal.IdProfileWorkPersonal);
                    MessageBoxResult questionDeleteUser = MessageBox.Show("Удалить анкету удаленного сотрудника?", "Удаление анкеты сотрудника", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (questionDeleteUser == MessageBoxResult.Yes)
                    {
                        IDeleteEmployee.DeleteProfile(employee.IdProfileWorkPersonal);
                        MessageBox.Show("Анкета сотрудника удалена!", "Удаление прошло успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Удаление профиля отменено!", "Вы отменили удаление!", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            Collections.ManagersInfo.Clear();
            Collections.WorkPersonals_Info.Clear();
            IDownloadWorkPersonal_Info.ShowAllWorkPersonalInfo(Collections.WorkPersonals_Info);
            IDownload_ManagerAllInfo.ShowAllManagerInfo(Collections.ManagersInfo);
            Collections.Users.Clear();
            IDownloadProfileFromDB.ShowProfile(Collections.Users);
        }


        #endregion

        #endregion

        public EmployeeInformation_VM()
        {
            Collections.ManagersInfo.Clear();
            Collections.WorkPersonals_Info.Clear();
            IDownloadWorkPersonal_Info.ShowAllWorkPersonalInfo(Collections.WorkPersonals_Info);
            IDownload_ManagerAllInfo.ShowAllManagerInfo(Collections.ManagersInfo);
        }
    }
}
