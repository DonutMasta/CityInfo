using Microsoft.AspNetCore.Mvc;

namespace CityInfo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CitiesController : ControllerBase
{
    [HttpGet]
    public JsonResult GetCities()
    {
        return new JsonResult(CitiesDataStore.Current.Cities);
    }

    [HttpGet("{id}")]
    public JsonResult GetCityById(int id)
    {
        return new JsonResult(
            CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == id));
    }
}