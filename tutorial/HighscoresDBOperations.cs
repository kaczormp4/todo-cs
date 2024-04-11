using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Xml.Serialization;

namespace tutorial
{
    static class HighscoresDBOperations
    {
        static public string CreateDatabase(string dbName)
        {

            Console.WriteLine("test");
            if (File.Exists(dbName))
            {
                File.Delete(dbName);
            }

            SQLiteConnection.CreateFile(dbName);

            string connectionString = $"Data Source={dbName};Version=3;";

            return connectionString;
        }

        static public void CreateTable(SQLiteConnection connection )
        {
            string sql = "Create Table highscores (name varchar(20), score int)";

            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        static public void InsertHighscores(SQLiteConnection connection, string name, int score) 
        {
            string query = $"Insert into highscores (name, score) values ('{name}', {score})";
            var command = new SQLiteCommand(query, connection);
            command.ExecuteNonQuery();
        }

        static public List<HighScore> SelectHihgscoresByName(SQLiteConnection connection, string name)
        {
            SQLiteCommand selectCommand = new SQLiteCommand($"select * from highscores where name = '{name}'", connection);
            var reader = selectCommand.ExecuteReader();

            var HighScores = new List<HighScore>();

            while (reader.Read())
            {
                var nameColumnId = reader.GetOrdinal("name");
                var scoreColumnId = reader.GetOrdinal("score");

                var _name = reader.GetString(nameColumnId);
                var score = reader.GetInt32(scoreColumnId);

                string message = $"Name:{_name} , Score:{score}";

                Console.WriteLine(message);
                HighScores.Add(new HighScore(_name, score));

            }

            return HighScores;
        }

        static public void UpdateScoreByName(SQLiteConnection connection, string nameCriteria, int newScore)
        {
            var newQuery = $"update highscores set score = {newScore} where name = '{nameCriteria}'";

            SQLiteCommand updateCommand = new SQLiteCommand(newQuery, connection);
            updateCommand.ExecuteNonQuery();
        }
    }
}
