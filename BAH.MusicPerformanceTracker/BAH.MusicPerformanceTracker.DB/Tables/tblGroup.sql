CREATE TABLE [music].[tblGroup]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(255) NOT NULL, 
    [FoundedDate] DATETIME NULL, 
    [Description] TEXT NOT NULL
)
