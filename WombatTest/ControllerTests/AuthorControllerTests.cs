using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using WombatLibrarianApi.Services;
using Moq;
using WombatLibrarianApi.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace WombatLibrarianApi.Controllers.Tests
{
    [TestFixture]
    public class AuthorControllerTests
    {
        private Mock<IBookAPIService> _bookAPIService;

        private IList<BookItem> GetTestBookItemList()
        {
            IList<BookItem> books = new List<BookItem>()
            {
                new BookItem()
                {
                    id = "GSLCDwAAQBAJ"
                },
                new BookItem()
                {
                    id = "ztmuv17TKFoC"
                }
            };
            return books;
        }

        [Test]
        public async Task GetAuthorBookItems_EnterSearchTerm_ShouldReturnListOfBookItems()
        {
            _bookAPIService = new Mock<IBookAPIService>();
            var searchTerm = "Test";
            _bookAPIService.Setup(service => service.GetAuthorBooksAsync(searchTerm))
                .ReturnsAsync(GetTestBookItemList());
            var authorController = new AuthorController(_bookAPIService.Object);

            var result = await authorController.GetAuthorBookItems(searchTerm);

            var okResult = result.Result.Should().BeOfType<OkObjectResult>();
            var returnValue = result.Should().BeOfType<ActionResult<IEnumerable<BookItem>>>();
            var books = (IEnumerable<BookItem>)((ObjectResult)returnValue.Subject.Result).Value;
            books.FirstOrDefault().id.Should().Be("GSLCDwAAQBAJ");
        }
    }
}