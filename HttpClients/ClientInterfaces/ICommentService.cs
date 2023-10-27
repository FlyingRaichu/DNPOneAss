using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface ICommentService
{
    Task<ICollection<Comment>> GetAsync(string? owner, string? parent, string? message, int? upvotes);

    Task<Comment> CreateAsync(CommentCreationDto dto);
}