BEGIN
	INSERT INTO [music].tblRace(Id, Description)
	VALUES
	(NEWID(), 'White'),
	(NEWID(), 'Hispanic'),
	(NEWID(), 'Asian'),
	(NEWID(), 'Black'),
	(NEWID(), 'Native American'),
	(NEWID(), 'Other')
END
GO