CREATE PROCEDURE [dbo].[User_Insert]
(
	@FirstName		NVARCHAR(150),
	@LastName		NVARCHAR(150),
	@Email			NVARCHAR(250),
	@Address		NVARCHAR(2500),
	@PhoneNumber	NVARCHAR(20),
	@PasswordHash	NVARCHAR(500),
	@PasswordSalt	NVARCHAR(500),
	@RoleId			INT
)
AS
BEGIN
	INSERT INTO Users (FirstName,LastName,Email,[Address],PhoneNumber,[PasswordHash],PasswordSalt,IsDeleted,RoleId,CreatedDate,LastEditDate)
	VALUES(@FirstName,@LastName,@Email,@Address,@PhoneNumber,@PasswordHash,@PasswordSalt,0,@RoleId,GETDATE(),GETDATE())

END
GO
