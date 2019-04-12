BEGIN
	INSERT INTO [music].tblComposerType(Id, Description)
	VALUES
	(NEWID(), 'Composer'),
	(NEWID(), 'Arranger')
END
GO