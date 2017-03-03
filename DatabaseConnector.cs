using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;


public class DatabaseConnector : MonoBehaviour
{
    private IDbConnection dbconn;

    void Start () {
        dbconn = (IDbConnection)new SqliteConnection("URI=file:" + Application.dataPath + "/Data.db");
        dbconn.Open(); //Open connection to the database.
    }
	
    public List<string> DataGetText(string x)
    {
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
        return lista;
    }

    public void UpdateDatabase(string x)
    {
        List<string> lista = new List<string>();
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = x;
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
    }

    public int getID(string x)
    {
        List<string> lista = new List<string>();
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
        return luku;
    }

    public List<int> FindList(string x)
    {
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
        return lista;
    }

    public List<int> GetPoints(string x)
    {
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
        return lista;
    }
}
