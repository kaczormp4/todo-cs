using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using static System.Formats.Asn1.AsnWriter;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

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
        public bool Initialize(string dbName, bool inMemory)
        {
            try
            {
                if (!inMemory && File.Exists(dbName))
                {
                    File.Delete(dbName);
                }

                if (!inMemory)
                {
                    SQLiteConnection.CreateFile(dbName);
                    _connectionString = $"DataSource={dbName};Version=3;";
                }

                if(inMemory)
                {
                    _connectionString = $"FullUri=file:{dbName}?mode=memory&cache=shared";
                }

                

                string sql = "create table todoitems (Id integer primary key autoincrement, Value text not null, IsFinished integer not null)";
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
            try
            {

                SQLiteConnection connection = new SQLiteConnection(_connectionString);
                connection.Open();

                string sql = "insert into todoitems (Id, Value, IsFinished) values (null, @textParam, 0)";

                var command = new SQLiteCommand(sql, connection);
                command.Parameters.AddWithValue("@textParam", text);

                command.ExecuteNonQuery();

                connection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ChangeStatus(int id)
        {
            try
            {

                SQLiteConnection connection = new SQLiteConnection(_connectionString);
                connection.Open();

                var prevItem = GetItemById(id);

                var newStatus = !prevItem.IsFinished;
                string sql = "update todoitems set IsFinished = @IsFinished where Id = @IdParam";

                var command = new SQLiteCommand(sql, connection);
                command.Parameters.AddWithValue("@IsFinished", newStatus);
                command.Parameters.AddWithValue("@IdParam", id);

                command.ExecuteNonQuery();

                connection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public ToDoItem GetItemById(int id)
        {
            using SQLiteConnection connection = new SQLiteConnection(_connectionString);

            try
            {
                connection.Open();

                string sql = "select * from todoitems where Id = @IdParam";

                var command = new SQLiteCommand(sql, connection);
                command.Parameters.AddWithValue("@IdParam", id);

                var reader = command.ExecuteReader();

                ToDoItem item = null;

                while (reader.Read())
                {
                    var idR = reader.GetOrdinal("Id");
                    var valueR = reader.GetOrdinal("Value");
                    var isFinishedR = reader.GetOrdinal("IsFinished");

                    var _id = reader.GetInt32(idR);
                    var _value = reader.GetString(valueR);
                    var _isFinished = reader.GetInt32(isFinishedR);
                    var _isFinishedBool = Convert.ToBoolean(_isFinished);

                    item = new ToDoItem(_id, _value, _isFinishedBool);

                }
                connection.Close();
                return item;
            }
            catch
            {
                connection.Close();
                return null;
            }
        }
        public IEnumerable<ToDoItem> GetAllItems()
        {
            using SQLiteConnection connection = new SQLiteConnection(_connectionString);

            connection.Open();

            string sql = "select * from todoitems";

            var command = new SQLiteCommand(sql, connection);
            var reader = command.ExecuteReader();

            List<ToDoItem> items = [];

            while (reader.Read())
            {
                var idR = reader.GetOrdinal("Id");
                var valueR = reader.GetOrdinal("Value");
                var isFinishedR = reader.GetOrdinal("IsFinished");

                var _id = reader.GetInt32(idR);
                var _value = reader.GetString(valueR);
                var _isFinished = reader.GetInt32(isFinishedR);
                var _isFinishedBool = Convert.ToBoolean(_isFinished);

                var item = new ToDoItem(_id, _value, _isFinishedBool);
                items.Add(item);

            }

            return items;
        }
    }

}
