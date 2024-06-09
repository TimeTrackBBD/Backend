using TimeTrackingApp.Models;
using Microsoft.AspNetCore.Mvc;
using TimeTrackingApp.Repository;
using TimeTrackingApp.Interfaces;
using TimeTrackingApp.Results;

namespace TimeTrackingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProjectController : Controller
    {

        private readonly IProjectRepository projectRepository;

        public ProjectController(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }
        

        [HttpGet("{ProjectID}")]
        [ProducesResponseType(200, Type = typeof(Project))]
        [ProducesResponseType(400)]
        public IActionResult GetProject(int projectId)
      {
            Result<User> loggedInUser = GetLoggedInUser();
            if (loggedInUser.IsFailure)
            {
                Console.WriteLine($"An error occurred: No logged in user? Should not have been able to get through the middleware");

                return StatusCode(401, "Unauthorised - but you got through the middlware - must be hacking");
            }
            int userId = loggedInUser.Value.UserId;

            try
        {
            if (!projectRepository.ProjectExists(projectId))
            {
                return NotFound();
            }

            var project = projectRepository.GetProject(projectId);

            if (project == null)
            {
                return NotFound(); 
            }
            if (project.UserId != userId) 
            {
                return StatusCode(401, "Unauthorised.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(project);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");

            return StatusCode(500, "An error occurred while processing your request.");
        }
      }

        [HttpGet("{ProjectID}/tasks")]
        [ProducesResponseType(200, Type = typeof(Project))]
        [ProducesResponseType(400)]
        public IActionResult GetProjectTasks(int projectId)
        {
            Result<User> loggedInUser = GetLoggedInUser();
            if (loggedInUser.IsFailure)
            {
                Console.WriteLine($"An error occurred: No logged in user? Should not have been able to get through the middleware");

                return StatusCode(401, "Unauthorised - but you got through the middlware - must be hacking");
            }
            int userId = loggedInUser.Value.UserId;
            try
            {
                if (!projectRepository.ProjectExists(projectId))
                {
                    return NotFound();
                }
                if (projectRepository.GetProject(projectId).UserId != userId)
                {
                    return StatusCode(401, "Unauthorised.");
                }
                var tasks = projectRepository.GetTasks(projectId);

                if (tasks == null)
                {
                    return NotFound();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(tasks);
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
       public IActionResult CreateProject([FromBody] Project createProject)
    {
            Result<User> loggedInUser = GetLoggedInUser();
            if (loggedInUser.IsFailure)
            {
                Console.WriteLine($"An error occurred: No logged in user? Should not have been able to get through the middleware");

                return StatusCode(401, "Unauthorised - but you got through the middlware - must be hacking");
            }
            int userId = loggedInUser.Value.UserId;
            createProject.UserId = userId;
            try
        {
            if (createProject == null)
            {
                return BadRequest("Project data is null.");
            }

            var existingProject = projectRepository.GetProjects()
                .FirstOrDefault(u => (u.ProjectName == createProject.ProjectName && u.UserId==userId));

            if (existingProject != null)
            {
                return Conflict("Project already exists for this user."); 
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!projectRepository.CreateProject(createProject))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }
        catch (Exception ex)
        {
            
            Console.WriteLine($"An error occurred: {ex.Message}");

            return StatusCode(500, "An error occurred while processing your request.");
        }
    }


        [HttpPatch("{ProjectID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateProject(int projectId, [FromBody] Project UpdateProject)
    {
            Result<User> loggedInUser = GetLoggedInUser();
            if (loggedInUser.IsFailure)
            {
                Console.WriteLine($"An error occurred: No logged in user? Should not have been able to get through the middleware");

                return StatusCode(401, "Unauthorised - but you got through the middlware - must be hacking");
            }
            int userId = loggedInUser.Value.UserId;
            
            try
        {
            if (UpdateProject == null)
            {
                return BadRequest("Updated project data is null.");
            }

            if (projectId != UpdateProject.ProjectId) //duplicate info - two sources of truth bad
            {
                ModelState.AddModelError("", "Project ID in the URL does not match the ID in the request body.");
                return BadRequest(ModelState);
            }
            var project = projectRepository.GetProject(projectId);
            if (project.UserId != userId)
            {
                return StatusCode(401, "Unauthorised.");
            }
            if (!projectRepository.ProjectExists(projectId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            project.Description = UpdateProject.Description;
            project.ProjectName = UpdateProject.ProjectName;
            if (!projectRepository.UpdateProject(project))
            {
                ModelState.AddModelError("", "Something went wrong updating the project.");
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

        [HttpDelete("{ProjectID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
       public IActionResult DeleteProject(int projectId)
        {
            Result<User> loggedInUser = GetLoggedInUser();
            if (loggedInUser.IsFailure)
            {
                Console.WriteLine($"An error occurred: No logged in user? Should not have been able to get through the middleware");

                return StatusCode(401, "Unauthorised - but you got through the middlware - must be hacking");
            }
            int userId = loggedInUser.Value.UserId;


            try
            {
                if (!projectRepository.ProjectExists(projectId))
                {
                    return NotFound();
                }

                var projectToDelete = projectRepository.GetProject(projectId);

                if (projectToDelete == null)
                {
                    return NotFound(); 
                }
                if (projectToDelete.UserId != userId)
                {
                    return StatusCode(401, "Unauthorised.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!projectRepository.DeleteProject(projectToDelete))
                {
                    ModelState.AddModelError("", "Something went wrong deleting the project.");
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