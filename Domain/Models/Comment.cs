namespace Domain.Models;
//Dana
public class Comment
{
    public int Id { get; set; }
    public User Owner { get; }
    public Post Parent { get; }
    public string Content { get; }
    public int Upvotes { get; set; }

    public Comment(User owner, Post parent, string content)
    {
        Owner = owner;
        Parent = parent;
        Content = content;
        Upvotes = 0;
    }
}