using System.Net.Http.Json;
using System.Text.Json;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class UserHttpClient : IUserService
{
    private readonly HttpClient client;

    public UserHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task<User> CreateAsync(UserCreationDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/users", dto);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        User user = JsonSerializer.Deserialize<User>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return user;
    }

    public async Task<Boolean> AuthenticateAsync(string userName, string password)
    {
        string uri = "/users";

        HttpResponseMessage message = await client.GetAsync(userName);
        string res = await message.Content.ReadAsStringAsync();
        if (!message.IsSuccessStatusCode)
        {
            throw new Exception(res);
        }

        User? user =
            JsonSerializer.Deserialize<User>(res, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        if (user == null)
        {
            throw new Exception("Username/Password is not correct");
        }

        if (user.Password != password || user.Password == null)
        {
            throw new Exception("Username/Password is not correct");
        }

        return true;
        //TODO Might be useless;
    }
}