using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class CommentLogic: ICommentLogic
{
    private readonly ICommentDao commentDao;
    private readonly IUserDao userDao;
    private readonly IPostDao postDao;

    public CommentLogic(ICommentDao commentDao, IUserDao userDao, IPostDao postDao)
    {
        this.commentDao = commentDao;
        this.userDao = userDao;
        this.postDao = postDao;
    }

    public async Task<Comment> CreateAsync(CommentCreationDto dto)
    {
        User? user = await userDao.GetByIdAsync(dto.OwnerId);
        if (user == null)
        {
            throw new Exception($"Comment user is null");
        }
        Post? post = await postDao.GetByIdAsync(dto.PostId);
        if (post == null)
        {
            throw new Exception($"Comment post is null");
        }
        
        ValidateData(dto);
        Comment comment = new Comment(user, post, dto.Content);
        Comment created = await commentDao.CreateAsync(comment);
        return created;
    }
    
    private void ValidateData(CommentCreationDto dto)
    {
        if (string.IsNullOrEmpty(dto.Content)) throw new Exception("Comment cannot be empty.");
    }

    public Task<IEnumerable<Comment>> GetAsync(SearchCommentParametersDto searchParameters)
    {
        return commentDao.GetAsync(searchParameters);
    }

    public async Task UpdateAsync(CommentUpdateDto dto)
    {
        Comment? existing = await commentDao.GetByIdAsync(dto.Id);
        if (existing == null)
        {
            throw new Exception($"Comment with ID {dto.Id} not found!");
        }
        
        User? user = null;
        if (dto.OwnerId != null)
        {
            user = await userDao.GetByIdAsync((int)dto.OwnerId);
            if (user == null)
            {
                throw new Exception($"User with id {dto.OwnerId} was not found.");
            }
        }

        Post? post = null;
        if (dto.PostId != null)
        {
            post = await postDao.GetByIdAsync((int)dto.PostId);
            if (post == null)
            {
                throw new Exception($"Post with id {dto.PostId} was not found.");
            }
        }

        User userToUse = user ?? existing.Owner;
        Post postToUse = post ?? existing.Parent;
        string contentToUse = dto.Content ?? existing.Content;
        int upvotes = dto.Upvotes ?? existing.Upvotes;

        Comment update = new Comment(userToUse, postToUse, contentToUse)
        {
            Id = existing.Id,
            Upvotes = upvotes
        };
        
        ValidateComment(update);
        await commentDao.UpdateAsync(update);
    }
    
    private void ValidateComment(Comment comment)
    {
        if (string.IsNullOrEmpty(comment.Content)) throw new Exception("Comment cannot be empty. (1)");
    }

    public async Task DeleteAsync(int id)
    {
        Comment? comment = await commentDao.GetByIdAsync(id);
        if (comment == null)
        {
            throw new Exception($"Comment with ID {id} was not found!");
        }
        await commentDao.DeleteAsync(id);
    }
}