using Microsoft.Data.Sqlite;
using System;
using System.Collections.ObjectModel;
using TestFromDeeplayComp.Data;
using TestFromDeeplayComp.Models;
namespace TestFromDeeplayComp.Services.Interfaces
{
    internal interface IDownload_ManagerAllInfo
    {
        protected static void ShowAllManagerInfo(ObservableCollection<ManagerPersonal_Info> ManagersInfo)
        {
            string sqlQuery = "SELECT manager_personal.id_manager, Profile.ID_profile, Profile.Last_Name, Profile.First_Name, " +
                "Profile.Middle_Name, Profile.Birthday, Profile.Adress, Profile.Phone_Number, manager_personal.id_post, Post.Name_Post, Departament.Name_Departament, " +
                "type_controler.typeControler, manager_personal.Date_Admission, contract.endDateContract, manager_personal.Date_Fired " +
                "FROM manager_personal " +
                "JOIN Profile ON manager_personal.id_profile=Profile.ID_profile " +
                "LEFT OUTER JOIN Post ON manager_personal.id_post=Post.Id_Post " +
                "LEFT OUTER JOIN Departament ON manager_personal.id_departament=Departament.Id_Departament " +
                "LEFT OUTER JOIN type_controler ON Post.id_typeControler=type_controler.id_typeControler " +
                "LEFT OUTER JOIN contract ON manager_personal.id_contract = contract.id_contract";

            Connection2db connection = new Connection2db();
            connection.OpenConnection();
            SqliteCommand cmdSelectAllInfoManagers = new SqliteCommand(sqlQuery, connection.GetConnection());
            SqliteDataReader sqliteDataReader = cmdSelectAllInfoManagers.ExecuteReader();
            while (sqliteDataReader.Read())
            {
                ManagerPersonal_Info manager = new ManagerPersonal_Info();
                manager.IdManager = Convert.ToInt32(sqliteDataReader[0]);
                manager.IdProfileManager = Convert.ToInt32(sqliteDataReader[1]);
                manager.LastNameManager = sqliteDataReader[2].ToString();
                manager.FirstNameManager = sqliteDataReader[3].ToString();
                manager.MiddleNameManager = sqliteDataReader[4].ToString();
                manager.BirthdayManager = sqliteDataReader[5].ToString();
                manager.AdressManager = sqliteDataReader[6].ToString();
                manager.PhoneNumberManager = sqliteDataReader[7].ToString();
                manager.Id_post = Convert.ToInt32(sqliteDataReader[8]);
                manager.PostManager = sqliteDataReader[9].ToString();
                if (DBNull.Value.Equals(sqliteDataReader[10]) == false)
                {
                    manager.DepartamentManager = sqliteDataReader[10].ToString();
                }
                else
                    manager.DepartamentManager = "Нет информации";
                if (DBNull.Value.Equals(sqliteDataReader[11]) == false)
                {
                    manager.TypeControler = sqliteDataReader[11].ToString();
                }
                else
                {
                    manager.TypeControler = "Нет информации";
                }
                manager.ManagerDateAdmission = sqliteDataReader[12].ToString();

                if (DBNull.Value.Equals(sqliteDataReader[13]) == false)
                {
                    manager.ManagerEndDateContract = sqliteDataReader[13].ToString();
                }
                else
                {
                    manager.ManagerEndDateContract = "Нет информации";
                }
                if(DBNull.Value.Equals(sqliteDataReader[14]) == false)
                {
                    manager.DateFired = sqliteDataReader[14].ToString();
                }
                else
                {
                    manager.DateFired = "Нет информации";
                }

                ManagersInfo.Add(manager);
            }
            sqliteDataReader.Close();
            connection.CloseConnection();
        }
    }
}
