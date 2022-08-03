using Microsoft.Data.Sqlite;
using System;
using System.Collections.ObjectModel;
using TestFromDeeplayComp.Data;
using TestFromDeeplayComp.Models;

namespace TestFromDeeplayComp.Services.Interfaces
{
    internal interface IEditProfile
    {
        protected static void EditProfile(UserProfile profile)
        {
            string sqlQuery = "UPDATE Profile SET " +
                "Last_Name = " + "'"+ profile.LastName + "'" +
                ", First_Name = " + "'" + profile.FirstName + "'" +
                ", Middle_Name = " + "'" + profile.MiddleName + "'" +
                ", Birthday = " + "'" + profile.Birthday + "'" +
                ", Adress = " + "'" + profile.Adress + "'" +
                ", Phone_Number = " + "'" + profile.PhoneNumber + "'" +
                "WHERE Profile.ID_profile = " + profile.IdUser;

            Connection2db connection = new Connection2db();
            connection.OpenConnection();

            SqliteCommand UpdateProfileCmd = new SqliteCommand(sqlQuery, connection.GetConnection());
            UpdateProfileCmd.ExecuteNonQuery();
            connection.CloseConnection();
        }
    }
}
