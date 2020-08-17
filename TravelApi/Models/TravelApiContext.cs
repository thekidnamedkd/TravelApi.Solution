using Microsoft.EntityFrameworkCore;

namespace TravelApi.Models
{
  public class TravelApiContext : DbContext
  {
    public TravelApiContext(DbContextOptions<TravelApiContext> options)
      : base(options)
      {
      }

      public DbSet<Location> Locations {get; set;}
  }
}