CREATE PROCEDURE [dbo].[Appointment_GetAppointmentByUserId]
(
	@UserId INT
)
AS
BEGIN
	SELECT	a.AppointmentId,
			AppointmentDate,
			atm.DisplayTime,
			ats.Title,
			[Description],
			ISNULL(c.Comment, 'N/A') AS 'Comment'
	FROM Appointments AS a
	INNER JOIN AppointmentTime As atm ON atm.AppointmentTimeId = a.AppointmentTimeId
	INNER JOIN AppointmentTypes AS ats ON ats.AppointmentTypeId = a.AppointmentTypeId
	LEFT JOIN Comments AS c ON c.AppointmentId = a.AppointmentId
	WHERE PatientId = @UserId;
END
GO