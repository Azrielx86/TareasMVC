using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TareasMVC.Entities;
using TareasMVC.Services;

namespace TareasMVC.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController(ApplicationDbContext context, IServiceUsers serviceUsers, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Entities.Task>> Post([FromBody] string title)
    {
        var userId = serviceUsers.GetUserId();
        var tasksExists = await context.Tasks.AnyAsync(t => t.UserCreationId == userId);

        var ordenMayor = 0;

        if (tasksExists)
        {
            ordenMayor = await context.Tasks.Where(t => t.UserCreationId == userId)
                .Select(t => t.Order)
                .MaxAsync();
        }

        var task = new Entities.Task
        {
            Title = title,
            UserCreationId = userId,
            CreationDate = DateTime.UtcNow,
            Order = ordenMayor + 1,
        };

        context.Add(task);
        await context.SaveChangesAsync();

        return Ok(task);
    }

    /// <summary>
    /// This function uses Anonymous Types with IActionResult.
    /// </summary>
    /// <returns>List with all the tasks</returns>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var userId = serviceUsers.GetUserId();
        var tasks = await context.Tasks
            .Where(t => t.UserCreationId == userId)
            .OrderBy(t => t.Order)
            .ProjectTo<TaskDto>(mapper.ConfigurationProvider)
            .ToListAsync();
        return Ok(tasks);
    }
}