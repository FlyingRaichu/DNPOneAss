namespace Domain.DTOs;

public class SearchSubForumParameters
{
    public string? Title { get; }
    public string? Owner { get; }
    public string? Description { get; }

    public SearchSubForumParameters(string? title, string? owner, string? description)
    {
        Title = title;
        Owner = owner;
        Description = description;
    }
}