using CityInfo.Api.Business.City;
using CityInfo.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CityInfo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CitiesController : ControllerBase
{
    private readonly IMediator mediator;
    public CitiesController(IMediator mediator) => this.mediator = mediator;

    [HttpGet]
    public async Task<GetCities.Result> GetCities(string? name, string? searchQuery, int pageNumber = 1,
        int pageSize = 10)
    {
       var result = await mediator.Send(new GetCities(name, searchQuery, pageNumber, pageSize));
       Response.Headers.Add("X-Pagination",JsonConvert.SerializeObject(result.PaginationMetadata));
       return result;
    }

    [HttpGet("{id}")]
    public async Task<CityDto> GetCityById(int id) => await mediator.Send(new GetCityById(id));
}