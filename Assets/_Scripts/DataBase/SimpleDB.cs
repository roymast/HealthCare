using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System;

namespace DBExample
{
    public class SimpleDB : MonoBehaviour
    {
        private string dbName = "URI=file:DateBase.db";

        // Start is called before the first frame update
        void Start()
        {
            CreateDB();
        }

        private void CreateDB()
        {
            using (var connection = new SqliteConnection(dbName))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "CREATE TABLE IF NOT EXISTS weapons (name VARCHAR(20), damage INT);";
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
    }
}
