using TimeTrackingApp.Models;
using Microsoft.AspNetCore.Mvc;
using TimeTrackingApp.Repository;
using TimeTrackingApp.Interfaces;

namespace TimeTrackingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PriorityController : Controller
    {

        private readonly IPriorityRepository priorityRepository;

        public PriorityController(IPriorityRepository priorityRepository)
        {
            this.priorityRepository = priorityRepository;
        }
        
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Priority>))]
        public IActionResult GetPriorities()
        {
            var priority = priorityRepository.GetPriorities();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(priority);
        }

        [HttpGet("{PriorityID}")]
        [ProducesResponseType(200, Type = typeof(Priority))]
        [ProducesResponseType(400)]
        public IActionResult GetStatus(int priorityID)
      {
        try
        {
            if (!priorityRepository.PriorityExists(priorityID))
            {
                return NotFound();
            }

            var priority = priorityRepository.GetPriority(priorityID);

            if (priority == null)
            {
                return NotFound(); 
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(priority);
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
       public IActionResult CreatePriority([FromBody] Priority createPriority)
    {
        try
        {
            if (createPriority == null)
            {
                return BadRequest("Priority data is null.");
            }

            var existingPriority = priorityRepository.GetPriorities()
                .FirstOrDefault(u => u.PriorityName == createPriority.PriorityName);

            if (existingPriority != null)
            {
                return Conflict("Priority already exists."); 
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!priorityRepository.CreatePriority(createPriority))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            var newPriority = priorityRepository.GetPriority(createPriority.PriorityId);
            
            return Ok(newPriority);
        }
        catch (Exception ex)
        {
            
            Console.WriteLine($"An error occurred: {ex.Message}");

            return StatusCode(500, "An error occurred while processing your request.");
        }
    }


        [HttpPut("{PriorityID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePriority(int priorityId, [FromBody] Priority updatedPriority)
    {
        try
        {
            if (updatedPriority == null)
            {
                return BadRequest("Updated priority data is null.");
            }

            if (priorityId != updatedPriority.PriorityId)
            {
                ModelState.AddModelError("", "Priority id in the URL does not match the ID in the request body.");
                return BadRequest(ModelState);
            }

            if (!priorityRepository.PriorityExists(priorityId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!priorityRepository.UpdatePriority(updatedPriority))
            {
                ModelState.AddModelError("", "Something went wrong updating the Priority.");
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


        [HttpDelete("{PriorityID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
       public IActionResult DeletePriority(int priorityId)
        {
            try
            {
                if (!priorityRepository.PriorityExists(priorityId))
                {
                    return NotFound();
                }

                var priorityToDelete = priorityRepository.GetPriority(priorityId);

                if (priorityToDelete == null)
                {
                    return NotFound(); 
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!priorityRepository.DeletePriority(priorityToDelete))
                {
                    ModelState.AddModelError("", "Something went wrong deleting the priority.");
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


