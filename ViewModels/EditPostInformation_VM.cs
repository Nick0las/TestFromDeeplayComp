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
    internal class EditPostInformation_VM : Base_VM, IDownloadPostFromDb, IDownloadDepartament, IDownloadWorkPersonal_Info, 
                                            IDownload_ManagerAllInfo, IEmployeeFired, IInsertNewDateContract, IInsertNewEmployee, IDownloadProfileFromDB
    {
        #region заголовок окна
        private string _Tittle = "Изменение должностной информации";
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
            set => Set(ref _name, value);
        }
        // Отчество
        private string _middleName;
        public string MiddleName
        {
            get => _middleName;
            set => Set(ref _middleName, value);
        }

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

        // Дата увольнения

        public DateTime DateFired { get; set; } = DateTime.Now;
        #endregion

        
        ManagerPersonal_Info manager = new ManagerPersonal_Info();
        WorkPersonal_Info warker = new WorkPersonal_Info();
        int lastIdDateContract { get; set; } = 0;

        #region Команды
        public ICommand EditPositionDataCmd { get; }
        private bool CanEditPositionDataCmdExecute(object p) => true;
        private void OnEditPositionDataCmdExecuted(object p)
        {
            if(manager.DateFired != null)
            {
                warker = null;
                switch (SelectedPost.IdPost)
                {
                    case 1:
                        IEmployeeFired.UpdateDateFired(manager, DateFired);
                        if (EndDateContract == DateTime.Today)
                        {
                            MessageBoxResult res = MessageBox.Show("Выбрана текущая дата окончания договора! Измените!", "Внимание! Измените дату договора!", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                        else
                        {
                            
                            lastIdDateContract = IInsertNewDateContract.AddNewDateContract(EndDateContract);
                            IInsertNewEmployee.AddNewManager
                                (manager.IdProfileManager, SelectedPost.IdPost, lastIdDateContract, DateAdmission, 0);
                            MessageBoxResult res = MessageBox.Show("Данные внесены успешно!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        break;
                    case 2:
                    case 3:
                    case 4:
                        IEmployeeFired.UpdateDateFired(manager, DateFired);
                        IInsertNewEmployee.AddNewManager
                            (manager.IdProfileManager, SelectedPost.IdPost, lastIdDateContract, DateAdmission, SelectedDepartament.IdDepartment);
                        MessageBox.Show("Сотрудник был уволен и устроен на новую должность!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    case 5:
                        if(SelectedUser == null)
                        {
                            MessageBox.Show("Выберите руководителя!");
                            return;
                        }
                        IEmployeeFired.UpdateDateFired(manager, DateFired);
                        IInsertNewEmployee.AddNewWorker(manager.IdProfileManager, SelectedPost.IdPost, SelectedUser.IdProfile, DateAdmission);
                        MessageBox.Show("Сотрудник был уволен и устроен на новую должность!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                }
                Collections.ManagersInfo.Clear();
                IDownload_ManagerAllInfo.ShowAllManagerInfo(Collections.ManagersInfo);
            }
            if (warker != null)
            {
                manager = null;
                switch (SelectedPost.IdPost)
                {
                    case 1:
                        IEmployeeFired.UpdateDateFiredWorker(warker, DateFired);
                        if (EndDateContract == DateTime.Today)
                        {
                            MessageBoxResult res = MessageBox.Show("Выбрана текущая дата окончания договора! Измените!", "Внимание! Измените дату договора!", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                        else
                        {

                            lastIdDateContract = IInsertNewDateContract.AddNewDateContract(EndDateContract);
                            IInsertNewEmployee.AddNewManager
                                (warker.IdProfileWorkPersonal, SelectedPost.IdPost, lastIdDateContract, DateAdmission, 1);
                            MessageBox.Show("Операция прошла успешно!");
                        }
                        break;
                    case 2:
                        IEmployeeFired.UpdateDateFiredWorker(warker, DateFired);
                        if(SelectedDepartament == null)
                        {
                            MessageBox.Show("Выберите отдел!");
                            return;
                        }
                        IInsertNewEmployee.AddNewManager
                            (warker.IdProfileWorkPersonal, SelectedPost.IdPost, lastIdDateContract, DateAdmission, SelectedDepartament.IdDepartment);
                        MessageBox.Show("Операция прошла успешно!");
                        break;
                    case 3:
                    case 4:
                        IEmployeeFired.UpdateDateFiredWorker(warker, DateFired);
                        IInsertNewEmployee.AddNewManager
                            (warker.IdProfileWorkPersonal, SelectedPost.IdPost, lastIdDateContract, DateAdmission, 0);
                        MessageBox.Show("Операция прошла успешно!");
                        break;
                    case 5:
                        IEmployeeFired.UpdateDateFiredWorker(warker, DateFired);
                        IInsertNewEmployee.AddNewWorker(warker.IdProfileWorkPersonal, SelectedPost.IdPost, SelectedUser.IdProfile, DateAdmission);
                        MessageBox.Show("Операция прошла успешно!");
                        break;
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

        public EditPostInformation_VM()
        {
            EditPositionDataCmd = new LamdaCommand(OnEditPositionDataCmdExecuted, CanEditPositionDataCmdExecute);
            
            Collections.Posts.Clear();
            Collections.PostsInfos.Clear();
            Collections.ManagersDepartaments.Clear();
            Collections.Departaments.Clear();

            IDownloadPostFromDb.ShowPost(Collections.Posts);
            IDownloadPostFromDb.ShowPostInfo(Collections.PostsInfos);
            IDownloadPostFromDb.ShowManagerDepartament(Collections.ManagersDepartaments);
            IDownloadDepartament.ShowDepartament(Collections.Departaments);
            if(Collections.ManagersInfo.Count == 1)
            {
                manager = FillInFieldsManager();
            }
            if(Collections.WorkPersonals_Info.Count == 1)
            {
                warker = FillInFieldsWorker();
            }
            

            Collections.WorkPersonals_Info.Clear();
            Collections.ManagersInfo.Clear();

            IDownloadWorkPersonal_Info.ShowAllWorkPersonalInfo(Collections.WorkPersonals_Info);
            IDownload_ManagerAllInfo.ShowAllManagerInfo(Collections.ManagersInfo);
        }

        private ManagerPersonal_Info FillInFieldsManager( )
        {
            ManagerPersonal_Info managerInfo = Collections.ManagersInfo[0];
            Surname = managerInfo.LastNameManager;
            Name = managerInfo.FirstNameManager;
            MiddleName = managerInfo.MiddleNameManager;
            DateAdmission = Convert.ToDateTime(managerInfo.ManagerDateAdmission);
            int idProfileManager = managerInfo.IdProfileManager;
            if (managerInfo.Id_post != 1)
            {
                EndDateContract = DateTime.Today;
            }
            else
            {
                EndDateContract = Convert.ToDateTime(managerInfo.ManagerEndDateContract);
            }
            
            for(int i = 0; i < Collections.PostsInfos.Count; i++)
            {
                if(Collections.PostsInfos[i].IdPost == managerInfo.Id_post)
                {
                    SelectedPost = Collections.PostsInfos[i];
                }
            }
            for (int i = 0; i < Collections.Departaments.Count; i++)
            {
                if (Collections.Departaments[i].NameDepartment == managerInfo.DepartamentManager)
                {
                    SelectedDepartament = Collections.Departaments[i];
                }
            }
            return managerInfo;
        }
        private WorkPersonal_Info FillInFieldsWorker()
        {
            var warker = Collections.WorkPersonals_Info[0];
            Surname = warker.LastNameWorkPersonal;
            Name = warker.FirstNameWorkPersonal;
            MiddleName = warker.MiddleNameWorkPersonal;
            DateAdmission = Convert.ToDateTime(warker.WorkPersonalDateAdmission);
            int idProfileWarker = warker.IdProfileWorkPersonal;

            for (int i = 0; i < Collections.PostsInfos.Count; i++)
            {
                if (Collections.PostsInfos[i].IdPost == warker.IdPostWorkPersonal)
                {
                    SelectedPost = Collections.PostsInfos[i];
                }
            }
            for (int i = 0; i < Collections.ManagersDepartaments.Count; i++)
            {
                if(Collections.ManagersDepartaments[i].IdProfile == warker.PersonalManagerIdProfile)
                {
                    SelectedUser = Collections.ManagersDepartaments[i];
                }
            }
            return warker;
        }
    }
}
