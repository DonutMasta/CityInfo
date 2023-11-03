using CityInfo.Api.Entities;

namespace CityInfo.Api.Models;

public class PointOfInterestDto
{

    public PointOfInterestDto()
    {
    }
    public PointOfInterestDto(PointOfInterest pointOfInterest)
    {
        Id = pointOfInterest.Id;
        Name = pointOfInterest.Name;
        Description = pointOfInterest.Description;
    }
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } 
}