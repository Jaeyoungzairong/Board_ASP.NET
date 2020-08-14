CREATE PROCEDURE [dbo].[Procedure1]
	@param1 int = 0,
	@param2 int
AS
	SELECT [Id], [Name], [Email], [Title], [PostDate], [PostIp], [Content], [Password], [ReadCount], [Encoding], [Homepage], [ModifyDate], [ModifyIp] FROM Boards
RETURN 0
