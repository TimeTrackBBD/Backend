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

    public class UserProjectController : Controller
    {

        private readonly IUserProjectRepository userProjectRepository;

        public UserProjectController(IUserProjectRepository userProjectRepository)
        {
            this.userProjectRepository = userProjectRepository;
        }
        
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserProject>))]
        public IActionResult GetUserProjects()
        {
            var userProjects = userProjectRepository.GetUserProjects();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(userProjects);
        }

        [HttpGet("{UserProjectID}")]
        [ProducesResponseType(200, Type = typeof(UserProject))]
        [ProducesResponseType(400)]
        public IActionResult GetUserProject(int userProjectId)
      {
        try
        {
            if (!userProjectRepository.UserProjectExists(userProjectId))
            {
                return NotFound();
            }

            var userProject = userProjectRepository.GetUserProject(userProjectId);

            if (userProject == null)
            {
                return NotFound(); 
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(userProject);
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
       public IActionResult CreateUserProject([FromBody] UserProject createUserProject)
    {
        try
        {
            if (createUserProject == null)
            {
                return BadRequest("User data is null.");
            }

            var existingUserProject = userProjectRepository.GetUserProjects()
                .FirstOrDefault(u => u.UserProjectId == createUserProject.UserProjectId);

            if (existingUserProject != null)
            {
                return Conflict("UserProject already exists."); 
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!userProjectRepository.CreateUserProject(createUserProject))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            var newUserProject = userProjectRepository.GetUserProject(createUserProject.UserProjectId);
            return Ok(newUserProject);
        }
        catch (Exception ex)
        {
            
            Console.WriteLine($"An error occurred: {ex.Message}");

            return StatusCode(500, "An error occurred while processing your request.");
        }
    }


        [HttpPut("{UserProjectID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUserProject(int userProjectId, [FromBody] UserProject updateUserProject)
    {
        try
        {
            if (updateUserProject == null)
            {
                return BadRequest("Updated UserProject data is null.");
            }

            if (userProjectId != updateUserProject.UserProjectId)
            {
                ModelState.AddModelError("", "UserProject ID in the URL does not match the ID in the request body.");
                return BadRequest(ModelState);
            }

            if (!userProjectRepository.UserProjectExists(userProjectId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!userProjectRepository.UpdateUserProject(updateUserProject))
            {
                ModelState.AddModelError("", "Something went wrong updating the UserProject.");
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

        [HttpDelete("{UserProjectID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
       public IActionResult DeleteUserProject(int userProjectId)
        {
            try
            {
                if (!userProjectRepository.UserProjectExists(userProjectId))
                {
                    return NotFound();
                }

                var userProjectToDelete = userProjectRepository.GetUserProject(userProjectId);

                if (userProjectToDelete == null)
                {
                    return NotFound(); 
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!userProjectRepository.DeleteUserProject(userProjectToDelete))
                {
                    ModelState.AddModelError("", "Something went wrong deleting the UserProject.");
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


