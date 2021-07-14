CREATE PROCEDURE [dbo].[Comment_Insert]
(
	@AppointmentId	INT,
	@Comments		NVARCHAR(MAX),
	@CommentDate	DATETIME2
)
AS
BEGIN
	INSERT INTO Comments(AppointmentId,Comment,CommentDate,IsDeleted)
	VALUES(@AppointmentId,@Comments,@CommentDate,0);
END
GO