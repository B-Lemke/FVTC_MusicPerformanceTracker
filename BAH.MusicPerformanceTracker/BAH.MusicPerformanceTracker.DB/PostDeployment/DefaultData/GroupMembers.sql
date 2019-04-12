BEGIN
	
	DECLARE @BrodyId uniqueidentifier;
	DECLARE @LizzieId uniqueidentifier;
	DECLARE @HunterId uniqueidentifier;
	DECLARE @HaileyId uniqueidentifier;


	SELECT @BrodyId = Id FROM music.tblPerformer WHERE FirstName = 'Broderick';
	SELECT @LizzieId = Id FROM music.tblPerformer WHERE FirstName = 'Lizzie';
	SELECT @HunterId = Id FROM music.tblPerformer WHERE FirstName = 'Hunter';
	SELECT @HaileyId = Id FROM music.tblPerformer WHERE FirstName = 'Hailey';


	DECLARE @WindEnsembleId uniqueidentifier;
	DECLARE @ConcertBandId uniqueidentifier;
	DECLARE @JazzBandId uniqueidentifier;

	SELECT @WindEnsembleId = Id FROM music.tblGroup WHERE Name = 'Wind Ensemble';
	SELECT @ConcertBandId = Id FROM music.tblGroup WHERE Name = 'Concert Band';
	SELECT @JazzBandId = Id FROM music.tblGroup WHERE Name = 'Jazz Ensemble';

	DECLARE @SaxId uniqueidentifier;
	DECLARE @FluteId uniqueidentifier;
	DECLARE @PercussionId uniqueidentifier;
	DECLARE @EuphoniumId uniqueidentifier;

	SELECT @SaxId = Id FROM music.tblInstrument WHERE Description = 'Saxophone';
	SELECT @FluteId = Id FROM music.tblInstrument WHERE Description = 'Flute';
	SELECT @PercussionId = Id FROM music.tblInstrument WHERE Description = 'Trap Percussion';
	SELECT @EuphoniumId = Id FROM music.tblInstrument WHERE Description = 'Euphonium';

	INSERT INTO music.tblGroupMember(Id, PerformerId, GroupId, StartDate, EndDate, Instrument)
	VALUES
	(NEWID(), @BrodyId, @WindEnsembleId, '2015-01-12', null, @SaxId),
	(NEWID(), @BrodyId, @ConcertBandId, '2014-08-12', '2019-01-05', @FluteId),
	(NEWID(), @BrodyId, @JazzBandId, '2014-08-12', '2019-01-05', @SaxId),
	(NEWID(), @HunterId, @JazzBandId, '2016-06-12', null, @PercussionId),
	(NEWID(), @HaileyId, @JazzBandId, '2016-06-12', '2018-01-01', @FluteId),
	(NEWID(), @LizzieId, @WindEnsembleId, '2017-08-06', '2019-09-25', @SaxId),
	(NEWID(), @LizzieId, @JazzBandId, '2016-08-08', '2019-01-05', @EuphoniumId)

END
GO