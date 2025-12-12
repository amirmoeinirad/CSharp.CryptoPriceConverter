
// Amir Moeini Rad
// Dec 10, 2025

using CryptoPriceConverter.Services;

namespace CryptoPriceConverter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create a WebApplication builder instance.
            var builder = WebApplication.CreateBuilder(args);


            // =======================================================
            // Add services to the Dependecy Injection (DI) container.
            // =======================================================


            // Add MVC services to the DI container.
            builder.Services.AddControllersWithViews();

            // Register custom services for DI.
            // 'AddHttpClient' registers services that make HTTP requests to external APIs.
            // 'AddScoped' registers services with a scoped lifetime (per HTTP request).
            builder.Services.AddHttpClient<ICryptoService, CryptoService>();
            builder.Services.AddHttpClient<IExchangeRateService, ExchangeRateService>();
            builder.Services.AddScoped<ICryptoToCurrencyConversionService, CryptoToCurrencyConversionService>();


            // ===================


            // Build the WebApplication instance.
            var app = builder.Build();


            // ===============================================
            // Configure the HTTP request Middleware pipeline.
            // ===============================================


            if (!app.Environment.IsDevelopment())
            {
                // Use a custom error handling page for production environment.
                app.UseExceptionHandler("/Home/Error");

                // HSTS (HTTP Strict Transport Security) adds a special response header
                // that informs browsers that the site should only be accessed using HTTPS.
                app.UseHsts();
            }

            // Redirect HTTP requests to HTTPS.
            app.UseHttpsRedirection();

            // Enable routing capabilities such as attribute routing.
            app.UseRouting();

            // Enable Authorization capabilities such as [Authorize] attribute.
            app.UseAuthorization();

            // Enable using static files like CSS, JS, images, etc.
            app.UseStaticFiles();

            // Define the default route for MVC controllers.
            // This is the home page URL pattern, i.e., "/Home/Index".
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}");


            // ===================


            // Execute the web application.
            app.Run();
        }
    }
}
