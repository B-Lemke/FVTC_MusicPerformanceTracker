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

	INSERT INTO music.tblGroupMember(Id, PerformerId, GroupId, StartDate, EndDate)
	VALUES
	(NEWID(), @BrodyId, @WindEnsembleId, '2015-01-12', null),
	(NEWID(), @BrodyId, @ConcertBandId, '2014-08-12', '2019-01-05'),
	(NEWID(), @BrodyId, @JazzBandId, '2014-08-12', '2019-01-05'),
	(NEWID(), @HunterId, @JazzBandId, '2016-06-12', null),
	(NEWID(), @HaileyId, @JazzBandId, '2016-06-12', '2018-01-01'),
	(NEWID(), @LizzieId, @WindEnsembleId, '2017-08-06', '2019-09-25'),
	(NEWID(), @LizzieId, @JazzBandId, '2016-08-08', '2019-01-05')

END
GO