CREATE TABLE [dbo].[AppointmentTypes]
(
	[AppointmentTypeId]	INT IDENTITY(1,1) NOT NULL,
	[Title]				NVARCHAR(100),

	CONSTRAINT [PK_AppointmentTypes] PRIMARY KEY CLUSTERED ([AppointmentTypeId] ASC),
)
