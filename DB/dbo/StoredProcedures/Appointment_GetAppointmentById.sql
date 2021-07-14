CREATE PROCEDURE [dbo].[Appointment_GetAppointmentById]
(
	@AppointmentId INT	
)
AS
BEGIN
	SELECT * FROM Appointments WHERE @AppointmentId = AppointmentId;
END
GO