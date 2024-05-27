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

    public class StatusController : Controller
    {

        private readonly IStatusRepository statusRepository;

        public StatusController(IStatusRepository statusRepository)
        {
            this.statusRepository = statusRepository;
        }
        
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Status>))]
        public IActionResult GetStatuses()
        {
            var status = statusRepository.GetStatuses();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(status);
        }

        [HttpGet("{StatusID}")]
        [ProducesResponseType(200, Type = typeof(Status))]
        [ProducesResponseType(400)]
        public IActionResult GetStatus(int statusId)
      {
        try
        {
            if (!statusRepository.StatusExists(statusId))
            {
                return NotFound();
            }

            var status = statusRepository.GetStatus(statusId);

            if (status == null)
            {
                return NotFound(); 
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(status);
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
       public IActionResult CreateStatus([FromBody] Status createStatus)
    {
        try
        {
            if (createStatus == null)
            {
                return BadRequest("Status data is null.");
            }

            var existingStatus = statusRepository.GetStatuses()
                .FirstOrDefault(u => u.StatusName == createStatus.StatusName);

            if (existingStatus != null)
            {
                return Conflict("Status already exists."); 
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!statusRepository.CreateStatus(createStatus))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            var newStatus = statusRepository.GetStatus(createStatus.StatusId);
            return Ok(newStatus);
        }
        catch (Exception ex)
        {
            
            Console.WriteLine($"An error occurred: {ex.Message}");

            return StatusCode(500, "An error occurred while processing your request.");
        }
    }


        [HttpPut("{StatusID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateStatus(int statusId, [FromBody] Status updatedStatus)
    {
        try
        {
            if (updatedStatus == null)
            {
                return BadRequest("Updated status data is null.");
            }

            if (statusId != updatedStatus.StatusId)
            {
                ModelState.AddModelError("", "Status id in the URL does not match the ID in the request body.");
                return BadRequest(ModelState);
            }

            if (!statusRepository.StatusExists(statusId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!statusRepository.UpdateStatus(updatedStatus))
            {
                ModelState.AddModelError("", "Something went wrong updating the status.");
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


        [HttpDelete("{StatusID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
       public IActionResult DeleteStatus(int statusId)
        {
            try
            {
                if (!statusRepository.StatusExists(statusId))
                {
                    return NotFound();
                }

                var statusToDelete = statusRepository.GetStatus(statusId);

                if (statusToDelete == null)
                {
                    return NotFound(); 
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!statusRepository.DeleteStatus(statusToDelete))
                {
                    ModelState.AddModelError("", "Something went wrong deleting the status.");
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


