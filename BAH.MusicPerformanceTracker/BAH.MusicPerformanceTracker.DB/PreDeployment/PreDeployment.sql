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
DROP TABLE IF EXISTS [music].tblGroupMember;
DROP TABLE IF EXISTS [music].tblPerformancePiece;
DROP TABLE IF EXISTS [music].tblPieceWriter;
DROP TABLE IF EXISTS [music].tblPieceGenre;
DROP TABLE IF EXISTS [music].tblPiece;
DROP TABLE IF EXISTS [music].tblComposer;
DROP TABLE IF EXISTS [music].tblComposerType;
DROP TABLE IF EXISTS [music].tblDirector;
DROP TABLE IF EXISTS [music].tblGenre;
DROP TABLE IF EXISTS [music].tblGroup;
DROP TABLE IF EXISTS [music].tblPerformance;
DROP TABLE IF EXISTS [music].tblPerformer;
DROP TABLE IF EXISTS [music].tblGender;
DROP TABLE IF EXISTS [music].tblInstrument;
DROP TABLE IF EXISTS [music].tblLocation;
DROP TABLE IF EXISTS [music].tblRace;

DROP SCHEMA IF EXISTS [music]