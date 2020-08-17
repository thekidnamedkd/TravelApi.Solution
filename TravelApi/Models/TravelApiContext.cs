using Microsoft.EntityFrameworkCore;

namespace TravelApi.Models
{
  public class TravelApiContext : DbContext
  {
    public TravelApiContext(DbContextOptions<TravelApiContext> options)
      : base(options)
      {
      }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<Location>()
          .HasData(
              new Location { LocationId = 1, City = "Portland", Country = "USA", Continent = "North America" },
              new Location { LocationId = 2, City = "Paris", Country = "France", Continent = "Europe" },
              new Location { LocationId = 3, City = "Mexico City", Country = "Mexico", Continent = "North America" },
              new Location { LocationId = 4, City = "Tokyo", Country = "Japan", Continent = "Asia" },
              new Location { LocationId = 5, City = "Rome", Country = "Italy", Continent = "Europe" }
          );
    }

      public DbSet<Location> Locations { get; set; }
  }
}