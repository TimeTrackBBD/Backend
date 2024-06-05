using TimeTrackingApp.Models;
using Microsoft.AspNetCore.Mvc;
using TimeTrackingApp.Repository;
using TimeTrackingApp.Interfaces;
using TimeTrackingApp.Results;

namespace TimeTrackingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TasksController : Controller
    {

        private readonly ITasksRepository taskRepository;
        private readonly IProjectRepository projectRepository;

        public TasksController(ITasksRepository taskRepository, IProjectRepository projectRepository)
        {
            this.taskRepository = taskRepository;
            this.projectRepository = projectRepository;
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

         //TODO:REMOVE
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
            Result<User> loggedInUser = GetLoggedInUser();
            if (loggedInUser.IsFailure)
            {
                Console.WriteLine($"An error occurred: No logged in user? Should not have been able to get through the middleware");

                return StatusCode(401, "Unauthorised - but you got through the middlware - must be hacking");
            }
            int userId = loggedInUser.Value.UserId;
            Project project = projectRepository.GetProject(createTask.ProjectId);
            if (project == null)
            {
                return StatusCode(400, "Please enter a valid project id.");
            }
            if (project.UserId != userId)
            {
                return StatusCode(401, "Unauthorised.");
            }
            try
        {
            if (createTask == null)
            {
                return BadRequest("Task data is null.");
            }

            var existingTask = taskRepository.GetTasks()
                .FirstOrDefault(u => (u.TaskName == createTask.TaskName && u.ProjectId==project.ProjectId));

            if (existingTask != null)
            {
                return Conflict("Task with this name already exists for this user."); 
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (createTask.PriorityId <1 || createTask.PriorityId > 3) 
            {
                    return Conflict("Task priority must be 1,2 or 3");
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

        [HttpPatch("{TaskID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTask(int taskId, [FromBody] Tasks UpdateTask)
    {

            Result<User> loggedInUser = GetLoggedInUser();
            if (loggedInUser.IsFailure)
            {
                Console.WriteLine($"An error occurred: No logged in user? Should not have been able to get through the middleware");

                return StatusCode(401, "Unauthorised - but you got through the middlware - must be hacking");
            }
            int userId = loggedInUser.Value.UserId;

            if (UpdateTask == null)
            {
                return BadRequest("Updated task data is null.");
            }
            if (taskId != UpdateTask.TaskId)
            {
                ModelState.AddModelError("", "Task ID in the URL does not match the ID in the request body.");
                return BadRequest(ModelState);
            }

            var project = projectRepository.GetProject(UpdateTask.ProjectId);

            if (project.UserId != userId)
            {
                return StatusCode(401, "Unauthorised.");
            }

            try
            {
            if (!taskRepository.TaskExists(taskId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                Tasks task = taskRepository.GetTask(taskId);
                task.Duration = UpdateTask.Duration != null ? UpdateTask.Duration : task.Duration;
                task.TaskName = UpdateTask.TaskName != null ? UpdateTask.TaskName : task.TaskName;
                task.Description = UpdateTask.Description != null ? UpdateTask.Description : task.Description;

                if (!taskRepository.UpdateTask(task))
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
            Result<User> loggedInUser = GetLoggedInUser();
            if (loggedInUser.IsFailure)
            {
                Console.WriteLine($"An error occurred: No logged in user? Should not have been able to get through the middleware");

                return StatusCode(401, "Unauthorised - but you got through the middlware - must be hacking");
            }
            int userId = loggedInUser.Value.UserId;

            if (!taskRepository.TaskExists(taskId))
            {
                return NotFound();
            }
            Tasks taskToDelete = taskRepository.GetTask(taskId);
            Project project = projectRepository.GetProject(taskToDelete.ProjectId);
            if (project == null)
            {
                return StatusCode(500, "Task appears to niot be asigned to a project or the project has been deleted");
            }
            if (project.UserId != userId)
            {
                return StatusCode(401, "Unauthorised.");
            }

            try
            {

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

        private Result<User> GetLoggedInUser()
        {
            var loggedInUser = HttpContext.Items["loggedInUser"] as User;

            if (loggedInUser is null)
                return Result.Fail<User>(new ValidationError("Could Not Determine Logged In User"));

            return Result.Ok(loggedInUser);
        }
    }
}


