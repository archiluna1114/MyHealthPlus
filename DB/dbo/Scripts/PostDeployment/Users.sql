INSERT INTO Users
(
	FirstName,
	LastName,
	[Address],
	Email,
	PhoneNumber,
	PasswordHash,
	PasswordSalt,
	RoleId,
	IsDeleted,
	LastEditDate,
	CreatedDate
)
VALUES ('John', 'Doe', '4118 Carlota De Leon St.','admin@myhealthplus.com','333664832','1yC/0YRfuzUM2eGlTAxaxVJ3H87tp3dQ/Qg/OVFpos8=','XgHOXfsQerf4dSvIIcKZu30=',1,0,GETDATE(),GETDATE()),
('Jane', 'Staff', '4118 Carlota De Leon St.','staff@myhealthplus.com','333664832','1yC/0YRfuzUM2eGlTAxaxVJ3H87tp3dQ/Qg/OVFpos8=','XgHOXfsQerf4dSvIIcKZu30=',2,0,GETDATE(),GETDATE()),
('Johnny', 'Doe', '4118 Carlota De Leon St.','user@myhealthplus.com','333664832','1yC/0YRfuzUM2eGlTAxaxVJ3H87tp3dQ/Qg/OVFpos8=','XgHOXfsQerf4dSvIIcKZu30=',3,0,GETDATE(),GETDATE());


GO




