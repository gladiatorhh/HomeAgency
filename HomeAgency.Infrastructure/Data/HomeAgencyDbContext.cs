using HomeAgency.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeAgency.Infrastructure.Data;

public class HomeAgencyDbContext : DbContext
{
    public HomeAgencyDbContext(DbContextOptions<HomeAgencyDbContext> options) : base(options)
    {

    }

    public DbSet<Villa> villas { get; set; }

    public DbSet<VillaNumber> VillaNumbers { get; set; }

    public DbSet<Amenity> Amenity { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Villa>().HasData(
            new Villa { Id = 1, Name = "LasVegas Mantion", Description = "This is a great villa", Price = 99.9d },
            new Villa { Id = 2, Name = "NewYork Mantion", Description = "This is a great villa", Price = 99.9d },
            new Villa { Id = 3, Name = "LasVegas Mantion", Description = "This is a great villa", Price = 99.9d });

        modelBuilder.Entity<VillaNumber>().HasData(
            new VillaNumber { Villa_Number = 101, VillaId = 1 },
            new VillaNumber { Villa_Number = 102, VillaId = 1 },
            new VillaNumber { Villa_Number = 103, VillaId = 1 },
            new VillaNumber { Villa_Number = 201, VillaId = 2 },
            new VillaNumber { Villa_Number = 202, VillaId = 2 },
            new VillaNumber { Villa_Number = 203, VillaId = 2 },
            new VillaNumber { Villa_Number = 301, VillaId = 3 },
            new VillaNumber { Villa_Number = 302, VillaId = 3 },
            new VillaNumber { Villa_Number = 303, VillaId = 3 });


        modelBuilder.Entity<Amenity>().HasData(
            new Amenity
            {
                Id = 1,
                Name = "Swimming Pool",
                Description = "A large outdoor pool.",
                VillaId = 1
            },
            new Amenity
            {
                Id = 2,
                Name = "Gym",
                Description = "A well-equipped fitness center.",
                VillaId = 1
            },
            new Amenity
            {
                Id = 3,
                Name = "Free Wi-Fi",
                Description = "High-speed internet access throughout the villa.",
                VillaId = 2
            },
            new Amenity
            {
                Id = 4,
                Name = "Parking",
                Description = "Secure on-site parking.",
                VillaId = 2
            }
         );


        //base.OnModelCreating(modelBuilder);
    }
}
