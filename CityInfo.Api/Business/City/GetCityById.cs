using CityInfo.Api.Models;
using Fusonic.Extensions.MediatR;
using MediatR;

namespace CityInfo.Api.Business.City;

public record GetCityById(int Id) : IQuery<CityDto> 
{
    public class Handler : IRequestHandler<GetCityById, CityDto>
    {
        public Task<CityDto> Handle(GetCityById request, CancellationToken cancellationToken) =>
            Task.FromResult(CitiesDataStore.Current.Cities.Single(x => x.Id == request.Id));
    }
}