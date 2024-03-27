using HomeAgency.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeAgency.Infrastructure.Data;

public class HomeAgencyDbContext : DbContext
{
    public HomeAgencyDbContext(DbContextOptions<HomeAgencyDbContext> options) : base(options)
    {
        
    }

    public DbSet<Villa> villas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Villa>().HasData(
            new Villa { Id = 1,Name = "LasVegas Mantion",Description = "This is a great villa",Price = 99.9d},
            new Villa { Id = 2,Name = "NewYork Mantion",Description = "This is a great villa",Price = 99.9d},
            new Villa { Id = 3,Name = "LasVegas Mantion",Description = "This is a great villa",Price = 99.9d});

        //base.OnModelCreating(modelBuilder);
    }
}
