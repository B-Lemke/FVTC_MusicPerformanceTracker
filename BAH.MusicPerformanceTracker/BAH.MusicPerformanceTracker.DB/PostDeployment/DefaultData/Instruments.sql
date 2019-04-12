BEGIN
	INSERT INTO [music].tblInstrument(Id, Description)
	VALUES
	(NEWID(), 'Saxophone'),
	(NEWID(), 'Flute'),
	(NEWID(), 'Trap Percussion'),
	(NEWID(), 'Euphonium'),
	(NEWID(), 'Tuba'),
	(NEWID(), 'Mallet Percussion')
END
GO