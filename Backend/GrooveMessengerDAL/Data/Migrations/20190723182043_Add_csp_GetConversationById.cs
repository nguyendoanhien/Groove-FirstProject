using Microsoft.EntityFrameworkCore.Migrations;

namespace GrooveMessengerDAL.Migrations
{
    public partial class Add_csp_GetConversationById : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"
                    -- =============================================
                    -- Author:		TrucPhan
                    -- Create date: 23-07-2019
                    -- Description:	GetLastestMessageOfAConversation
                    -- =============================================
                    CREATE PROCEDURE csp_GetConversationById
                    -- Add the parameters for the stored procedure here
                        @ConversationId UniqueIdentifier
                    AS
                    BEGIN
                        -- SET NOCOUNT ON added to prevent extra result sets from
                        -- interfering with SELECT statements.

                        SET NOCOUNT ON;

                                --Insert statements for procedure here
                        SELECT
                        M.ConversationId AS Id,
	                    M.Content AS [Message],
	                    M.SenderId AS [Who],
	                    M.CreatedOn AS [Time]       
                               FROM[Message] M       
                               WHERE M.ConversationId = @ConversationId
                           END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
               @"
                DROP PROCEDURE [dbo].[csp_GetConversationById]
                GO
                "
               );
        }
    }
}
