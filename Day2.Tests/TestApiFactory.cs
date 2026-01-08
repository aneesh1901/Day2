using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Day2.Data;

namespace Day2.Tests;

public class TestApiFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development"); // so you get useful errors

        builder.ConfigureServices(services =>
        {
            // Build the service provider so we can resolve AppDbContext
            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // This creates the database schema (tables) if they don't exist
            db.Database.EnsureCreated();
        });
    }
}
