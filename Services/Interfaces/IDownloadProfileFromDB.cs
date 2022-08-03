using Microsoft.Data.Sqlite;
using System;
using System.Collections.ObjectModel;
using TestFromDeeplayComp.Data;
using TestFromDeeplayComp.Models;

namespace TestFromDeeplayComp.Services.Interfaces
{
    internal interface IDownloadProfileFromDB
    {
        protected static void ShowProfile(ObservableCollection<UserProfile> Users)
        {
            string sqlQuery = "SELECT * FROM Profile";
            Connection2db connection = new Connection2db();
            connection.OpenConnection();
            SqliteCommand cmdSelectAllFromProfile = new SqliteCommand(sqlQuery, connection.GetConnection());
            SqliteDataReader sqliteDataReader = cmdSelectAllFromProfile.ExecuteReader();
            while (sqliteDataReader.Read())
            {
                UserProfile user = new UserProfile();
                user.IdUser = Convert.ToInt32(sqliteDataReader[0]);
                user.LastName = sqliteDataReader[1].ToString();
                user.FirstName = sqliteDataReader[2].ToString();
                user.MiddleName = sqliteDataReader[3].ToString();
                user.Birthday = Convert.ToDateTime(sqliteDataReader[4]);
                user.Adress = sqliteDataReader[5].ToString();
                user.PhoneNumber = sqliteDataReader[6].ToString();
                Users.Add(user);
            }
            sqliteDataReader.Close();
            connection.CloseConnection();
        }
    }
}
