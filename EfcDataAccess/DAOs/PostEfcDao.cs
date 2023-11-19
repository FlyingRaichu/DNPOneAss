using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class PostEfcDao : IPostDao
{
    private readonly Context context;

    public PostEfcDao(Context context)
    {
        this.context = context;
    }
    
    public async Task<Post> CreateAsync(Post post)
    {
        EntityEntry<Post> newPost = await context.Posts.AddAsync(post);
        await context.SaveChangesAsync();
        return newPost.Entity;
    }

    public async Task<IEnumerable<Post>> GetAsync(SearchPostParameters parameters)
    {
        IQueryable<Post> postQuery = context.Posts.AsQueryable();
        
        if (!string.IsNullOrEmpty(parameters.Title))
        {
            postQuery = postQuery.Where(fPost => fPost.Title.Contains(parameters.Title, StringComparison.OrdinalIgnoreCase));
        }

        if (parameters.ParentSubForum != null)
        {
            postQuery = postQuery.Where(fPost => fPost.Parent.Id == parameters.ParentSubForum);
        }

        if (!string.IsNullOrEmpty(parameters.Owner))
        {
            postQuery = postQuery.Where(fPost => EF.Functions.Like(fPost.Owner.UserName, $"%{parameters.Owner}%"));
        }

        if (!string.IsNullOrEmpty(parameters.Content))
        {
            postQuery = postQuery.Where(fPost => fPost.Content.Contains(parameters.Content, StringComparison.OrdinalIgnoreCase));
        }

        if (parameters.Upvotes != null)
        {
            postQuery = postQuery.Where(fPost => fPost.Upvotes == parameters.Upvotes);
        }
        
        IEnumerable<Post> result = await postQuery.ToListAsync();
        return result;
    }

    public async Task UpdateAsync(Post post)
    {
        Post? existing = GetByIdAsync(post.Id).Result;

        if (existing != null)
        {
            context.Posts.Remove(existing);
            await context.AddAsync(post);
        }
        else
        {
            throw new Exception("Post not found.");
        }
    }

    public async Task<Post?> GetByIdAsync(int dtoId)
    {
        Post? post = await context.Posts.FindAsync(dtoId);
        if (post == null)
        {
            throw new Exception("Post not found.");
        }
        return post;
    }

    public async Task DeleteAsync(int id)
    {
        Post? temp = GetByIdAsync(id).Result;

        if (temp != null)
        {
            context.Posts.Remove(temp);
            await context.SaveChangesAsync();
        }
    }
}