using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;
using LibraryProject.Models.Users;
using LibraryProject.Models.Library;
using LibraryProject.utils;
using LibraryProject.Menus;

namespace LibraryProject.Tests
{
    [TestFixture]
    public class LibraryTests
    {
        

        [SetUp]
        public void SetUp()
        {
            // Czyœcimy dane przed ka¿dym testem
            Library.shelfs.Clear();
        }

        [Test]
        public void Book_ChangeInfo_ShouldUpdateValues()
        {
            var user = new User("testuser", Role.Admin);
            var book = new Books.BookFantasy(1, 0, 200, "Original Title", user);

            book.ChangeInfo("New Title", 300);

            Assert.That(book.objectsName, Is.EqualTo("New Title"));
            Assert.That(book.count, Is.EqualTo(300));
        }

        [Test]
        public void FilterByTitle_ShouldReturnMatchingBooks()
        {
            var shelf = new Shelf();
            shelf.AddBook(new Books.BookFantasy(1, 0, 100, "Harry Potter", new User("test", Role.Admin)));
            shelf.AddBook(new Books.BookThriller(2, 0, 120, "Dracula", new User("test", Role.Admin)));

            Library.shelfs.Add(shelf);

            var result = Filter.FilterByTitle("Harry");

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().objectsName, Is.EqualTo("Harry Potter"));
        }

        [Test]
        public void AddBook_ShouldIncreaseBookCount()
        {
            var shelf = new Shelf();
            var user = new User("admin", Role.Admin);
            var book = new Books.BookThriller(0, 0, 250, "Test Book", user);

            shelf.AddBook(book);

            Assert.That(shelf.Books.Count, Is.EqualTo(1));
        }
        [Test]
        public void FilterByTitle_ShouldReturnEmptyList_WhenNoMatchingBooks()
        {
            var shelf = new Shelf();
            shelf.AddBook(new Books.BookFantasy(1, 0, 100, "Harry Potter", new User("test", Role.Admin)));
            shelf.AddBook(new Books.BookThriller(2, 0, 120, "Dracula", new User("test", Role.Admin)));

            Library.shelfs.Add(shelf);

            var result = Filter.FilterByTitle("Nonexistent Title");

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void FilterByType_ShouldReturnEmptyList_WhenNoMatchingType()
        {
            var shelf = new Shelf();
            shelf.AddBook(new Books.BookFantasy(1, 0, 100, "Harry Potter", new User("test", Role.Admin)));
            shelf.AddBook(new Books.BookThriller(2, 0, 120, "Dracula", new User("test", Role.Admin)));

            Library.shelfs.Add(shelf);

            var result = Filter.FilterByType("Nonexistent Type");

            Assert.That(result.Count, Is.EqualTo(0));
        }

    }
}
