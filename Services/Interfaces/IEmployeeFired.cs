using Microsoft.Data.Sqlite;
using System;
using TestFromDeeplayComp.Data;
using TestFromDeeplayComp.Models;

namespace TestFromDeeplayComp.Services.Interfaces
{
    internal interface IEmployeeFired
    {
        protected static void UpdateDateFired(ManagerPersonal_Info manager, DateTime DateFired)
        {
            string dateFired = "'" + DateFired.ToShortDateString() + "'";
            string sqlQuery = "UPDATE manager_personal set date_Fired = " + dateFired + "WHERE id_manager = " + manager.IdManager;

            Connection2db connection = new Connection2db();
            connection.OpenConnection();

            SqliteCommand UpdateDateFiredCmd = new SqliteCommand(sqlQuery, connection.GetConnection());
            UpdateDateFiredCmd.ExecuteNonQuery();
            connection.CloseConnection();
        }

        protected static void UpdateDateFiredWorker(WorkPersonal_Info worker, DateTime DateFired)
        {
            string dateFired = "'" + DateFired.ToShortDateString() + "'";
            string sqlQuery = "UPDATE work_personal SET date_Fired = " + dateFired + " WHERE id_workPersonal = " + worker.IdWorkPersonal;

            Connection2db connection = new Connection2db();
            connection.OpenConnection();

            SqliteCommand UpdateDateFiredCmd = new SqliteCommand(sqlQuery, connection.GetConnection());
            UpdateDateFiredCmd.ExecuteNonQuery();
            connection.CloseConnection();
        }
    }
}
