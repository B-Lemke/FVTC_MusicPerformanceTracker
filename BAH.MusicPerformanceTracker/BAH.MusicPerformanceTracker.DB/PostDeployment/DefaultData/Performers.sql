BEGIN
	INSERT INTO [music].tblPerformer(Id, FirstName, LastName)
	VALUES
	(NEWID(), 'Broderick', 'Lemke'),
	(NEWID(), 'Hailey', 'Mattingly'),
	(NEWID(), 'Hunter', 'Siebers'),
	(NEWID(), 'Nick', 'Sternitzky'),
	(NEWID(), 'Lizzie', 'Tesch'),
	(NEWID(), 'Erin', 'Hanke'),
	(NEWID(), 'Caitlin', 'Deuchert')
END
GO