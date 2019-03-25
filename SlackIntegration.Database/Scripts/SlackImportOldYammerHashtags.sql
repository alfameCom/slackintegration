CREATE PROCEDURE [dbo].[SlackImportOldYammerHashtags]
AS
	DECLARE @Messageid bigint;
	DECLARE @Sender nvarchar(50);
	DECLARE @comment nvarchar(max);
	DECLARE @createdat datetime2;
	DECLARE @successTable table( id bigint );
	DECLARE @receiver nvarchar(50);


	DECLARE db_cursor CURSOR FOR 
		SELECT messageid, sender, comment, createdat
		FROM [dbo].[YammerHashtags]
		WHERE message like '#success%'
		GROUP by messageid, sender, comment, createdat
		ORDER by messageid

	open db_cursor
	FETCH NEXT FROM db_cursor INTO @MessageId, @Sender, @comment, @createdat
	WHILE @@FETCH_STATUS=0
	BEGIN
		DECLARE @msg1 varchar(max) = @sender + ' praised with message "' + @comment + '" at ' + convert(varchar(max), @createdat, 120);
		INSERT INTO SlackSuccess(Praiser, Message, SuccessDate) 
		OUTPUT INSERTED.ID INTO @SuccessTable
		VALUES (@Sender, @comment, @createdat)
		RAISERROR (@msg1, 0, 1) WITH NOWAIT

		DECLARE @tmpSuccess bigint;
		select @tmpSuccess=id from @successTable;

		DECLARE @msg3 varchar(max) = 'SuccessId ' + convert(varchar(max), @tmpSuccess) + ' hashtagid ' + convert(varchar(max), @messageid);
		RAISERROR (@msg3, 0, 1) WITH NOWAIT

		--break;
		declare db_cursor2 CURSOR FOR
			SELECT recipient
			FROM [dbo].[YammerHashtags]
			WHERE messageID=@MessageId

		open db_cursor2
		FETCH NEXT FROM db_cursor2 into @receiver
		WHILE @@FETCH_STATUS=0
		BEGIN
			DECLARE @msg2 varchar(max) = @receiver + ' received praise';
			INSERT INTO slacksuccessreceiver(successid, receiver) values (@tmpSuccess, @receiver) 
			RAISERROR (@msg2, 0, 1) WITH NOWAIT
			FETCH NEXT FROM db_cursor2 into @receiver
		END

		CLOSE db_cursor2
		DEALLOCATE db_cursor2
		FETCH NEXT FROM db_cursor INTO @MessageId, @Sender, @comment, @createdat
	END
	CLOSE db_cursor
	DEALLOCATE db_cursor
RETURN 0
