using Domain.DTOs;
using Domain.Models;

namespace Application.DaoInterfaces;

public interface IUserDao
{
    Task<User> CreateAsync(UserCreationDto dto);
    Task<User?> GetByIdAsync(int id);
}