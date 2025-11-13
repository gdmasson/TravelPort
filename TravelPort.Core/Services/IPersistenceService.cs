using TravelPort.Core.Models;

namespace TravelPort.Core.Services;

public interface IPersistenceService
{
    Task<bool> CreateUser(User user);
    Task<bool> UpdateUser(User user);
    Task<User?> GetUserByPassportNumber(string passportNumber);
    Task<bool> DeleteUser(string passportNumber);
}