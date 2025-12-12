using CryptoPriceConverter.Models;
using CryptoPriceConverter.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace CryptoPriceConverter.Controllers
{
    // Controller class for handling cryptocurrency-related requests.
    public class CryptoController : Controller
    {
        private readonly IExchangeRateService _exchangeRateService;
        private readonly ICryptoService _cryptoService;
        private readonly ICryptoToCurrencyConversionService _cryptoToCurrencyConversionService;

        
        // ===========================================================


        // Constructor to inject the required services.
        public CryptoController(IExchangeRateService exchangeRateService, ICryptoService cryptoService, ICryptoToCurrencyConversionService cryptoToCurrencyConversionService)
        {
            _exchangeRateService = exchangeRateService;
            _cryptoService = cryptoService;
            _cryptoToCurrencyConversionService = cryptoToCurrencyConversionService;
        }

        
        // ===========================================================


        // Action method for handling an HTTP POST request to get the cryptocurrency quotes.
        [HttpPost]
        public async Task<IActionResult> GetQuotes(string cryptoCode)
        {
            // Validate the input cryptocurrency code.
            if (string.IsNullOrWhiteSpace(cryptoCode))
            {
                return BadRequest("Cryptocurrency code is required.");
            }
            if (cryptoCode.Length != 3)
            {
                return BadRequest("Cryptocurrency code must be exactly 3 characters long.");
            }
            string pattern = "^[A-Za-z]+$";
            if (!Regex.IsMatch(cryptoCode, pattern))
            {
                return BadRequest("Cryptocurrency code must contain only letters.");
            }



            // Fetch exchange rates from the exchange rate service.
            var exchangeRatesDto = await _exchangeRateService.GetExchangeRatesAsync();

            
            // Fetch cryptocurrency prices from the crypto service.
            var cryptoPriceDto = await _cryptoService.GetCryptoPriceAsync(cryptoCode, "USD");


            // Convert cryptocurrency price to various currencies using the conversion service.
            var convertedCryptoPricesDto = _cryptoToCurrencyConversionService.ConvertCryptoToCurrencies(exchangeRatesDto, cryptoPriceDto);


            var model = new CryptoViewModel
            {
                ExchangeRates = exchangeRatesDto,
                ConvertedPrices = convertedCryptoPricesDto,                
                CryptoCode = cryptoCode
            };            


            // Redirect to the Home controller's Index action to display the results.
            return View("~/Views/Home/Index.cshtml", model);
        }
    }
}
