CREATE TABLE [dbo].[SlackDebugMessages]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Received] DATETIME2 NOT NULL DEFAULT getdate(), 
    [Message] NVARCHAR(MAX) NULL
)
