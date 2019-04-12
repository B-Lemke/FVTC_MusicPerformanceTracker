BEGIN
	DECLARE @MaleId uniqueidentifier;
	DECLARE @FemaleId uniqueidentifier;

	SELECT @MaleId = Id FROM music.tblgender WHERE Description = 'Male';
	SELECT @FemaleId = Id FROM music.tblgender WHERE Description = 'Female';

	DECLARE @WhiteId uniqueidentifier;
	SELECT @WhiteId = Id FROM music.tblRace WHERE Description = 'White';

	DECLARE @RussiaId uniqueidentifier;
	DECLARE @WashingtonId uniqueidentifier;
	DECLARE @CaliforniaId uniqueidentifier;
	DECLARE @AustraliaId uniqueidentifier;
	DECLARE @WisconsinId uniqueidentifier;

	SELECT @RussiaId = Id FROM music.tblLocation WHERE Description = 'Russia';
	SELECT @WashingtonId = Id FROM music.tblLocation WHERE Description = 'Washington';
	SELECT @CaliforniaId = Id FROM music.tblLocation WHERE Description = 'California';
	SELECT @AustraliaId = Id FROM music.tblLocation WHERE Description = 'Australia';
	SELECT @WisconsinId = Id FROM music.tblLocation WHERE Description = 'Wisconsin';

	INSERT INTO [music].tblComposer(Id, FirstName, LastName, LocationId, RaceId, GenderId, Bio)
	VALUES 
	(NEWID(), 'Modest', 'Mussorgsky', @RussiaId, @WhiteId, @MaleId, 'Modest Mussorgsky was a Russian Composer and a member of "The Five". He wrote music during the romantic period and strove to establsih a unique russian identity through music. Many of his works were inspired by Russian folk music.'),
	(NEWID(), 'Alex', 'Shapiro', @WashingtonId, @WhiteId, @FemaleId, 'Composer Alex Shapiro aligns note after note with the hope that at least a few of them will actually sound good next to each other. Her persistence at this activity, as well as non-fiction music writing, arts advocacy, public speaking, wildlife photography, and the shameless instigation of insufferable puns on Facebook, has led to a happy life. Drawing from a broad musical palette that giddily ignores genre, Alex''s acoustic and electroacoustic works are published by Activist Music LLC, performed and broadcast daily, and can be found on nearly thirty commercial releases from record labels around the world.'),
	(NEWID(), 'Eric', 'Whitacre', @CaliforniaId, @WhiteId, @MaleId, 'Grammy-winning composer and conductor Eric Whitacre is one of the most popular musicians of his generation. His concert music has been performed throughout the world by millions of amateur and professional musicians alike, while his ground-breaking Virtual Choirs have united singers from over 120 different countries.'),
	(NEWID(), 'Percy', 'Grainger', @AustraliaId, @WhiteId, @MaleId, 'Percy Aldridge Grainger was an Australian-born composer, arranger, and pianist. Over the course of a long and innovative career he played a prominent role in the revival of interest in British folk music in the early years of the 20th century. He also made many adaptations of other composers'' works. Although much of his work was experimental and unusual, the piece with which he is most generally associated is his piano arrangement of the folk-dance tune "Country Gardens."'),
	(NEWID(), 'Paul', 'Lavender', @WisconsinId, @WhiteId, @MaleId, 'As Vice President of Instrumental Publications for Hal Leonard Corporation, Paul Lavender directs the product development and marketing of Hal Leonard’s extensive catalog of performance publications for orchestra, concert band, marching band, and jazz ensemble, as well as instrumental books, collections and methods.
Paul supervises the creative work of many of the industry’s most respected composers and arrangers, publishing over 600 new instrumental publications each year. His longtime association with renowned film composer John Williams has produced the prestigious John Williams Signature Series, featuring Williams’ authentic film scores and concert music for professional orchestras. In addition, Paul has served as music supervisor and arranger for several of Williams’ concerts and special events, including the 2003 and 2008 Marine Band Anniversary Concerts, the 2004 Rose Bowl, and the 2004 Kennedy Center Honors program (televised on CBS).
Also a prolific writer, Paul has contributed more than 1,200 arrangements and compositions to the educational and concert repertoire, and he continues to be one of the most widely played writers today. Most recently, he has received international acclaim with two notable transcriptions for symphonic band: Leonard Bernstein''s Symphonic Dances from West Side Story, and Pictures at an Exhibition by Modest Mussorgsky and Maurice Ravel. Both works were written for and recorded by the world-renowned United States Marine Band, and performed on national tours under the direction of Colonel Michael J. Colburn.')


END 
GO