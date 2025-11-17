using Core.Entities;

namespace Core.Interfaces
{
    public interface IAttendanceService
    {
        Task<Attendance> CheckInAsync(int employeeId, string notes = null);
        Task<Attendance> CheckOutAsync(int employeeId, string notes = null);
        Task<Attendance> GetTodayAttendanceAsync(int employeeId);
        Task<IEnumerable<Attendance>> GetEmployeeAttendanceAsync(int employeeId, DateTime startDate, DateTime endDate);
    }
}