using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace TestFromDeeplayComp.Data
{
    internal class Connection2db
    {
        //string path = AppContext.BaseDirectory;
        //string path = @"D:\Документы\Visual Studio 2022\Project\WPF\ADO.NET\TestFromDeeplayComp\Data\Employees.db";

         static string pathProject = Environment.CurrentDirectory; // получает путь к файлу .exe(D:\Документы\Visual Studio 2022\Project\WPF\ADO.NET\TestFromDeeplayComp\bin\Debug\netcoreapp3.1)
         static string pathToDB = Path.GetFullPath(Path.Combine(pathProject, @"..\\..\\..\\Data\\Employees.db"));


        private readonly SqliteConnection connection = new SqliteConnection("Data Source=" + pathToDB + "; Mode=ReadWrite");
        public void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
        public SqliteConnection GetConnection()
        {
            return connection;
        }
    }
}
