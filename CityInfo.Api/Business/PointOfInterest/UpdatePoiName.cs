using CityInfo.Api.DbContexts;
using Fusonic.Extensions.EntityFrameworkCore;
using Fusonic.Extensions.MediatR;
using MediatR;

namespace CityInfo.Api.Business.PointOfInterest;

public record UpdatePoiName(int CityId, int PoiId , string Name) : ICommand
{
    public class Handler : IRequestHandler<UpdatePoiName>
    {
        private readonly CityInfoContext context;
        
        public Handler(CityInfoContext context) => this.context = context;
        public async Task<Unit> Handle(UpdatePoiName request, CancellationToken cancellationToken)
        {
            var poi = await context.PointsOfInterest.SingleRequiredAsync(
                x => x.Id == request.PoiId && x.CityId == request.CityId, cancellationToken);

            poi.Name = request.Name;
            await context.SaveChangesAsync(cancellationToken);
            return default;
        }
    }
}