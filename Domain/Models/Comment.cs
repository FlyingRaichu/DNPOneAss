namespace Domain.Models;

public class Comment
{
    public int Id { get; set; }
    public User Owner { get; }
    public Post Parent { get; }
    public string Content { get; }
    public int Upvotes { get; set; }
}