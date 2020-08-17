using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TravelApi.Models;

namespace TravelApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class LocationsController : ControllerBase
  {
    private TravelApiContext _db;

    public LocationsController(TravelApiContext db)
    {
      _db = db;
    }

    // GET api/locations
    [HttpGet]
    public IActionResult GetLocations([FromQuery] UrlQuery urlQuery)
    {
      IEnumerable<Location> locations = null;

      using (SqlConnection connection = new SqlConnection(_connectionString))
      {
        connection.Open();

        string sql = @"SELECT LocationId, City, Country, Continent FROM Location";

        if (urlQuery.PageNumber.HasValue)
        {
          sql += @" ORDER BY Location.LocationPK
              OFFSET @PageSize * (@PageNumber-1) ROWS
              FETCH NEXT @PageSize ROWS ONLY";
        }
        locations = connection.Query<Location>(sql, urlQuery);
      }
      
      return Ok(locations);
      
      // var query = _db.Locations.AsQueryable();

      // if (continent != null)
      // {
      //   query = query.Where(entry => entry.Continent == continent);
      // }
      // if (country != null)
      // {
      //   query = query.Where(entry => entry.Country == country);
      // }
      // if (city != null)
      // {
      //   query = query.Where(entry => entry.City == city);
      // }

      // return query.ToList();
    }

    // POST api/locations
    [HttpPost]
    public void Post([FromBody] Location location)
    {
      _db.Locations.Add(location);
      _db.SaveChanges();
    }
    // GET api/locations/5
    [HttpGet("{id}")]
    public ActionResult<Location> GetAction(int id)
    {
      return _db.Locations.FirstOrDefault(entry => entry.LocationId == id);
    }

    // PUT api/locations/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] Location location)
    {
      location.LocationId = id;
      _db.Entry(location).State = EntityState.Modified;
      _db.SaveChanges();
    }

    // DELETE api/locatons/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
      var locationToDelete = _db.Locations.FirstOrDefault(entry => entry.LocationId == id);
      _db.Locations.Remove(locationToDelete);
      _db.SaveChanges();
    }
  }
}