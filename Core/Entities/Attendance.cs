namespace Core.Entities
{
    public class Attendance
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public TimeSpan? TotalHours { get; set; }
        public string Notes { get; set; }
        public DateTime Date { get; set; }
        
        public virtual Employee Employee { get; set; }
    }
}