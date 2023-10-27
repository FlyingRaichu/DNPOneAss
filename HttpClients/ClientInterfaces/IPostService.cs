using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IPostService
{
    Task<Post> GetPostAsync(int postId);
    Task<ICollection<Post>> GetAsync(
        string? description, 
        int? userId, 
        string? titleContains,
        int? parentId
    );
    Task<Post> CreateAsync(PostCreationDto dto);
}