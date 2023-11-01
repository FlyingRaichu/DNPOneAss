using System.Net.Http.Json;
using System.Text;
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

    public async Task<ICollection<Comment>> GetAsync(int? owner, int? parent, string? message, int? upvotes, string? token)
    {
        string query = ConstructQuery(owner, parent, message, upvotes);
        var request = new HttpRequestMessage(HttpMethod.Get, "/comments" + query);
        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Add("Authorization", "Bearer " + token);
        }

        HttpResponseMessage response = await client.SendAsync(request);
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine(request);
            Console.WriteLine(response);
            throw new Exception(content);
        }

        ICollection<Comment> comments = JsonSerializer.Deserialize<ICollection<Comment>>(content,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        return comments;
    }
    
    private static string ConstructQuery(int? owner, int? parent, string? message, int? upvotes)
    {
        string query = "";
        if (owner != null)
        {
            query += $"?owner={owner}";
        }

        if (parent != null)
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"parent={parent}";
        }

        if (message != null)
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"content={message}";
        }

        if (upvotes != null)
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"upvotes={upvotes}";
        }

        return query;
    }

    public async Task<Comment> CreateAsync(CommentCreationDto dto, string? token)
    {
        string jsonDto = JsonSerializer.Serialize(dto);
        StringContent requestContent = new StringContent(jsonDto, Encoding.UTF8, "application/json");
        
        var request = new HttpRequestMessage(HttpMethod.Post, "/comments");
        request.Content = requestContent;

        if (!string.IsNullOrEmpty(token))
        {
         request.Headers.Add("Authorization", "Bearer " + token);   
        }
        
        HttpResponseMessage response = await client.SendAsync(request);
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