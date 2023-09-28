using Domain.DTOs;
using Domain.Models;

namespace Application.DaoInterfaces;

public interface ISubForumDao
{
    Task<SubForum> CreateAsync(SubForum dto);
}