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
    internal class EditPersonalData_VM : Base_VM, IDownload_ManagerAllInfo, IDownloadWorkPersonal_Info, IEditProfile, IDownloadProfileFromDB
    {
        #region заголовок окна
        private string _Tittle = "Редактирование персональных данных сотрудника";
        /// <summary>Заголовок окна </summary>
        public string Tittle
        {
            get => _Tittle;
            set => Set(ref _Tittle, value);
        }
        #endregion

        #region Свойства, привязанные к полям окна
        //Фамилия
        public string LastNameEmployee { get; set; }

        //Имя
        public string FirstNameEmployee { get; set; }
        //Отчество
        public string MiddleNameEmployee { get; set; }
        //День рождения
        public DateTime BirthdayEmployee { get; set; }
        // Адрес
        public string AdressEmployee { get; set; }
        //Номер телефона
        public string PhoneNumberEmployee { get; set; }
        private int _IdProfileEmployee;
        public int IdProfileEmployee
        {
            get => _IdProfileEmployee;
            set => Set(ref _IdProfileEmployee, value);
        }
        public Action CloseAction { get; set; }

        #endregion
        int idProfile;

        #region Команды
        public ICommand EditPersonalDataCmd { get; }
        private bool CanEditPersonalDataExecute(object p) => true;
        private void OnEditPersonalDataExecuted(object p)
        {
            UserProfile user = new UserProfile
            {
                IdUser = idProfile,
                LastName = LastNameEmployee,
                FirstName = FirstNameEmployee,
                MiddleName = MiddleNameEmployee,
                Adress = AdressEmployee,
                Birthday = BirthdayEmployee,
                PhoneNumber = PhoneNumberEmployee
            };
            IEditProfile.EditProfile(user);
            Collections.ManagersInfo.Clear();
            Collections.WorkPersonals_Info.Clear();
            Collections.Users.Clear();
            IDownload_ManagerAllInfo.ShowAllManagerInfo(Collections.ManagersInfo);
            IDownloadWorkPersonal_Info.ShowAllWorkPersonalInfo(Collections.WorkPersonals_Info);
            IDownloadProfileFromDB.ShowProfile(Collections.Users);
            MessageBoxResult result = MessageBox.Show("Данные успешно изменены. Окно закроется автоматически", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            foreach (Window window in Application.Current.Windows)
            {
                if (window is EditPersonalData_Window)
                {
                    window.Close();
                    break;
                }
            }

        }

        #endregion

        public EditPersonalData_VM()
        {
            EditPersonalDataCmd = new LamdaCommand(OnEditPersonalDataExecuted, CanEditPersonalDataExecute);
            idProfile = FillInFields();
        }

        private int FillInFields()
        {
            int idProfileManager = 0;
            int idProfileWorkPersonal = 0;
            int idProfile = 0;
            if (Collections.ManagersInfo.Count == 1 && (Collections.WorkPersonals_Info.Count > 1 || Collections.WorkPersonals_Info.Count == 0 ))
            {
                var employee = Collections.ManagersInfo[0];
                LastNameEmployee = employee.LastNameManager;
                FirstNameEmployee = employee.FirstNameManager;
                MiddleNameEmployee = employee.MiddleNameManager;
                BirthdayEmployee = Convert.ToDateTime(employee.BirthdayManager);
                AdressEmployee = employee.AdressManager;
                PhoneNumberEmployee = employee.PhoneNumberManager;
                Collections.ManagersInfo.Clear();
                IDownload_ManagerAllInfo.ShowAllManagerInfo(Collections.ManagersInfo);
                idProfileManager = employee.IdProfileManager;
                //return idProfileManager;
            }
            if (Collections.WorkPersonals_Info.Count == 1 && Collections.ManagersInfo.Count > 1)
            {
                var employee = Collections.WorkPersonals_Info[0];
                LastNameEmployee = employee.LastNameWorkPersonal;
                FirstNameEmployee = employee.FirstNameWorkPersonal;
                MiddleNameEmployee = employee.MiddleNameWorkPersonal;
                BirthdayEmployee = Convert.ToDateTime(employee.BirthdayWorkPersonal);
                AdressEmployee = employee.AdressWorkPersonal;
                PhoneNumberEmployee = employee.PhoneNumberWorkPersonal;
                Collections.WorkPersonals_Info.Clear();
                IDownloadWorkPersonal_Info.ShowAllWorkPersonalInfo(Collections.WorkPersonals_Info);
                idProfileWorkPersonal = employee.IdProfileWorkPersonal;
                //return idProfileWorkPersonal;
            }
            if(idProfileWorkPersonal !=0 && idProfileManager == 0)
            {
                idProfile = idProfileWorkPersonal;
            }
            if(idProfileManager !=0 && idProfileWorkPersonal == 0)
            {
                idProfile = idProfileManager;
            }
            return idProfile;
        }
    }
}
