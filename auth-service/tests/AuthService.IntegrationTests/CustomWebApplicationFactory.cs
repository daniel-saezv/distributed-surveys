using AuthService.Infra.Context;
using AuthService.Infra.Models;
using AuthService.Models.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace AuthService.IntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private Action<AuthDbContext>? _seeder;
    private SqliteConnection? _connection;

    public void SeedDatabase(Action<AuthDbContext> seeder)
    {
        _seeder = seeder;
        using var scope = Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
        _seeder?.Invoke(db);
        db.SaveChanges();
    }
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        builder.ConfigureAppConfiguration((context, config) =>
        {
            config.AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: false);
        });
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<IDbContextOptionsConfiguration<AuthDbContext>>();

            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            services.AddDbContext<AuthDbContext>(options =>
            {
                options.UseSqlite(_connection);
            });

            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
            db.Database.EnsureCreated();
            var hasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher<User>>();
            var user = new User
            {
                Username = "test"
            };
            user.PasswordHash = hasher.HashPassword(user, "test");
            db.Users.Add(user);
            db.SaveChanges();
        });

    }
}