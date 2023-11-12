using CityInfo.Api.DbContexts;
using CityInfo.Api.Models;
using Fusonic.Extensions.EntityFrameworkCore;
using Fusonic.Extensions.MediatR;
using MediatR;

namespace CityInfo.Api.Business.PointOfInterest;

public record AddPointOfInterest(string Name, string? Description, int CityId) : ICommand<PointOfInterestDto>
{
    public class Handler : IRequestHandler<AddPointOfInterest, PointOfInterestDto>
    {
        private readonly CityInfoContext context;
        public Handler(CityInfoContext context) => this.context = context;
        public async Task<PointOfInterestDto> Handle(AddPointOfInterest request, CancellationToken cancellationToken)
        {
            var city = await context.Cities.SingleRequiredAsync(x => x.Id == request.CityId, cancellationToken);

            var poi = new Entities.PointOfInterest(request.Name)
            {
                Description = request.Description
            };
            city.PointsOfInterest.Add(poi);
            await context.SaveChangesAsync(cancellationToken);
            return new PointOfInterestDto(poi);
        }
    }
}