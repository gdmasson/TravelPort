using Microsoft.EntityFrameworkCore;
using TravelPort.Core.Models;

namespace TravelPort.Core.Services;

public class PgContext(DbContextOptions<PgContext> options) : Microsoft.EntityFrameworkCore.DbContext(options)
{
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(u => u.PassportNumber);
    }
}