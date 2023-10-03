namespace Domain.DTOs;

public class UserUpdateDto
{
    public int Id { get;}
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    
    public UserUpdateDto(int id)
    {
        Id = id;
    }
}