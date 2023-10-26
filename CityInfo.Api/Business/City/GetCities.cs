using Fusonic.Extensions.MediatR;
using MediatR;
using CityInfo.Api.Models;

namespace CityInfo.Api.Business.City;

public record GetCities : IQuery<GetCities.Result>
{
    public record Result(IEnumerable<CityDto> Items);

    public class Handler : IRequestHandler<GetCities, Result>
    {
        public Task<Result> Handle(GetCities request, CancellationToken cancellationToken) =>
            Task.FromResult(new Result(CitiesDataStore.Current.Cities));
    }
}