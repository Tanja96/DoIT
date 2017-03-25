using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
using System.IO;
using UnityEngine.UI;


public class DatabaseConnector : MonoBehaviour
{
    private IDbConnection dbconn;
    string conn = "";

    void Awake()
    {
        //TÄTÄ EI OIKEAAN
        //KÄYTÄ JOS MUUTAT DATABASEA
        DirectoryInfo dataDir = new DirectoryInfo(Application.persistentDataPath);
        dataDir.Delete(true);
        //TÄMÄ OIKEAAN BUILDIIN
        string filepath = Application.persistentDataPath + "/" + "Data.db";
        if (!File.Exists(filepath)) {
            WWW loadDB = new WWW(streamingAssetsPath + "Data.db");
            while (!loadDB.isDone) { }
            File.WriteAllBytes(filepath, loadDB.bytes);
        }
        conn = "URI=file:" + filepath;
    }

    public static string streamingAssetsPath
    {
        get
        {
            if (Application.platform == RuntimePlatform.Android)
                return "jar:file://" + Application.dataPath + "!/assets/";
            else
                return "file://" + Application.streamingAssetsPath + "/";
        }
    }

    void Start () {

    }

    void Update()
    {

    }
	
    public List<string> DataGetText(string x)
    {
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        List<string> lista = new List<string>();
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = x;
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            lista.Add(reader.GetString(0));
            lista.Add(reader.GetString(1));
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
        return lista;
    }

    public void UpdateDatabase(string x)
    {
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = x;
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbconn.Close();
        dbconn = null;
        dbcmd = null;
    }

    public int getID(string x)
    {
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = x;
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        int luku = 0;
        while (reader.Read()) {
            luku = reader.GetInt32(0);
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
        return luku;
    }

    public List<int> FindList(string x)
    {
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        List<int> lista = new List<int>();
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = x;
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            lista.Add(reader.GetInt32(0));
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
        return lista;
    }

    public List<int> GetPoints(string x)
    {
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        List<int> lista = new List<int>();
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = x;
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            lista.Add(reader.GetInt32(0));
            lista.Add(reader.GetInt32(1));
            lista.Add(reader.GetInt32(2));
            lista.Add(reader.GetInt32(3));
            lista.Add(reader.GetInt32(4));
        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
        return lista;
    }
}
