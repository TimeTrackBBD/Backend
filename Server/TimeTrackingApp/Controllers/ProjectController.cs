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

    public class ProjectController : Controller
    {

        private readonly IProjectRepository projectRepository;

        public ProjectController(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }
        
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Project>))]
        public IActionResult GetProjects()
        {
            var projects = projectRepository.GetProjects();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(projects);
        }

        [HttpGet("{ProjectID}")]
        [ProducesResponseType(200, Type = typeof(Project))]
        [ProducesResponseType(400)]
        public IActionResult GetProject(int projectId)
      {
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


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
       public IActionResult CreateProject([FromBody] Project createProject)
    {
        try
        {
            if (createProject == null)
            {
                return BadRequest("Project data is null.");
            }

            var existingProject = projectRepository.GetProjects()
                .FirstOrDefault(u => u.ProjectName == createProject.ProjectName);

            if (existingProject != null)
            {
                return Conflict("Project already exists."); 
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

            var newProject = projectRepository.GetProject(createProject.ProjectId);
            return Ok(newProject);
        }
        catch (Exception ex)
        {
            
            Console.WriteLine($"An error occurred: {ex.Message}");

            return StatusCode(500, "An error occurred while processing your request.");
        }
    }


        [HttpPut("{ProjectID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateProject(int projectId, [FromBody] Project UpdateProject)
    {
        try
        {
            if (UpdateProject == null)
            {
                return BadRequest("Updated project data is null.");
            }

            if (projectId != UpdateProject.ProjectId)
            {
                ModelState.AddModelError("", "Project ID in the URL does not match the ID in the request body.");
                return BadRequest(ModelState);
            }

            if (!projectRepository.ProjectExists(projectId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!projectRepository.UpdateProject(UpdateProject))
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

    }
}


