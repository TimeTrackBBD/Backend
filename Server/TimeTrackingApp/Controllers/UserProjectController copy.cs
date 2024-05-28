using TimeTrackingApp.Model;
using TimeTrackingApp.Data;
//using TimeTrackingApp.Dto;
using Microsoft.AspNetCore.Mvc;
using TimeTrackingApp.Repository;
using TimeTrackingApp.Interface;

namespace TimeTrackingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProjectTaskController : Controller
    {

        private readonly IProjectTaskRepository projectTaskRepository;

        public ProjectTaskController(IProjectTaskRepository projectTaskRepository)
        {
            this.projectTaskRepository = projectTaskRepository;
        }
        
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProjectTask>))]
        public IActionResult GetProjectTasks()
        {
            var projectTasks = projectTaskRepository.GetProjectTasks();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(projectTasks);
        }

        [HttpGet("{ProjectTaskID}")]
        [ProducesResponseType(200, Type = typeof(ProjectTask))]
        [ProducesResponseType(400)]
        public IActionResult GetProjectTask(int projectTaskId)
      {
        try
        {
            if (!projectTaskRepository.ProjectTaskExists(projectTaskId))
            {
                return NotFound();
            }

            var projectTask = projectTaskRepository.GetProjectTask(projectTaskId);

            if (projectTask == null)
            {
                return NotFound(); 
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(projectTask);
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
       public IActionResult CreateProjectTask([FromBody] ProjectTask createProjectTask)
    {
        try
        {
            if (createProjectTask == null)
            {
                return BadRequest("projecttask data is null.");
            }

            var existingProjectTask = projectTaskRepository.GetProjectTasks()
                .FirstOrDefault(u => u.ProjectTaskId == createProjectTask.ProjectTaskId);

            if (existingProjectTask != null)
            {
                return Conflict("ProjectTask already exists."); 
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!projectTaskRepository.CreateProjectTask(createProjectTask))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            var newProjectTask = projectTaskRepository.GetProjectTask(createProjectTask.ProjectTaskId);
            return Ok(newProjectTask);
        }
        catch (Exception ex)
        {
            
            Console.WriteLine($"An error occurred: {ex.Message}");

            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

        [HttpPut("{ProjectTaskID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateProjectTask(int projectTaskId, [FromBody] ProjectTask updateProjectTask)
    {
        try
        {
            if (UpdateProjectTask == null)
            {
                return BadRequest("Updated ProjectTask data is null.");
            }

            if (projectTaskId != updateProjectTask.ProjectTaskId)
            {
                ModelState.AddModelError("", "ProjectTask ID in the URL does not match the ID in the request body.");
                return BadRequest(ModelState);
            }

            if (!projectTaskRepository.ProjectTaskExists(projectTaskId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!projectTaskRepository.UpdateProjectTask(updateProjectTask))
            {
                ModelState.AddModelError("", "Something went wrong updating the ProjectTask.");
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

        [HttpDelete("{ProjectTaskID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
       public IActionResult DeleteProjectTask(int projectTaskId)
        {
            try
            {
                if (!projectTaskRepository.ProjectTaskExists(projectTaskId))
                {
                    return NotFound();
                }

                var projectTaskToDelete = projectTaskRepository.GetProjectTask(projectTaskId);

                if (projectTaskToDelete == null)
                {
                    return NotFound(); 
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!projectTaskRepository.DeleteProjectTask(projectTaskToDelete))
                {
                    ModelState.AddModelError("", "Something went wrong deleting the ProjectTask.");
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


