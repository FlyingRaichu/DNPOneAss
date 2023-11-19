// using Application.DaoInterfaces;
// using Domain.DTOs;
// using Domain.Models;
//
// namespace FileData.DAOs;
//
// public class PostFileDao : IPostDao
// {
//     private readonly FileContext context;
//
//     public PostFileDao(FileContext context)
//     {
//         this.context = context;
//     }
//
//     public Task<Post> CreateAsync(Post post)
//     {
//         var postId = 1;
//
//         if (context.Posts.Any())
//         {
//             postId = context.Posts.Max(Post => Post.Id);
//             postId++;
//         }
//
//         post.Id = postId;
//         context.Posts.Add(post);
//         context.SaveData();
//
//         return Task.FromResult(post);
//     }
//
//     public Task<IEnumerable<Post>> GetAsync(SearchPostParameters parameters)
//     {
//         IEnumerable<Post> posts = context.Posts.AsEnumerable();
//
//         if (!string.IsNullOrEmpty(parameters.Title))
//         {
//             posts = posts.Where(fPost => fPost.Title.Contains(parameters.Title, StringComparison.OrdinalIgnoreCase));
//         }
//         
//         if (parameters.ParentSubForum != null)
//         {
//             posts = posts.Where(fPost =>
//                 fPost.Parent.Id == parameters.ParentSubForum);
//         }
//
//         if (!string.IsNullOrEmpty(parameters.Owner))
//         {
//             posts = posts.Where(fPost =>
//                 fPost.Owner.UserName.Contains(parameters.Owner, StringComparison.OrdinalIgnoreCase));
//         }
//
//         if (!string.IsNullOrEmpty(parameters.Content))
//         {
//             posts = posts.Where(fPost =>
//                 fPost.Content.Contains(parameters.Content, StringComparison.OrdinalIgnoreCase));
//         }
//
//         if (parameters.Upvotes != null)
//         {
//             posts = posts.Where(fPost => fPost.Upvotes == parameters.Upvotes);
//         }
//
//         return Task.FromResult(posts);
//     }
//
//     public Task UpdateAsync(Post post)
//     {
//         Post? existing = context.Posts.FirstOrDefault(fPost => fPost.Id == post.Id);
//
//         if (existing == null)
//         {
//             throw new Exception($"Post with ID {post.Id} does not exist.");
//         }
//
//         context.Posts.Remove(existing);
//         context.Posts.Add(post);
//         context.SaveData();
//
//
//         return Task.CompletedTask;
//     }
//
//     public Task<Post?> GetByIdAsync(int dtoId)
//     {
//         Post? post = context.Posts.FirstOrDefault(fPost => fPost.Id == dtoId);
//
//         return Task.FromResult<Post>(post);
//     }
//
//     public Task DeleteAsync(int id)
//     {
//         Post? post = context.Posts.FirstOrDefault(fPost => fPost.Id == id);
//
//         if (post == null)
//         {
//             throw new Exception($"Post with ID {id} not found.");
//         }
//         context.Posts.Remove(post);
//         context.SaveData();
//         
//         return Task.CompletedTask;
//     }
// }