/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
DROP TABLE IF EXISTS dbo.tblGroupMember;
DROP TABLE IF EXISTS dbo.tblPerformancePiece;
DROP TABLE IF EXISTS dbo.tblPieceWriter;
DROP TABLE IF EXISTS dbo.tblPieceGenre;
DROP TABLE IF EXISTS dbo.tblPiece;
DROP TABLE IF EXISTS dbo.tblComposer;
DROP TABLE IF EXISTS dbo.tblComposerType;
DROP TABLE IF EXISTS dbo.tblDirector;
DROP TABLE IF EXISTS dbo.tblGenre;
DROP TABLE IF EXISTS dbo.tblGroup;
DROP TABLE IF EXISTS dbo.tblPerformance;
DROP TABLE IF EXISTS dbo.tblPerformer;


