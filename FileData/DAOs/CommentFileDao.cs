// using Application.DaoInterfaces;
// using Application.LogicInterfaces;
// using Domain.DTOs;
// using Domain.Models;
//
// namespace FileData.DAOs;
//
// public class CommentFileDao : ICommentDao
// {
//     private readonly FileContext context;
//
//     public CommentFileDao(FileContext context)
//     {
//         this.context = context;
//     }
//     
//     public Task<Comment> CreateAsync(Comment comment)
//     {
//         int id = 1;
//         if (context.Comments.Any())
//         {
//             id = context.Comments.Max(t => t.Id);
//             id++;
//         }
//
//         comment.Id = id;
//         
//         context.Comments.Add(comment);
//         context.SaveData();
//
//         return Task.FromResult(comment);
//     }
//
//     public Task<IEnumerable<Comment>> GetAsync(SearchCommentParametersDto searchParameters)
//     {
//         IEnumerable<Comment> result = context.Comments.AsEnumerable();
//
//         if (searchParameters.Owner != null)
//         {
//             result = result.Where(comment => comment.Owner.Id == searchParameters.Owner);
//         }
//
//         if (searchParameters.Parent != null)
//         {
//             result = result.Where(comment =>
//                 comment.Parent.Id == searchParameters.Parent);
//         }
//
//         if (searchParameters.Content != null)
//         {
//             result = result.Where(comment => comment.Content.Contains(searchParameters.Content));
//         }
//
//         if (searchParameters.Upvotes != null)
//         {
//             result = result.Where(comment =>
//                 comment.Upvotes == searchParameters.Upvotes);
//         }
//
//         return Task.FromResult(result);
//     }
//
//     public Task UpdateAsync(Comment comment)
//     {
//         Comment? existing = context.Comments.FirstOrDefault(c => c.Id == comment.Id);
//         if (existing == null)
//         {
//             throw new Exception($"Comment with id {comment.Id} does not exist!");
//         }
//
//         context.Comments.Remove(existing);
//         context.Comments.Add(comment);
//     
//         context.SaveData();
//     
//         return Task.CompletedTask;
//     }
//     
//     public Task<Comment> GetByIdAsync(int id)
//     {
//         Comment? existing = context.Comments.FirstOrDefault(c => c.Id == id);
//         return Task.FromResult(existing);
//     }
//
//
//     public Task DeleteAsync(int id)
//     {
//         Comment? existing = context.Comments.FirstOrDefault(comment => comment.Id == id);
//         if (existing == null)
//         {
//             throw new Exception($"Comments with id {id} does not exist!");
//         }
//
//         context.Comments.Remove(existing); 
//         context.SaveData();
//     
//         return Task.CompletedTask;
//     }
// }