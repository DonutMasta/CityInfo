namespace CityInfo.Api.Models;
using System.ComponentModel.DataAnnotations;
public class PointOfInterestCreationDto
{
    [Required(ErrorMessage = "You should provide a name value.")]
    [MaxLength(50)]
    public string name { get; set; } = string.Empty;
    [MaxLength(50)]
    public string? Description { get; set; }
}
