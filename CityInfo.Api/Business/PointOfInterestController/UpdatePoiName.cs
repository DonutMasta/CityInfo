using Fusonic.Extensions.MediatR;
using MediatR;

namespace CityInfo.Api.Business.PointOfInterestController;

public record UpdatePoiName(int CityId, int PoiId , string Name) : ICommand
{
    public class Handler : IRequestHandler<UpdatePoiName>
    {
        public Task<Unit> Handle(UpdatePoiName request, CancellationToken cancellationToken)
        {
            var poi = CitiesDataStore.Current.Cities.Single(x => x.Id == request.CityId).PointsOfInterest
                .Single(x => x.Id == request.PoiId);

            poi.Name = request.Name;
            return Task.FromResult<Unit>(default);
        }
    }
}