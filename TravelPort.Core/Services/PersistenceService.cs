using Microsoft.EntityFrameworkCore;
using TravelPort.Core.Models;

namespace TravelPort.Core.Services;

public class PersistenceService : IPersistenceService
{
    private readonly PgContext _context;

    public PersistenceService(PgContext context)
    {
        _context = context;
    }  
    
    public async Task<bool> CreateUser(User user)
    {
        if (await _context.Users.AnyAsync(u => u.PassportNumber == user.PassportNumber))
        {
            return false;
        }
        
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateUser(User user)
    {
        var userEntity = await _context.Users.AsTracking()
            .FirstOrDefaultAsync(u => u.PassportNumber == user.PassportNumber);
        if (userEntity == null)
        {
            return false;
        }
        
        userEntity.SelectedAirport = user.SelectedAirport;
        userEntity.Name = user.Name;
        userEntity.Surname = user.Surname;
        userEntity.Email = user.Email;
        userEntity.Address = user.Address;
        userEntity.PhoneNumber = user.PhoneNumber;
        
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<User?> GetUserByPassportNumber(string passportNumber)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.PassportNumber == passportNumber);
    }

    public async Task<bool> DeleteUser(string passportNumber)
    {
        var user = await _context.Users.AsTracking().FirstOrDefaultAsync(u => u.PassportNumber == passportNumber);
        if (user == null)
        {
            return false;
        }
        
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }
}