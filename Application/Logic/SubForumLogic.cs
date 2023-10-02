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

        ValidateSubForum(dto);
        SubForum subForum = new SubForum(user, dto.Title, dto.Description);
        SubForum created = await subForumDao.CreateAsync(subForum);
        return created;
    }

    public Task<IEnumerable<SubForum>> GetAsync(SearchSubForumParameters searchParameters)
    {
        return subForumDao.GetAsync(searchParameters);
    }

    public async Task UpdateAsync(SubForumUpdateDto dto)
    {
        SubForum? existing = await subForumDao.getByIdAsync(dto.Id);

        if (existing == null)
        {
            throw new Exception($"Sub forum with ID {dto.Id} not found.");
        }

        User? user = null;

        if (dto.OwnerId != null)
        {
            user = await userDao.GetByIdAsync((int)dto.OwnerId);
            if (user == null)
            {
                throw new Exception($"User with ID {dto.OwnerId} not found.");
            }
        }

        User userToUse = user ?? existing.Owner;
        string titleToUse = dto.Title ?? existing.Title;
        string descriptionToUse = dto.Description ?? existing.Description;

        SubForum updated = new(userToUse, titleToUse, descriptionToUse)
        {
            Id = existing.Id
        };
        
        ValidateSubForum(updated);

        await subForumDao.UpdateAsync(updated);
    }

    public async Task DeleteAsync(int id)
    {
        SubForum? subForum = await subForumDao.getByIdAsync(id);

        if (subForum == null)
        {
            throw new Exception($"Sub forum with ID {id} does not exist");
        }

        await subForumDao.DeleteAsync(id);
    }

    private void ValidateSubForum(SubForumCreationDto dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
    }

    private void ValidateSubForum(SubForum subForum)
    {
        if (string.IsNullOrEmpty(subForum.Title)) throw new Exception("Title cannot be empty.");
    }
}