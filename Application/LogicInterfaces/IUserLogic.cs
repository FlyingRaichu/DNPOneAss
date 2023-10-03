using Domain.DTOs;
using Domain.Models;
using FileData.DAOs;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    Task<User> CreateAsync(UserCreationDto userCreationDto);
    Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameter);
}