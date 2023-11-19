namespace Domain.DTOs;

public class SearchCommentParametersDto
{
    public int? Owner { get; }
    public int? Parent { get; }
    public string? Content { get;}
    public int? Upvotes { get;}

    public SearchCommentParametersDto(int? owner, int? parent, string? content, int? upvotes)
    {
        Owner = owner;
        Parent = parent;
        Content = content;
        Upvotes = upvotes;
    }
}