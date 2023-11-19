using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class SubForum
{
    [Key]
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public User Owner { get; set; }

    public SubForum(User owner, string title, string description)
    {
        Owner = owner;
        Title = title;
        Description = description;
    }

    private SubForum()
    {
    }
}