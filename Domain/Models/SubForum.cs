namespace Domain.Models;

public class SubForum
{
    //Lyubo
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public User Owner { get;}

    public SubForum(User owner, string title, String description)
    {
        Owner = owner;
        Title = title;
        Description = description;
    }
}