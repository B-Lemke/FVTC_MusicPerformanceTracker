BEGIN
	INSERT INTO [music].tblLocation(Id, Description)
	VALUES
	(NEWID(), 'Russia'),
	(NEWID(), 'Washington'),
	(NEWID(), 'California'),
	(NEWID(), 'Australia'),
	(NEWID(), 'Wisconsin')
END
GO