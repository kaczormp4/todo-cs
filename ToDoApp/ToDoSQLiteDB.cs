using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace ToDoApp
{
    public interface IToDoRepository
    {
        bool Add(string text);
        IEnumerable<ToDoItem> GetAllItems();
        bool ChangeStatus(int id);
    }

    public class ToDoSQLiteDB : IToDoRepository
    {
        private string _connectionString;
        public bool Initialize(string dbName)
        {
            try
            {
                if (File.Exists(dbName))
                {
                    File.Delete(dbName);
                }

                SQLiteConnection.CreateFile(dbName);

                _connectionString = $"Data Source={dbName};Version=3;";

                string sql = "Create Table highscores (name varchar(20), score int)";
                // TODO make sure to close connection when error occours
                SQLiteConnection connection = new SQLiteConnection(_connectionString);
                connection.Open();

                SQLiteCommand command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch
            {
                return false;
            }
   
        }
        public bool Add(string text)
        {

            SQLiteConnection connection = new SQLiteConnection(_connectionString);
            connection.Open();

            string sql = "Create Table highscores (name varchar(20), score int)";
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();


            connection.Close();
            return true;
        }

        public bool ChangeStatus(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ToDoItem> GetAllItems()
        {
            throw new NotImplementedException();
        }
    }

}
