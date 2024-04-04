// this creates a zero-byte file
using System.Data.SQLite;

const string DB_NAME = "MyDatabase.sqlite";

Console.WriteLine("test");
if (File.Exists(DB_NAME))
{
    File.Delete(DB_NAME);
}
SQLiteConnection.CreateFile(DB_NAME);

string connectionString = $"Data Source={DB_NAME};Version=3;";
SQLiteConnection m_dbConnection = new SQLiteConnection(connectionString);
m_dbConnection.Open();

//varchar will likely be handled internally as TEXT
// the (20) will be ignored
// see https://www.sqlite.org/datatype3.html#affinity_name_examples
string sql = "Create Table highscores (name varchar(20), score int)";
//// you could also write sql = "CREATE TABLE IF NOT EXISTS highscores ..."
SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
command.ExecuteNonQuery();

sql = "Insert into highscores (name, score) values ('Me', 9001)";
command = new SQLiteCommand(sql, m_dbConnection);
command.ExecuteNonQuery();

SQLiteCommand selectCommand = new SQLiteCommand("select * from highscores where name = 'Me' limit 1", m_dbConnection);
var reader = selectCommand.ExecuteReader();

HighScore newRecord = null;

while (reader.Read())
{
    var nameColumnId = reader.GetOrdinal("name");
    var scoreColumnId = reader.GetOrdinal("score");

    var name = reader.GetString(nameColumnId);
    var score = reader.GetInt32(scoreColumnId);

    string message = $"Name:{name} , Score:{score}";

    Console.WriteLine(message);

    newRecord = new HighScore(name, score).Add(10);

}

var newQuery = $"update highscores set score = ${newRecord.score} where name = ${newRecord.name}";

SQLiteCommand updateCommand = new SQLiteCommand(newQuery, m_dbConnection);
updateCommand.ExecuteNonQuery();

SQLiteCommand selectCommand2 = new SQLiteCommand("select * from highscores where name = 'Me' limit 1", m_dbConnection);
var reader2 = selectCommand2.ExecuteReader();

while (reader2.Read())
{
    var nameColumnId = reader2.GetOrdinal("name");
    var scoreColumnId = reader2.GetOrdinal("score");

    var name = reader2.GetString(nameColumnId);
    var score = reader2.GetInt32(scoreColumnId);

    string message = $"Name:{name} , Score:{score}";

    Console.WriteLine(message);

}
m_dbConnection.Close();

while (reader.Read())
{
    var nameColumnId = reader.GetOrdinal("name");
    var scoreColumnId = reader.GetOrdinal("score");

    var name = reader.GetString(nameColumnId);
    var score = reader.GetInt32(scoreColumnId);

    string message = $"Name:{name} , Score:{score}";

    Console.WriteLine(message);

    newRecord = new HighScore(name, score).Add(10);

}
class HighScore
{
    public HighScore(string _name, int _score)
    {
        name = _name;
        score = _score;
    }

    public readonly string name;
    public readonly int score;

    public HighScore Add(int points)
    {
        var newScore = this.score + points;

        return new HighScore(this.name, newScore);
    }
}

