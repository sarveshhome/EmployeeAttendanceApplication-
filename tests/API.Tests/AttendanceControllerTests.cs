using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using API.Controllers;
using Core.Interfaces;
using Core.DTOs;
using Core.Entities;

namespace API.Tests;

public class AttendanceControllerTests
{
    private readonly Mock<IAttendanceService> _mockService;
    private readonly AttendanceController _controller;

    public AttendanceControllerTests()
    {
        _mockService = new Mock<IAttendanceService>();
        _controller = new AttendanceController(_mockService.Object);
    }

    [Fact]
    public async Task CheckIn_ValidRequest_ReturnsOk()
    {
        var request = new CheckInRequest { EmployeeId = 1, Notes = "Test" };
        var attendance = CreateSampleAttendance();
        _mockService.Setup(s => s.CheckInAsync(1, "Test")).ReturnsAsync(attendance);

        var result = await _controller.CheckIn(request);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var dto = Assert.IsType<AttendanceDto>(okResult.Value);
        Assert.Equal(1, dto.EmployeeId);
    }

    [Fact]
    public async Task CheckIn_ServiceThrows_ReturnsBadRequest()
    {
        var request = new CheckInRequest { EmployeeId = 1 };
        _mockService.Setup(s => s.CheckInAsync(1, "")).ThrowsAsync(new Exception("Error"));

        var result = await _controller.CheckIn(request);

        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public async Task CheckOut_ValidRequest_ReturnsOk()
    {
        var request = new CheckOutRequest { EmployeeId = 1, Notes = "Test" };
        var attendance = CreateSampleAttendance();
        _mockService.Setup(s => s.CheckOutAsync(1, "Test")).ReturnsAsync(attendance);

        var result = await _controller.CheckOut(request);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var dto = Assert.IsType<AttendanceDto>(okResult.Value);
        Assert.Equal(1, dto.EmployeeId);
    }

    [Fact]
    public async Task CheckOut_ServiceThrows_ReturnsBadRequest()
    {
        var request = new CheckOutRequest { EmployeeId = 1 };
        _mockService.Setup(s => s.CheckOutAsync(1, "")).ThrowsAsync(new Exception("Error"));

        var result = await _controller.CheckOut(request);

        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetTodayAttendance_AttendanceExists_ReturnsOk()
    {
        var attendance = CreateSampleAttendance();
        _mockService.Setup(s => s.GetTodayAttendanceAsync(1)).ReturnsAsync(attendance);

        var result = await _controller.GetTodayAttendance(1);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var dto = Assert.IsType<AttendanceDto>(okResult.Value);
        Assert.Equal(1, dto.EmployeeId);
    }

    [Fact]
    public async Task GetTodayAttendance_AttendanceNotFound_ReturnsNotFound()
    {
        _mockService.Setup(s => s.GetTodayAttendanceAsync(1)).ReturnsAsync((Attendance)null);

        var result = await _controller.GetTodayAttendance(1);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetEmployeeAttendance_ValidRequest_ReturnsOk()
    {
        var attendances = new[] { CreateSampleAttendance() };
        var startDate = DateTime.Today;
        var endDate = DateTime.Today.AddDays(1);
        _mockService.Setup(s => s.GetEmployeeAttendanceAsync(1, startDate, endDate)).ReturnsAsync(attendances);

        var result = await _controller.GetEmployeeAttendance(1, startDate, endDate);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var dtos = Assert.IsAssignableFrom<IEnumerable<AttendanceDto>>(okResult.Value);
        Assert.Single(dtos);
    }

    private Attendance CreateSampleAttendance()
    {
        return new Attendance
        {
            Id = 1,
            EmployeeId = 1,
            CheckInTime = DateTime.Now,
            Notes = "Test",
            Date = DateTime.Today,
            Employee = new Employee { FirstName = "John", LastName = "Doe" }
        };
    }
}