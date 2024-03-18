namespace ToDoApp
{
    public class ToDoItem
    {
        public string Value { get; set; }
        public int Id { get; }

        public bool IsFinished { get; set; }
        public ToDoItem(int id,string text)
        {
            Id = id;
            Value = text;
        }
    }
    public class ToDoInMemoryDb
    {
        private List<ToDoItem> _items = new List<ToDoItem>();
        public bool Add(string text)
        {
            var newId = _items.Count;
            var newToDoItem = new ToDoItem(newId,text);
            _items.Add(newToDoItem);
            return true;
        }

        public IEnumerable<ToDoItem> GetAllItems()
        {
            return _items.AsReadOnly();
        }

        public bool ChangeStatus(int id)
        {
            var existingItem = _items.FirstOrDefault(x =>  x.Id == id);
            if(existingItem == null) { 
                return false;
            }
            else
            {
                existingItem.IsFinished = !existingItem.IsFinished;
                return true;
            }
        }
        public ToDoInMemoryDb()
        {
            
        }
    }
}
