
using CityInfo.Api.DbContexts;
using Fusonic.Extensions.EntityFrameworkCore;
using Fusonic.Extensions.MediatR;
using MediatR;

namespace CityInfo.Api.Business.PointOfInterestController;

public record UpdatePoiDescription(int CityId, int PoiId, string? Description): ICommand
{
    public class Handler : IRequestHandler<UpdatePoiDescription>
    {
        private readonly CityInfoContext context;
        public Handler(CityInfoContext context) => this.context = context;
        public async Task<Unit> Handle(UpdatePoiDescription request, CancellationToken cancellationToken)
        {
            
          var poi = await context.PointsOfInterest.SingleRequiredAsync(x => x.Id == request.PoiId && x.CityId == request.CityId, cancellationToken);
          poi.Description = request.Description;
          await context.SaveChangesAsync(cancellationToken);
          return default;   
        }
    }
    
}