using Microsoft.AspNetCore.Identity;

namespace CityInfo.Api.Entities;

public class User : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    
}