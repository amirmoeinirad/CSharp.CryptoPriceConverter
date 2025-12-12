using CryptoPriceConverter.Models.DTOs;

namespace CryptoPriceConverter.Services
{
    // Interface for the Exchange Rate Service.
    public interface IExchangeRateService
    {
        // Method to get exchange rates asynchronously.
        Task<ExchangeRatesDto> GetExchangeRatesAsync();
    }
}
