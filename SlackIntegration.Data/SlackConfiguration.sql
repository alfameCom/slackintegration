CREATE TABLE [dbo].[SlackConfiguration]
(
	[Id] BIGINT NOT NULL PRIMARY KEY, 
    [Key] VARCHAR(50) NOT NULL, 
    [Value] VARCHAR(MAX) NOT NULL
)
