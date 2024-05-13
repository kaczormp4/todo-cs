namespace TodoApi
{
    public class MockStartUp
    {
        public static ToDoApp.IToDoRepository Initialize()
        {
            var db = new ToDoApp.ToDoSQLiteDB();
            db.Initialize("toDoTestDb",false);

            db.Add("test 1");
            return db;
        }
    }
}
