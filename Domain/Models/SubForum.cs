namespace Domain.Models;

public class SubForum
{
    public int id { get; set; }
    public string Title { get; set; }
    public User Owner { get;}

    public SubForum(User owner, string title)
    {
        Owner = owner;
        Title = title;
    }
}