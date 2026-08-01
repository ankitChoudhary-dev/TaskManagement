CREATE DATABASE TaskManagement;
GO

USE TaskManagement;
GO

-- =============================================
-- 1. USERS TABLE
-- =============================================
CREATE TABLE Users
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(150) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(MAX) NOT NULL,
    Role NVARCHAR(50) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedOn DATETIME NOT NULL DEFAULT GETDATE()
);
GO

-- =============================================
-- 2. PROJECTS TABLE
-- =============================================
CREATE TABLE Projects
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(150) NOT NULL,
    Description NVARCHAR(MAX),
    StartDate DATETIME NULL,
    EndDate DATETIME NULL,
    Status NVARCHAR(50) NOT NULL,
    CreatedBy INT NOT NULL,
    CreatedOn DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_Projects_Users
        FOREIGN KEY (CreatedBy) REFERENCES Users(Id)
);
GO

-- =============================================
-- 3. TASKS TABLE
-- =============================================
CREATE TABLE Tasks
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ProjectId INT NOT NULL,
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    Status NVARCHAR(50) NOT NULL,
    Priority NVARCHAR(50) NOT NULL,
    AssignedTo INT NULL,
    DueDate DATETIME NULL,
    CreatedBy INT NOT NULL,
    CreatedOn DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_Tasks_Projects
        FOREIGN KEY (ProjectId) REFERENCES Projects(Id) ON DELETE CASCADE,

    CONSTRAINT FK_Tasks_AssignedUser
        FOREIGN KEY (AssignedTo) REFERENCES Users(Id),

    CONSTRAINT FK_Tasks_CreatedUser
        FOREIGN KEY (CreatedBy) REFERENCES Users(Id)
);
GO

-- =============================================
-- SEED DATA - USERS
-- =============================================

SET IDENTITY_INSERT Users ON;

INSERT INTO Users
(
    Id,
    Name,
    Email,
    PasswordHash,
    Role,
    IsActive,
    CreatedOn
)
VALUES
(
    1,
    'Ankit Choudhary',
    'ankit@gmail.com',
    '$2a$11$4PdifPBc.wsnX8EndVU.Me/aZyusd0tCoe.MPdF.QgjUWJrbhUS5y',
    'Admin',
    1,
    '2026-07-29 22:36:49.187'
),
(
    2,
    'Sushil Ojha',
    'sushil@gmail.com',
    '$2a$11$YVc/Fp8jB1h//VzO97bEVeDaXCJstBZ4kvSMLvt.9ZwCkpv4elU3K',
    'User',
    1,
    '2026-07-29 22:45:51.467'
);

SET IDENTITY_INSERT Users OFF;
GO

-- =============================================
-- SEED DATA - PROJECTS
-- =============================================

SET IDENTITY_INSERT Projects ON;

INSERT INTO Projects
(
    Id,
    Name,
    Description,
    StartDate,
    EndDate,
    Status,
    CreatedBy,
    CreatedOn
)
VALUES
(
    1,
    'Employee Management System',
    'Web application to manage employee records, departments, attendance, and payroll.',
    '2026-07-14',
    '2026-07-29',
    'Inactive',
    1,
    '2026-07-29 22:40:05.203'
),
(
    2,
    'Hospital Management System',
    'System for managing patients, appointments, doctors, billing, and medical records.',
    '2026-07-21',
    '2026-07-29',
    'Inactive',
    1,
    '2026-07-29 22:42:13.973'
);

SET IDENTITY_INSERT Projects OFF;
GO

-- =============================================
-- SEED DATA - TASKS
-- =============================================

SET IDENTITY_INSERT Tasks ON;

INSERT INTO Tasks
(
    Id,
    ProjectId,
    Title,
    Description,
    Status,
    Priority,
    AssignedTo,
    DueDate,
    CreatedBy,
    CreatedOn
)
VALUES
(
    1,
    1,
    'Design Database Schema',
    'Create SQL scripts for tables, constraints, and relationships for EMS.',
    'Completed',
    'High',
    1,
    '2026-07-20',
    1,
    '2026-07-29 22:45:00.000'
),
(
    2,
    1,
    'Implement JWT Authentication',
    'Build auth endpoints for Login and Register with JWT token generation.',
    'Completed',
    'High',
    2,
    '2026-07-25',
    1,
    '2026-07-29 22:46:00.000'
),
(
    3,
    1,
    'Build Angular Task Module',
    'Create reactive forms and task listing table with filter & search capabilities.',
    'In Progress',
    'High',
    2,
    '2026-08-05',
    1,
    '2026-07-29 22:48:00.000'
),
(
    4,
    1,
    'Unit Testing for API Services',
    'Write xUnit tests for project and task service handlers.',
    'Pending',
    'Medium',
    1,
    '2026-08-10',
    1,
    '2026-07-29 22:50:00.000'
),
(
    5,
    2,
    'Patient Registration Module',
    'Implement patient onboarding forms and validation.',
    'In Progress',
    'High',
    1,
    '2026-08-02',
    1,
    '2026-07-29 22:52:00.000'
),
(
    6,
    2,
    'Doctor Appointment Scheduling',
    'Develop UI and API endpoints to manage doctor slot bookings.',
    'Pending',
    'Medium',
    2,
    '2026-08-15',
    1,
    '2026-07-29 22:55:00.000'
),
(
    7,
    2,
    'Medical Billing System',
    'Integrate billing system for invoice generation upon checkout.',
    'Pending',
    'Low',
    NULL,
    '2026-08-20',
    1,
    '2026-07-29 22:58:00.000'
);

SET IDENTITY_INSERT Tasks OFF;
GO

-- =============================================
-- VIEW DATA
-- =============================================

USE TaskManagement;
GO

SELECT * FROM Users;
GO

SELECT * FROM Projects;
GO

SELECT * FROM Tasks;
GO