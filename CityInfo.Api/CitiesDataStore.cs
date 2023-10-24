using CityInfo.Api.Models;

namespace CityInfo.Api;

public class CitiesDataStore
{
    public List<CityDto> Cities { get; set; }
    public static CitiesDataStore Current { get; } = new CitiesDataStore();

    private CitiesDataStore()
    {
        // init dummy data
        Cities = new List<CityDto>()
        {
            new CityDto()
            {
                Id = 1,
                Name = "Dornbirn",
                Description = "Where the FHV is located",
                PointsOfInterest = new List<PointOfInterestDto>()
                {
                    new PointOfInterestDto()
                    {
                        Id = 1,
                        Name = "FHV",
                        Description = "The best FH in the world"
                    },
                    new PointOfInterestDto()
                    {
                        Id = 2,
                        Name = "Mohren Brauerei",
                        Description = "Second best beer in Vbg"
                    },
                }
            },

            new CityDto()
            {
                Id = 2,
                Name = "Feldkirch",
                Description = "The one with the cat tower",
                PointsOfInterest = new List<PointOfInterestDto>()
                {
                    new PointOfInterestDto()
                    {
                        Id = 1,
                        Name = "Cat tower",
                        Description = "Some old important building"
                    },
                    new PointOfInterestDto()
                    {
                        Id = 2,
                        Name = "Schattenburg", Description = "Biiiiiiiiig schnitzel"
                    },
                }
            },

            new CityDto()
            {
                Id = 3,
                Name = "Bludenz",
                Description = "Last city before the Arlberg",
                PointsOfInterest = new List<PointOfInterestDto>()
                {
                    new PointOfInterestDto()
                    {
                        Id = 1,
                        Name = "Schoki",
                        Description = "What else do you need?"
                    },
                    new PointOfInterestDto()
                    {
                        Id = 2,
                        Name = "Fohrenburger Brauerei",
                        Description = "The worst beer in Vbg"
                    },
                }
            }
        };
    }
}