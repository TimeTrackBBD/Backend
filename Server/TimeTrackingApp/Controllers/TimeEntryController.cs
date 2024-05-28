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

    public class TimeEntryController : Controller
    {

        private readonly ITimeEntryRepository timeEntryRepository;

        public TimeEntryController(ITimeEntryRepository timeEntryRepository)
        {
            this.timeEntryRepository = timeEntryRepository;
        }
        
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TimeEntry>))]
        public IActionResult GetTimeEntrys()
        {
            var timeEntrys = timeEntryRepository.GetTimeEntrys();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(timeEntrys);
        }

        [HttpGet("{TimeEntryID}")]
        [ProducesResponseType(200, Type = typeof(TimeEntry))]
        [ProducesResponseType(400)]
        public IActionResult GetTimeEntry(int timeEntryId)
      {
        try
        {
            if (!timeEntryRepository.TimeEntryExists(timeEntryId))
            {
                return NotFound();
            }

            var timeEntry = timeEntryRepository.GetTimeEntry(timeEntryId);

            if (timeEntry == null)
            {
                return NotFound(); 
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(timeEntry);
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
       public IActionResult CreateTimeEntry([FromBody] TimeEntry createTimeEntry)
    {
        try
        {
            if (createTimeEntry == null)
            {
                return BadRequest("TimeEntry data is null.");
            }

            var existingTimeEntry = timeEntryRepository.GetTimeEntrys()
                .FirstOrDefault(u => u.TimeEntryId == createTimeEntry.TimeEntryId);

            if (existingTimeEntry != null)
            {
                return Conflict("TimeEntry already exists."); 
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!timeEntryRepository.CreateTimeEntry(createTimeEntry))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            var newTimeEntry = timeEntryRepository.GetTimeEntry(createTimeEntry.TimeEntryId);
            return Ok(newTimeEntry);
        }
        catch (Exception ex)
        {
            
            Console.WriteLine($"An error occurred: {ex.Message}");

            return StatusCode(500, "An error occurred while processing your request.");
        }
    }


        [HttpPut("{TimeEntryID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTimeEntry(int timeEntryId, [FromBody] TimeEntry UpdateTimeEntry)
    {
        try
        {
            if (UpdateTimeEntry == null)
            {
                return BadRequest("Updated TimeEntry data is null.");
            }

            if (timeEntryId != UpdateTimeEntry.TimeEntryId)
            {
                ModelState.AddModelError("", "TimeEntry ID in the URL does not match the ID in the request body.");
                return BadRequest(ModelState);
            }

            if (!timeEntryRepository.TimeEntryExists(timeEntryId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!timeEntryRepository.UpdateTimeEntry(UpdateTimeEntry))
            {
                ModelState.AddModelError("", "Something went wrong updating the TimeEntry.");
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

        [HttpDelete("{TimeEntryID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
       public IActionResult DeleteTimeEntry(int timeEntryId)
        {
            try
            {
                if (!timeEntryRepository.TimeEntryExists(timeEntryId))
                {
                    return NotFound();
                }

                var timeEntryToDelete = timeEntryRepository.GetTimeEntry(timeEntryId);

                if (timeEntryToDelete == null)
                {
                    return NotFound(); 
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!timeEntryRepository.DeleteTimeEntry(timeEntryToDelete))
                {
                    ModelState.AddModelError("", "Something went wrong deleting the TimeEntry.");
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


