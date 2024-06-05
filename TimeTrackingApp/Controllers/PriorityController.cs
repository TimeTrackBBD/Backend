using TimeTrackingApp.Models;
using Microsoft.AspNetCore.Mvc;
using TimeTrackingApp.Repository;
using TimeTrackingApp.Interfaces;
using TimeTrackingApp.Results;

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

    }
}


