using AuthService.Infra.Models;
using AuthService.Utils;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infra.Context;

/// <summary>
/// Represents the Entity Framework database context for authentication-related data.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="AuthDbContext"/> class using the specified options.
/// </remarks>
/// <param name="options">The options to be used by the DbContext.</param>
public class AuthDbContext(DbContextOptions<AuthDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Gets the <see cref="DbSet{TEntity}"/> of users.
    /// </summary>
    public DbSet<User> Users => Set<User>();

    /// <summary>
    /// Configures the model for the authentication database context.
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(DatabaseConstants.AuthSchema);
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();
        base.OnModelCreating(modelBuilder);
    }
}