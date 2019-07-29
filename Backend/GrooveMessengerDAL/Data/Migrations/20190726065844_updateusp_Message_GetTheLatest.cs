using Microsoft.EntityFrameworkCore.Migrations;

namespace GrooveMessengerDAL.Migrations
{
    public partial class updateusp_Message_GetTheLatest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"
                    -- =============================================
                    -- Author:		TrucPhan
                    -- Create date: 23-07-2019
                    -- Description:	GetLastestMessageOfAConversation
                    -- =============================================
                    ALTER PROCEDURE [dbo].[msp_GetLastestMessageOfAConversation] 
	                    -- Add the parameters for the stored procedure here
	                    @UserId uniqueIdentifier
                    AS
                    BEGIN
	                    -- SET NOCOUNT ON added to prevent extra result sets from
	                    -- interfering with SELECT statements.
	                    SET NOCOUNT ON;
						-- Insert statements for procedure here
	                    DECLARE @TempContactChatBox TABLE (
										                    Id uniqueIdentifier
									                    )
	                    DECLARE @Result TABLE ( ConvId uniqueIdentifier not null,
							                    ContactId nvarchar(450) not null,
							                    DisplayName nvarchar(150) not null,
							                    Unread nvarchar(max) not null,
							                    LastMessage nvarchar(1000) not null,
							                    LastMessageTime datetime2(7) not null									
							                    )
	                    INSERT INTO @TempContactChatBox SELECT C.Id
	                    FROM Participant P 
	                    INNER JOIN [Conversation] C on P.ConversationId = C.Id
	                    WHERE P.UserId = @UserId

	                    WHILE(1 = 1)
                        BEGIN
		                    DECLARE @ConversationId uniqueIdentifier 
		                    SET @ConversationId = (SELECT TOP(1) Id FROM @TempContactChatBox)
		                    
		
		                    DECLARE @ContactId nvarchar(450)
		                    SET @ContactId = (
								SELECT TOP(1)P.UserId
								FROM Participant P 
								INNER JOIN [Conversation] C on P.ConversationId = C.Id
								WHERE P.UserId != @UserId AND P.ConversationId = @ConversationId)
		
							DECLARE @ContactInfoId uniqueIdentifier 
		                    SET @ContactInfoId = (
			                    SELECT UI.Id
								FROM UserInfo UI
			                    WHERE UI.UserId = @ContactId
		                    ) 
		
		                    DECLARE @DisplayName nvarchar(150) 
		                    SET @DisplayName = (SELECT UI.DisplayName FROM UserInfo UI WHERE UI.Id = @ContactInfoId)
		
		                    DECLARE @LastMessage nvarchar(1000) 
                            SET @LastMessage = (
								                    SELECT TOP(1) M.Content
								                    FROM UserInfoContact UC
								                    INNER JOIN UserInfo UI ON UI.Id = UC.UserId
								                    INNER JOIN Message M ON M.ConversationId = @ConversationId
								                    WHERE UI.UserId = @UserId
								                    ORDER BY M.CreatedOn DESC)
                            DECLARE @LastMessageTime datetime2(7) 
                            SET @LastMessageTime = (	SELECT TOP(1) M.CreatedOn
									                    FROM UserInfoContact UC
									                    INNER JOIN UserInfo UI ON UI.Id = UC.UserId
									                    INNER JOIN Message M ON M.ConversationId = @ConversationId
									                    WHERE UI.UserId = @UserId
									                    ORDER BY M.CreatedOn DESC)
                            DECLARE @Unread nvarchar(max) 
		                    SET @Unread = ( SELECT Count(*) 
											FROM Message 
											WHERE ConversationId = @ConversationId AND SenderId !=@UserId
											AND isnull(SeenBy,'') NOT LIKE '%'+cast(@UserId as nvarchar(255))+'%')
							IF(@Unread > 99) SET @Unread = '99+' 
							ELSE IF (@Unread) = 0 SET @Unread = ''
                            IF @ConversationId IS NULL
                                BREAK
							
							INSERT INTO @Result SELECT @ConversationId,@ContactId,@DisplayName,@Unread,@LastMessage,@LastMessageTime

                            DELETE TOP(1) FROM @TempContactChatBox

                        END
	                    SELECT * FROM @Result ORDER BY LastMessageTime DESC
                    END
					Go
                   EXEC sp_rename 'dbo.msp_GetLastestMessageOfAConversation', 'usp_Message_GetTheLatest';
";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
                DROP PROCEDURE [dbo].[usp_Message_GetTheLatest]
                GO
                "
            );
        }
    }
}