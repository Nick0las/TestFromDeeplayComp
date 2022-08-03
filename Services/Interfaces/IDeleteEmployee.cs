using Microsoft.Data.Sqlite;
using System;
using System.Collections.ObjectModel;
using TestFromDeeplayComp.Data;
using TestFromDeeplayComp.Models;
namespace TestFromDeeplayComp.Services.Interfaces
{
    internal interface IDeleteEmployee
    {
        protected static void DeleteWork_personal( int idProfile)
        {
            string sqlQuery = "DELETE FROM work_personal WHERE id_profile = " + idProfile;

            Connection2db connection = new Connection2db();
            connection.OpenConnection();

            SqliteCommand DeleteWorkerCmd = new SqliteCommand(sqlQuery, connection.GetConnection());
            DeleteWorkerCmd.ExecuteNonQuery();
            connection.CloseConnection();
        }

        protected static void DeleteManager_personal(int idProfile)
        {
            string sqlQuery = "DELETE FROM manager_personal WHERE id_profile = " + idProfile;

            Connection2db connection = new Connection2db();
            connection.OpenConnection();

            SqliteCommand DeleteManagerCmd = new SqliteCommand(sqlQuery, connection.GetConnection());
            DeleteManagerCmd.ExecuteNonQuery();
            connection.CloseConnection();
        }

        protected static void DeleteProfile(int idProfile)
        {
            string sqlQuery = "DELETE FROM Profile WHERE id_profile = " + idProfile;

            Connection2db connection = new Connection2db();
            connection.OpenConnection();

            SqliteCommand DeleteProfileCmd = new SqliteCommand(sqlQuery, connection.GetConnection());
            DeleteProfileCmd.ExecuteNonQuery();
            connection.CloseConnection();
        }
    }
}
