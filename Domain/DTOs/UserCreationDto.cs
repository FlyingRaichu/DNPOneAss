namespace Domain.DTOs;

public class UserCreationDto
{
    public string UserName { get;}
    public string Password { get;}
    public string Email { get;}
    
    public UserCreationDto(string userName, string password, string email)
    {
        UserName = userName;
        Password = password;
        Email = email;
    }
    
    
}