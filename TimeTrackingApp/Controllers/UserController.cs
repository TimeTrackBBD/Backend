using TimeTrackingApp.Models;
using Microsoft.AspNetCore.Mvc;
using TimeTrackingApp.Repository;
using TimeTrackingApp.Interfaces;

namespace TimeTrackingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : Controller
    {

        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            var users = userRepository.GetUsers();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(int userId)
      {
        try
        {
            if (!userRepository.UserExists(userId))
            {
                return NotFound();
            }

            var user = userRepository.GetUser(userId);

            if (user == null)
            {
                return NotFound(); 
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(user);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");

            return StatusCode(500, "An error occurred while processing your request.");
        }
      }

        [HttpGet("{userId}/projects")]
        [ProducesResponseType(200, Type = typeof(Project[]))]
        [ProducesResponseType(400)]
        public IActionResult GetUserProjects(int userId)
        {
            try
            {
                if (!userRepository.UserExists(userId))
                {
                    return NotFound();
                }

                var projects = userRepository.GetProjects(userId);

                if (projects == null)
                {
                    return NotFound();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(projects);
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
       public IActionResult CreateUser([FromBody] User createUser)
    {
        try
        {
            if (createUser == null)
            {
                return BadRequest("User data is null.");
            }

            var existingUser = userRepository.GetUsers()
                .FirstOrDefault(u => u.UserName == createUser.UserName);

            if (existingUser != null)
            {
                return Conflict("User already exists."); 
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!userRepository.CreateUser(createUser))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            var newUser = userRepository.GetUser(createUser.UserId);
            return Ok(newUser);
        }
        catch (Exception ex)
        {
            
            Console.WriteLine($"An error occurred: {ex.Message}");

            return StatusCode(500, "An error occurred while processing your request.");
        }
    }


        [HttpPut("{UserID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(int userId, [FromBody] User updatedUser)
    {
        try
        {
            if (updatedUser == null)
            {
                return BadRequest("Updated user data is null.");
            }

            if (userId != updatedUser.UserId)
            {
                ModelState.AddModelError("", "User ID in the URL does not match the ID in the request body.");
                return BadRequest(ModelState);
            }

            if (!userRepository.UserExists(userId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!userRepository.UpdateUser(updatedUser))
            {
                ModelState.AddModelError("", "Something went wrong updating the user.");
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


        [HttpDelete("{UserID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
       public IActionResult DeleteUser(int userId)
        {
            try
            {
                if (!userRepository.UserExists(userId))
                {
                    return NotFound();
                }

                var userToDelete = userRepository.GetUser(userId);

                if (userToDelete == null)
                {
                    return NotFound(); 
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!userRepository.DeleteUser(userToDelete))
                {
                    ModelState.AddModelError("", "Something went wrong deleting the user.");
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


