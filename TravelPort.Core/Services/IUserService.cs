using TravelPort.Core.Enums;
using TravelPort.Core.Models;

namespace TravelPort.Core.Services;

public interface IUserService
{
    Task<bool> CreateUser(
        string passportNumber, 
        Airport airport, 
        string name,
        string surname,
        string? email,
        string? address,
        string? phoneNumber);
    
    Task<bool> UpdateUser(
        string passportNumber, 
        Airport airport, 
        string name,
        string surname,
        string? email,
        string? address,
        string? phoneNumber);
    
    Task<User?> GetUserByPassportNumber(string passportNumber);
    
    Task<bool> DeleteUser(string passportNumber);
}