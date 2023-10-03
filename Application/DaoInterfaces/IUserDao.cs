using Domain.DTOs;
using Domain.Models;
using FileData.DAOs;

namespace Application.DaoInterfaces;

public interface IUserDao
{
    Task<User> CreateAsync(User user);
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByUsernameAsync(string dtoUserName);
    Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters);
}