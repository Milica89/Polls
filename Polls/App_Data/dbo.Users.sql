CREATE TABLE [dbo].[Users] (
    [UserID] INT            IDENTITY (1, 1) NOT NULL,    
    [Name]       NVARCHAR (MAX) NOT NULL,
	[IsDeleted] BIT NOT NULL DEFAULT 0,
    CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED ([UserID] ASC)
);