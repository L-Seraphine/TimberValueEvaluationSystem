﻿using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimberValueEvaluationSystem.Services
{
    //数据库操作类
    public class DatabaseHelper
    {
        private static string connectionString = "Data Source=";

        //新建数据库
        //传入数据库的名称
        //手动指定文件路径，然后创建数据库
        public static bool CreateDatabase(string dbName)
        {
            string filepath = FileHelper.GetFolderPath();
            if (filepath == null)
            {
                Growl.Warning("已取消创建");
                return false;
            }
            else
            {
                //数据库文件路径
                string dbPath = System.IO.Path.Combine(filepath, $"{dbName}.db");
                //创建数据库
                SQLiteConnection.CreateFile(dbPath);
                Growl.Info("创建数据库成功");
                return true;
            }
        }

        //新建数据库
        //传入路径和数据库的名称
        //手动指定文件路径，然后创建数据库
        public static bool CreateDatabase(string filepath,string dbName)
        {
            if (filepath == null)
            {
                Growl.Warning("已取消创建");
                return false;
            }
            else
            {
                //数据库文件路径
                string dbPath = System.IO.Path.Combine(filepath, $"{dbName}.db");
                //创建数据库
                SQLiteConnection.CreateFile(dbPath);
                Growl.Info("创建数据库成功");
                return true;
            }
        }


        ////创建数据库表
        //public static void CreateTable()
        //{
        //    //创建数据库表
        //    string sql = "CREATE TABLE IF NOT EXISTS 'timber' ('id' INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, 'name' TEXT NOT NULL, 'type' TEXT NOT NULL, 'unit' TEXT NOT NULL, 'price' REAL NOT NULL, 'volume' REAL NOT NULL, 'total' REAL NOT NULL, 'date' TEXT NOT NULL, 'remark' TEXT NOT NULL)";
        //    UpdateDatabase(sql);
        //}

        //创建数据库——立地质量表
        public static void CreateMSQMTable()
        {
            SQLiteConnection connection = ConnectDatabase();
            string sql = "CREATE TABLE YourTableName (" +
                "id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "SoilThickness TEXT," +
                "Slope TEXT," +
                "Aspect TEXT," +
                "Gradient TEXT," +
                "SiteQuality TEXT);";
            UpdateDatabase(connection,sql);
        }

        ////从CSV导入数据到数据库表中
        //public static void ImportCSV(string filepath)
        //{
        //    //读取CSV文件
        //    string[] lines = System.IO.File.ReadAllLines(filepath);
        //    //获取表头
        //    string[] headers = lines[0].Split(',');
        //    //获取表内容
        //    string[,] contents = new string[lines.Length - 1, headers.Length];
        //    for (int i = 1; i < lines.Length; i++)
        //    {
        //        string[] line = lines[i].Split(',');
        //        for (int j = 0; j < headers.Length; j++)
        //        {
        //            contents[i - 1, j] = line[j];
        //        }
        //    }
        //    //插入数据库表
        //    for (int i = 0; i < contents.GetLength(0); i++)
        //    {
        //        string sql = "INSERT INTO timber (name, type, unit, price, volume, total, date, remark) VALUES ('" + contents[i, 0] + "', '" + contents[i, 1] + "', '" + contents[i, 2] + "', '" + contents[i, 3] + "', '" + contents[i, 4] + "', '" + contents[i, 5] + "', '" + contents[i, 6] + "', '" + contents[i, 7] + "')";
        //        InsertDatabase(sql);
        //    }
        //    Growl.Info("导入数据成功");
        //}

        //连接数据库
        public static SQLiteConnection ConnectDatabase()
        {
            connectionString = "Data Source=";
            connectionString += FileHelper.GetFilePath();
            if (connectionString == "Data Source=")
            {
                Growl.Warning("已取消创建表");
                return null;
            }
            else
            {
                //创建数据库连接
                SQLiteConnection conn = new SQLiteConnection(connectionString);
                return conn;
            }
        }

        //链接数据库重载
        public static SQLiteConnection ConnectDatabase(string path)
        {
            connectionString = "Data Source=";
            connectionString += path;
            if (connectionString == "Data Source=")
            {
                Growl.Warning("已取消创建表");
                return null;
            }
            else
            {
                //创建数据库连接
                SQLiteConnection conn = new SQLiteConnection(connectionString);
                return conn;
            }
        }

        //根据sql语句查询数据库内容
        public static SQLiteDataReader QueryDatabase(string sql)
        {
            SQLiteConnection conn = ConnectDatabase();
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            SQLiteDataReader reader = cmd.ExecuteReader();
            return reader;
        }

        //根据sql语句更新数据库内容
        public static void UpdateDatabase(SQLiteConnection connection,string sql)
        {
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand(sql,connection);
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        //根据sql语句插入数据库内容
        public static void InsertDatabase(string sql)
        {
            SQLiteConnection conn = ConnectDatabase();
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        } 

        ////删除数据库内容
        //public static void DeleteDatabase(string sql)
        //{
        //    SQLiteConnection conn = ConnectDatabase();
        //    conn.Open();
        //    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
        //    cmd.ExecuteNonQuery();
        //    conn.Close();
        //}

        //查找数据库中的所有表
        public static List<string> FindAllTables(string path)
        {
            SQLiteConnection conn = ConnectDatabase(path);
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' ORDER BY name;", conn);
            SQLiteDataReader reader = cmd.ExecuteReader();
            List<string> tables = new List<string>();
            while (reader.Read())
            {
                tables.Add(reader.GetString(0));
            }
            conn.Close();
            return tables;
        }

        //查找数据库中所有表的数量
        public static int FindAllTablesCount(string path)
        {
            SQLiteConnection conn = ConnectDatabase(path);
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' ORDER BY name;", conn);
            SQLiteDataReader reader = cmd.ExecuteReader();
            int count = 0;
            while (reader.Read())
            {
                count++;
            }
            conn.Close();
            return count;
        }

        //从指定表中获取数据返回datatable
        public static DataTable GetDataTable(string path,string tableName,int countLocation)
        {
            SQLiteConnection conn = ConnectDatabase(path);
            conn.Open();
            string query = $"SELECT * FROM {tableName} LIMIT 10 OFFSET {countLocation}; ";
            //SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM " + tableName, conn);
            //SQLiteDataReader reader = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conn);
            adapter.Fill(dt);

            //for (int i = 0; i < reader.FieldCount; i++)
            //{
            //    dt.Columns.Add(reader.GetName(i));
            //}
            //while (reader.Read())
            //{
            //    System.Data.DataRow dr = dt.NewRow();
            //    for (int i = 0; i < reader.FieldCount; i++)
            //    {
            //        dr[i] = reader[i];
            //    }
            //    dt.Rows.Add(dr);
            //}

            conn.Close();
            return dt;
        }

        //获取指定表的总行数
        public static int GetTableCount(string path,string tableName)
        {
            SQLiteConnection conn = ConnectDatabase(path);
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand($"SELECT COUNT(*) FROM {tableName}", conn);
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
            return count;
        }
    }
}
