CREATE TABLE [dbo].[Users]
(
	[UserId]		INT IDENTITY (1, 1)NOT NULL,
	[FirstName]		NVARCHAR(100),
	[LastName]		NVARCHAR(100),
	[Email]			NVARCHAR(250),
	[Address]		NVARCHAR(2500),
	[RoleId]		INT,
	[PhoneNumber]	NVARCHAR(20),
	[PasswordHash]	NVARCHAR(500),
	[PasswordSalt]	NVARCHAR(500),
	[IsDeleted]		BIT,
	[NeedToApprove]	BIT,
	[LastEditDate]	DATETIME2,
	[CreatedDate]	DATETIME2
	CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([UserId] ASC),
	CONSTRAINT [FK_Users_Role_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([RoleId])
)
