using Microsoft.Data.Sqlite;
using System;
using System.Collections.ObjectModel;
using TestFromDeeplayComp.Data;
using TestFromDeeplayComp.Models;

namespace TestFromDeeplayComp.Services.Interfaces
{
    internal interface IDownloadWorkPersonal_Info
    {
        protected static void ShowAllWorkPersonalInfo(ObservableCollection<WorkPersonal_Info> workPersonal_Infos)
        {
            string sqlQuery = "SELECT Profile.ID_profile, work_personal.id_workPersonal, " +
                "Profile.Last_Name, Profile.First_Name, Profile.Middle_Name, Profile.Birthday, " +
                "Profile.Adress, Profile.Phone_Number, Post.Id_Post, Post.Name_Post, b.id_profile, b.id_manager, " +
                "b.Last_Name, b.First_Name, b.Middle_Name, b.Name_Departament, work_personal.date_Admission, work_personal.date_Fired " +
                "FROM work_personal JOIN Profile ON work_personal.id_profile = Profile.ID_profile " +
                "JOIN Post ON Post.Id_Post = work_personal.id_post JOIN" +
                "(SELECT manager_personal.id_manager, manager_personal.id_profile, " +
                "Profile.Last_Name, Profile.First_Name, Profile.Middle_Name, Post.Name_Post, " +
                "Departament.Name_Departament " +
                "FROM manager_personal " +
                "JOIN Profile ON manager_personal.id_profile = Profile.ID_profile " +
                "JOIN Post ON manager_personal.id_post = Post.Id_Post " +
                "JOIN Departament ON manager_personal.id_departament = Departament.Id_Departament ) " +
                "as b ON b.id_manager = work_personal.id_manager";

            Connection2db connection = new Connection2db();
            connection.OpenConnection();
            SqliteCommand cmdSelectAllInfoWorkPersonal = new SqliteCommand(sqlQuery, connection.GetConnection());
            SqliteDataReader sqliteDataReader = cmdSelectAllInfoWorkPersonal.ExecuteReader();

            while (sqliteDataReader.Read())
            {
                WorkPersonal_Info workPersonal = new WorkPersonal_Info();

                workPersonal.IdProfileWorkPersonal = Convert.ToInt32(sqliteDataReader[0]);
                workPersonal.IdWorkPersonal = Convert.ToInt32(sqliteDataReader[1]);
                workPersonal.LastNameWorkPersonal = sqliteDataReader[2].ToString();
                workPersonal.FirstNameWorkPersonal = sqliteDataReader[3].ToString();
                workPersonal.MiddleNameWorkPersonal = sqliteDataReader[4].ToString();
                workPersonal.BirthdayWorkPersonal = sqliteDataReader[5].ToString();
                workPersonal.AdressWorkPersonal = sqliteDataReader[6].ToString();
                workPersonal.PhoneNumberWorkPersonal = sqliteDataReader[7].ToString();
                workPersonal.IdPostWorkPersonal = Convert.ToInt32(sqliteDataReader[8]);
                workPersonal.PostWorkPersonal = sqliteDataReader[9].ToString();
                workPersonal.PersonalManagerIdProfile = Convert.ToInt32(sqliteDataReader[10]);
                workPersonal.PersonalManagerIdManager = Convert.ToInt32(sqliteDataReader[11]);

                workPersonal.PersonalManagerLastName = sqliteDataReader[12].ToString();
                workPersonal.PersonalManagerFirstName = sqliteDataReader[13].ToString();
                workPersonal.PersonalManagerMiddleName = sqliteDataReader[14].ToString();
                workPersonal.PersonalManagerDepartament = sqliteDataReader[15].ToString();
                workPersonal.WorkPersonalDateAdmission = sqliteDataReader[16].ToString();

                if (DBNull.Value.Equals(sqliteDataReader[17]) == false)
                {
                    workPersonal.WorkPersonalDateFired = sqliteDataReader[17].ToString();
                }
                else
                {
                    workPersonal.WorkPersonalDateFired = "Нет информации";
                }
                workPersonal_Infos.Add(workPersonal);
            }
            sqliteDataReader.Close();
            connection.CloseConnection();
        }
    }
}
