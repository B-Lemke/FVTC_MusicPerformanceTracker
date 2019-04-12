BEGIN
	
	DECLARE @ShapiroId uniqueidentifier;
	DECLARE @MussorgskyId uniqueidentifier;
	DECLARE @LavenderId uniqueidentifier;
	DECLARE @GraingerId uniqueidentifier;

	DECLARE @RockMusicId uniqueidentifier;
	DECLARE @PicturesId uniqueidentifier;
	DECLARE @LinconlshireId uniqueidentifier;

	DECLARE @ComposerId uniqueidentifier;
	DECLARE @ArrangerId uniqueidentifier;

	SELECT @ShapiroId = Id FROM music.tblComposer WHERE LastName = 'Shapiro';
	SELECT @MussorgskyId = Id FROM music.tblComposer WHERE LastName = 'Mussorgsky';
	SELECT @LavenderId = Id FROM music.tblComposer WHERE LastName = 'Lavender';
	SELECT @GraingerId = Id FROM music.tblComposer WHERE LastName = 'Grainger';

	SELECT @RockMusicId = Id FROM music.tblPiece WHERE Name = 'Rock Music';
	SELECT @PicturesId = Id FROM music.tblPiece WHERE Name = 'Pictures At An Exhibition';
	SELECT @LinconlshireId = Id FROM music.tblPiece WHERE Name = 'Lincolnshire Posy';

	SELECT @ComposerId = Id FROM music.tblComposerType WHERE Description = 'Composer';
	SELECT @ArrangerId = Id FROM music.tblComposerType WHERE Description = 'Arranger';



	INSERT INTO [music].tblPieceWriter(Id, PieceId, ComposerId,  ComposerTypeId)
	VALUES
	(NEWID(), @RockMusicId, @ShapiroId, @ComposerId),
	(NEWID(), @PicturesId, @MussorgskyId, @ComposerId),
	(NEWID(), @PicturesId, @LavenderId, @ArrangerId),
	(NEWID(), @LinconlshireId, @GraingerId, @ComposerId)
END
GO