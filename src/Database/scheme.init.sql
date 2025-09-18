use master
GO

create database TodoApp2
GO
use TodoApp2

CREATE TABLE Users (
    user_ID INT PRIMARY KEY IDENTITY(1,1),
    user_Name NVARCHAR(100) NULL,
    user_Email NVARCHAR(100) NULL,
    user_PasswordHash NVARCHAR(255) NOT NULL
)


CREATE TABLE Jobs (
    job_ID INT PRIMARY KEY IDENTITY(1,1),
    job_name NVARCHAR(100) NOT NULL,
    job_Update_At DATETIME NOT NULL DEFAULT GETDATE(),
    job_Create_At DATETIME NOT NULL DEFAULT GETDATE(),
    job_Members INT NOT NULL DEFAULT 1,
    job_Date_End DATETIME NOT NULL,
    job_Date_Start DATETIME NOT NULL,
    job_Delete_At DATETIME NULL,
    job_Remaining_Time DATETIME NULL,
    job_Status NVARCHAR(50) NOT NULL CHECK (job_Status IN ('Unfinish', 'Pause', 'Completed')),
    job_Flag INT NOT NULL CHECK (job_Flag IN (0,1)),
    user_ID INT NOT NULL,
    FOREIGN KEY (user_ID) REFERENCES Users(user_ID)
);




USE master;
GO
ALTER DATABASE [TodoApp2] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
DROP DATABASE [TodoApp2];
