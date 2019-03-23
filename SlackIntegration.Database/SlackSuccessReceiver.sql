CREATE TABLE [dbo].[SlackSuccessReceiver]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
	[SuccessId] BIGINT NOT NULL,
    [Receiver] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_SlackSuccessReceiver_SlackSuccess] FOREIGN KEY ([SuccessId]) REFERENCES [SlackSuccess]([Id]) 

)
