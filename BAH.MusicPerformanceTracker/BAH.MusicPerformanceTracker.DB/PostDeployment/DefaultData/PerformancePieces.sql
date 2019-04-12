BEGIN

	DECLARE @RockMusicId uniqueidentifier;
	DECLARE @PicturesId uniqueidentifier;
	DECLARE @LinconlshireId uniqueidentifier;

	SELECT @RockMusicId = Id FROM music.tblPiece WHERE Name = 'Rock Music';
	SELECT @PicturesId = Id FROM music.tblPiece WHERE Name = 'Pictures At An Exhibition';
	SELECT @LinconlshireId = Id FROM music.tblPiece WHERE Name = 'Lincolnshire Posy';

	DECLARE @SpringConcertId uniqueidentifier;
	DECLARE @DreamConcertId uniqueidentifier;
	DECLARE @FallConcertId uniqueidentifier;

	SELECT @SpringConcertId = Id FROM music.tblPerformance WHERE Name = 'Spring Concert';
	SELECT @FallConcertId = Id FROM music.tblPerformance WHERE Name = 'Fall Concert';
	SELECT @DreamConcertId = Id FROM music.tblPerformance WHERE Name = 'Dream Concert';

	DECLARE @WindEnsembleId uniqueidentifier;
	DECLARE @ConcertBandId uniqueidentifier;

	SELECT @WindEnsembleId = Id FROM music.tblGroup WHERE Name = 'Wind Ensemble';
	SELECT @ConcertBandId = Id FROM music.tblGroup WHERE Name = 'Concert Band';

	DECLARE @KlickmanId uniqueidentifier;
	DECLARE @BrodyId uniqueidentifier;

	SELECT @KlickmanId = Id FROM music.tblDirector WHERE FirstName = 'Philip';
	SELECT @BrodyId = Id FROM music.tblDirector WHERE FirstName = 'Broderick';

	INSERT INTO [music].tblPerformancePiece(Id, PerformanceId, PieceId, GroupId, DirectorId, MP3Path, Notes)
	VALUES
		(NEWID(), @SpringConcertId, @PicturesId, @WindEnsembleId, @KlickmanId, null, 'We performed all movements'),
		(NEWID(), @SpringConcertId, @LinconlshireId, @WindEnsembleId, @KlickmanId, null, 'We performed movements I, II, IV, V, and VI'),
		(NEWID(), @FallConcertId, @RockMusicId, @ConcertBandId, @KlickmanId, null, null),
		(NEWID(), @DreamConcertId, @PicturesId, @WindEnsembleId, @BrodyId, null, null),
		(NEWID(), @DreamConcertId, @LinconlshireId, @WindEnsembleId, @BrodyId, null, null),
		(NEWID(), @DreamConcertId, @RockMusicId, @WindEnsembleId, @BrodyId, null, null)

END
GO