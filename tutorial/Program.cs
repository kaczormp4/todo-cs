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

SQLiteCommand selectCommand = new SQLiteCommand("select * from highscores", m_dbConnection);
var reader = selectCommand.ExecuteReader();

while (reader.Read())
{
    var nameColumnId = reader.GetOrdinal("name");
    var scoreColumnId = reader.GetOrdinal("score");

    var name = reader.GetString(nameColumnId);
    var score = reader.GetInt32(scoreColumnId);

    string message = $"Name:{name} , Score:{score}";

    Console.WriteLine(message);
}
//let selectCommand = new SQLiteCommand("select * from users", connection)
//    let reader = selectCommand.ExecuteReader()

//    while reader.Read() do
//       let printmsg = $"""{reader.GetInt32(reader.GetOrdinal("Id"))} {reader.GetString(reader.GetOrdinal("Email"))} {reader.GetInt32(reader.GetOrdinal("age"))}"""
//       Console.WriteLine(printmsg)

m_dbConnection.Close();

