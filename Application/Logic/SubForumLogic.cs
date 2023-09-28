using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class SubForumLogic : ISubForumLogic
{
    private readonly ISubForumDao subForumDao;
    private readonly IUserDao userDao;

    public SubForumLogic(ISubForumDao subForumDao, IUserDao userDao)
    {
        this.subForumDao = subForumDao;
        this.userDao = userDao;
    }
    public async Task<SubForum> CreateAsync(SubForumCreationDto dto)
    {
        User? user = await userDao.GetByIdAsync(dto.OwnerId);
        if (user == null)
        {
            throw new Exception($"User with id {dto.OwnerId} was not found.");
        }

        validateSubForum(dto);
        SubForum subForum = new SubForum(user, dto.Title);
        SubForum created = await subForumDao.CreateAsync(subForum);
        return created;
    }

    private void validateSubForum(SubForumCreationDto dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
    }
}