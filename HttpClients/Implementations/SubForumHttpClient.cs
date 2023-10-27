using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class SubForumHttpClient : ISubForumService
{
    private readonly HttpClient client;

    public SubForumHttpClient(HttpClient client)
    {
        this.client = client;
    }
    
    public async Task<SubForum> CreateAsync(SubForumCreationDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/subForums", dto);
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
        
        SubForum subForum = JsonSerializer.Deserialize<SubForum>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return subForum;
    }

    public async Task<ICollection<SubForum>> GetAsync(string? titleContains, string? owner, string? descriptionContains)
    {
        string query = ConstructQuery(titleContains, owner, descriptionContains);
        HttpResponseMessage response = await client.GetAsync("/subForums" + query);
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<SubForum> subForums = JsonSerializer.Deserialize<ICollection<SubForum>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return subForums;
    }
    
    private static string ConstructQuery(string? titleContains, string? owner, string? descriptionContains)
    {
        string query = "";
        if (!string.IsNullOrEmpty(titleContains))
        {
            query += $"?title={titleContains}";
        }

        if (descriptionContains != null)
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"description={descriptionContains}";
        }

        if (owner != null)
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"owner={owner}";
        }

        return query;
    }

    public async Task UpdateAsync(SubForumUpdateDto dto)
    {
        string dtoAsJson = JsonSerializer.Serialize(dto);
        StringContent body = new StringContent(dtoAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PatchAsync("/subForums", body);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task<SubForum> GetByIdAsync(int id)
    {
        HttpResponseMessage response = await client.GetAsync($"/subForums/{id}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        SubForum subForum = JsonSerializer.Deserialize<SubForum>(content, 
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        )!;
        return subForum;
    }

    public async Task DeleteAsync(int id)
    {
        HttpResponseMessage response = await client.DeleteAsync($"subForums/{id}");
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
}