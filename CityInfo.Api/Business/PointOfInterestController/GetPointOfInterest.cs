using CityInfo.Api.Models;
using Fusonic.Extensions.MediatR;
using MediatR;

namespace CityInfo.Api.Business.PointOfInterestController;

public record GetPointOfInterest(int CityId, int PoiId) : IQuery<PointOfInterestDto>
{
    public class Handler : IRequestHandler<GetPointOfInterest, PointOfInterestDto>
    {
        
        public Task<PointOfInterestDto> Handle(GetPointOfInterest request, CancellationToken cancellationToken) =>
            Task.FromResult(CitiesDataStore.Current.Cities.Single(x => x.Id == request.CityId).PointsOfInterest
                .Single(x => x.Id == request.PoiId));
    }
}