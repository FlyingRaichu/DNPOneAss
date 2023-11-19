using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    Task<User> CreateAsync(UserCreationDto userCreationDto);
    Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameter);
    Task UpdateAsync(UserUpdateDto user);
    Task DeleteAsync(int id);
    Task<User> ValidateUser(UserValidationDto dto);
}