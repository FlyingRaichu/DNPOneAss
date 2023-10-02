﻿using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface ISubForumLogic
{
    Task<SubForum> CreateAsync(SubForumCreationDto dto);
    Task<IEnumerable<SubForum>> GetAsync(SearchSubForumParameters searchParameters);
    Task UpdateAsync(SubForumUpdateDto dto);
    Task DeleteAsync(int id);
}