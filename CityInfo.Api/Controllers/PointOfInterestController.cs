using System.ComponentModel.DataAnnotations;
using CityInfo.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.Api.Business.PointOfInterestController;

[Route("api/cities/{cityId}/[controller]")]
[ApiController]
public class PointOfInterestController : ControllerBase
{
    private readonly IMediator mediator;
    public PointOfInterestController(IMediator mediator) => this.mediator = mediator;

    [HttpGet]
    public Task<GetPointsOfInterestByCityId.Result> GetPointsOfInterest(int cityId) =>
        mediator.Send(new GetPointsOfInterestByCityId(cityId));

    [HttpGet("{pointOfInterestId}", Name = "GetPointOfInterest")]
    public Task<PointOfInterestDto> GetPointOfInterest(int cityId, int pointOfInterestId) =>
        mediator.Send(new GetPointOfInterest(cityId, pointOfInterestId));

    public class PoiCreationModel
    {
        [Required(ErrorMessage = "You should provide a name value.")]
        [MaxLength(50)]
        public string name { get; set; } = string.Empty;

        [MaxLength(50)] public string? Description { get; set; }
    }

    [HttpPost]
    public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterestAsync(
        [FromRoute] int cityId,
        [FromBody] PoiCreationModel body)
    {
        var poi = await mediator.Send(
            new AddPointOfInterest(body.name, body.Description, cityId));
        return CreatedAtRoute("GetPointOfInterest",
            new
            {
                cityId,
                pointOfInterestId = poi.Id
            }, poi);
    }

    public record UpdatePoiNameModel([MaxLength(50)] string Name);

    [HttpPost("{pointOfInterestId}/UpdateName")]
    public Task<Unit>
        UpdatePointOfInterestName(int cityId, int pointOfInterestId, [FromBody] UpdatePoiNameModel body) =>
        mediator.Send(new UpdatePoiName(cityId, pointOfInterestId, body.Name));

    public record UpdatePoiDescriptionModel([MaxLength(200)] string? Description);

    [HttpPost("{pointOfInterestId}/UpdateDescription")]
    public Task<Unit> UpdatePointOfInterestDescription(int cityId, int pointOfInterestId,
        [FromBody] UpdatePoiDescriptionModel body) =>
        mediator.Send(new UpdatePoiDescription(cityId, pointOfInterestId, body.Description));

    [HttpDelete("{pointOfInterestId}")]
    public Task<Unit> DeletePointOfInterest(int cityId, int pointOfInterestId) =>
        mediator.Send(new DeletePoi(cityId, pointOfInterestId));
}