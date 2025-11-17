using Core.Entities;
using Core.Interfaces;
using Core.DTOs;

namespace Infrastructure.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IRepository<Employee> _employeeRepository;

        public AttendanceService(
            IAttendanceRepository attendanceRepository,
            IRepository<Employee> employeeRepository)
        {
            _attendanceRepository = attendanceRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<Attendance> CheckInAsync(int employeeId, string notes = null)
        {
            // Check if employee exists and is active
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            if (employee == null || !employee.IsActive)
                throw new ArgumentException("Employee not found or inactive");

            // Check if already checked in today
            var todayAttendance = await _attendanceRepository.GetTodayAttendanceAsync(employeeId);
            if (todayAttendance != null)
                throw new InvalidOperationException("Employee already checked in today");

            var attendance = new Attendance
            {
                EmployeeId = employeeId,
                CheckInTime = DateTime.UtcNow,
                Date = DateTime.UtcNow.Date,
                Notes = notes
            };

            await _attendanceRepository.AddAsync(attendance);
            return attendance;
        }

        public async Task<Attendance> CheckOutAsync(int employeeId, string notes = null)
        {
            var todayAttendance = await _attendanceRepository.GetTodayAttendanceAsync(employeeId);
            
            if (todayAttendance == null)
                throw new InvalidOperationException("No check-in found for today");
                
            if (todayAttendance.CheckOutTime.HasValue)
                throw new InvalidOperationException("Already checked out today");

            todayAttendance.CheckOutTime = DateTime.UtcNow;
            todayAttendance.TotalHours = todayAttendance.CheckOutTime.Value - todayAttendance.CheckInTime;
            
            if (!string.IsNullOrEmpty(notes))
                todayAttendance.Notes += $"\nCheck-out: {notes}";

            _attendanceRepository.Update(todayAttendance);
            return todayAttendance;
        }

        public async Task<Attendance> GetTodayAttendanceAsync(int employeeId)
        {
            return await _attendanceRepository.GetTodayAttendanceAsync(employeeId);
        }

        public async Task<IEnumerable<Attendance>> GetEmployeeAttendanceAsync(int employeeId, DateTime startDate, DateTime endDate)
        {
            return await _attendanceRepository.GetEmployeeAttendanceAsync(employeeId, startDate, endDate);
        }
    }
}