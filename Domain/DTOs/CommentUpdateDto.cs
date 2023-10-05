namespace Domain.DTOs;

public class CommentUpdateDto
{
    public int Id { get; }
    public int OwnerId { get; }
    public int PostId { get; }
    public string? Content { get;}

    public CommentUpdateDto(int id, int ownerId, int postId, string content)
    {
        Id = id;
        OwnerId = ownerId;
        PostId = postId;
        Content = content;
    }
}