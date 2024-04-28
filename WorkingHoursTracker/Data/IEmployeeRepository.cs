using EmployeeWorkTracker_Test.Models;

namespace EmployeeWorkTracker_Test.Data
{
    public interface IEmployeeRepository
    {
        void AddEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
        Task<Employee[]> GetAllEmployees();
        Task<Employee> GetEmployee(int id);
        Task<bool> SaveChangesAsync();

        Task<WorkingHours[]> GetAllWorkingHours(int employeeId);
        Task<WorkingHours[]> GetAllWorkingHours(int employeeId, DateTime startTime, DateTime endTime);
        Task<WorkingHours> GetWorkingHoursLastEntry(int employeeId);
        void StartWorkTracking(WorkingHours wh);
        void StopWorkTracking(WorkingHours wh);
    }
}
