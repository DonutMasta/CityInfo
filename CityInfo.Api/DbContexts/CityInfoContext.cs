using CityInfo.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.Api.DbContexts;

public class CityInfoContext : DbContext
{
    public DbSet<City> Cities { get; set; } = null!;
    public DbSet<PointOfInterest> PointsOfInterest { get; set; } = null!;
    
    public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>().Navigation(x => x.PointsOfInterest).AutoInclude();
        modelBuilder.Entity<City>().HasData(new City("Dornbirn"){
                Id = 1,
                Description = "Where the FHV is located"
            },
            new City("Feldkirch")
            {
                Id = 2,
                Description = "The one with the cat tower"
            },
            new City("Bludenz")
            {
                Id = 3,
                Description = "Last city before the Arlberg"
            });

        modelBuilder.Entity<PointOfInterest>().HasData(new PointOfInterest("FHV")
            {
                Id = 1,
                CityId = 1,
                Description = "The best FH in the world"
            },
            new PointOfInterest("Mohren Brauerei")
            {
                Id = 2,
                CityId = 1,
                Description = "Second best beer in Vbg"
            },
            new PointOfInterest("Cat tower")
            {
                Id = 3,
                CityId = 2,
                Description = "Some old important building"
            },
            new PointOfInterest("Schattenburg")
            {
                Id = 4,
                CityId = 2,
                Description = "Biiiiiiiiig schnitzel"
            },
            new PointOfInterest("Schoki")
            {
                Id = 5,
                CityId = 3,
                Description = "What else do you need?"
            },new PointOfInterest("Fohrenburger Brauerei")
            {
                Id = 6,
                CityId = 3,
                Description = "The worst beer in Vbg"
            });
        base.OnModelCreating(modelBuilder);
    }
}