using System.Text.Json;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class PostHttpClient : IPostService
{
    private readonly HttpClient client;

    public PostHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task<Post> GetPostAsync(int postId)
    {
        HttpResponseMessage response = await client.GetAsync($"/posts/{postId}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        Post post = JsonSerializer.Deserialize<Post>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        return post;
    }

    public async Task<ICollection<Post>> GetAsync(string? description, int? userId,
        string? titleContains, int? parentId)
    {
        string query = ConstructQuery(description, userId, titleContains, parentId); 
        
        HttpResponseMessage response = await client.GetAsync("/posts"+query);
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Post> posts = JsonSerializer.Deserialize<ICollection<Post>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return posts;
    }

    private static string ConstructQuery(string? content, int? userId, string? titleContains, int? parentId)
    {
        //Posts? title=This%20is%20a%20good%20q & parentSubForum=1 & owner=1 & content=L
        string temp = "";
        if (!string.IsNullOrEmpty(titleContains))
        {
            temp += string.IsNullOrEmpty(temp) ? "?" : "&";
            temp += $"title={titleContains}";
        }

        if (parentId != null)
        {
            temp += string.IsNullOrEmpty(temp) ? "?" : "&";
            temp += $"parentSubForum={parentId}";
        }

        if (userId != null)
        {
            temp += string.IsNullOrEmpty(temp) ? "?" : "&";
            temp += $"owner={userId}";
        }

        if (!string.IsNullOrEmpty(content))
        {
            temp += $"?content={content}";
        }

        return temp;
    }
}