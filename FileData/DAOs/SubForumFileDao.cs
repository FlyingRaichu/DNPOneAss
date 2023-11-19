// using Application.DaoInterfaces;
// using Domain.DTOs;
// using Domain.Models;
//
// namespace FileData.DAOs;
//
// public class SubForumFileDao : ISubForumDao
// {
//     private readonly FileContext context;
//
//     public SubForumFileDao(FileContext context)
//     {
//         this.context = context;
//     }
//
//     public Task<SubForum> CreateAsync(SubForum subForum)
//     {
//         var subForumId = 1;
//
//         if (context.SubForums.Any())
//         {
//             subForumId = context.SubForums.Max(sForum => sForum.Id);
//             subForumId++;
//         }
//
//         subForum.Id = subForumId;
//
//         context.SubForums.Add(subForum);
//         context.SaveData();
//
//         return Task.FromResult(subForum);
//     }
//
//     public Task<IEnumerable<SubForum>> GetAsync(SearchSubForumParameters searchParameters)
//     {
//         IEnumerable<SubForum> subForums = context.SubForums.AsEnumerable();
//
//         if (!string.IsNullOrEmpty(searchParameters.Title))
//         {
//             subForums = subForums.Where(sForum =>
//                 sForum.Title.Equals(searchParameters.Title, StringComparison.OrdinalIgnoreCase));
//         }
//
//         if (!string.IsNullOrEmpty(searchParameters.Description))
//         {
//             subForums = subForums.Where(sForum =>
//                 sForum.Description.Contains(searchParameters.Description, StringComparison.OrdinalIgnoreCase));
//         }
//
//         if (!string.IsNullOrEmpty(searchParameters.Owner))
//         {
//             subForums = subForums.Where(sForum =>
//                 sForum.Owner.UserName.Equals(searchParameters.Owner, StringComparison.OrdinalIgnoreCase));
//         }
//
//         return Task.FromResult(subForums);
//     }
//
//     public Task UpdateAsync(SubForum subForum)
//     {
//         SubForum? existing = context.SubForums.FirstOrDefault(sForum => sForum.Id == subForum.Id);
//         if (existing == null)
//         {
//             throw new Exception($"Sub forum with ID {subForum.Id} does not exist.");
//         }
//
//         context.SubForums.Remove(existing);
//         context.SubForums.Add(subForum);
//
//         context.SaveData();
//
//         return Task.CompletedTask;
//     }
//
//     public Task<SubForum?> GetByIdAsync(int dtoId)
//     {
//         SubForum? subForum = context.SubForums.FirstOrDefault(sForum => sForum.Id == dtoId);
//
//         return Task.FromResult<SubForum>(subForum);
//     }
//
//     public Task DeleteAsync(int id)
//     {
//         SubForum? existing = context.SubForums.FirstOrDefault(sForum => sForum.Id == id);
//
//         if (existing == null)
//         {
//             throw new Exception($"Sub forum with ID {id} does not exist.");
//         }
//
//         context.SubForums.Remove(existing);
//         context.SaveData();
//
//         return Task.CompletedTask;
//     }
// }