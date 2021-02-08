using WombatLibrarianApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WombatLibrarianApi.Services;
using Moq;
using WombatTest;
using WombatLibrarianApi.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace WombatLibrarianApi.Controllers.Tests
{
    [TestFixture]
    public class SearchControllerTests
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
        public async Task GetBookItems_EnterSearchTerm_ShouldReturnListOfBookItems()
        {
            _bookAPIService = new Mock<IBookAPIService>();
            var searchTerm = "Test";
            _bookAPIService.Setup(service => service.GetSearchResultsAsync(searchTerm))
                .ReturnsAsync(GetTestBookItemList());
            var searchController = new SearchController(_bookAPIService.Object);

            var result = await searchController.GetBookItems(searchTerm);

            var okResult = result.Result.Should().BeOfType<OkObjectResult>();
            var returnValue = result.Should().BeOfType<ActionResult<IEnumerable<BookItem>>>();
            var books = (IEnumerable<BookItem>)((ObjectResult)returnValue.Subject.Result).Value;
            books.FirstOrDefault().id.Should().Be("GSLCDwAAQBAJ");
        }
    }
}