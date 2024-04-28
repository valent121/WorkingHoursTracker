using EmployeeWorkTracker_Test.Data;
using EmployeeWorkTracker_Test.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWorkTracker_Test.Controllers
{
    [Route("api/employees/{id:int}/tracking")]
    public class TrackingController : ControllerBase
    {
        private readonly IEmployeeRepository _empRepository;

        public TrackingController(IEmployeeRepository empRepository)
        {
            _empRepository = empRepository;
        }
        [HttpPut]
        public async Task<ActionResult>Put(int id, string type)
        {
            if (String.IsNullOrEmpty(type))
            {
                return BadRequest("Parameter 'type' not set. Allowed values: start|stop");
            }
            try
            {
                var employee = await _empRepository.GetEmployee(id);
                if (employee == null)
                {
                    return NotFound("Cannot start work tracking for non-existent employee!");
                }
                var workingHours = await _empRepository.GetWorkingHoursLastEntry(id);

                if (type == "start")
                {
                    if (workingHours?.EndTime == DateTime.MinValue)
                    {
                        return BadRequest("Cannot start time tracking. Tracking already in progress. First end current tracking before starting a new one.");
                    }

                    WorkingHours newWorkingHoursEntry = new WorkingHours { Employee = employee, StartTime = DateTime.Now };
                    _empRepository.StartWorkTracking(newWorkingHoursEntry);
                    if (await _empRepository.SaveChangesAsync())
                    {
                        return Created($"/api/emplyees/{id}/tracking/", newWorkingHoursEntry);
                    }
                }
                else if (type == "stop")
                {
                    if (workingHours?.EndTime != DateTime.MinValue)
                    {
                        return BadRequest("Cannot end time tracking. No tracking in progress. First start new tracking before trying to end it.");
                    }
                    workingHours.EndTime = DateTime.Now;
                    long duration = (workingHours.EndTime - workingHours.StartTime).Minutes;
                    workingHours.Duration = duration;
                    _empRepository.StopWorkTracking(workingHours);
                    if (await _empRepository.SaveChangesAsync())
                    {
                        return Created($"/api/emplyees/{id}/tracking/", workingHours);
                    }
                }
                else
                {
                    return BadRequest("Invalid value for tracking parameter 'type'. Allowed values: start|stop");
                }                
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error processing request.");
            }
            return BadRequest($"Couldn't record tracking type {type}");
        }
    }
}
