CREATE PROCEDURE [dbo].[Appointment_GetAvailableTimeSlotBySelectedDate]
(
	@SelectedDate DATETIME2
)
AS
BEGIN
	SELECT * FROM AppointmentTime AS ats
	INNER JOIN Appointments AS a ON a.AppointmentTimeId = ats.AppointmentTimeId
	WHERE CONVERT(NVARCHAR(10), AppointmentDate, 102) = CONVERT(NVARCHAR(10), @SelectedDate, 102) 
END
GO