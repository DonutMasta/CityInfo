

using CityInfo.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CitiesController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<CityDto>> GetCities()
    {
        return Ok(CitiesDataStore.Current.Cities);
    }

    [HttpGet("{id}")]
    public ActionResult<CityDto> GetCityById(int id)
    {
        var result = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == id);
        if (result == null)
            return NotFound();

        return Ok(result);
    }
}