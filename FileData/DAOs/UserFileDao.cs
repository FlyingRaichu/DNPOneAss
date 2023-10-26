using Application.DaoInterfaces;
using Domain.Models;

namespace FileData.DAOs;

public class UserFileDao : IUserDao
{
    private readonly FileContext context;

    public UserFileDao(FileContext context)
    {
        this.context = context;
    }

    public Task<User> CreateAsync(User user)
    {
        int userId = 1;
        if (context.Users.Any())
        {
            userId = context.Users.Max(u => u.Id);
            userId++;
        }

        user.Id = userId;
        
        context.Users.Add(user);
        context.SaveData();

        return Task.FromResult(user);
    }

    public Task UpdateAsync(User user)
    {
        User? existing = context.Users.FirstOrDefault(u => u.Id == user.Id);
        if (existing == null)
        {
            throw new Exception($"User with ID {user.Id} does not exist");
        }

        context.Users.Remove(existing);
        context.Users.Add(user);
        
        context.SaveData();

        return Task.CompletedTask;

    }

    public Task<User?> GetByIdAsync(int id)
    {
        User? existing = context.Users.FirstOrDefault(u => u.Id == id);
        return Task.FromResult(existing);
    }

    public Task DeleteAsync(int id)
    {
        User? existing = context.Users.FirstOrDefault(u => u.Id == id);
        if (existing == null)
        {
            throw new Exception($"Todo with id {id} does not exist!");
        }

        context.Users.Remove(existing);
        context.SaveData();
        return Task.CompletedTask;
    }


    public Task<User?> GetByUsernameAsync(string userName)
    {
        User? existing = context.Users.FirstOrDefault(u => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(existing);
    }
    
    public Task<User?> GetByEmailAsync(string email)
    {
        User? existing = context.Users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(existing);
    }

    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
    {
        IEnumerable<User> users = context.Users.AsEnumerable();
        if (searchParameters.UsernameContains != null)
        {
            users = context.Users.Where(u =>
                u.UserName.Contains(searchParameters.UsernameContains, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(users);
    }
    
    
}