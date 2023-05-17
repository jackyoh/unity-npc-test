using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using TMPro;

public class TestDataBase : MonoBehaviour {
    public TextMeshProUGUI textComponent;

    void Start() {
        string connection = "URI=file:" + Application.persistentDataPath + "/My_Database";
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();
        IDbCommand dbcmd;
        IDataReader reader;
        dbcmd = dbcon.CreateCommand();
        string q_createTable = 
            "CREATE TABLE IF NOT EXISTS my_table(val INTEGER)";
        dbcmd.CommandText = q_createTable;
        reader = dbcmd.ExecuteReader();
        
        IDbCommand cmd = dbcon.CreateCommand();
        cmd.CommandText = "INSERT INTO my_table(val) VALUES(100)";
        cmd.ExecuteNonQuery();

        IDbCommand cmd_read = dbcon.CreateCommand();
        //IDataReader reader;
        string query = "SELECT * FROM my_table";
        cmd_read.CommandText = query;
        IDataReader reader2 = cmd_read.ExecuteReader();

        while (reader2.Read()) {
            // Debug.Log("val:" + reader2[1].ToString());
            textComponent.text = reader2[0].ToString();
        }
        dbcon.Close();
    }
}
