using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IUserService
{
    Task<User> CreateAsync(UserCreationDto dto);

    Task<Boolean> AuthenticateAsync(string userName, string password);
}