using CityInfo.Api.Entities;

namespace CityInfo.Api.Models;

public class CityDto
{
    public CityDto()
    {
    }

    public CityDto(City city)
    {
        Id = city.Id;
        Name = city.Name;
        Description = city.Description;
        PointsOfInterest = city.PointsOfInterest.Select(poi => new PointOfInterestDto(poi)).ToList();
    }

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int NumberOfPointsOfInterest => PointsOfInterest.Count;
    public ICollection<PointOfInterestDto> PointsOfInterest { get; set; } = new List<PointOfInterestDto>();
}