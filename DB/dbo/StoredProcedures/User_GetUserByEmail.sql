CREATE PROCEDURE [dbo].User_GetUserByEmail
(
	@Email NVARCHAR(2500)
)
AS
BEGIN
	SELECT * FROM Users WHERE Email = @Email
END
GO