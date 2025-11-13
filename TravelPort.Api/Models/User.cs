using TravelPort.Core.Enums;

namespace TravelPort.Api.Models;

public record User(
    string PassportNumber, 
    Airport SelectedAirport,
    string Name,
    string Surname, 
    string Email, 
    string Address,
    string PhoneNumber);