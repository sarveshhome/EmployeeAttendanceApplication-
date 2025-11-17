namespace Core.DTOs
{
    public class AttendanceDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public TimeSpan? TotalHours { get; set; }
        public string Notes { get; set; }
        public DateTime Date { get; set; }
    }

    public class CheckInRequest
    {
        public int EmployeeId { get; set; }
        public string Notes { get; set; }
    }

    public class CheckOutRequest
    {
        public int EmployeeId { get; set; }
        public string Notes { get; set; }
    }
}