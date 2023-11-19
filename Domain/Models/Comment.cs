using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;
//Dana
public class Comment
{
    [Key]
    public int Id { get; set; }
    
    //[ForeignKey("Id")]
    public User Owner { get; set; }
    
    //[ForeignKey("Id")]
    public Post Parent { get; set; }
    
    [Column]
    public string Content { get; set; }
    
    [Column]
    public int Upvotes { get; set; }

    public Comment(User owner, Post parent, string content)
    {
        Owner = owner;
        Parent = parent;
        Content = content;
        Upvotes = 0;
    }

    private Comment()
    {
        
    }
}