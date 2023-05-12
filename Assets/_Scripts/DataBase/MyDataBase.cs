using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System;
using System.Data;
using MyNetworking;

namespace DataBase
{
    public class MyDataBase : MonoBehaviour
    {
        string userNames = "URI=file:usernames.db";
        string usersData = "URI=file:prettyUnicorn.db";

        // Start is called before the first frame update
        void Start()
        {            
            CreateUsersDB();
            PrintAllUsers();
        }
        #region DBCreations
        private void CreateUsersDB()
        {
            using (SqliteConnection conn = new SqliteConnection(userNames))
            {
                conn.Open();
                using (SqliteCommand command = conn.CreateCommand())
                {
                    command.CommandText = "CREATE TABLE IF NOT EXISTS users (userName VARCHAR(20), userPass VARCHAR(10));";
                    command.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
        private void CreateUsersDatasDB()
        {
            using (SqliteConnection conn = new SqliteConnection(usersData))
            {
                conn.Open();
                using (SqliteCommand command = conn.CreateCommand())
                {
                    command.CommandText = "CREATE TABLE IF NOT EXISTS usernames " +
                        "(name VARCHAR(20), " +
                        "pass VARCHAR(10)," +
                        "year INT," +
                        "month INT," +
                        "day INT," +
                        "data VARCHAR(20));";
                    command.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
        #endregion

        #region DBInsertions
        public bool InsertNewUser(string name, string pass)
        {
            using (SqliteConnection conn = new SqliteConnection(userNames))
            {
                conn.Open();
                using (SqliteCommand command = conn.CreateCommand())
                {
                    command.CommandText = $"INSERT INTO users (userName , userPass ) VALUES ('{name}', '{pass}');";
                    command.ExecuteNonQuery();
                }
                conn.Close();
            }
            return true;
        }
        #endregion

        public bool TrySignUp(Messages.SignUpMsg signUpMsg)
        {
            if (IsUserExists(signUpMsg.user_name))
                return false;
            
            return InsertNewUser(signUpMsg.user_name, signUpMsg.user_password);
        }
        public bool IsUserExists(string name)
        {
            bool isFound = false;
            using (SqliteConnection conn = new SqliteConnection(userNames))
            {                
                conn.Open();
                using (SqliteCommand command = conn.CreateCommand())
                {
                    command.CommandText = $"SELECT userName FROM users WHERE userName='{name}';";
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            isFound = true;
                        reader.Close();
                    }
                }
                conn.Close();
            }
            return isFound;
        }

        public Messages.LoginMsg GetUser(Messages.LoginMsg loginMsg)
        {
            Messages.LoginMsg dbData = new Messages.LoginMsg();
            using (SqliteConnection conn = new SqliteConnection(userNames))
            {
                conn.Open();
                using (SqliteCommand command = conn.CreateCommand())
                {
                    command.CommandText = $"SELECT * FROM users WHERE userName='{loginMsg.user_name}' AND userPass='{loginMsg.user_password}';";
                    using (IDataReader reader = command.ExecuteReader())
                    {                        
                        if (reader.Read())
                        {
                            dbData.user_name = reader["userName"].ToString();
                            dbData.user_password = reader["userPass"].ToString();
                        }
                        else
                        {
                            dbData.user_name = string.Empty;
                            dbData.user_password = string.Empty;
                        }
                        reader.Close();
                    }
                }
                conn.Close();
            }
            return dbData;
        }

        #region Debugging
        public void PrintAllUsers()
        {
            Messages.LoginMsg dbData = new Messages.LoginMsg();
            using (SqliteConnection conn = new SqliteConnection(userNames))
            {
                conn.Open();
                using (SqliteCommand command = conn.CreateCommand())
                {
                    command.CommandText = $"SELECT * FROM users;";
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            Debug.Log($"DB_Debug_ Name: {reader["userName"].ToString()} Pass: {reader["userPass"].ToString()}");
                        reader.Close();
                    }
                }
                conn.Close();
            }
        }
        #endregion
    }
}
