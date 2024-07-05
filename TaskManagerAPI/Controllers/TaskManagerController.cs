using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.Data;
using TaskManagerAPI.Models;
using TaskManagerAPI.Services;
namespace TaskManagerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskManagerController : ControllerBase
    {
        private TaskManagerApiDbContext _taskManagerApiDbContext;
        public TaskManagerController(TaskManagerApiDbContext dbContext)
        {
            _taskManagerApiDbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<List<MyTask>> GetAll()
        {
            return TaskManagerAPIService.GetAll(_taskManagerApiDbContext);
        }
        [HttpGet("id/{id}")]
        public ActionResult<MyTask> Get(int id)
        {
            var RequiredTask = TaskManagerAPIService.GetById(_taskManagerApiDbContext, id);
            if (RequiredTask.Id == 0)
            {
                return NotFound();
            }
            return Ok(RequiredTask);
        }
        [HttpPost("/title/{title}/description/{description}/status/{status}")]
        public IActionResult Post(string title, string description, string status)
        {
            TaskManagerAPIService.Add(_taskManagerApiDbContext, title, description, status);
            List<MyTask> AllTasks = TaskManagerAPIService.GetAll(_taskManagerApiDbContext);
            AllTasks = [.. AllTasks.OrderBy(x => x.Id)];
            MyTask LatestTask = AllTasks[^1];//getting late elemet
            return CreatedAtAction(nameof(Get), new { id = LatestTask.Id }, LatestTask);
        }
        [HttpPut("id/{id}/title/{title}/description/{description}/status/{status}")]
        public IActionResult Put(int id, string title, string description, string status)
        {
            var targetTask = TaskManagerAPIService.GetById(_taskManagerApiDbContext, id);
            if (targetTask.Id == 0)
            {
                return NotFound();
            }
            TaskManagerAPIService.Update(_taskManagerApiDbContext, id, title, description, status); return CreatedAtAction(nameof(Get), new { id = id }, targetTask);
        }
        [HttpDelete("id/{id}")]
        public IActionResult Delete(int id)
        {
            var targetTask = TaskManagerAPIService.GetById(_taskManagerApiDbContext, id);
            if (targetTask.Id == 0)
            {
                return NotFound();
            }
            TaskManagerAPIService.DeleteById(_taskManagerApiDbContext, id);
            return NoContent();
        }

    }
}