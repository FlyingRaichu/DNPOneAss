namespace Domain.DTOs;

public class SubForumUpdateDto
{
    public int Id { get; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int? OwnerId { get; set; }

    public SubForumUpdateDto(int id)
    {
        Id = id;
    }
}