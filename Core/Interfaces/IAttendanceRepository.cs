using Core.Entities;

namespace Core.Interfaces
{
    public interface IAttendanceRepository : IRepository<Attendance>
    {
        Task<Attendance> GetTodayAttendanceAsync(int employeeId);
        Task<IEnumerable<Attendance>> GetEmployeeAttendanceAsync(int employeeId, DateTime startDate, DateTime endDate);
        Task<bool> HasCheckedInTodayAsync(int employeeId);
    }
}