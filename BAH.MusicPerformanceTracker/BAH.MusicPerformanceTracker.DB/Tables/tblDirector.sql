﻿CREATE TABLE [music].[tblDirector]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [FirstName] VARCHAR(50) NOT NULL, 
    [LastName] VARCHAR(50) NOT NULL, 
    [Bio] TEXT NOT NULL, 
    [BirthDate] DATETIME NOT NULL
)
