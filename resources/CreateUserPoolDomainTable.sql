CREATE TABLE [dbo].[UserPoolDomain](
	[UserPoolDomainId] uniqueidentifier not null primary key default newid(),
	[UserPoolId] uniqueidentifier references dbo.UserPool (UserPoolId) on delete cascade, 
	[Domain] varchar(260) not null unique
)