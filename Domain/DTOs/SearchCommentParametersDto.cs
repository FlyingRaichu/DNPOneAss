using Domain.Models;

namespace Domain.DTOs;

public class SearchCommentParametersDto
{
    public string? Owner { get; }
    public string? Parent { get; }
    public string? Content { get;}
    public int? Upvotes { get;}

    public SearchCommentParametersDto(string? owner, string? parent, string? content, int? upvotes)
    {
        Owner = owner;
        Parent = parent;
        Content = content;
        Upvotes = upvotes;
    }
}