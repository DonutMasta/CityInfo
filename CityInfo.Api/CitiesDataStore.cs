using CityInfo.Api.Models;
namespace CityInfo.Api;

public class CitiesDataStore
{
    public List<CityDto> Cities { get; set; } = new()
    {
        new CityDto()
        {
            Id = 1,
            Name = "Dornbirn",
            Description = "Where the FHV is located",
        },
        new CityDto()
        {
            Id = 2,
            Name = "Feldkirch",
            Description = "The one with the cat tower",
        },
        new CityDto()
        {
            Id= 3,
            Name = "Bludenz",
            Description = "Last city before the Arlberg",
        }
    };

    public static CitiesDataStore Current { get; } = new CitiesDataStore();
}