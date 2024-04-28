namespace EmployeeWorkTracker_Test.Models
{
    public class Report
    {
        public Employee Employee { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string WorkingHours { get; set; }
    }
}
