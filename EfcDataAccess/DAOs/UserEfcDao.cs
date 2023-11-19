using System.Runtime.InteropServices.ComTypes;
using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class UserEfcDao : IUserDao
{
    private readonly Context context;

    public UserEfcDao(Context context)
    {
        this.context = context;
    }
    
    public async Task<User> CreateAsync(User user)
    {
        EntityEntry<User> newUser = await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return newUser.Entity;
    }

    public async Task<User?> GetByUsernameAsync(string dtoUserName)
    {
        User? existing = await context.Users.FirstOrDefaultAsync(u => 
            u.UserName.ToLower().Equals(dtoUserName.ToLower()));
        // if (existing == null)
        // {
        //     throw new Exception("User not found.");
        // }
        return existing;
    }

    public async Task<User?> GetByEmailAsync(string dtoEmail)
    {
        User? existing = await context.Users.FirstOrDefaultAsync(u =>
            u.Email.Equals(dtoEmail));
        // if (existing == null)
        // {
        //     throw new Exception("User's email not found.");
        // }
        return existing;
    }

    public async Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
    {
        IQueryable<User> usersQuery = context.Users.AsQueryable();
        if (searchParameters.UsernameContains != null)
        {
            usersQuery = usersQuery.Where(u => u.UserName.ToLower().Contains(searchParameters.UsernameContains.ToLower()));
        }

        IEnumerable<User> result = await usersQuery.ToListAsync();
        return result;
    }

    public async Task UpdateAsync(User user)
    {
        User? existing = GetByIdAsync(user.Id).Result;

        if (existing != null)
        {
            context.Users.Remove(existing);
            await context.AddAsync(user);
        }
        else
        {
            throw new Exception("User not found.");
        }
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        User? user = await context.Users.FindAsync(id);
        if (user == null)
        {
            throw new Exception("User not found.");
        }
        return user;
    }

    public async Task DeleteAsync(int id)
    {
        User? temp = GetByIdAsync(id).Result;

        if (temp != null)
        {
            context.Users.Remove(temp);
            await context.SaveChangesAsync();
        }
    }

    public async Task<User> ValidateUser(string username, string password)
    {
        User? existingUser = context.Users.FirstOrDefault(u => u.UserName.Equals(username));

        if (existingUser == null)
        {
            throw new Exception("User not found. Please ensure username/password is correct.");
        }

        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("User not found. Please ensure username/password is correct.");
        }

        return existingUser;
    }
}