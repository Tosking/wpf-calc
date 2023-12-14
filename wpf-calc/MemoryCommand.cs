using System;
using System.IO;
using Microsoft.Data.Sqlite;

namespace wpf_calc
{
    public interface IMemory{
        public void Save(string str);
        public void Clear();
        string Read();
    }
    public class MemoryRAM : IMemory{
        string _memory;
        public void Save(string str){
            _memory = str;
        }
        public void Clear(){
            _memory = "";
        }

        public string Read(){
            return _memory;
        }
    }

    public class MemoryFile : IMemory{
        public void Save(string str){
            using (StreamWriter sw = File.CreateText("./save.txt")){
                sw.WriteLine(str);
            }
        }
        public void Clear(){
            using (StreamWriter sw = File.CreateText("./save.txt")){
                sw.WriteLine("");
            }
        }

        public string Read(){
            if(!File.Exists(@"./save.txt"))
                    return "";
            using (StreamReader sw = File.OpenText("./save.txt")){
                return sw.ReadLine();
            }
        }
    }

    public class MemoryDB : IMemory{
        SqliteConnection connection;
        public MemoryDB(){
            connection = new SqliteConnection("Data Source=save.db");
            connection.Open();
        }
        public void Save(string str){
            string Createsql = "DELETE FROM save";
            string Createsql1 = "INSERT INTO save (number) VALUES (\"" + str + "\")";
            var sqlite = connection.CreateCommand();
            sqlite.CommandText = Createsql;
            sqlite.ExecuteNonQuery();
            sqlite.CommandText = Createsql1;
            sqlite.ExecuteNonQuery();
        }
        public void Clear(){
            string Createsql = "DELETE FROM save";
            var sqlite = connection.CreateCommand();
            sqlite.CommandText = Createsql;
            sqlite.ExecuteNonQuery();
        }

        public string Read(){
            string result = "";
            string Createsql = "SELECT * FROM save";
            var sqlite = connection.CreateCommand();
            sqlite.CommandText = Createsql;
            var sqlReader = sqlite.ExecuteReader();
            while (sqlReader.Read())
                result = sqlReader.GetString(0);
            return result;
        }
    }
}