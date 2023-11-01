using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface ICommentService
{
    Task<ICollection<Comment>> GetAsync(int? owner, int? parent, string? message, int? upvotes, string? token);

    Task<Comment> CreateAsync(CommentCreationDto dto, string? token);
}