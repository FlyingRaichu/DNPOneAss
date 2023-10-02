using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class SubForumController : ControllerBase
{
    private readonly ISubForumLogic subForumLogic;

    public SubForumController(ISubForumLogic subForumLogic)
    {
        this.subForumLogic = subForumLogic;
    }

    [HttpPost]
    public async Task<ActionResult<SubForum>> CreateAsync([FromBody] SubForumCreationDto dto)
    {
        try
        {
            SubForum created = await subForumLogic.CreateAsync(dto);
            return Created($"/subForums/{created.Id}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SubForum>>> GetAsync([FromQuery] string? title,
        [FromQuery] string? description, [FromQuery] string? owner)
    {
        try
        {
            SearchSubForumParameters parameters = new(title, owner, description);
            var subForums = await subForumLogic.GetAsync(parameters);
            return Ok(subForums);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch]
    public async Task<ActionResult<SubForum>> UpdateAsync(SubForumUpdateDto dto)
    {
        try
        {
            await subForumLogic.UpdateAsync(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<SubForum>> DeleteAsync([FromRoute] int id)
    {
        try
        {
            await subForumLogic.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}