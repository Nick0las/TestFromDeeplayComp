using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TestFromDeeplayComp.Data;
using TestFromDeeplayComp.Models;

namespace TestFromDeeplayComp.Services.Interfaces
{
    internal interface IDownloadDepartament
    {
        protected static void ShowDepartament(ObservableCollection<Departament> departments)
        {
            string sqlQuery = "SELECT * FROM departament";
            Connection2db connection = new Connection2db();
            connection.OpenConnection();
            SqliteCommand cmdSelectAllFromDepartament = new SqliteCommand(sqlQuery, connection.GetConnection());
            SqliteDataReader sqliteDataReader = cmdSelectAllFromDepartament.ExecuteReader();
            while (sqliteDataReader.Read())
            {
                Departament departament = new Departament();
                departament.IdDepartment = Convert.ToInt32(sqliteDataReader[0]);
                departament.NameDepartment = sqliteDataReader[1].ToString();
                departments.Add(departament);
            }
            sqliteDataReader.Close();
            connection.CloseConnection();
        }
    }
}
