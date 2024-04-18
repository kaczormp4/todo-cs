namespace TodoApi
{
    public class MockStartUp
    {
        public static ToDoApp.IToDoRepository Initialize()
        {
            var db = new ToDoApp.ToDoInMemoryDb();
            db.Add("initial item");

            return db;
        }
    }
}
