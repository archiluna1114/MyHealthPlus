CREATE TABLE [dbo].[Appointments]
(
	[AppointmentId]		INT IDENTITY(1,1) NOT NULL,
	[AppointmentTypeId] INT,
	[Description]		NVARCHAR(MAX),
	[PatientId]			INT,
	[IsDeleted]			BIT,
	[AppointmentDate]	DATETIME2,
	[AppointmentTimeId]	INT,
	[LastEditDate]		DATETIME2 NULL,
	[CreatedDate]		DATETIME2,

	CONSTRAINT [PK_Appointments] PRIMARY KEY CLUSTERED ([AppointmentId] ASC),
	CONSTRAINT [FK_Appointments_Users_UserId] FOREIGN KEY ([PatientId]) REFERENCES [dbo].[Users] ([UserId]),
	CONSTRAINT [FK_Appointments_AppointmentTypes_AppointmentTypeId] FOREIGN KEY (AppointmentTypeId) REFERENCES [dbo].[AppointmentTypes] ([AppointmentTypeId]),
	CONSTRAINT [FK_Appointments_AppointmentTime_AppointmentTimeId] FOREIGN KEY (AppointmentTimeId) REFERENCES [dbo].[AppointmentTime] ([AppointmentTimeId])
)