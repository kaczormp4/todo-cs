using ToDoApp;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void In_memory_db_item_should_not_exist()
        {
            // Arrange
            var sut = new ToDoInMemoryDb();

            // Act
            var emptyList = sut.GetAllItems();

            // Assert
            Assert.Empty(emptyList);
        }

        [Fact]
        public void In_memory_db_item_should_exist()
        {
            // Arrange
            var sut = new ToDoInMemoryDb();
            var item = "Example item";

            // Act
            sut.Add(item);

            // Assert
            var itemsList = sut.GetAllItems();

            Assert.NotEmpty(itemsList);
        }
    }
}