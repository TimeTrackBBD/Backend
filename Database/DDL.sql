CREATE DATABASE TimeTracking;
GO

USE TimeTracking;
GO

CREATE TABLE [User] (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    UserName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL
);
GO

CREATE TABLE [Project] (
    ProjectID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,
    ProjectName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255),
    StartDate DATETIME NOT NULL,
    EndDate DATETIME,
    FOREIGN KEY (UserID) REFERENCES [User](UserID)
);
GO

--('Pending'), ('In Progress'), ('Completed')
CREATE TABLE [Status] (
    StatusID INT IDENTITY(1,1) PRIMARY KEY,
    StatusName NVARCHAR(20) NOT NULL UNIQUE
);
GO

  -- Priority NVARCHAR(20) NOT NULL CHECK (Priority IN ('Low', 'Medium', 'High')),
CREATE TABLE [Priority] (
    PriorityID INT IDENTITY(1,1) PRIMARY KEY,
    PriorityName NVARCHAR(20) NOT NULL UNIQUE
);
GO

CREATE TABLE [Task] (
    TaskID INT IDENTITY(1,1) PRIMARY KEY,
    ProjectID INT NOT NULL,
    TaskName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255),
    StatusID INT NOT NULL,
    PriorityID INT NOT NULL,
    FOREIGN KEY (ProjectID) REFERENCES Project(ProjectID),
	FOREIGN KEY (StatusID) REFERENCES Status(StatusID),
	FOREIGN KEY (PriorityID) REFERENCES Priority(PriorityID)
);
GO

CREATE TABLE [TimeEntry] (
    TimeEntryID INT IDENTITY(1,1) PRIMARY KEY,
    TaskID INT NOT NULL,
    StartTime DATETIME NOT NULL,
    EndTime DATETIME NOT NULL,
    Duration AS DATEDIFF(MINUTE, StartTime, EndTime) PERSISTED,
    Notes NVARCHAR(255),
    FOREIGN KEY (TaskID) REFERENCES Task(TaskID)
);
GO

