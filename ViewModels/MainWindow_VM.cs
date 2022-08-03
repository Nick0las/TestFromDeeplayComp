using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using TestFromDeeplayComp.Commands;
using TestFromDeeplayComp.Models;
using TestFromDeeplayComp.Services;
using TestFromDeeplayComp.Services.Interfaces;
using TestFromDeeplayComp.ViewModels.ViewModel_Base;

namespace TestFromDeeplayComp.ViewModels
{
    internal class MainWindow_VM : Base_VM, IDownloadProfileFromDB, IDeleteEmployee
    {
        #region заголовок окна
        private string _Tittle = "Персонал v1.0";
        /// <summary>Заголовок окна </summary>
        public string Tittle
        {
            get => _Tittle;
            set => Set(ref _Tittle, value);
        }
        #endregion

        #region Свойства, привязанные к окну
        public UserProfile SelectedUser { get; set; }
        #endregion

        #region Команды
        // Удаление сотрудника
        public ICommand DeleteProfileCmd { get; }
        private bool CanDeleteProfileCmdExecute(object p) => true;
        private void OnDeleteProfileCmdExecuted(object p)
        {
            var user = SelectedUser;
            MessageBoxResult question = MessageBox.Show("Удалить выбранного сотрудника?", "Удаление профиля анкеты", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(question == MessageBoxResult.Yes)
            {
                try
                {
                    IDeleteEmployee.DeleteProfile(user.IdUser);
                    MessageBox.Show("Анкета из базы данных удалена!", "Удаление успешно!");
                    Collections.Users.Clear();
                    IDownloadProfileFromDB.ShowProfile(Collections.Users);
                }
                catch
                {
                    MessageBox.Show("Проверьте данный профиль в информации о сотрудниках","Ошибка удаления профиля из базы данных");
                }
                
            }
        }



        #endregion

        public MainWindow_VM() 
        {
            DeleteProfileCmd = new LamdaCommand(OnDeleteProfileCmdExecuted, CanDeleteProfileCmdExecute);
            IDownloadProfileFromDB.ShowProfile(Collections.Users);
        }
    }
}
