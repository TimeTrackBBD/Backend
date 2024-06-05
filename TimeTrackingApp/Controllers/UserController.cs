using TimeTrackingApp.Models;
using Microsoft.AspNetCore.Mvc;
using TimeTrackingApp.Repository;
using TimeTrackingApp.Interfaces;
using TimeTrackingApp.Results;

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
        
        [HttpGet()]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUser()
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

        [HttpGet("projects")]
        [ProducesResponseType(200, Type = typeof(Project[]))]
        [ProducesResponseType(400)]
        public IActionResult GetUserProjects()
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



        [HttpPut()]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser( [FromBody] User updatedUser)
    {
            Result<User> loggedInUser = GetLoggedInUser();
            if (loggedInUser.IsFailure)
            {
                Console.WriteLine($"An error occurred: No logged in user? Should not have been able to get through the middleware");

                return StatusCode(401, "Unauthorised - but you got through the middlware - must be hacking");
            }
            int userId = loggedInUser.Value.UserId;
            string userName = loggedInUser.Value.UserName;
            try
        {
            if (updatedUser == null)
            {
                return BadRequest("Updated user data is null.");
            }

            if (userName != updatedUser.UserName)
            {
                ModelState.AddModelError("", "The UserName from your accesss token does not match the username in the request body.");
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


        [HttpDelete()]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
       public IActionResult DeleteUser()
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

        private Result<User> GetLoggedInUser()
        {
            var loggedInUser = HttpContext.Items["loggedInUser"] as User;

            if (loggedInUser is null)
                return Result.Fail<User>(new ValidationError("Could Not Determine Logged In User. This would be 401 - but you got through the middlware - must be hacking"));

            return Result.Ok(loggedInUser);
        }
    }
}


