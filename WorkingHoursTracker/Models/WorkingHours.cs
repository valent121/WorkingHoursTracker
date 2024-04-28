namespace EmployeeWorkTracker_Test.Models
{
    public class WorkingHours
    {
        public int Id { get; set; }
        public Employee Employee { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public long Duration { get; set; } //Duration in minutes
    }
}
