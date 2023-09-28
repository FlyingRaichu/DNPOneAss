namespace Domain.Models;

public class Post
{
    public int Id { get; set; }
    public SubForum Parent { get; }
    public User Owner { get; }
    public string Title { get; }
    public string Content { get; }
    public int Upvotes { get; set; }

    public Post(SubForum parent, User owner, string title, string content)
    {
        Parent = parent;
        Owner = owner;
        Title = title;
        Content = content;
        Upvotes = 0;
    }
}