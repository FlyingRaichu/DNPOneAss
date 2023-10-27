using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface ISubForumService
{
    Task<SubForum> CreateAsync(SubForumCreationDto dto);
    Task<ICollection<SubForum>> GetAsync(
        string? titleContains,
        string? owner,
        string? descriptionContains);
    Task UpdateAsync(SubForumUpdateDto dto);
    Task<SubForum> GetByIdAsync(int id);
    Task DeleteAsync(int id);

}