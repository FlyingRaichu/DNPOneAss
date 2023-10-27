namespace Domain.DTOs;

public class SearchPostParameters
{
    public string? Title { get; }
    public int? ParentSubForum { get; }
    public string? Owner { get; }
    public string? Content { get; }
    public int? Upvotes { get; }

    public SearchPostParameters(string? title, int? parentSubForum, string? owner, string? content, int? upvotes)
    {
        Title = title;
        ParentSubForum = parentSubForum;
        Owner = owner;
        Content = content;
        Upvotes = upvotes;
    }
}