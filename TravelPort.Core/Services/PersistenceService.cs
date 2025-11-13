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
        if (await _context.Users.FirstOrDefaultAsync(u => u.PassportNumber == user.PassportNumber) == null)
        {
            return false;
        }

        await _context.Users
            .Where(u => u.PassportNumber == user.PassportNumber)
            .ExecuteUpdateAsync(u => u
                .SetProperty(p => p.SelectedAirport, user.SelectedAirport)
                .SetProperty(p => p.Name, user.Name)
                .SetProperty(p => p.Surname, user.Surname)
                .SetProperty(p => p.Email, user.Email)
                .SetProperty(p => p.Address, user.Address)
                .SetProperty(p => p.PhoneNumber, user.PhoneNumber));
        return true;
    }

    public async Task<User?> GetUserByPassportNumber(string passportNumber)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.PassportNumber == passportNumber);
    }

    public async Task<bool> DeleteUser(string passportNumber)
    {
        if (await _context.Users.FirstOrDefaultAsync(u => u.PassportNumber == passportNumber) == null)
        {
            return false;
        }

        await _context.Users
            .Where(u => u.PassportNumber == passportNumber)
            .ExecuteDeleteAsync();
        return true;
    }
}