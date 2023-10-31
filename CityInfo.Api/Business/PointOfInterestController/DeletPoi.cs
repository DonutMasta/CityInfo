using Fusonic.Extensions.MediatR;
using MediatR;

namespace CityInfo.Api.Business.PointOfInterestController;

public record DeletePoi(int CityId , int PoiId ): ICommand
{
    public class Handler : IRequestHandler<DeletePoi>
    {
        public Task<Unit> Handle(DeletePoi request, CancellationToken cancellationToken)
        {
            var city = CitiesDataStore.Current.Cities.Single(x => x.Id == request.CityId);
            var poi = city.PointsOfInterest.Single(x => x.Id == request.PoiId);

            city.PointsOfInterest.Remove(poi);
            return Task.FromResult<Unit>(default);
        }
    }
}