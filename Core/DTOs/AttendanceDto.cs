using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class AttendanceDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public DateTime CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public TimeSpan? TotalHours { get; set; }
        public string Notes { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }

    public class CheckInRequest
    {
        [Required(ErrorMessage = "Employee ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Employee ID must be greater than 0")]
        public int EmployeeId { get; set; }

        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
        public string Notes { get; set; } = string.Empty;
    }

    public class CheckOutRequest
    {
        [Required(ErrorMessage = "Employee ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Employee ID must be greater than 0")]
        public int EmployeeId { get; set; }

        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
        public string Notes { get; set; } = string.Empty;
    }
}
