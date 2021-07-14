CREATE PROCEDURE [dbo].[User_GetUserList]
AS
BEGIN
	SELECT * FROM Users WHERE IsDeleted != 1;
END
GO
