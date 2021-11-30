using HotelListing.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Data.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasData(
                new Country {Id = 1, Name = "Jamaica", ShortName = "JM"},
                new Country {Id = 2, Name = "Bahamas", ShortName = "BS"},
                new Country {Id = 3, Name = "Cayman Island", ShortName = "CI"}
            );

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel {Id = 1, Name = "Sandals Resort and Spa", Address = "Negril", CountryId = 1, Rating = 4.5},
                new Hotel {Id = 2, Name = "Grand Palladium", Address = "Nassua", CountryId = 2, Rating = 4},
                new Hotel {Id = 3, Name = "Comfort Suites", Address = "George Town", CountryId = 3, Rating = 3}
            );
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
    }
}