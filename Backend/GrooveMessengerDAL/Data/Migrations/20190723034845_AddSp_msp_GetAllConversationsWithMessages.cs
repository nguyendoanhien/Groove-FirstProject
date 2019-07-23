using Microsoft.EntityFrameworkCore.Migrations;

namespace GrooveMessengerDAL.Migrations
{
    public partial class AddSp_msp_GetAllConversationsWithMessages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE msp_GetAllConversationsWithMessages 
	-- Add the parameters for the stored procedure here
	@UserId UniqueIdentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
	M.ConversationId AS Id,
	M.Content AS [Message],
	M.SenderId AS [Who],
	M.CreatedOn AS [Time]
	FROM [Conversation] C
	INNER JOIN [Message] M on M.ConversationId = C.Id
	WHERE M.SenderId = @UserId
END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
