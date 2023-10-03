namespace Domain.DTOs;

public class PostUpdateDto
{
    public int Id { get; }
    public string? Title { get; set; }
    public int? ParentSubForum { get; set; }
    public string? Content { get; set; }

    public PostUpdateDto(int id)
    {
        Id = id;
    }
}