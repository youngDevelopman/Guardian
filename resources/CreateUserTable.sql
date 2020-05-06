CREATE TABLE [dbo].[Users](
	[UserId] uniqueidentifier NOT NULL primary key,
	[Username] varchar(50) NOT NULL unique,
	[FirstName] varchar(50) NOT NULL,
	[LastName] varchar(50) NOT NULL,
	[PasswordHash] varchar(max) NOT NULL,
	[PasswordSalt] varchar(max) NOT NULL,
	[Email] [varchar](30) NOT NULL unique
)

ALTER TABLE [dbo].[Users] ADD  DEFAULT (newid()) FOR [UserId]
GO

ALTER TABLE [dbo].[Users]  WITH CHECK ADD CHECK  (([Email] like '%___@___%.__%'))
GO
