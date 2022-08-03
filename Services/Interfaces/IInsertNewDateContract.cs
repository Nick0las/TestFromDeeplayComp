using Microsoft.Data.Sqlite;
using System;
using TestFromDeeplayComp.Data;
using TestFromDeeplayComp.Models;

namespace TestFromDeeplayComp.Services.Interfaces
{
    internal interface IInsertNewDateContract
    {
        protected static int AddNewDateContract(DateTime endDateContract)
        {
            string date = "'" + endDateContract.ToShortDateString() + "')";
            string sqlQuery = "INSERT INTO contract VALUES (NULL, " + date;

            int lastId = 0;
            string sqlLastIdContract = "SELECT  max(id_contract) FROM contract";

            Connection2db connection = new Connection2db();
            connection.OpenConnection();
            SqliteCommand InsertNewContractCmd = new SqliteCommand(sqlQuery, connection.GetConnection());
            InsertNewContractCmd.ExecuteNonQuery();

            // Получение Id добавленной записи
            SqliteCommand SelectLastIdCmd = new SqliteCommand(sqlLastIdContract, connection.GetConnection());
            SqliteDataReader sqliteDataReader = SelectLastIdCmd.ExecuteReader();
            while (sqliteDataReader.Read())
            {
                lastId = Convert.ToInt32(sqliteDataReader[0]);
            }
            connection.CloseConnection();
            return lastId;
        }
    }
}
