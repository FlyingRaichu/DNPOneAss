namespace Domain.DTOs;

public class SubForumCreationDto
{
    public int OwnerId { get; }
    public string Title { get; }
    public string Description { get; }

    public SubForumCreationDto(int ownerId, string title, string description)
    {
        OwnerId = ownerId;
        Title = title;
        Description = description;
    }
}