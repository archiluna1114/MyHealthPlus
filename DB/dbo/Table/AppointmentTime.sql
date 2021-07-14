CREATE TABLE [dbo].[AppointmentTime]
(
	AppointmentTimeId	INT IDENTITY(1,1) NOT NULL,
	DisplayTime			NVARCHAR(20),

	CONSTRAINT [PK_AppointmentTime] PRIMARY KEY CLUSTERED ([AppointmentTimeId] ASC)
)
