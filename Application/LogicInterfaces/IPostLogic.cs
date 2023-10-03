using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IPostLogic
{
    Task<Post> CreateAsync(PostCreationDto dto);
    Task<IEnumerable<Post>> GetAsync(SearchPostParameters parameters);
    Task UpdateAsync(PostUpdateDto dto);
    Task DeleteAsync(int id);
}