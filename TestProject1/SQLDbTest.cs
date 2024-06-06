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

        [Fact]
        public void Should_get_item_by_id_and_return_correct_item()
        {
            // Arrange
            var db = new ToDoApp.ToDoSQLiteDB();
            db.Initialize(Guid.NewGuid().ToString(), true);

            var value = "example1";
            db.Add(value);
            var items = db.GetAllItems();

            var id = items.First(x => x.Value == value).Id;

            // Act
            var item = db.GetItemById(id);

            // Assert
            Assert.Equal(value, item.Value);
        }

        [Fact]
        public void Should_not_get_item_by_id_and_when_item_doesnt_exist()
        {
            // Arrange
            var db = new ToDoApp.ToDoSQLiteDB();
            db.Initialize(Guid.NewGuid().ToString(), true);

            var value = "example1";
            db.Add(value);
            var items = db.GetAllItems();

            var notExistedId = items.Select(x => x.Id).Max() + 100;

            // Act
            var item = db.GetItemById(notExistedId);

            // Assert
            Assert.Null(item);
        }

        [Fact]
        public void Should_get_all_items()
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

    }
}
