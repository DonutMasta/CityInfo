
using Fusonic.Extensions.MediatR;
using MediatR;

namespace CityInfo.Api.Business.PointOfInterestController;

public record UpdatePoiDescription(int CityId, int PoiId, string? Description): ICommand
{
    public class Handler : IRequestHandler<UpdatePoiDescription>
    {
        public Task<Unit> Handle(UpdatePoiDescription request, CancellationToken cancellationToken)
        {
            var poi = CitiesDataStore.Current.Cities.Single(x => x.Id == request.CityId).PointsOfInterest
                .Single(x => x.Id == request.PoiId);
            poi.Description = request.Description;
            return Task.FromResult<Unit>(default);
        }
    }
    
}