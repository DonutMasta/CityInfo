using CityInfo.Api.DbContexts;
using CityInfo.Api.Models;
using Fusonic.Extensions.EntityFrameworkCore;
using Fusonic.Extensions.MediatR;
using MediatR;

namespace CityInfo.Api.Business.PointOfInterest;

public record GetPointsOfInterestByCityId(int CityId) :
    IQuery<GetPointsOfInterestByCityId.Result>
{
    public record Result(IEnumerable<PointOfInterestDto> Items);

    public class Handler : IRequestHandler<GetPointsOfInterestByCityId, Result>
    {
        private readonly CityInfoContext context;
        public Handler(CityInfoContext context) => this.context = context;

        public async Task<Result> Handle(GetPointsOfInterestByCityId request, CancellationToken cancellationToken)
        {
            var city = await context.Cities.SingleRequiredAsync(x => x.Id == request.CityId, cancellationToken);
            return new Result(city.PointsOfInterest.Select(x => new PointOfInterestDto(x)));
        }
    }
}