using EmployeeWorkTracker_Test.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeWorkTracker_Test.Data
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeWorkingHoursContext _context;

        public EmployeeRepository(EmployeeWorkingHoursContext context)
        {
            _context = context;
        }
        public void AddEmployee(Employee employee)
        {
            _context.Add(employee);
        }

        public void DeleteEmployee(Employee employee)
        {
            _context.Remove(employee);
        }

        public async Task<Employee> GetEmployee(int id)
        {
            var result = _context.Employees.Where(e => e.Id == id);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<Employee[]> GetAllEmployees()
        {
            var result = _context.Employees.OrderBy(e => e.LastName);
            return await result.ToArrayAsync();
        }

        public async Task<WorkingHours[]> GetAllWorkingHours(int employeeId)
        {
            var result = _context.WorkingHours
                .Where(e => e.Employee.Id == employeeId);
            return await result.ToArrayAsync();
        }

        public async Task<WorkingHours> GetWorkingHoursLastEntry(int employeeId)
        {
            var result = _context.WorkingHours
                .Where(e => e.Employee.Id == employeeId)
                .OrderByDescending(w => w.StartTime);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public void StartWorkTracking(WorkingHours wh)
        {
            _context.WorkingHours.Add(wh);
        }

        public void StopWorkTracking(WorkingHours wh)
        {
            _context.WorkingHours.Update(wh);
        }

        public void UpdateEmployee(Employee emp)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == emp.Id);
            if (employee != null)
            {
                employee.LastName = emp.LastName;
                employee.FirstName = emp.FirstName;
                _context.SaveChanges();
            }
        }

        public async Task<WorkingHours[]> GetAllWorkingHours(int employeeId, DateTime startTime, DateTime endTime)
        {
            var result = _context.WorkingHours
                .Where(e => e.Employee.Id == employeeId && e.StartTime >= startTime && e.EndTime < endTime);
            return await result.ToArrayAsync();
        }
    }
}
