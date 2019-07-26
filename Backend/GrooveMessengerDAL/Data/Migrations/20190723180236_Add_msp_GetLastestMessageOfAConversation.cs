using Microsoft.EntityFrameworkCore.Migrations;

namespace GrooveMessengerDAL.Migrations
{
    public partial class Add_msp_GetLastestMessageOfAConversation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"
                    -- =============================================
                    -- Author:		TrucPhan
                    -- Create date: 23-07-2019
                    -- Description:	GetLastestMessageOfAConversation
                    -- =============================================
                    CREATE PROCEDURE msp_GetLastestMessageOfAConversation 
	                    -- Add the parameters for the stored procedure here
	                    @UserId uniqueIdentifier
                    AS
                    BEGIN
	                    -- SET NOCOUNT ON added to prevent extra result sets from
	                    -- interfering with SELECT statements.
	                    SET NOCOUNT ON;

                        -- Insert statements for procedure here
	                    DECLARE @TempContactChatBox TABLE (
										                    Id uniqueIdentifier,
										                    Name nvarchar(50) not null,
										                    Avatar nvarchar(50) not null,
										                    CreatedBy nvarchar null,
										                    CreatedOn datetime2(7) null,
										                    Deleted  bit null,
										                    UpdatedBy nvarchar(MAX) null,
										                    UpdatedOn  datetime2(7) null
									                    )
	                    DECLARE @Result TABLE ( ConvId uniqueIdentifier,
							                    ContactId nvarchar(450),
							                    DisplayName nvarchar(150),
							                    Unread nvarchar(max),
							                    LastMessage nvarchar(1000),
							                    LastMessageTime datetime2(7)										
							                    )
	                    INSERT INTO @TempContactChatBox SELECT C.Id,C.Name,C.Avatar,C.CreatedBy,C.CreatedOn,C.Deleted,C.UpdatedBy,C.UpdatedOn
	                    FROM Participant P 
	                    INNER JOIN [Conversation] C on P.ConversationId = C.Id
	                    WHERE P.UserId = @UserId

	                    WHILE(1 = 1)
                        BEGIN
		                    DECLARE @ConversationId uniqueIdentifier 
		                    SET @ConversationId = (SELECT TOP(1) Id FROM @TempContactChatBox)
		                    DECLARE @ContactIdTemp uniqueIdentifier
		                    SET @ContactIdTemp = (
			                    SELECT UC.ContactId
			                    FROM UserInfoContact UC
			                    INNER JOIN UserInfo UI ON UI.Id = UC.UserId
			                    INNER JOIN Conversation C ON C.Id = @ConversationId
			                    WHERE UI.UserId = @UserId
		                    ) 
		
		                    DECLARE @ContactId uniqueIdentifier
		                    SET @ContactId = (SELECT UI.UserId FROM UserInfo UI WHERE UI.Id = @ContactIdTemp)
		
		                    DECLARE @DisplayName nvarchar(150)
		                    SET @DisplayName = (SELECT UI.DisplayName FROM UserInfo UI WHERE UI.Id = @ContactIdTemp)
		
		                    DECLARE @LastMessage nvarchar(1000)
                            SET @LastMessage = (
								                    SELECT TOP(1) M.Content
								                    FROM UserInfoContact UC
								                    INNER JOIN UserInfo UI ON UI.Id = UC.UserId
								                    INNER JOIN Message M ON M.ConversationId = @ConversationId
								                    WHERE UI.UserId = @UserId
								                    ORDER BY M.CreatedOn DESC)
                            DECLARE @LastMessageTime datetime2
                            SET @LastMessageTime = (	SELECT TOP(1) M.CreatedOn
									                    FROM UserInfoContact UC
									                    INNER JOIN UserInfo UI ON UI.Id = UC.UserId
									                    INNER JOIN Message M ON M.ConversationId = @ConversationId
									                    WHERE UI.UserId = @UserId
									                    ORDER BY M.CreatedOn DESC)
                            DECLARE @Unread nvarchar(max)
		                    SET @Unread = (	SELECT Count( M.SeenBy)
						                    FROM UserInfoContact UC
						                    INNER JOIN UserInfo UI ON UI.Id = UC.UserId
						                    INNER JOIN Message M ON M.ConversationId = @ConversationId
						                    WHERE UI.UserId = @UserId AND M.SeenBy = '0')

                            IF @ConversationId IS NULL
                                BREAK

                            INSERT INTO @Result SELECT @ConversationId,LOWER(@ContactId),@DisplayName,@Unread,@LastMessage,@LastMessageTime

                            DELETE TOP(1) FROM @TempContactChatBox

                        END
	                    SELECT * FROM @Result
                    END";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
