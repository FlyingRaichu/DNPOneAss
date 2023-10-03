namespace Domain.DTOs;

public class SearchPostParameters
{
    public string? Title { get; }
    public string? ParentSubForum { get; }
    public string? Owner { get; }
    public string? Content { get; }
    public int? Upvotes { get; }

    public SearchPostParameters(string? title, string? parentSubForum, string? owner, string? content, int? upvotes)
    {
        Title = title;
        ParentSubForum = parentSubForum;
        Owner = owner;
        Content = content;
        Upvotes = upvotes;
    }
}