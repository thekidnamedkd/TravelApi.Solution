using System.ComponentModel.DataAnnotations;

namespace TravelApi.Models
{
  public class Location
  {
    public int LocationId   {get; set;}
    [Required]
    [StringLength(20)]
    public string City  {get; set;}
    [Required]
    public string Country { get; set; }
    [Required]
    public string Continent { get; set; }
  }
}