CREATE TABLE [dbo].[tblComposer]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [FirstName] NCHAR(10) NOT NULL, 
    [LastName] NCHAR(10) NOT NULL, 
    [Gender] NCHAR(10) NULL, 
    [Race] NCHAR(10) NULL, 
    [Location] NCHAR(10) NULL, 
    [Bio] NCHAR(10) NULL
)
