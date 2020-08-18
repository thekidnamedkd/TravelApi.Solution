using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
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

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
    {
        var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
        var pagedData = await context.Locations
          .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
          .Take(validFilter.PageSize)
          .ToListAsync();
        var totalRecords = await context.Locations.CountAsync();
        return Ok(new PagedResponse<List<Location>>(pagedData, validFilter.PageNumber, validFilter.PageSize));
    }

    // GET api/locations
    [HttpGet]
    public ActionResult<IEnumerable<Location>> Get(string continent, string country, string city)
    {
      var query = _db.Locations.AsQueryable();

      if (continent != null)
      {
        query = query.Where(entry => entry.Continent == continent);
      }
      if (country != null)
      {
        query = query.Where(entry => entry.Country == country);
      }
      if (city != null)
      {
        query = query.Where(entry => entry.City == city);
      }

      return query.ToList();
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