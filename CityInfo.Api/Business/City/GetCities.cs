using CityInfo.Api.DbContexts;
using Fusonic.Extensions.MediatR;
using MediatR;
using CityInfo.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.Api.Business.City;

public record GetCities
    (string? Name = null, string? SearchQuery = null, int PageNumber = 1, int PageSize = 10) : IQuery<GetCities.Result>
{
    public record Result(IEnumerable<CityDto> Items, PaginationMetadata PaginationMetadata);

    public class Handler : IRequestHandler<GetCities, Result>
    {
        private readonly CityInfoContext context;

        public Handler(CityInfoContext context) => this.context = context;

        public async Task<Result> Handle(GetCities request, CancellationToken cancellationToken)
        {
            var queryable = context.Cities.AsQueryable();
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                queryable = queryable.Where(x => x.Name == request.Name);
            }

            if (!string.IsNullOrWhiteSpace(request.SearchQuery))
            {
                queryable = queryable.Where(x =>
                    x.Name.Contains(request.SearchQuery) || x.Description!.Contains(request.SearchQuery));
            }
            
            var totalItemsCount = await queryable.CountAsync(cancellationToken);
            
            var paginationMetadata = new PaginationMetadata(totalItemsCount, request.PageSize, request.PageNumber);
            
            return new Result(await queryable.Skip(request.PageSize * (request.PageNumber - 1)).Take(request.PageSize)
                .Select(x => new CityDto(x))
                .ToListAsync(cancellationToken), paginationMetadata);
        }
    }
}