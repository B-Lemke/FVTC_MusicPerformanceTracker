BEGIN
	INSERT INTO [music].tblGroup(Id,[Name],[Description], FoundedDate)
	VALUES
	(NEWID(), 'Wind Ensemble', 'Wind ensemble is St. Norbert’s top instrumental ensemble, open to majors and non-majors alike. The wind ensemble is dedicated to excellence in the performance of both traditional and contemporary wind and percussion literature. The group represents St. Norbert College through concert tours, exchange concerts and performance clinics. Having performed for music educators at the Wisconsin State Music Convention and the Wisconsin Alliance for Composers, the wind ensemble has toured the state of Florida and traveled to Europe, Australia, New Zealand and the Fiji Islands.',
			'1962-08-29'),
	(NEWID(), 'Concert Band', 'Concert Band is open to all students, regardless of major, with no audition required. Typically consisting of non-music majors and music majors on secondary instruments, the atmosphere is designed for enjoyment while preparing fine literature to the best of the ensemble''s ability. Fall and Spring semesters.',
			'1982-08-26'),
	(NEWID(), 'Jazz Ensemble', 'The goal of jazz ensemble is the study and performance of jazz ensemble literature from a variety of styles and eras. All ensemble members are required to attend daily rehearsals, dress rehearsals and concerts. Smaller ensemble work is also offered through the jazz combo program and is organized based on instrumentation and student interest. All participants will work on the development of improvisational skills, effective musical style and teamwork. Fall and Spring semesters.',
			'1992-01-06')
END