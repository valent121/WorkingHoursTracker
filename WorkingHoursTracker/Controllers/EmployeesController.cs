using EmployeeWorkTracker_Test.Data;
using EmployeeWorkTracker_Test.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeWorkTracker_Test.Controllers
{
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _empRepository;

        public EmployeesController(IEmployeeRepository empRepository)
        {
            _empRepository = empRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetEmplyees()
        {
            try
            {
                var results = await _empRepository.GetAllEmployees();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error processing request.");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetEmployee(int id)
        {
            try
            {
                var result = await _empRepository.GetEmployee(id);
                if (result == null) { return NotFound($"Employee with ID {id} does not exist"); }
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error processing request.");
            }
        }
        public async Task<ActionResult> Post([FromBody]Employee employee)
        {
            try
            {
                if (employee.FirstName == null || employee.LastName == null) { 
                    return BadRequest(); 
                }

                employee.CreationTime = DateTime.Now;
                employee.LastUpdate = DateTime.Now;

                _empRepository.AddEmployee(employee);
                if (await _empRepository.SaveChangesAsync())
                {
                    return Created($"/api/emplyees/", employee);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error processing request.");
            }
            return BadRequest();
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Employee employee)
        {
            try
            {
                var existingEmployee = await _empRepository.GetEmployee(id);
                if (existingEmployee == null)
                {
                    return NotFound($"Employee with ID {id} does not exist");
                }
                if (employee.FirstName == null || employee.LastName == null)
                {
                    return BadRequest();
                }

                existingEmployee.FirstName = employee.FirstName;
                existingEmployee.LastName = employee.LastName;
                existingEmployee.LastUpdate = DateTime.Now;

                if (await _empRepository.SaveChangesAsync())
                {
                    return Ok(existingEmployee);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error processing request.");
            }
            return BadRequest($"Failed to update employee (ID {id})");
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var existingEmployee = await _empRepository.GetEmployee(id);
                if (existingEmployee == null)
                {
                    return NotFound();
                }
                _empRepository.DeleteEmployee(existingEmployee);
                if (await _empRepository.SaveChangesAsync()) 
                { 
                    return Ok(); 
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error processing request.");
            }
            return BadRequest($"Failed to delete employee (ID {id})");
        }
    }
}
