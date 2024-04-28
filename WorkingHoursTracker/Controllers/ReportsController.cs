using EmployeeWorkTracker_Test.Data;
using EmployeeWorkTracker_Test.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace EmployeeWorkTracker_Test.Controllers
{
    [Route("api/employees/reports")]
    public class ReportsController : ControllerBase
    {
        private readonly IEmployeeRepository _empRepository;

        public ReportsController(IEmployeeRepository empRepository)
        {
            _empRepository = empRepository;
        }
        [HttpGet]
        public async Task<ActionResult> Get(string employeeId, string startTime, string endTime)
        {
            string dateTimeFormat = "dd-MM-yyyyTHH-mm";
            try
            {
                DateTime startDateTime, endDateTime;
                startDateTime = DateTime.ParseExact(startTime, dateTimeFormat, CultureInfo.InvariantCulture);
                endDateTime = DateTime.ParseExact(endTime, dateTimeFormat, CultureInfo.InvariantCulture);

                if (employeeId == null)
                {
                    List<Report> reports = new List<Report>();
                    var employees = await _empRepository.GetAllEmployees();
                    foreach (var employee in employees)
                    {
                        Report report = await GetReport(employee, startDateTime, endDateTime);
                        reports.Add(report);
                    }
                    return Ok(reports);
                }
                else
                {
                    int employeeIdAsInt;
                    if (!Int32.TryParse(employeeId, out employeeIdAsInt))
                    {
                        return BadRequest("Invalid parameter value 'employeeID'. Value should be integer.");
                    }                    
                    var employee = await _empRepository.GetEmployee(employeeIdAsInt);
                    if (employee == null)
                    {
                        return BadRequest($"Cannot create report for non-exitent employee (ID {employeeIdAsInt})");
                    }
                    Report report = await GetReport(employee, startDateTime, endDateTime);
                    return Ok(report);
                }
            }
            catch (ArgumentNullException)
            {
                return BadRequest($"Invalid value for parameter 'startTime' or 'endTime'. Required date time format id {dateTimeFormat}.");
            }
            catch (FormatException)
            {
                return BadRequest($"Invalid value for parameter 'startTime' or 'endTime'. Required date time format id {dateTimeFormat}.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error processing request.");
            }
            
        }

        private async Task<Report> GetReport(Employee employee)
        {
            WorkingHours[] workingHours = await _empRepository.GetAllWorkingHours(employee.Id);
            TimeSpan workingHoursSum = TimeSpan.FromMinutes(workingHours.Select(w => w.Duration).Sum());
            return new Report() { Employee = employee, WorkingHours = String.Format($"{workingHoursSum.Hours}h {workingHoursSum.Minutes}m") };
        }
        private async Task<Report> GetReport(Employee employee, DateTime startTime, DateTime endTime)
        {
            WorkingHours[] workingHours = await _empRepository.GetAllWorkingHours(employee.Id, startTime, endTime);
            TimeSpan workingHoursSum = TimeSpan.FromMinutes(workingHours.Select(w => w.Duration).Sum());
            return new Report() { Employee = employee, From = startTime, To = endTime, WorkingHours = String.Format($"{workingHoursSum.Hours}h {workingHoursSum.Minutes}m") };
        }
    }
}
