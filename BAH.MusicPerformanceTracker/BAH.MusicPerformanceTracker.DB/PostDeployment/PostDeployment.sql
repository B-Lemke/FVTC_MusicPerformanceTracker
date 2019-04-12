/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

--Tables that don't rely on others
:r .\DefaultData\Instruments.sql
:r .\DefaultData\Genders.sql
:r .\DefaultData\Genres.sql
:r .\DefaultData\Locations.sql
:r .\DefaultData\Races.sql
:r .\DefaultData\ComposerTypes.sql
:r .\DefaultData\Performers.sql
:r .\DefaultData\Groups.sql
:r .\DefaultData\Pieces.sql
:r .\DefaultData\Directors.sql
:r .\DefaultData\Performances.sql

--Uses piece genres
:r .\DefaultData\PieceGenres.sql
GO

--Uses location race gender
:r .\DefaultData\Composers.sql
GO

--Uses Performer, Group, Instrument
:r .\DefaultData\GroupMembers.sql
GO
--Uses piece, composer, composertype
:r .\DefaultData\PieceWriters.sql
GO
--Uses piece, director, group, and performance
:r .\DefaultData\PerformancePieces.sql