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
            db.Initialize(Guid.NewGuid().ToString(), true);

            // Act
            var items1 = db.GetAllItems();
            Assert.Empty(items1);

            db.Add("example");

            // Assert
            var items = db.GetAllItems();
            Assert.NotEmpty(items);
        }

        [Fact]
        public void Should_add_item_to_db2()
        {
            // Arrange
            var db = new ToDoApp.ToDoSQLiteDB();
            db.Initialize(Guid.NewGuid().ToString(), true);

            // Act
            var items1 = db.GetAllItems();
            Assert.Empty(items1);

            db.Add("example1");
            db.Add("example2");
            db.Add("example3");

            // Assert
            var items = db.GetAllItems();
            Assert.Equal(3, items.Count());
        }

        [Fact]
        public void Should_change_status_by_id()
        {
            // Arrange
            var db = new ToDoApp.ToDoSQLiteDB();
            db.Initialize(Guid.NewGuid().ToString(), true);

            var items1 = db.GetAllItems();
            Assert.Empty(items1);

            db.Add("change_status_1");

            var firstItem = db.GetAllItems().First();
 
            Assert.False(firstItem.IsFinished);

            // Act
            db.ChangeStatus(firstItem.Id);

            firstItem = db.GetAllItems().First();

            Assert.True(firstItem.IsFinished);

        }
        [Fact]
        public void Should_not_change_status_by_not_existed_id()
        {
            // Arrange
            var db = new ToDoApp.ToDoSQLiteDB();
            db.Initialize(Guid.NewGuid().ToString(), true);

            var items1 = db.GetAllItems();
            Assert.Empty(items1);

            // Act
            var result = db.ChangeStatus(112);

            // Assert
            Assert.False(result);

        }
    }
}
