using Microsoft.Data.Sqlite;
using System;
using System.Collections.ObjectModel;
using TestFromDeeplayComp.Data;
using TestFromDeeplayComp.Models;

namespace TestFromDeeplayComp.Services.Interfaces
{
    internal interface IDownloadPostFromDb
    {
        protected static void ShowPost(ObservableCollection<Post> posts)
        {
            string sqlQuery = "SELECT * FROM post";
            Connection2db connection = new Connection2db();
            connection.OpenConnection();
            SqliteCommand cmdSelectAllFromPost = new SqliteCommand(sqlQuery, connection.GetConnection());
            SqliteDataReader sqliteDataReader = cmdSelectAllFromPost.ExecuteReader();
            while (sqliteDataReader.Read())
            {
                Post post = new Post();
                post.IdPost = Convert.ToInt32(sqliteDataReader[0]);
                post.NamePost = sqliteDataReader[1].ToString();
                //post.IdTypeControler = Convert.ToInt32(sqliteDataReader[2]);
                if (DBNull.Value.Equals(sqliteDataReader[2]) == false)
                {
                    post.IdTypeControler = Convert.ToInt32(sqliteDataReader[2]);
                }
                else
                {
                    post.IdTypeControler = null;
                }
                posts.Add(post);
            }
            sqliteDataReader.Close();
            connection.CloseConnection();
        }

        protected static void ShowPostInfo(ObservableCollection<PostsInfo> InfoPosts)
        {
            string sqlQuery = "SELECT Post.Id_Post, Post.Name_Post, type_controler.typeControler " +
                "FROM Post LEFT OUTER JOIN type_controler " +
                "ON Post.id_typeControler=type_controler.id_typeControler";
            Connection2db connection = new Connection2db();
            connection.OpenConnection();
            SqliteCommand cmdSelectPostInfo = new SqliteCommand(sqlQuery, connection.GetConnection());
            SqliteDataReader sqliteDataReader = cmdSelectPostInfo.ExecuteReader();
            while (sqliteDataReader.Read())
            {
                PostsInfo postInfo = new PostsInfo();
                postInfo.IdPost = Convert.ToInt32(sqliteDataReader[0]);
                postInfo.NamePost = sqliteDataReader[1].ToString();
                postInfo.TypeControler = sqliteDataReader[2].ToString();
                InfoPosts.Add(postInfo);
            }
            sqliteDataReader.Close();
            connection.CloseConnection();
        }

        protected static void ShowManagerDepartament(ObservableCollection<DepartamentManager> ManagersDepartaments)
        {
            string sqlQuery = "SELECT manager_personal.id_profile, Profile.Last_Name, Profile.First_Name, " +
                "Profile.Middle_Name, Post.Name_Post,  Departament.Name_Departament FROM manager_personal " +
                "JOIN Profile " +
                "ON manager_personal.id_profile=Profile.ID_profile " +
                "JOIN Post " +
                "ON manager_personal.id_post=Post.Id_Post " +
                "JOIN Departament " +
                "ON manager_personal.id_departament=Departament.Id_Departament";

            Connection2db connection = new Connection2db();
            connection.OpenConnection();
            SqliteCommand cmdSelectManagersDepartament = new SqliteCommand(sqlQuery, connection.GetConnection());
            SqliteDataReader sqliteDataReader = cmdSelectManagersDepartament.ExecuteReader();
            while (sqliteDataReader.Read())
            {
                DepartamentManager manager = new DepartamentManager();
                manager.IdProfile = Convert.ToInt32(sqliteDataReader[0]);
                manager.LastName = sqliteDataReader[1].ToString();
                manager.FirstName = sqliteDataReader[2].ToString();
                manager.MiddleName = sqliteDataReader[3].ToString();
                manager.NamePost = sqliteDataReader[4].ToString();
                manager.NameDepartament = sqliteDataReader[5].ToString();
                ManagersDepartaments.Add(manager);
            }
            sqliteDataReader.Close();
            connection.CloseConnection();
        }
    }
}
