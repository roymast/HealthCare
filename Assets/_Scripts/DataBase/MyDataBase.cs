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
            CreateUsersDatasDB();            
        }

        //---------------------------------------------------------------------------------------------------------------------------------------
        //-------------------------Users DB------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------------------------
        #region UsersDB

        private void CreateUsersDB()
        {
            using (SqliteConnection conn = new SqliteConnection(userNames))
            {
                conn.Open();
                using (SqliteCommand command = conn.CreateCommand())
                {
                    command.CommandText = "CREATE TABLE IF NOT EXISTS users (userName VARCHAR(20) NOT NULL PRIMARY KEY, userPass VARCHAR(10) NOT NULL);";
                    command.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        #region LoginSignUp
        public bool InsertNewUser(string name, string pass)
        {
            bool isInserted = false;
            using (SqliteConnection conn = new SqliteConnection(userNames))
            {
                conn.Open();
                using (SqliteCommand command = conn.CreateCommand())
                {
                    try
                    {
                        command.CommandText = $"INSERT INTO users (userName , userPass ) VALUES ('{name}', '{pass}');";
                        command.ExecuteNonQuery();
                        isInserted = true;
                    }
                    catch (Exception)
                    {
                        isInserted = false;
                    }
                }
                conn.Close();
            }
            return isInserted;
        }        
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

        public Messages.LoginMsg TryLogin(Messages.LoginMsg loginMsg)
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
        #endregion
        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------
        //-------------------------Users Datas DB------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------------------------
        #region UsersDatasDB

        private void CreateUsersDatasDB()
        {
            using (SqliteConnection conn = new SqliteConnection(usersData))
            {
                conn.Open();
                using (SqliteCommand command = conn.CreateCommand())
                {
                    command.CommandText = "CREATE TABLE IF NOT EXISTS usersDayDatas " +
                        "(name VARCHAR(20) NOT NULL, " +
                        "year INT NOT NULL ," +
                        "month INT NOT NULL ," +
                        "day INT NOT NULL ," +
                        "BreakFast VARCHAR(255)," +
                        "Lanch VARCHAR(255)," +
                        "Dinner VARCHAR(255)," +
                        "Night VARCHAR(255)," +
                        "WaterInML VARCHAR(255)," +
                        "GeneralFeel VARCHAR(255)," +
                        "Tierness VARCHAR(20)," +
                        "TodaysAchivments VARCHAR(255));";
                    command.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public bool InsertNewDay(Messages.SpecificDayData specificDayData)
        {
            bool isInserted = false;
            using (SqliteConnection conn = new SqliteConnection(usersData))
            {
                conn.Open();
                using (SqliteCommand command = conn.CreateCommand())
                {
                    try
                    {
                        command.CommandText = $"INSERT INTO usersDayDatas VALUES (" +
                        $"'{specificDayData.user_name}', " +
                        $"'{specificDayData.DayData.year}'," +
                        $"'{specificDayData.DayData.month}'," +
                        $"'{specificDayData.DayData.day}'," +
                        $"'{specificDayData.DayData.BreakFast}'," +
                        $"'{specificDayData.DayData.Lanch}'," +
                        $"'{specificDayData.DayData.Dinner}'," +
                        $"'{specificDayData.DayData.Night}'," +
                        $"'{specificDayData.DayData.WaterInML}'," +
                        $"'{specificDayData.DayData.GeneralFeel}'," +
                        $"'{specificDayData.DayData.Tierness}'," +
                        $"'{specificDayData.DayData.TodaysAchivments}'" +
                        $");";
                        command.ExecuteNonQuery();
                        isInserted = true;
                    }
                    catch (Exception)
                    {
                        isInserted = false;
                    }
                }
                conn.Close();
            }
            return isInserted;
        }
        public Messages.SpecificDayData GetSpecificDayData(Messages.AskForSpecificDayData ask)
        {
            Messages.SpecificDayData specificDayData = new Messages.SpecificDayData();
            specificDayData.user_name = ask.user_name;
            specificDayData.DayData = new DayData(ask.year, ask.month, ask.day);
            using (SqliteConnection conn = new SqliteConnection(usersData))
            {
                conn.Open();
                using (SqliteCommand command = conn.CreateCommand())
                {
                    command.CommandText = $"SELECT * FROM usersDayDatas " +
                        $"WHERE name='{ask.user_name}' AND year='{ask.year}' AND month='{ask.month}' AND day='{ask.day}';";
                    using (IDataReader reader = command.ExecuteReader())
                    {                        
                        try
                        {
                            if (reader.Read())
                            {                                
                                specificDayData.DayData.BreakFast = reader["BreakFast"].ToString();
                                specificDayData.DayData.Lanch = reader["Lanch"].ToString();
                                specificDayData.DayData.Dinner = reader["Dinner"].ToString();
                                specificDayData.DayData.Night = reader["Night"].ToString();
                                specificDayData.DayData.WaterInML = (int)reader["WaterInML"];
                                specificDayData.DayData.GeneralFeel = reader["GeneralFeel"].ToString();
                                specificDayData.DayData.Tierness = (int)reader["Tierness"];
                                //specificDayData.DayData.TodaysAchivments = reader["TodaysAchivments"].ToString();
                            }                            
                            reader.Close();                            
                        }
                        catch (Exception)
                        {
                            Debug.LogError("there was an error: ");
                        }                        
                    }
                }
                conn.Close();
            }
            return specificDayData;
        }
        public void PrintUsersDayDatas()
        {
            using (SqliteConnection conn = new SqliteConnection(usersData))
            {
                conn.Open();
                using (SqliteCommand command = conn.CreateCommand())
                {
                    command.CommandText = $"SELECT * FROM usersDayDatas;";
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            Debug.Log($"name: {reader["name"]}\n" +
                                $"date: {reader["day"]}/{reader["month"]}/{reader["year"]}\n" +
                                $"breakfast {reader["BreakFast"]}\n" +
                                $"Lanch {reader["Lanch"]}\n" +
                                $"Dinner {reader["Dinner"]}\n" +
                                $"Night {reader["Night"]}\n" +
                                $"WaterMl {reader["WaterInML"]}\n" +
                                $"GeneralFeel {reader["GeneralFeel"]}\n" +
                                $"Tierness {reader["Tierness"]}\n" +
                                $"TodaysAchivments {reader["TodaysAchivments"]}");
                        reader.Close();
                    }
                }
                conn.Close();
            }
        }
        //public bool UpdateDay(Messages.SpecificDayData specificDayData)
        //{
        //    using (SqliteConnection conn = new SqliteConnection(usersData))
        //    {
        //        conn.Open();
        //        using (SqliteCommand command = conn.CreateCommand())
        //        {
        //            command.CommandText = $"UPDATE"
        //        }
        //    }
        //}
            
        #endregion        
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
