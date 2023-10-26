using System.Net.Http.Json;
using System.Text.Json;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class CommentHttpClient : ICommentService
{
    private readonly HttpClient client;

    public CommentHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task<ICollection<Comment>> GetAsync(string? owner, string? parent, string? message, int? upvotes)
    {
        HttpResponseMessage response = await client.GetAsync("/comments");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Comment> comments = JsonSerializer.Deserialize<ICollection<Comment>>(content,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        return comments;
    }

    public async Task<Comment> CreateAsync(CommentCreationDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/comments", dto);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        Comment comment = JsonSerializer.Deserialize<Comment>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return comment;
    }
}