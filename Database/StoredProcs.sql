USE TimeTracking;
GO

--Create User
CREATE PROCEDURE CreateUser
    @UserName NVARCHAR(50),
    @Email NVARCHAR(100),
    @PasswordHash NVARCHAR(255)
AS
BEGIN
    INSERT INTO [User] (UserName, Email, PasswordHash)
    VALUES (@UserName, @Email, @PasswordHash);
END
GO

--Read User
CREATE PROCEDURE GetUser
    @UserID INT
AS
BEGIN
    SELECT * FROM [User] WHERE UserID = @UserID;
END
GO

--Update User
CREATE PROCEDURE UpdateUser
    @UserID INT,
    @UserName NVARCHAR(50),
    @Email NVARCHAR(100),
    @PasswordHash NVARCHAR(255)
AS
BEGIN
    UPDATE [User]
    SET UserName = @UserName,
        Email = @Email,
        PasswordHash = @PasswordHash
    WHERE UserID = @UserID;
END
GO

--Delete User
CREATE PROCEDURE DeleteUser
    @UserID INT
AS
BEGIN
    DELETE FROM [User] WHERE UserID = @UserID;
END
GO

--Create Project
CREATE PROCEDURE CreateProject
    @UserID INT,
    @ProjectName NVARCHAR(100),
    @Description NVARCHAR(255),
    @StartDate DATETIME,
    @EndDate DATETIME
AS
BEGIN
    INSERT INTO Project (UserID, ProjectName, Description, StartDate, EndDate)
    VALUES (@UserID, @ProjectName, @Description, @StartDate, @EndDate);
END
GO

--Read Project
CREATE PROCEDURE GetProject
    @ProjectID INT
AS
BEGIN
    SELECT * FROM Project WHERE ProjectID = @ProjectID;
END
GO

--Update Project
CREATE PROCEDURE UpdateProject
    @ProjectID INT,
    @UserID INT,
    @ProjectName NVARCHAR(100),
    @Description NVARCHAR(255),
    @StartDate DATETIME,
    @EndDate DATETIME
AS
BEGIN
    UPDATE Project
    SET UserID = @UserID,
        ProjectName = @ProjectName,
        Description = @Description,
        StartDate = @StartDate,
        EndDate = @EndDate
    WHERE ProjectID = @ProjectID;
END
GO

--Delete Project
CREATE PROCEDURE DeleteProject
    @ProjectID INT
AS
BEGIN
    DELETE FROM Project WHERE ProjectID = @ProjectID;
END
GO

--Create Status
CREATE PROCEDURE CreateStatus
    @StatusName NVARCHAR(20)
AS
BEGIN
    INSERT INTO Status (StatusName)
    VALUES (@StatusName);
END
GO

--Read Status
CREATE PROCEDURE GetStatus
    @StatusID INT
AS
BEGIN
    SELECT * FROM Status WHERE StatusID = @StatusID;
END
GO

--Update Status
CREATE PROCEDURE UpdateStatus
    @StatusID INT,
    @StatusName NVARCHAR(20)
AS
BEGIN
    UPDATE Status
    SET StatusName = @StatusName
    WHERE StatusID = @StatusID;
END
GO

--Delete Status
CREATE PROCEDURE DeleteStatus
    @StatusID INT
AS
BEGIN
    DELETE FROM Status WHERE StatusID = @StatusID;
END
GO

--Create Priority
CREATE PROCEDURE CreatePriority
    @PriorityName NVARCHAR(20)
AS
BEGIN
    INSERT INTO Priority (PriorityName)
    VALUES (@PriorityName);
END
GO

--Read Priority
CREATE PROCEDURE GetPriority
    @PriorityID INT
AS
BEGIN
    SELECT * FROM Priority WHERE PriorityID = @PriorityID;
END
GO

--Update Priority
CREATE PROCEDURE UpdatePriority
    @PriorityID INT,
    @PriorityName NVARCHAR(20)
AS
BEGIN
    UPDATE Priority
    SET PriorityName = @PriorityName
    WHERE PriorityID = @PriorityID;
END
GO

--Delete Priority
CREATE PROCEDURE DeletePriority
    @PriorityID INT
AS
BEGIN
    DELETE FROM Priority WHERE PriorityID = @PriorityID;
END
GO

--Create Task
CREATE PROCEDURE CreateTask
    @ProjectID INT,
    @TaskName NVARCHAR(100),
    @Description NVARCHAR(255),
    @StatusID INT,
    @PriorityID INT
AS
BEGIN
    INSERT INTO Task (ProjectID, TaskName, Description, StatusID, PriorityID)
    VALUES (@ProjectID, @TaskName, @Description, @StatusID, @PriorityID);
END
GO

--Read Task
CREATE PROCEDURE GetTask
    @TaskID INT
AS
BEGIN
    SELECT * FROM Task WHERE TaskID = @TaskID;
END
GO

--Update Task
CREATE PROCEDURE UpdateTask
    @TaskID INT,
    @ProjectID INT,
    @TaskName NVARCHAR(100),
    @Description NVARCHAR(255),
    @StatusID INT,
    @PriorityID INT
AS
BEGIN
    UPDATE Task
    SET ProjectID = @ProjectID,
        TaskName = @TaskName,
        Description = @Description,
        StatusID = @StatusID,
        PriorityID = @PriorityID
    WHERE TaskID = @TaskID;
END
GO

--Delete Task
CREATE PROCEDURE DeleteTask
    @TaskID INT
AS
BEGIN
    DELETE FROM Task WHERE TaskID = @TaskID;
END
GO

--Create TimeEntry
CREATE PROCEDURE CreateTimeEntry
    @TaskID INT,
    @StartTime DATETIME,
    @EndTime DATETIME,
    @Notes NVARCHAR(255)
AS
BEGIN
    INSERT INTO TimeEntry (TaskID, StartTime, EndTime, Notes)
    VALUES (@TaskID, @StartTime, @EndTime, @Notes);
END
GO

--Read TimeEntry
CREATE PROCEDURE GetTimeEntry
    @TimeEntryID INT
AS
BEGIN
    SELECT * FROM TimeEntry WHERE TimeEntryID = @TimeEntryID;
END
GO

--Update TimeEntry
CREATE PROCEDURE UpdateTimeEntry
    @TimeEntryID INT,
    @TaskID INT,
    @StartTime DATETIME,
    @EndTime DATETIME,
    @Notes NVARCHAR(255)
AS
BEGIN
    UPDATE TimeEntry
    SET TaskID = @TaskID,
        StartTime = @StartTime,
        EndTime = @EndTime,
        Notes = @Notes
    WHERE TimeEntryID = @TimeEntryID;
END
GO

--Delete TimeEntry
CREATE PROCEDURE DeleteTimeEntry
    @TimeEntryID INT
AS
BEGIN
    DELETE FROM TimeEntry WHERE TimeEntryID = @TimeEntryID;
END
GO



