using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp;

namespace TestProject1
{
    public class SQLDbTest
    {
        [Fact]
        public void Should_add_item_to_db()
        {
            // Arrange
            var db = new ToDoApp.ToDoSQLiteDB();
            db.Initialize(":memory:", true);

            // Act
            var items1 = db.GetAllItems();
            Assert.Empty(items1);

            db.Add("example");

            // Assert
            var items = db.GetAllItems();
            Assert.NotEmpty(items);
        }
    }
}
