using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class SubForumEfcDao : ISubForumDao
{
    private readonly Context context;

    public SubForumEfcDao(Context context)
    {
        this.context = context;
    }

    public async Task<SubForum> CreateAsync(SubForum subForum)
    {
        EntityEntry<SubForum> newSubForum = await context.SubForums.AddAsync(subForum);
        await context.SaveChangesAsync();
        return newSubForum.Entity;
    }

    public async Task<IEnumerable<SubForum>> GetAsync(SearchSubForumParameters searchParameters)
    {
        IQueryable<SubForum> subForumsQuery = context.SubForums.AsQueryable();
        if (!string.IsNullOrEmpty(searchParameters.Title))
        {
            subForumsQuery = subForumsQuery.Where(sForum =>
                sForum.Title.Equals(searchParameters.Title, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(searchParameters.Description))
        {
            subForumsQuery = subForumsQuery.Where(sForum =>
                sForum.Description.Contains(searchParameters.Description, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(searchParameters.Owner))
        {
            subForumsQuery = subForumsQuery.Where(sForum =>
                sForum.Owner.UserName.Equals(searchParameters.Owner, StringComparison.OrdinalIgnoreCase));
        }

        IEnumerable<SubForum> result = await subForumsQuery.ToListAsync();
        return result;
    }

    public async Task UpdateAsync(SubForum subForum)
    {
        SubForum? existing = GetByIdAsync(subForum.Id).Result;

        if (existing != null)
        {
            context.SubForums.Remove(existing);
            await context.SubForums.AddAsync(subForum);
        }
        else
        {
            throw new Exception("SubForum not found.");
        }
    }

    public async Task<SubForum?> GetByIdAsync(int dtoId)
    {
        SubForum? subForum = await context.SubForums.FindAsync(dtoId);
        if (subForum == null)
        {
            throw new Exception("SubForum not found");
        }

        return subForum;
    }

    public async Task DeleteAsync(int id)
    {
        SubForum? temp = GetByIdAsync(id).Result;
        if (temp != null)
        {
            context.SubForums.Remove(temp);
            await context.SaveChangesAsync();
        }
        
    }
}