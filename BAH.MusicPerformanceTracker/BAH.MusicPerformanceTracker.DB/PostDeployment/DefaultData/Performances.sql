BEGIN
	INSERT INTO [music].tblPerformance(Id, Name, Description, Location, pdfPath, PerformanceDate)
	VALUES
	(NEWID(), 'Spring Concert', 'This concert was a showcase of our group''s work for the spring semester. We put on a great show freaturing classic band works.', 'Abbot Pennings Hall of Fine Arts', 'springConcert.pdf', '2019-4-1'),
	(NEWID(), 'Fall Concert', 'This concert was a showcase of our group''s work for the fall semester. We put on a great show freaturing modern band works.', 'Abbot Pennings Hall of Fine Arts', 'fallConcert.pdf', '2018-11-12'),
	(NEWID(), 'Dream Concert', 'This is a concert that Brody would love to conduct some day', 'Anywhere', null, '2020-5-3')
END
GO