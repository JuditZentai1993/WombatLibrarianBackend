﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using WombatLibrarianApi.Services;
using Moq;
using WombatLibrarianApi.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WombatLibrarianApi.Mappings;
using AutoMapper;

namespace WombatLibrarianApi.Controllers.Tests
{
    [TestFixture]
    public class AuthorControllerTests
    {
        private Mock<IBookAPIService> _bookAPIService;
        private IMapper _mapper;

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

        [SetUp]
        public void Setup()
        {
            var servicecollection = new ServiceCollection();

            servicecollection.AddAutoMapper(opt =>
            {
                opt.AddProfile(typeof(BookProfile));
            });
            var serviceProvider = servicecollection.BuildServiceProvider();
            _mapper = serviceProvider.GetService<IMapper>();
        }

        [Test]
        public async Task GetAuthorBookItems_EnterSearchTerm_ShouldReturnListOfBookItems()
        {
            _bookAPIService = new Mock<IBookAPIService>();
            var searchTerm = "Test";
            _bookAPIService.Setup(service => service.GetAuthorBooksAsync(searchTerm))
                .ReturnsAsync(GetTestBookItemList());
            var authorController = new AuthorController(_bookAPIService.Object, _mapper);

            var result = await authorController.GetAuthorBookItems(searchTerm);

            var okResult = result.Result.Should().BeOfType<OkObjectResult>();
            var returnValue = result.Should().BeOfType<ActionResult<IEnumerable<Book>>>();
            var books = (IEnumerable<Book>)((ObjectResult)returnValue.Subject.Result).Value;
            books.FirstOrDefault().Id.Should().Be("GSLCDwAAQBAJ");
        }
    }
}