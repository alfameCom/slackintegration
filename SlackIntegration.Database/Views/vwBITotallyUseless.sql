CREATE VIEW [dbo].[vwBITotallyUseless]
	AS SELECT bn.*, ssr.Id as ReceiverID, ssr.Receiver
  FROM [dbo].[vwBINonsense] bn
  JOIN [dbo].[SlackSuccessReceiver] ssr on bn.id=ssr.SuccessId