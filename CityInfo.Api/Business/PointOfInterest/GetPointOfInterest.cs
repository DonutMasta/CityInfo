using CityInfo.Api.DbContexts;
using CityInfo.Api.Models;
using Fusonic.Extensions.EntityFrameworkCore;
using Fusonic.Extensions.MediatR;
using MediatR;

namespace CityInfo.Api.Business.PointOfInterest;

public record GetPointOfInterest(int CityId, int PoiId) : IQuery<PointOfInterestDto>
{
    public class Handler : IRequestHandler<GetPointOfInterest, PointOfInterestDto>
    {
        private readonly CityInfoContext context;
        public Handler(CityInfoContext context) => this.context = context;

        public async Task<PointOfInterestDto> Handle(GetPointOfInterest request, CancellationToken cancellationToken) =>
            new PointOfInterestDto(await context.PointsOfInterest.SingleRequiredAsync(
                x => x.Id == request.PoiId && x.CityId == request.CityId, cancellationToken));
    }
}