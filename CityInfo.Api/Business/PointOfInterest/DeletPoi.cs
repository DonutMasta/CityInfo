using CityInfo.Api.DbContexts;
using Fusonic.Extensions.EntityFrameworkCore;
using Fusonic.Extensions.MediatR;
using MediatR;

namespace CityInfo.Api.Business.PointOfInterest;

public record DeletePoi(int CityId, int PoiId) : ICommand
{
    public class Handler : IRequestHandler<DeletePoi>
    {
        private readonly CityInfoContext context;
        public Handler(CityInfoContext context) => this.context = context;

        public async Task<Unit> Handle(DeletePoi request, CancellationToken cancellationToken)
        {
            var poi = await context.PointsOfInterest.SingleRequiredAsync(
                x => x.Id == request.PoiId && x.CityId == request.CityId, cancellationToken);

            context.PointsOfInterest.Remove(poi);
            await context.SaveChangesAsync(cancellationToken);
            return default;
        }
    }
}