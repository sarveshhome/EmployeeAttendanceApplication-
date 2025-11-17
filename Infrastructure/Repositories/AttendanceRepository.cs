using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AttendanceRepository : Repository<Attendance>, IAttendanceRepository
    {
        public AttendanceRepository(ApplicationDbContext context)
            : base(context) { }

        public async Task<Attendance?> GetTodayAttendanceAsync(int employeeId)
        {
            var today = DateTime.UtcNow.Date;
            return await _context
                .Attendances.Include(a => a.Employee)
                .FirstOrDefaultAsync(a => a.EmployeeId == employeeId && a.Date == today);
        }

        public async Task<IEnumerable<Attendance>> GetEmployeeAttendanceAsync(
            int employeeId,
            DateTime startDate,
            DateTime endDate
        )
        {
            return await _context
                .Attendances.Include(a => a.Employee)
                .Where(a =>
                    a.EmployeeId == employeeId && a.Date >= startDate.Date && a.Date <= endDate.Date
                )
                .OrderByDescending(a => a.Date)
                .ToListAsync();
        }

        public async Task<bool> HasCheckedInTodayAsync(int employeeId)
        {
            var today = DateTime.UtcNow.Date;
            return await _context.Attendances.AnyAsync(a =>
                a.EmployeeId == employeeId && a.Date == today && a.CheckOutTime == null
            );
        }
    }
}
