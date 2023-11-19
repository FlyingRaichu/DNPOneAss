using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class Post
{
    //Lyubo
    [Key]
    public int Id { get; set; }
    
    //[ForeignKey("Id")]
    public SubForum Parent { get; set; }
    
    //[ForeignKey("Id")]
    public User Owner { get; set; }
    
    [Column]
    public string Title { get; set; }
    
    [Column]
    public string Content { get; set; }
    
    [Column]
    public int Upvotes { get; set; }

    public Post(SubForum parent, User owner, string title, string content)
    {
        Parent = parent;
        Owner = owner;
        Title = title;
        Content = content;
        Upvotes = 0;
    }

    private Post()
    {
        
    }
}