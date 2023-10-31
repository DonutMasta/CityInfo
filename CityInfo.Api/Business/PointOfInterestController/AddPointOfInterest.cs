using CityInfo.Api.Models;
using Fusonic.Extensions.MediatR;
using MediatR;

namespace CityInfo.Api.Business.PointOfInterestController;

public record AddPointOfInterest(string Name, string? Description, int CityId) : ICommand<PointOfInterestDto>
{
    public class Handler : IRequestHandler<AddPointOfInterest, PointOfInterestDto>
    {
        public Task<PointOfInterestDto> Handle(AddPointOfInterest request, CancellationToken cancellationToken)
        {
            var city = CitiesDataStore.Current.Cities.Single(x => x.Id == request.CityId);
            var nextId = CitiesDataStore.Current.Cities.SelectMany(x => x.PointsOfInterest).Select(x => x.Id).Max() + 1;

            var poi = new PointOfInterestDto()
            {
                Id = nextId,
                Name = request.Name,
                Description = request.Description
            };
            city.PointsOfInterest.Add(poi);
            return Task.FromResult(poi);
        }
    }
}