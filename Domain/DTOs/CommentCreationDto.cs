namespace Domain.DTOs;

public class CommentCreationDto
{
    
    public int OwnerId { get; }
    public int PostId { get; }
    public string Content { get;}

    public CommentCreationDto(int ownerId, int postId, string content)
    {
        OwnerId = ownerId;
        PostId = postId;
        Content = content;
    }
}