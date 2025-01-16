using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HMLw_WebApi.Models;

namespace HMLw_WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly ProjectContext _context;

    public ProjectsController(ProjectContext context)
    {
        _context = context;
    }

    // Get all projects
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
    {
        return await _context.Projects.Include(p => p.TaskLists)
                                       .ThenInclude(tl => tl.Tasks)
                                       .ToListAsync();
    }

    // Get a single project
    [HttpGet("{projectId}")]
    public async Task<ActionResult<Project>> GetProject(long projectId)
    {
        var project = await _context.Projects.Include(p => p.TaskLists)
                                             .ThenInclude(tl => tl.Tasks)
                                             .FirstOrDefaultAsync(p => p.Id == projectId);

        if (project == null) return NotFound();

        return project;
    }

    // Get task lists for a project
    [HttpGet("{projectId}/tasklists")]
    public async Task<ActionResult<IEnumerable<TaskList>>> GetTaskLists(long projectId)
    {
        var project = await _context.Projects.Include(p => p.TaskLists)
                                             .FirstOrDefaultAsync(p => p.Id == projectId);

        if (project == null) return NotFound();

        return project.TaskLists.ToList();
    }

    // Get tasks for a task list in a project
    [HttpGet("{projectId}/tasklists/{taskListId}/tasks")]
    public async Task<ActionResult<IEnumerable<Models.Task>>> GetTasks(long projectId, long taskListId)
    {
        var taskList = await _context.TaskLists.Include(tl => tl.Tasks)
                                               .FirstOrDefaultAsync(tl => tl.ProjectId == projectId && tl.Id == taskListId);

        if (taskList == null) return NotFound();

        return taskList.Tasks.ToList();
    }

    // Additional actions for creating/updating/deleting...
}
