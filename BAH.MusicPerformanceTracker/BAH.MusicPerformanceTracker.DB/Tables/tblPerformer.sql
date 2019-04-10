CREATE TABLE [dbo].[tblPerformer]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [FirstName] VARCHAR(50) NOT NULL, 
    [LastName] VARCHAR(50) NOT NULL, 
    [Instrument] VARCHAR(50) NULL
)
