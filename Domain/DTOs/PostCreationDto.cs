namespace Domain.DTOs;

public class PostCreationDto
{
    public int OwnerId { get; }
    public int SubForumParentId { get; }
    public string Title { get; }
    public string Content { get; }

    public PostCreationDto(int ownerId, int subForumParentId, string title, string content)
    {
        OwnerId = ownerId;
        SubForumParentId = subForumParentId;
        Title = title;
        Content = content;
    }
}