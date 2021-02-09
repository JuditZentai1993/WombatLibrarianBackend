using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WombatLibrarianApi.Services;
using WombatLibrarianApi.Settings;

namespace WombatTest
{
    [TestFixture]
    public class GoogleBooksAPIServiceTests
    {
        private IOptions<GoogleApiSettings> _settings;

        private Mock<IHttpClientFactory> _clientFactory;

        public static string JsonData = @"{
  'kind': 'books#volumes',
  'totalItems': 517,
  'items': [
    {
    'kind': 'books#volume',
    'id': 'F_f1YIjDyfsC',
    'etag': 'f+qT+mbVrfY',
    'selfLink': 'https://www.googleapis.com/books/v1/volumes/F_f1YIjDyfsC',
    'volumeInfo': {
      'title': 'Isaac Asimov: Webster\'s Timeline History 1895 - 2007',
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
      'canonicalVolumeLink': 'https://books.google.com/books/about/Isaac_Asimov_Webster_s_Timeline_History.html?hl=&id=F_f1YIjDyfsC',
      'authors': null,
      'publishedDate': null,
      'description': null,
      'pageCount': null,
      'categories': null,
      'panelizationSummary': null,
      'subtitle': null,
      'averageRating': null,
      'ratingsCount': null
    },
    'saleInfo': {
      'country': 'HU',
      'saleability': 'NOT_FOR_SALE',
      'isEbook': false,
      'listPrice': null,
      'retailPrice': null,
      'buyLink': null,
      'offers': null
    },
    'accessInfo': {
      'country': 'HU',
      'viewability': 'NO_PAGES',
      'embeddable': false,
      'publicDomain': false,
      'textToSpeechPermission': 'ALLOWED',
      'epub': {
        'isAvailable': false,
        'acsTokenLink': null
      },
      'pdf': {
        'isAvailable': false,
        'acsTokenLink': null
      },
      'webReaderLink': 'http://play.google.com/books/reader?id=F_f1YIjDyfsC&hl=&printsec=frontcover&source=gbs_api',
      'accessViewStatus': 'NONE',
      'quoteSharingAllowed': false
    },
    'searchInfo': null
  }
  ]
}
";

        [SetUp]
        public void Setup()
        {
            _settings = Options.Create(new GoogleApiSettings { GoogleBooksURL = "http://test.com" });
            _clientFactory = new Mock<IHttpClientFactory>();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonData),
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            _clientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
        }

        [Test]
        public async Task GetSearchResultsAsync_CountofResults_ShouldReturnPositive()
        {
            var service = new GoogleBooksAPIService(_settings, _clientFactory.Object);

            var result = await service.GetSearchResultsAsync("");
            result.Count().Should().BePositive();
        }

        [Test]
        public async Task GetAuthorBooksAsync_CountofResults_ShouldReturnPositive()
        {
            var service = new GoogleBooksAPIService(_settings, _clientFactory.Object);

            var result = await service.GetAuthorBooksAsync("");
            result.Count().Should().BePositive();
        }

        [Test]
        public async Task SerializeResultsFromGoogleBooksApiAsync_CountofResults_ShouldReturnPositive()
        {
            var service = new GoogleBooksAPIService(_settings, _clientFactory.Object);

            var result = await service.SerializeResultsFromGoogleBooksApiAsync("http://test.com");
            result.Count().Should().BePositive();
        }

    }
}
