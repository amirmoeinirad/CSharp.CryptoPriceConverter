using CryptoPriceConverter.Services;
using Moq;
using Moq.Protected;
using FluentAssertions;
using System.Net;
using Xunit;

namespace CryptoPriceConverter.Tests
{
    // A test class for ExchangeRateService.
    public class ExchangeRateServiceTest
    {
        // Fact attribute indicates a test method.
        [Fact]
        public async Task GetExchangeRatesAsync() 
        {
            // Fake JSON response (same structure as real API)
            var fakeJson = @"{
            ""rates"": {
                ""USD"": 1.12,
                ""EUR"": 1.0,
                ""BRL"": 6.2,
                ""GBP"": 0.85,
                ""AUD"": 1.45
                }
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

            // Service under test
            var service = new ExchangeRateService(httpClient);

            // Act
            var result = await service.GetExchangeRatesAsync();

            // Assert: verify returned DTO contains expected values
            result.USD.Should().Be((decimal)1.12);
            result.EUR.Should().Be((decimal)1.0);
            result.BRL.Should().Be((decimal)6.2);
            result.GBP.Should().Be((decimal)0.85);
            result.AUD.Should().Be((decimal)1.45);

            // Verify SendAsync was called exactly once
            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            );
        }
    }
}
