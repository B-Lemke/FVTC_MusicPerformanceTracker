CREATE TABLE [music].[SearchLog] (
	[SearchId] uniqueIdentifier PRIMARY KEY NOT NULL, 
    [SearchDate]    DATETIME       NULL,
    [SearchLevel]   VARCHAR (200)  NULL,
    [SearchLogger]  VARCHAR (200)  NULL,
    [SearchQuery] VARCHAR (2000) NULL,
	[SearchTable] VARCHAR (2000) NULL
);
