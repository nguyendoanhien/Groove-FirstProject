using Microsoft.EntityFrameworkCore.Migrations;

namespace GrooveMessengerDAL.Migrations
{
    public partial class AddSp_usp_GetLatestContactChatListByUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[usp_GetLatestContactChatListByUserId] 
	-- Add the parameters for the stored procedure here
	@UserId nvarchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	--SELECT 
	SELECT C.Id AS ConvId, UI.Id AS ContactId, UI.DisplayName, M.Content AS LastMessage, M.CreatedOn AS LastMessageTime, M.SeenBy AS Unread
	FROM UserInfo UI
	INNER JOIN AspNetUsers U on U.Id = UI.UserId
	INNER JOIN Participant P on U.Id = P.UserId
	INNER JOIN [Conversation] C on P.ConversationId = C.Id
	INNER JOIN [Message] M on M.ConversationId = C.Id
	WHERE U.Id = @UserId 
END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
