namespace Domain.Models;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public DateTime CakeDay { get; }

    public User(string userName, string password, string email)
    {
        UserName = userName;
        Password = password;
        Email = email;
        CakeDay = DateTime.Today.Date;
    }
}