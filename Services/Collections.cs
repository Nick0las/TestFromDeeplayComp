using System;
using System.Collections.ObjectModel;
using TestFromDeeplayComp.Models;

namespace TestFromDeeplayComp.Services
{
    internal class Collections
    {
        // Коллекция для хранения данных о пользователях (анкетные данные)
        public static ObservableCollection<UserProfile> Users { get; set; } = new ObservableCollection<UserProfile>();

        // Коллекция, хранящая данные о существующих должностях
        public static ObservableCollection<Post> Posts { get; set; } = new ObservableCollection<Post>();

        // Коллекция, хранящая данные о существующих отделах
        public static ObservableCollection<Departament> Departaments { get; set; } = new ObservableCollection<Departament>();

        // Коллекция, хранящая данные о руководителях (таблица manager_personal)
        public static ObservableCollection<Manager> Managers { get; set; } = new ObservableCollection<Manager>();

        // Коллекция, хранящая данные о рабочем персонале (таблица WorkPersonal)
        public static ObservableCollection<WorkPersonal> WorkPersonals { get; set; } = new ObservableCollection<WorkPersonal>();

        // Коллекция, хранящая всю информацию о должностях (id, namePost, TypeControler)
        public static ObservableCollection<PostsInfo> PostsInfos { get; set; } = new ObservableCollection<PostsInfo>();

        // Коллекция, хрянаящая информацию об управляющих подразделениями (ФИО, должность, отдел)
        public static ObservableCollection<DepartamentManager> ManagersDepartaments { get; set; } = new ObservableCollection<DepartamentManager>();

        // Коллекция, хранящая данные о рабочем персонале (ФИО,адрес, дата рождения, телефон, должность, id_руководителя,id_Профиля_Руководителя, ФИО_Руководителя, отдел)
        public static ObservableCollection<WorkPersonal_Info> WorkPersonals_Info { get; set; } = new ObservableCollection<WorkPersonal_Info>();

        // Коллекция, хранящая информацию о управляющем персонале (ФИО, адрес, дата рождения, телефон, должность, отдел, тип контролера)
        public static ObservableCollection<ManagerPersonal_Info> ManagersInfo { get; set; } = new ObservableCollection<ManagerPersonal_Info>();
    }
}
