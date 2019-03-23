﻿CREATE TABLE [dbo].[SlackSuccess]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [Praiser] NVARCHAR(50) NOT NULL, 
    [Message] NVARCHAR(MAX) NOT NULL, 
    [SuccessDate] DATETIME2 NOT NULL DEFAULT getdate()
)
