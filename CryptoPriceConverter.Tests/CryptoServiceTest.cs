using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Moq;
using CryptoPriceConverter.Services;
using CryptoPriceConverter.Models.DTOs;
using System.Threading;
using Moq.Protected;

public class CryptoServiceTests
{
    [Fact]
    public async Task GetCryptoPriceAsync()
    {
        // Arrange
        string fakeJson = @"{
          ""data"": [
            {
              ""symbol"": ""BTC"",
              ""quote"": {
                ""USD"": { ""price"": 50000.25 }
               }
             }
           ]
        }";

        // Create mocked HttpMessageHandler
        var handlerMock = new Mock<HttpMessageHandler>();

        // Setup the mock to return the fake JSON response
        handlerMock.Protected()
                   .Setup<Task<HttpResponseMessage>>(
                        "SendAsync",
                        ItExpr.IsAny<HttpRequestMessage>(),
                        ItExpr.IsAny<CancellationToken>()
                        )
                   .ReturnsAsync(new HttpResponseMessage
                   {
                       StatusCode = HttpStatusCode.OK,
                       Content = new StringContent(fakeJson)
                   });

        // Pass mocked handler into HttpClient
        var httpClient = new HttpClient(handlerMock.Object);

        var service = new CryptoService(httpClient);

        // Act
        CryptoPriceDto result = await service.GetCryptoPriceAsync("BTC", "USD");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("BTC", result.Symbol);  // Symbol extracted from JSON
        Assert.Equal(50000.25m, result.Price);
    }


    [Fact]
    public async Task GetCryptoPriceAsync_ThrowsException_WhenSymbolNotFound()
    {
        // Arrange
        string fakeJson = @"
        {
          ""data"": [
            {
              ""symbol"": ""ETH"",
              ""quote"": {
                ""USD"": { ""price"": 3000.10 }
              }
            }
          ]
        }";

        // Create mocked HttpMessageHandler
        var handlerMock = new Mock<HttpMessageHandler>();

        // Setup the mock to return the fake JSON response
        handlerMock.Protected()
                   .Setup<Task<HttpResponseMessage>>(
                        "SendAsync",
                        ItExpr.IsAny<HttpRequestMessage>(),
                        ItExpr.IsAny<CancellationToken>()
                        )
                   .ReturnsAsync(new HttpResponseMessage
                   {
                       StatusCode = HttpStatusCode.OK,
                       Content = new StringContent(fakeJson)
                   });

        // Pass mocked handler into HttpClient
        var httpClient = new HttpClient(handlerMock.Object);

        var service = new CryptoService(httpClient);

        // Act + Assert
        await Assert.ThrowsAsync<Exception>(async () =>
        {
            await service.GetCryptoPriceAsync("BTC", "USD");
        });
    }
}
