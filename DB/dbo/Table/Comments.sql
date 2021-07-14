CREATE TABLE [dbo].[Comments]
(
	[CommentId] INT IDENTITY(1,1) NOT NULL,
	[AppointmentId] INT,
	[Comment]		NVARCHAR(MAX),
	[CommentDate]	DATETIME2,
	[IsDeleted]		BIT,

	CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED ([CommentId] ASC),
	CONSTRAINT [FK_Comments_Appointments_AppointmentId] FOREIGN KEY ([AppointmentId]) REFERENCES [dbo].[Appointments] ([AppointmentId])
)
