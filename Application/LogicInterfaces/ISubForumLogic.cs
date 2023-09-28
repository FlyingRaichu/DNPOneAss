using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface ISubForumLogic
{
    Task<SubForum> CreateAsync(SubForumCreationDto dto);
}