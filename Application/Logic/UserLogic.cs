using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using FileData.DAOs;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDao userDao;
    
    public UserLogic(IUserDao userDao)
    {
        this.userDao = userDao;
    }
    
    public async Task<User> CreateAsync(UserCreationDto dto)
    {
        //checks if the username exists (it's a unique value)
        User? existing = await userDao.GetByUsernameAsync(dto.UserName);
        if (existing != null)
            throw new Exception("Username already taken!");

        ValidateUser(dto);
        User userToCreate = new User(dto.UserName, dto.Password, dto.Email);
        User created = await userDao.CreateAsync(userToCreate);

        return created;
    }

    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameter)
    {
        return userDao.GetAsync(searchParameter);
    }

    public async Task UpdateAsync(UserUpdateDto user)
    {
        User? existing = await userDao.GetByIdAsync(user.Id);
        if (existing == null)
        {
            throw new Exception($"User with ID {user.Id} not found");
        }

        string userToBe = user.UserName ?? existing.UserName;
        string password = user.Password ?? existing.Password;
        string email = user.Email ?? existing.Email;

        User updated = new(userToBe, password, email)
        {
            Id = existing.Id,
        };
        ValidateEdit(updated);

        await userDao.UpdateAsync(updated);

    }

    private void ValidateEdit(User user)
    {
        if(string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password)) 
            throw new Exception("The user has invalid information (null)");
        // other validation stuff
    }

    private void ValidateUser(UserCreationDto dto)
    {
        string userName = dto.UserName;
        if (userName.Length < 3)
            throw new Exception("Username must be at least 3 characters!");

        if (userName.Length > 20)
            throw new Exception("Username must be less than 20 characters!");
    }
}