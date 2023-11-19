namespace Domain.DTOs;

//searching user only by username
public class SearchUserParametersDto
{
    public string? UsernameContains { get; }

    public SearchUserParametersDto(string? usernameContains)
    {
        UsernameContains = usernameContains;
    }
    
}