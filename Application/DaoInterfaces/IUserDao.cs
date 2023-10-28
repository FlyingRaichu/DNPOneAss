using Domain.DTOs;
using Domain.Models;
using FileData.DAOs;

namespace Application.DaoInterfaces;

public interface IUserDao
{
    Task<User> CreateAsync(User user);
    Task<User?> GetByUsernameAsync(string dtoUserName);
    Task<User?> GetByEmailAsync(string dtoUserName);
    Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters);
    Task UpdateAsync(User user);
    Task<User?> GetByIdAsync(int id);
    Task DeleteAsync(int id);
    Task<User> ValidateUser(string username, string password);
    
}