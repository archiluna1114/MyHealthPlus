CREATE PROCEDURE [dbo].[Appointment_Insert]
(
	@AppointmentTypeId	INT,
	@AppointmentDate	DATETIME2,
	@AppointmentTimeId	INT,
	@Description		NVARCHAR(MAX),
	@PatientId			INT
)
AS
BEGIN

	INSERT INTO Appointments(
		AppointmentTypeId,
		AppointmentDate,
		AppointmentTimeId,
		[Description],
		PatientId,
		IsDeleted,
		LastEditDate,
		CreatedDate
		)
	VALUES(
		@AppointmentTypeId,
		@AppointmentDate,
		@AppointmentTimeId,
		@Description,
		@PatientId,
		0,
		GETDATE(),
		GETDATE()
	)
	
END
GO
