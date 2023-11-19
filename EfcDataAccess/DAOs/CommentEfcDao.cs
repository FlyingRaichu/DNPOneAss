using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class CommentEfcDao : ICommentDao
{
    private readonly Context context;

    public CommentEfcDao(Context context)
    {
        this.context = context;
    }


    public async Task<Comment> CreateAsync(Comment comment)
    {
        EntityEntry<Comment> newComment = await context.Comments.AddAsync(comment);
        await context.SaveChangesAsync();
        return newComment.Entity;
    }

    public async Task<IEnumerable<Comment>> GetAsync(SearchCommentParametersDto searchParameters)
    {
        IQueryable<Comment> commentsQuery = context.Comments.AsQueryable();
        
        if (searchParameters.Owner != null)
        {
            commentsQuery = commentsQuery.Where(comment => comment.Owner.Id == searchParameters.Owner);
        }

        if (searchParameters.Parent != null)
        {
            commentsQuery = commentsQuery.Where(comment =>
                comment.Parent.Id == searchParameters.Parent);
        }

        if (searchParameters.Content != null)
        {
            commentsQuery = commentsQuery.Where(comment => comment.Content.Contains(searchParameters.Content));
        }

        if (searchParameters.Upvotes != null)
        {
            commentsQuery = commentsQuery.Where(comment =>
                comment.Upvotes == searchParameters.Upvotes);
        }
        
        IEnumerable<Comment> result = await commentsQuery.ToListAsync();
        return result;
    }

    public async Task UpdateAsync(Comment comment)
    {
        Comment? existing = GetByIdAsync(comment.Id).Result;

        if (existing != null)
        {
            context.Comments.Remove(existing);
            await context.AddAsync(comment);
        }
        else
        {
            throw new Exception("Comment not found.");
        }
    }

    public async Task<Comment> GetByIdAsync(int id)
    {
        Comment? comment = await context.Comments.FindAsync(id);
        if (comment == null)
        {
            throw new Exception("Comment not found.");
        }
        return comment;
    }

    public async Task DeleteAsync(int id)
    {
        Comment? temp = GetByIdAsync(id).Result;

        if (temp != null)
        {
            context.Comments.Remove(temp);
            await context.SaveChangesAsync();
        }
    }
}