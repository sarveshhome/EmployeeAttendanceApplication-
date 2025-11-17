# Employee Time Tracking

A .NET 9.0 application for tracking employee time.

## Projects

- **Core** - Business logic and domain models
- **Infrastructure** - Data access and external services
- **API** - Web API endpoints

## Test Projects

- **Core.Tests** - Unit tests for Core project
- **API.Tests** - Unit and integration tests for API project

## Getting Started

```bash
# Build the solution
dotnet build

# Run tests
dotnet test

# Run the API
dotnet run --project API
```

```bash
# 1. First time setup
dotnet ef migrations add InitialCreate --project Infrastructure --startup-project API
dotnet ef database update --project Infrastructure --startup-project API

# 2. After adding new entities or properties
dotnet ef migrations add AddNewEntity --project Infrastructure --startup-project API
dotnet ef database update --project Infrastructure --startup-project API

# 3. Rollback to previous migration
dotnet ef database update PreviousMigrationName --project Infrastructure --startup-project API

# 4. Reset database (drop and recreate)
dotnet ef database drop --project Infrastructure --startup-project API
dotnet ef database update --project Infrastructure --startup-project API
```


# Insert Sample Employee Data
```bash

INSERT INTO Employees (EmployeeId, FirstName, LastName, Email, Department, IsActive, CreatedAt) VALUES
('EMP001', 'John', 'Smith', 'john.smith@company.com', 'Engineering', 1, '2024-01-01'),
('EMP002', 'Sarah', 'Johnson', 'sarah.johnson@company.com', 'Marketing', 1, '2024-01-01'),
('EMP003', 'Michael', 'Brown', 'michael.brown@company.com', 'Sales', 1, '2024-01-01'),
('EMP004', 'Emily', 'Davis', 'emily.davis@company.com', 'Human Resources', 1, '2024-01-01'),
('EMP005', 'David', 'Wilson', 'david.wilson@company.com', 'Finance', 1, '2024-01-01'),
('EMP006', 'Jennifer', 'Miller', 'jennifer.miller@company.com', 'Engineering', 1, '2024-01-01'),
('EMP007', 'Christopher', 'Taylor', 'christopher.taylor@company.com', 'IT Support', 1, '2024-01-01'),
('EMP008', 'Amanda', 'Anderson', 'amanda.anderson@company.com', 'Operations', 1, '2024-01-01'),
('EMP009', 'James', 'Thomas', 'james.thomas@company.com', 'Quality Assurance', 1, '2024-01-01'),
('EMP010', 'Jessica', 'Jackson', 'jessica.jackson@company.com', 'Customer Service', 1, '2024-01-01'),
('EMP011', 'Daniel', 'White', 'daniel.white@company.com', 'Engineering', 1, '2024-01-01'),
('EMP012', 'Lisa', 'Harris', 'lisa.harris@company.com', 'Marketing', 1, '2024-01-01'),
('EMP013', 'Matthew', 'Martin', 'matthew.martin@company.com', 'Sales', 1, '2024-01-01'),
('EMP014', 'Michelle', 'Thompson', 'michelle.thompson@company.com', 'Human Resources', 1, '2024-01-01'),
('EMP015', 'Kevin', 'Garcia', 'kevin.garcia@company.com', 'Finance', 1, '2024-01-01'),
('EMP016', 'Kimberly', 'Martinez', 'kimberly.martinez@company.com', 'Engineering', 1, '2024-01-01'),
('EMP017', 'Andrew', 'Robinson', 'andrew.robinson@company.com', 'IT Support', 1, '2024-01-01'),
('EMP018', 'Stephanie', 'Clark', 'stephanie.clark@company.com', 'Operations', 1, '2024-01-01'),
('EMP019', 'Joshua', 'Rodriguez', 'joshua.rodriguez@company.com', 'Quality Assurance', 1, '2024-01-01'),
('EMP020', 'Rebecca', 'Lewis', 'rebecca.lewis@company.com', 'Customer Service', 1, '2024-01-01'),
('EMP021', 'Brian', 'Lee', 'brian.lee@company.com', 'Engineering', 1, '2024-01-01'),
('EMP022', 'Laura', 'Walker', 'laura.walker@company.com', 'Marketing', 1, '2024-01-01'),
('EMP023', 'Jason', 'Hall', 'jason.hall@company.com', 'Sales', 1, '2024-01-01'),
('EMP024', 'Melissa', 'Allen', 'melissa.allen@company.com', 'Human Resources', 1, '2024-01-01'),
('EMP025', 'Eric', 'Young', 'eric.young@company.com', 'Finance', 1, '2024-01-01');

```




# Insert Sample Attendance Data

```bash
-- Sample check-ins for today
INSERT INTO Attendances (EmployeeId, CheckInTime, Date, Notes) VALUES
(1, DATEADD(HOUR, -8, GETUTCDATE()), CAST(GETUTCDATE() AS DATE), 'Morning shift'),
(2, DATEADD(HOUR, -8, GETUTCDATE()), CAST(GETUTCDATE() AS DATE), 'Working on marketing campaign'),
(3, DATEADD(HOUR, -7, GETUTCDATE()), CAST(GETUTCDATE() AS DATE), 'Client meetings'),
(4, DATEADD(HOUR, -8, GETUTCDATE()), CAST(GETUTCDATE() AS DATE), 'HR training session'),
(5, DATEADD(HOUR, -9, GETUTCDATE()), CAST(GETUTCDATE() AS DATE), 'Budget planning');

-- Sample completed attendance records from previous days
INSERT INTO Attendances (EmployeeId, CheckInTime, CheckOutTime, TotalHours, Date, Notes) VALUES
(1, '2024-01-15 09:00:00', '2024-01-15 17:00:00', '08:00:00', '2024-01-15', 'Regular work day'),
(1, '2024-01-16 08:55:00', '2024-01-16 17:05:00', '08:10:00', '2024-01-16', 'Team meeting'),
(2, '2024-01-15 09:15:00', '2024-01-15 17:30:00', '08:15:00', '2024-01-15', 'Campaign planning'),
(3, '2024-01-15 08:30:00', '2024-01-15 16:30:00', '08:00:00', '2024-01-15', 'Sales calls'),
(4, '2024-01-15 09:00:00', '2024-01-15 17:00:00', '08:00:00', '2024-01-15', 'Employee onboarding');
```