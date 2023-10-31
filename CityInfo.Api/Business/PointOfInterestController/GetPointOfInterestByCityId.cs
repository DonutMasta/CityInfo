using CityInfo.Api.Models;
using Fusonic.Extensions.MediatR;
using MediatR;

namespace CityInfo.Api.Business.PointOfInterestController;

public record GetPointsOfInterestByCityId(int CityId) :
    IQuery<GetPointsOfInterestByCityId.Result>
{
    public record Result(IEnumerable<PointOfInterestDto> Items);

    public class Handler : IRequestHandler<GetPointsOfInterestByCityId, Result>
    {
        public Task<Result> Handle(GetPointsOfInterestByCityId request, CancellationToken cancellationToken) =>
            Task.FromResult(new Result(CitiesDataStore.Current.Cities.Single(x => x.Id == request.CityId)
                .PointsOfInterest));
    }
}