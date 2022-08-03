using Microsoft.Data.Sqlite;
using System;
using TestFromDeeplayComp.Data;
using TestFromDeeplayComp.Models;

namespace TestFromDeeplayComp.Services.Interfaces
{
    internal interface IInsertNewProfile2DataBase
    {
        protected static int AddNewProfile(UserProfile profile)
        {
            int lastId = 0;
            string sqlQuery = "INSERT INTO Profile values (NULL, " + 
                "'" + profile.LastName + "', " +
                "'" + profile.FirstName + "', " +
                "'" + profile.MiddleName + "', " +
                "'" + profile.Birthday.ToShortDateString() + "', " +
                "'" + profile.Adress + "', " +
                "'" + profile.PhoneNumber + "')";

            Connection2db connection = new Connection2db();
            connection.OpenConnection();
            SqliteCommand InsertNewProfileCmd = new SqliteCommand(sqlQuery, connection.GetConnection());
            InsertNewProfileCmd.ExecuteNonQuery();

            // Получение последнего Id добавленной записи
            string sqlLastIdProfile = "SELECT  max(Id_profile) FROM Profile";
            SqliteCommand cmdSelectLastId = new SqliteCommand(sqlLastIdProfile, connection.GetConnection());
            SqliteDataReader sqliteDataReader = cmdSelectLastId.ExecuteReader();
            while (sqliteDataReader.Read())
            {
                lastId = Convert.ToInt32(sqliteDataReader[0]);
            }
            connection.CloseConnection();
            return lastId;
        }
    }
}
