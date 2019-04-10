BEGIN
	INSERT INTO [music].tblPerformer(Id, FirstName, LastName, Instrument)
	VALUES
	(NEWID(), 'Broderick', 'Lemke', 'Saxophone'),
	(NEWID(), 'Hailey', 'Mattingly', 'Flute'),
	(NEWID(), 'Hunter', 'Siebers', 'Trap Percussion'),
	(NEWID(), 'Nick', 'Sternitzky', 'French Horn'),
	(NEWID(), 'Lizzie', 'Tesch', 'Saxophone'),
	(NEWID(), 'Erin', 'Hanke', 'Saxophone'),
	(NEWID(), 'Caitlin', 'Deuchert', 'Euphonium')
END