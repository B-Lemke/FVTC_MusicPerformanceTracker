CREATE TABLE [dbo].[tblPerformance]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [PerformanceDate] DATETIME NOT NULL, 
    [Location] VARCHAR(255) NOT NULL, 
    [Name] VARCHAR(50) NOT NULL, 
    [pdfPath] VARCHAR(255) NULL, 
    [Description] TEXT NULL
)
