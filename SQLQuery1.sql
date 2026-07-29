CREATE DATABASE TaskManagement;
GO

USE TaskManagement;
GO


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
    FOREIGN KEY (CreatedBy)
    REFERENCES Users(Id)
);
GO



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
    FOREIGN KEY (ProjectId)
    REFERENCES Projects(Id),


    CONSTRAINT FK_Tasks_AssignedUser
    FOREIGN KEY (AssignedTo)
    REFERENCES Users(Id),


    CONSTRAINT FK_Tasks_CreatedUser
    FOREIGN KEY (CreatedBy)
    REFERENCES Users(Id)
);
GO



CREATE TABLE TaskComments
(
    Id INT IDENTITY(1,1) PRIMARY KEY,

    TaskId INT NOT NULL,

    Comment NVARCHAR(MAX) NOT NULL,

    CommentBy INT NOT NULL,

    CreatedOn DATETIME NOT NULL DEFAULT GETDATE(),


    CONSTRAINT FK_TaskComments_Tasks
    FOREIGN KEY (TaskId)
    REFERENCES Tasks(Id),


    CONSTRAINT FK_TaskComments_Users
    FOREIGN KEY (CommentBy)
    REFERENCES Users(Id)
);
GO



CREATE TABLE RefreshTokens
(
    Id INT IDENTITY(1,1) PRIMARY KEY,

    UserId INT NOT NULL,

    Token NVARCHAR(MAX) NOT NULL,

    ExpiryDate DATETIME NOT NULL,

    CreatedOn DATETIME NOT NULL DEFAULT GETDATE(),


    CONSTRAINT FK_RefreshTokens_Users
    FOREIGN KEY (UserId)
    REFERENCES Users(Id)
);
GO