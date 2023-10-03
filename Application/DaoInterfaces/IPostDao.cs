using Domain.DTOs;
using Domain.Models;

namespace Application.DaoInterfaces;

public interface IPostDao
{
    Task<Post> CreateAsync(Post post);
    Task<IEnumerable<Post>> GetAsync(SearchPostParameters parameters);
    Task UpdateAsync(Post post);
    Task<Post?> GetByIdAsync(int dtoId);
    Task DeleteAsync(int id);
}