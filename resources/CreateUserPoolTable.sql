CREATE TABLE [dbo].[UserPool](
	[UserPoolId] uniqueidentifier NOT NULL primary key default newid(),
	[Name] varchar(50) NOT NULL,
	[CreationDate] datetime NOT NULL
)