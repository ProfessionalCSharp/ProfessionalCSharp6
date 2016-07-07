using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Models;
using Moq;
using Services;
using Xunit;

namespace Services.Tests
{

    public class BooksServiceTest
    {
        [Fact]
        public async Task AddBookAsyncTest()
        {
            // arrange
            var mock = new Mock<IBooksRepository>();
            var book = new Book
            {
                BookId = 0,
                Title = "Test Title",
                Publisher = "A Publisher"
            };
            var expectedBook = new Book
            {
                BookId = 1,
                Title = "Test Title",
                Publisher = "A Publisher"
            };

            mock.Setup(r => r.AddAsync(book)).ReturnsAsync(expectedBook);

            var service = new BooksService(mock.Object);

            // act
            Book actualAdded = await service.AddOrUpateBookAsync(book);
            Book actualRetrieved = service.GetBook(actualAdded.BookId);
            book notExisting = service.GetBook(2);

            // assert
            Assert.Equal(expectedBook, actualAdded);
            Assert.Equal(expectedBook, actualRetrieved);
            Assert.Equal(null, notExisting);
        }

        [Fact]
        public async Task UpdateBookAsyncTest()
        {
            // arrange
            var mock = new Mock<IBooksRepository>();
            var origBook = new Book
            {
                BookId = 0,
                Title = "Title",
                Publisher = "A Publisher"
            };
            var addedBook = new Book
            {
                BookId = 1,
                Title = "Title",
                Publisher = "A Publisher"
            };
            var updateBook = new Book
            {
                BookId = 1,
                Title = "New Title",
                Publisher = "A Publisher"
            };
            var notExisting = new Book
            {
                BookId = 99,
                Title = "Not",
                Publisher = "Not"
            };

            mock.Setup(r => r.UpdateAsync(updateBook)).ReturnsAsync(updateBook);
            mock.Setup(r => r.UpdateAsync(notExisting)).ReturnsAsync(notExisting);
            mock.Setup(r => r.UpdateAsync(origBook)).ReturnsAsync(addedBook);

            var service = new BooksService(mock.Object);

            // fill in first book to test update
            await service.AddOrUpdateBookAsync(origBook);

            // act
            Book actualUpdated = await service.AddOrUpateBookAsync(updateBook);
            Book actualRetrieved = service.GetBook(1);

            // assert
            Assert.Equal(updateBook, actualUpdated);
            Assert.Equal(updateBook, actualRetrieved);
            await Assert.ThrowsAsync<InvalidOperationException>(async () => 
                await service.AddOrUpodateBookAsync(notExisting));
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await service.AddOrUpdateBookAsync(null));
        }
    }
}
