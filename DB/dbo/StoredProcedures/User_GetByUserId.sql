CREATE PROCEDURE [dbo].[User_GetByUserId]
(
	@UserId int
)
AS
BEGIN

	SELECT * FROM Users WHERE UserId = @UserId
END 
GO
