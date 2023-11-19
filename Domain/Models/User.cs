using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;
//Dana
public class User
{
    [Key]
    public int Id { get; set; }
    
    [Column]
    public string UserName { get; set; }
    
    [Column]
    public string Password { get; set; }
    
    [Column]
    public string Email { get; set; }
    
    [Column]
    public DateTime CakeDay { get; set; }

    public User(string userName, string password, string email)
    {
        UserName = userName;
        Password = password;
        Email = email;
        CakeDay = DateTime.Today.Date;
    }

    private User()
    {
        
    }
}