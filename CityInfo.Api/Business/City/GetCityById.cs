using CityInfo.Api.DbContexts;
using CityInfo.Api.Models;
using Fusonic.Extensions.EntityFrameworkCore;
using Fusonic.Extensions.MediatR;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.Api.Business.City;

public record GetCityById(int Id) : IQuery<CityDto>
{
    public class Handler : IRequestHandler<GetCityById, CityDto>
    {
        private readonly CityInfoContext context;

        public Handler(CityInfoContext context) => this.context = context;


        public async Task<CityDto> Handle(GetCityById request, CancellationToken cancellationToken) =>
            new CityDto(await context.Cities
                .SingleRequiredAsync(x => x.Id == request.Id, cancellationToken));
    }
}