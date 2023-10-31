using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class CommentsController : ControllerBase
{
    private readonly ICommentLogic commentLogic;



    public CommentsController(ICommentLogic commentLogic)
    {
        this.commentLogic = commentLogic;
    }

    

    [HttpPost]
    public async Task<ActionResult<Comment>> CreateAsync([FromBody] CommentCreationDto dto)
    {
        try
        {
            Comment created = await commentLogic.CreateAsync(dto);
            return Created($"/comment/{created.Id}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Comment>>> GetAsync([FromQuery] int? owner, [FromQuery] int? parent,
        [FromQuery] string? content, [FromQuery] int? upvotes)
    {
        try
        {
            SearchCommentParametersDto parameters = new(owner, parent, content, upvotes);
            var comments = await commentLogic.GetAsync(parameters);
            return Ok(comments);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPatch]
    public async Task<ActionResult> UpdateAsync([FromBody] CommentUpdateDto dto)
    {
        try
        {
            await commentLogic.UpdateAsync(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int id)
    {
        try
        {
            await commentLogic.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}