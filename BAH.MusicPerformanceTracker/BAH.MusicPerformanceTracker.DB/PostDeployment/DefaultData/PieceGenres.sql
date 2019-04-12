BEGIN
	DECLARE @RockMusicId uniqueidentifier;
	DECLARE @PicturesId uniqueidentifier;
	DECLARE @LinconlshireId uniqueidentifier;


	SELECT @RockMusicId = Id FROM music.tblPiece WHERE Name = 'Rock Music';
	SELECT @PicturesId = Id FROM music.tblPiece WHERE Name = 'Pictures At An Exhibition';
	SELECT @LinconlshireId = Id FROM music.tblPiece WHERE Name = 'Lincolnshire Posy';

	DECLARE @MinimalistId uniqueidentifier;
	DECLARE @ElectricId uniqueidentifier;
	DECLARE @FolkSongId uniqueidentifier;
	DECLARE @RomaticID uniqueidentifier;
	DECLARE @NationalisticId uniqueidentifier;
	DECLARE @ArrangementId uniqueidentifier;

	SELECT @MinimalistId = Id FROM music.tblGenre WHERE [Description] = 'Minimalism';
	SELECT @ElectricId = Id FROM music.tblGenre WHERE [Description] = 'Electric';
	SELECT @FolkSongId = Id FROM music.tblGenre WHERE [Description] = 'Folk Song';
	SELECT @RomaticID = Id FROM music.tblGenre WHERE [Description] = 'Romantic';
	SELECT @NationalisticId = Id FROM music.tblGenre WHERE [Description] = 'Nationalistic';
	SELECT @ArrangementId = Id FROM music.tblGenre WHERE [Description] = 'Arrangement';


	INSERT INTO [music].tblPieceGenre(Id, PieceId, GenreId)
	VALUES
	(NEWID(), @RockMusicId, @MinimalistId),
	(NEWID(), @RockMusicId, @ElectricId),
	(NEWID(), @PicturesId, @NationalisticId),
	(NEWID(), @PicturesId, @ArrangementId),
	(NEWID(), @PicturesId, @RomaticID),
	(NEWID(), @LinconlshireId, @FolkSongId)
END
GO