using Domain.DTOs;
using Domain.Models;

namespace Application.DaoInterfaces;

public interface ICommentDao
{
    Task<Comment> CreateAsync(Comment comment);
    Task<IEnumerable<Comment>> GetAsync(SearchCommentParametersDto searchParameters);
    Task UpdateAsync(Comment comment);
    Task<Comment> GetByIdAsync(int id);
    Task DeleteAsync(int id);
}