using Domain.DTOs;
using Domain.Models;

namespace Application.DaoInterfaces;

public interface ISubForumDao
{
    Task<SubForum> CreateAsync(SubForum subForum);
    Task<IEnumerable<SubForum>> GetAsync(SearchSubForumParameters searchParameters);
    Task UpdateAsync(SubForum subForum);
    Task<SubForum?> getByIdAsync(int dtoId);
    Task DeleteAsync(int id);
}