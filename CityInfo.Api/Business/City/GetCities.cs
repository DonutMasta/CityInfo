using CityInfo.Api.DbContexts;
using Fusonic.Extensions.MediatR;
using MediatR;
using CityInfo.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.Api.Business.City;

public record GetCities : IQuery<GetCities.Result>
{
    public record Result(IEnumerable<CityDto> Items);

    public class Handler : IRequestHandler<GetCities, Result>
    {
        private readonly CityInfoContext context;

        public Handler(CityInfoContext context) => this.context = context;

        public async Task<Result> Handle(GetCities request, CancellationToken cancellationToken) =>
            new Result(await context.Cities.Select(x => new CityDto(x))
                .ToListAsync(cancellationToken));
    }
}