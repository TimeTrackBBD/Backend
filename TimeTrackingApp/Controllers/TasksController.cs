using TimeTrackingApp.Models;
using Microsoft.AspNetCore.Mvc;
using TimeTrackingApp.Repository;
using TimeTrackingApp.Interfaces;

namespace TimeTrackingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TasksController : Controller
    {

        private readonly ITasksRepository taskRepository;

        public TasksController(ITasksRepository taskRepository)
        {
            this.taskRepository = taskRepository;
        }
        
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Task>))]
        public IActionResult GetTasks()
        {
            var tasks = taskRepository.GetTasks();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(tasks);
        }

        [HttpGet("{TaskID}")]
        [ProducesResponseType(200, Type = typeof(Tasks))]
        [ProducesResponseType(400)]
        public IActionResult GetTask(int taskId)
      {
        try
        {
            if (!taskRepository.TaskExists(taskId))
            {
                return NotFound();
            }

            var task = taskRepository.GetTask(taskId);

            if (task == null)
            {
                return NotFound(); 
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(task);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");

            return StatusCode(500, "An error occurred while processing your request.");
        }
      }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
       public IActionResult CreateTask([FromBody] Tasks createTask)
    {
        try
        {
            if (createTask == null)
            {
                return BadRequest("Task data is null.");
            }

            var existingTask = taskRepository.GetTasks()
                .FirstOrDefault(u => u.TaskName == createTask.TaskName);

            if (existingTask != null)
            {
                return Conflict("Task already exists."); 
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!taskRepository.CreateTask(createTask))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            var newTask = taskRepository.GetTask(createTask.TaskId);
            return Ok(newTask);
        }
        catch (Exception ex)
        {
            
            Console.WriteLine($"An error occurred: {ex.Message}");

            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

        [HttpPut("{TaskID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTask(int taskId, [FromBody] Tasks UpdateTask)
    {
        try
        {
            if (UpdateTask == null)
            {
                return BadRequest("Updated task data is null.");
            }

            if (taskId != UpdateTask.TaskId)
            {
                ModelState.AddModelError("", "Task ID in the URL does not match the ID in the request body.");
                return BadRequest(ModelState);
            }

            if (!taskRepository.TaskExists(taskId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!taskRepository.UpdateTask(UpdateTask))
            {
                ModelState.AddModelError("", "Something went wrong updating the task.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        catch (Exception ex)
        {
          
            Console.WriteLine($"An error occurred: {ex.Message}");

            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

        [HttpDelete("{TaskID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
       public IActionResult DeleteTask(int taskId)
        {
            try
            {
                if (!taskRepository.TaskExists(taskId))
                {
                    return NotFound();
                }

                var taskToDelete = taskRepository.GetTask(taskId);

                if (taskToDelete == null)
                {
                    return NotFound(); 
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!taskRepository.DeleteTask(taskToDelete))
                {
                    ModelState.AddModelError("", "Something went wrong deleting the task.");
                    return StatusCode(500, ModelState);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"An error occurred: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

    }
}


