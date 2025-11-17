using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.DTOs;
using Core.Entities;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpPost("checkin")]
        public async Task<ActionResult<AttendanceDto>> CheckIn([FromBody] CheckInRequest request)
        {
            try
            {
                var attendance = await _attendanceService.CheckInAsync(request.EmployeeId, request.Notes);
                var dto = MapToDto(attendance);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("checkout")]
        public async Task<ActionResult<AttendanceDto>> CheckOut([FromBody] CheckOutRequest request)
        {
            try
            {
                var attendance = await _attendanceService.CheckOutAsync(request.EmployeeId, request.Notes);
                var dto = MapToDto(attendance);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("today/{employeeId}")]
        public async Task<ActionResult<AttendanceDto>> GetTodayAttendance(int employeeId)
        {
            var attendance = await _attendanceService.GetTodayAttendanceAsync(employeeId);
            if (attendance == null)
                return NotFound();

            return Ok(MapToDto(attendance));
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<ActionResult<IEnumerable<AttendanceDto>>> GetEmployeeAttendance(
            int employeeId, 
            [FromQuery] DateTime startDate, 
            [FromQuery] DateTime endDate)
        {
            var attendances = await _attendanceService.GetEmployeeAttendanceAsync(employeeId, startDate, endDate);
            var dtos = attendances.Select(MapToDto);
            return Ok(dtos);
        }

        private AttendanceDto MapToDto(Attendance attendance)
        {
            return new AttendanceDto
            {
                Id = attendance.Id,
                EmployeeId = attendance.EmployeeId,
                EmployeeName = $"{attendance.Employee.FirstName} {attendance.Employee.LastName}",
                CheckInTime = attendance.CheckInTime,
                CheckOutTime = attendance.CheckOutTime,
                TotalHours = attendance.TotalHours,
                Notes = attendance.Notes,
                Date = attendance.Date
            };
        }
    }
}