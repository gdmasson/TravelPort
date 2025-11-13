using TravelPort.Core.Enums;
using TravelPort.Core.Models;

namespace TravelPort.Core.Services;

public class UserService : IUserService
{
    private readonly IPersistenceService _persistenceService;
    
    public UserService(IPersistenceService persistenceService)
    {
        _persistenceService = persistenceService;
    }
    
    public async Task<bool> CreateUser(
        string passportNumber, 
        Airport airport, 
        string name, 
        string surname,
        string? email, 
        string? address,
        string? phoneNumber)
    {
        if(!ValidateAirport(airport) || !ValidatePassportNumber(passportNumber))
        {
            return false;
        }
        
        var user = new User
        {
            PassportNumber = passportNumber,
            SelectedAirport = airport,
            Name = name,
            Surname = surname,
            Email = email,
            Address = address,
            PhoneNumber = phoneNumber
        };

        return await _persistenceService.CreateUser(user);
    }

    public async Task<bool> UpdateUser(
        string passportNumber,
        Airport airport, 
        string name, 
        string surname, 
        string? email, 
        string? address,
        string? phoneNumber)
    {
        if(!ValidateAirport(airport) || !ValidatePassportNumber(passportNumber))
        {
            return false;
        }
        
        var user = new User
        {
            PassportNumber = passportNumber,
            SelectedAirport = airport,
            Name = name,
            Surname = surname,
            Email = email,
            Address = address,
            PhoneNumber = phoneNumber
        };

        return await _persistenceService.UpdateUser(user);
    }

    public async Task<User?> GetUserByPassportNumber(string passportNumber)
    {
        if(!ValidatePassportNumber(passportNumber))
        {
            return null;
        }

        return await _persistenceService.GetUserByPassportNumber(passportNumber);
    }

    public async Task<bool> DeleteUser(string passportNumber)
    {
        if(!ValidatePassportNumber(passportNumber))
        {
            return false;
        }

        return await _persistenceService.DeleteUser(passportNumber);
    }

    public static bool ValidateAirport(Airport airport)
    {
        return airport != Airport.Undefined;
    }
    
    public static bool ValidatePassportNumber(string passportNumber)
    {
        var normalizedPassport = passportNumber.Trim();
        if (!normalizedPassport.StartsWith("P") && !normalizedPassport.StartsWith("L"))
        {
            return false;
        }
        var secondChar = normalizedPassport[1];
        if (!char.IsLetter(secondChar))
        {
            return false;
        }
        var remainingChars = normalizedPassport[2..];
        return remainingChars.Length == 7 && remainingChars.All(char.IsDigit);
    }
}