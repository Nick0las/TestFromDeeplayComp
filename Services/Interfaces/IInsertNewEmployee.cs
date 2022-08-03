using Microsoft.Data.Sqlite;
using System;
using TestFromDeeplayComp.Data;
using TestFromDeeplayComp.Models;

namespace TestFromDeeplayComp.Services.Interfaces
{
    internal interface IInsertNewEmployee
    {
        protected static void AddNewManager(int idProfile, int idPost, int idDateContract, DateTime dateAdmission, int IdDepartment)
        {
            string sqlQuery = "";
            switch (idPost)
            {
                case 1:
                    sqlQuery = "INSERT INTO manager_personal VALUES (NULL, " + 
                                idProfile + ", " + 
                                idPost + ", " + 
                                "NULL, " +
                                idDateContract + ", " + 
                                "'" + dateAdmission.ToShortDateString() + "'" + ", NULL)";
                    break;

                case 2:
                    sqlQuery = "INSERT INTO manager_personal VALUES " +
                        "(NULL, " +
                        idProfile + ", " + idPost + ", " + 
                        IdDepartment + ", NULL, " +
                        "'" + dateAdmission.ToShortDateString() + "'" + ", NULL)";
                    break;
                case (3):
                    sqlQuery = "INSERT INTO manager_personal VALUES " +
                        "(NULL, " + idProfile + ", " + idPost + ", NULL, NULL, " + "'" + dateAdmission.ToShortDateString() + "'" + ", NULL)";
                    break;
                case (4):
                    sqlQuery = "INSERT INTO manager_personal VALUES " +
                        "(NULL, " + idProfile + ", " + idPost + ", NULL, NULL, " + "'" + dateAdmission.ToShortDateString() + "'" + ", NULL)";
                    break;
            }

            Connection2db connection = new Connection2db();
            connection.OpenConnection();
            SqliteCommand InsertIntoManagerPersonalCmd = new SqliteCommand(sqlQuery, connection.GetConnection());
            InsertIntoManagerPersonalCmd.ExecuteNonQuery();
            connection.CloseConnection();
        }

        protected static void AddNewWorker(int idProfile, int idPost, int idManager, DateTime dateAdmission)
        {
            string sqlQuery = "INSERT INTO work_personal VALUES " +
                               "(NULL, " + idProfile + ", " + 
                               idPost + ", " + idManager + ", " +
                               "'" + dateAdmission.ToShortDateString() + "', NULL)";
            Connection2db connection = new Connection2db();
            connection.OpenConnection();
            SqliteCommand InsertIntoWorkPersonalCmd = new SqliteCommand(sqlQuery, connection.GetConnection());
            InsertIntoWorkPersonalCmd.ExecuteNonQuery();
            connection.CloseConnection();
        }
    }
}
