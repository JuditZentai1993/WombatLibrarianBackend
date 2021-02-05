using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WombatLibrarianApi.Services;
using WombatLibrarianApi.Settings;

namespace WombatTest
{
    //[TestFixture]
    public class GoogleBooksAPIServiceTest
    {
        private Mock<IOptions<GoogleApiSettings>> _settings;

        private const string JsonData = @"{
  'kind': 'books#volumes',
  'totalItems': 517,
  'items': [
    {
      'kind': 'books#volume',
      'id': 'F_f1YIjDyfsC',
      'etag': 'RepYvkK2kyM',
      'selfLink': 'https://www.googleapis.com/books/v1/volumes/F_f1YIjDyfsC',
      'volumeInfo': {
        'title': 'Isaac Asimov: Webster's Timeline History 1895 - 2007',
        'publisher': 'ICON Group International',
        'industryIdentifiers': [
          {
            'type': 'ISBN_10',
            'identifier': '111440361X'
          },
          {
            'type': 'ISBN_13',
            'identifier': '9781114403611'
          }
        ],
        'readingModes': {
          'text': false,
          'image': false
        },
        'printType': 'BOOK',
        'maturityRating': 'NOT_MATURE',
        'allowAnonLogging': false,
        'contentVersion': '0.0.1.0.preview.0',
        'imageLinks': {
          'smallThumbnail': 'http://books.google.com/books/content?id=F_f1YIjDyfsC&printsec=frontcover&img=1&zoom=5&source=gbs_api',
          'thumbnail': 'http://books.google.com/books/content?id=F_f1YIjDyfsC&printsec=frontcover&img=1&zoom=1&source=gbs_api'
        },
        'language': 'en',
        'previewLink': 'http://books.google.hu/books?id=F_f1YIjDyfsC&dq=asimov&hl=&cd=1&source=gbs_api',
        'infoLink': 'http://books.google.hu/books?id=F_f1YIjDyfsC&dq=asimov&hl=&source=gbs_api',
        'canonicalVolumeLink': 'https://books.google.com/books/about/Isaac_Asimov_Webster_s_Timeline_History.html?hl=&id=F_f1YIjDyfsC'
      },
      'saleInfo': {
        'country': 'HU',
        'saleability': 'NOT_FOR_SALE',
        'isEbook': false
      },
      'accessInfo': {
        'country': 'HU',
        'viewability': 'NO_PAGES',
        'embeddable': false,
        'publicDomain': false,
        'textToSpeechPermission': 'ALLOWED',
        'epub': {
          'isAvailable': false
        },
        'pdf': {
          'isAvailable': false
        },
        'webReaderLink': 'http://play.google.com/books/reader?id=F_f1YIjDyfsC&hl=&printsec=frontcover&source=gbs_api',
        'accessViewStatus': 'NONE',
        'quoteSharingAllowed': false
      }
    }
  ]
}
";

        [SetUp]
        public void Setup()
        {
            _settings = new Mock<IOptions<GoogleApiSettings>>();
        }

        [Test]
        public async Task GetSearchResultsAsync()
        {
            var httpClient = GetHttpClientMock(JsonData);
            var service = new GoogleBooksAPIService(_settings.Object, httpClient.Object);

            await service.GetSearchResultsAsync("http://test.com");
            service.SearchResults.Count().Should().BePositive();
        }

        private Mock<HttpClient> GetHttpClientMock(string jsonData)
        {
            var _httpClient = new Mock<HttpClient>();
            _httpClient.Setup(e => e.GetAsync(It.IsAny<Uri>())).Returns(() =>
            {
                var response = new HttpResponseMessage();
                response.Content = new StringContent(jsonData);
                return Task.FromResult(response);
            });
            return _httpClient;
        }
    }

    public class MockHttpClient : HttpClient
    {
        public override Get
    }
}
