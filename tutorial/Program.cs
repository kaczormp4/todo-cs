// this creates a zero-byte file
using System.Data.SQLite;
using tutorial;

const string DB_NAME = "MyDatabase.sqlite";



string connectionString = HighscoresDBOperations.CreateDatabase(DB_NAME);

SQLiteConnection m_dbConnection = new SQLiteConnection(connectionString);
m_dbConnection.Open();

HighscoresDBOperations.CreateTable(m_dbConnection);

HighscoresDBOperations.InsertHighscores(m_dbConnection, "me", 9221);

var highScores = HighscoresDBOperations.SelectHihgscoresByName(m_dbConnection, "me");

highScores.ForEach(highScore => HighscoresDBOperations.UpdateScoreByName(m_dbConnection, highScore.name, 22222));


HighscoresDBOperations.SelectHihgscoresByName(m_dbConnection, "me");

m_dbConnection.Close();

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

