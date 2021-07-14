CREATE PROCEDURE [dbo].[Appointment_GetTodayAppointments]
AS 
BEGIN
	SELECT	a.AppointmentId,
			AppointmentDate,
			atm.DisplayTime,
			ats.Title,
			[Description],
			ISNULL(c.Comment, 'N/A') AS 'Comment',
			FirstName,
			LastName
	FROM Appointments AS a
	INNER JOIN AppointmentTime As atm ON atm.AppointmentTimeId = a.AppointmentTimeId
	INNER JOIN AppointmentTypes AS ats ON ats.AppointmentTypeId = a.AppointmentTypeId
	INNER JOIN Users AS u ON u.UserId = a.PatientId
	LEFT JOIN Comments AS c ON c.AppointmentId = a.AppointmentId
	WHERE CONVERT(NVARCHAR(10), AppointmentDate, 102) = CONVERT(NVARCHAR(10), GETDATE(), 102)
END
GO