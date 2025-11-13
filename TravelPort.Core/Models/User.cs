using System.ComponentModel.DataAnnotations;
using TravelPort.Core.Enums;

namespace TravelPort.Core.Models;

public class User
{
    [Key]
    public required string PassportNumber { get; set; }
    public Airport SelectedAirport { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
}