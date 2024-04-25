using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp
{
    public class ToDoItem
    {
        public ToDoItem(int id, string value, bool isFinished) {
            Id = id;
            Value = value;
            IsFinished = isFinished;
        }
        public string Value { get; set; }
        public int Id { get; }

        public bool IsFinished { get; set; }
        public ToDoItem(int id, string text)
        {
            Id = id;
            Value = text;
        }
    }


}
