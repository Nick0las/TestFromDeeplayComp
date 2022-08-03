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
    internal class NewEmployee_VM : Base_VM, IInsertNewProfile2DataBase, IInsertNewDateContract, IInsertNewEmployee, IDownloadProfileFromDB, IDownloadPostFromDb, IDownloadDepartament
    {
        #region заголовок окна
        private string _Tittle = "Новый сотрудник";
        /// <summary>Заголовок окна </summary>
        public string Tittle
        {
            get => _Tittle;
            set => Set(ref _Tittle, value);
        }
        #endregion

        #region Свойства, привязанные к окну
        // Фамилия
        private string _surname;
        public string Surname
        {
            get => _surname;
            set => Set(ref _surname, value);
        }
        // Имя
        private string _name;
        public string Name
        {
            get => _name;
            set=>Set(ref _name, value);
        }
        // Отчество
        private string _middleName;
        public string MiddleName
        {
            get => _middleName;
            set=>Set(ref _middleName, value);
        }
        // Дата рождения
        public DateTime UserBirthday { get; set; } = DateTime.Now;
        // Домашний адрес        
        public string Address { get; set; }
        // Телефон        
        public string Phone { get; set; }

        // ComboBox Должность
        public PostsInfo SelectedPost { get; set; }
        // ComboBox Отдел
        public Departament SelectedDepartament { get; set; }

        // Выбор руководителя
        public DepartamentManager SelectedUser { get; set; }
        // Дата приема на работу
        public DateTime DateAdmission { get; set; } = DateTime.Now;

        //Дата окончания договора (для должности директор)
        public DateTime EndDateContract { get; set; } = DateTime.Now;

        #endregion

        #region Команда добавления нового сотрудника
        public ICommand AddNewEmployeeCmd { get; }
        private bool CanAddNewEmployeeCmdExecute(object p) => true;
        private void OnAddNewEmployeeCmdExecuted(object p)
        {
            UserProfile user = new UserProfile
            {
                LastName = Surname,
                FirstName = Name,
                MiddleName = MiddleName,
                Birthday = UserBirthday,
                Adress = Address,
                PhoneNumber = Phone
            };

            int lastIdProfile = 0;
            int lastIdDateContract = 0;

            if (Surname == null || Name == null || MiddleName == null || Address == null || UserBirthday == DateTime.Today)
            {
                MessageBox.Show("Заполните все данные о новом сотруднике!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            switch (SelectedPost.IdPost)
            {
                case 1:
                    if(EndDateContract == DateTime.Today)
                    {
                        MessageBoxResult res = MessageBox.Show("Выбрана текущая дата окончания договора! Измените!", "Внимание! Измените дату договора!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    else
                    {
                        lastIdProfile = IInsertNewProfile2DataBase.AddNewProfile(user);
                        lastIdDateContract = IInsertNewDateContract.AddNewDateContract(EndDateContract);
                        IInsertNewEmployee.AddNewManager
                            (lastIdProfile, SelectedPost.IdPost, lastIdDateContract, DateAdmission, 0);
                    }                    
                    break;
                case 2:
                    if(SelectedDepartament == null)
                    {
                        MessageBox.Show("Укажите отдел!");
                        return;
                    }
                    lastIdProfile = IInsertNewProfile2DataBase.AddNewProfile(user);
                    IInsertNewEmployee.AddNewManager
                            (lastIdProfile, SelectedPost.IdPost, lastIdDateContract, DateAdmission, SelectedDepartament.IdDepartment);
                    MessageBox.Show("Сотрудник добавлен!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case 3:
                case 4:
                    lastIdProfile = IInsertNewProfile2DataBase.AddNewProfile(user);
                    IInsertNewEmployee.AddNewManager
                            (lastIdProfile, SelectedPost.IdPost, lastIdDateContract, DateAdmission, 0);
                    MessageBox.Show("Сотрудник добавлен!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                                    
                case 5:
                    if (SelectedUser == null)
                    {
                        MessageBox.Show("Выберите руководителя!");
                        return;
                    }
                    lastIdProfile = IInsertNewProfile2DataBase.AddNewProfile(user);
                    IInsertNewEmployee.AddNewWorker(lastIdProfile, SelectedPost.IdPost, SelectedUser.IdProfile, DateAdmission);
                    break;
            }
            
            Collections.Users.Clear();
            IDownloadProfileFromDB.ShowProfile(Collections.Users);
            MessageBoxResult result = MessageBox.Show("Сотрудник добавлен! Добавить нового сотрудника?", "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Information);
            if(result == MessageBoxResult.Cancel)
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window is AddNewEmployee_Window)
                    {
                        window.Close();
                        break;
                    }
                }
            }            
        }

        #endregion

        // Конструктор без парам
        public NewEmployee_VM()
        {
            AddNewEmployeeCmd = new LamdaCommand(OnAddNewEmployeeCmdExecuted, CanAddNewEmployeeCmdExecute);
            Collections.Posts.Clear();
            Collections.PostsInfos.Clear();
            Collections.ManagersDepartaments.Clear();
            Collections.Departaments.Clear();

            IDownloadPostFromDb.ShowPost(Collections.Posts);
            IDownloadPostFromDb.ShowPostInfo(Collections.PostsInfos);
            IDownloadPostFromDb.ShowManagerDepartament(Collections.ManagersDepartaments);
            IDownloadDepartament.ShowDepartament(Collections.Departaments);
        }
    }
}
