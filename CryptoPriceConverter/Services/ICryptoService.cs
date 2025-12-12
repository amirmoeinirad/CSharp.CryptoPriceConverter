using CryptoPriceConverter.Models.DTOs;

namespace CryptoPriceConverter.Services
{
    // Interface for the Crypto Service.
    public interface ICryptoService
    {
        // Method to get cryptocurrency price asynchronously.
        // Parameters:
        //   cryptoCode: The code of the cryptocurrency (e.g., BTC for Bitcoin).
        //   currencyCode: The code of the currency to convert to (e.g., USD for US Dollar).
        Task<CryptoPriceDto> GetCryptoPriceAsync(string cryptoCode, string currencyCode);
    }
}
