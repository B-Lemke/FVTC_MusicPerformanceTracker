BEGIN
	INSERT INTO [music].tblGender(Id, Description)
	VALUES
	(NEWID(), 'Male'),
	(NEWID(), 'Female'),
	(NEWID(), 'Other')
END
GO