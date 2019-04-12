﻿CREATE TABLE [music].[tblPiece]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(50) NOT NULL, 
    [GradeLevel] VARCHAR(12) NULL, 
    [YearWritten] INT NULL, 
    [PefromanceNotes] TEXT NULL
)
