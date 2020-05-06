CREATE TABLE [dbo].[PoolUsers](
	[PoolUsersId] uniqueidentifier NOT NULL unique default newid(),
	[UserId] uniqueidentifier references dbo.Users (UserId),
	[UserPoolId] uniqueidentifier references dbo.UserPool (UserPoolId),
	primary key([UserId], [UserPoolId])
)
