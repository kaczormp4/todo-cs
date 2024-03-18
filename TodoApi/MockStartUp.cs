namespace TodoApi
{
    public class MockStartUp
    {
        public static ToDoApp.ToDoInMemoryDb Initialize()
        {
            var db = new ToDoApp.ToDoInMemoryDb();
            db.Add("initial item");

            return db;
        }
    }
}
