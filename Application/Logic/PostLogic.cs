using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
    private readonly IPostDao postDao;
    private readonly IUserDao userDao;
    private readonly ISubForumDao subForumDao;

    public PostLogic(IPostDao postDao, IUserDao userDao, ISubForumDao subForumDao)
    {
        this.postDao = postDao;
        this.userDao = userDao;
        this.subForumDao = subForumDao;
    }

    public async Task<Post> CreateAsync(PostCreationDto dto)
    {
        User? user = await userDao.GetByIdAsync(dto.OwnerId);
        if (user == null)
        {
            throw new Exception($"User not found.");
        }

        SubForum? subForum = await subForumDao.getByIdAsync(dto.SubForumParentId);
        if (subForum == null)
        {
            throw new Exception($"Sub forum not found.");
        }

        ValidatePost(dto);
        Post post = new Post(subForum, user, dto.Title, dto.Content);
        Post created = await postDao.CreateAsync(post);
        return created;
    }

    public Task<IEnumerable<Post>> GetAsync(SearchPostParameters parameters)
    {
        return postDao.GetAsync(parameters);
    }

    public async Task UpdateAsync(PostUpdateDto dto)
    {
        Post? existing = await postDao.GetByIdAsync(dto.Id);

        if (existing == null)
        {
            throw new Exception($"Post with ID {dto.Id} not found.");
        }

        SubForum? subForum = null;

        if (dto.ParentSubForum != null)
        {
            subForum = await subForumDao.getByIdAsync((int)dto.ParentSubForum);
            if (subForum == null)
            {
                throw new Exception($"Sub forum with ID {dto.ParentSubForum} not found.");
            }
        }

        SubForum subForumToUse = subForum ?? existing.Parent;
        string titleToUse = dto.Title ?? existing.Title;
        string contentToUse = dto.Content ?? existing.Content;

        Post updated = new(subForumToUse, existing.Owner, titleToUse, contentToUse)
        {
            Id = existing.Id
        };

        ValidatePost(updated);
        await postDao.UpdateAsync(updated);
    }

    public async Task<Post> GetByIdAsync(int id)
    {
        Post? post = await postDao.GetByIdAsync(id);
        if (post == null)
        {
            throw new Exception($"Post with id {id} doesn't exist");
        }

        return post;
    }

    public async Task DeleteAsync(int id)
    {
        Post? post = await postDao.GetByIdAsync(id);

        if (post == null)
        {
            throw new Exception($"Post with ID {id} not found.");
        }

        await postDao.DeleteAsync(id);
    }


    private void ValidatePost(PostCreationDto dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
    }

    private void ValidatePost(Post post)
    {
        if (string.IsNullOrEmpty(post.Title)) throw new Exception("Title cannot be empty.");
    }
}