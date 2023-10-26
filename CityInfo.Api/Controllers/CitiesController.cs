using CityInfo.Api.Business.City;
using CityInfo.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CitiesController : ControllerBase
{
    private readonly IMediator mediator;
    public CitiesController(IMediator mediator) => this.mediator = mediator;

    [HttpGet]
    public async Task<GetCities.Result> GetCities() => await mediator.Send(new GetCities());

    [HttpGet("{id}")]
    public async Task<CityDto> GetCityById(int id) => await mediator.Send(new GetCityById(id));
}