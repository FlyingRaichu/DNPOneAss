using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface ICommentLogic
{
    Task<Comment> CreateAsync(CommentCreationDto dto);
    Task<IEnumerable<Comment>> GetAsync(SearchCommentParametersDto searchParameters);
    Task UpdateAsync(CommentUpdateDto dto);
    Task DeleteAsync(int id);
}