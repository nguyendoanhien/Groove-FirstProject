using Microsoft.EntityFrameworkCore.Migrations;

namespace GrooveMessengerDAL.Migrations
{
    public partial class updateusp_Message_GetConversationById_AddMoreProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
                    -- =============================================
                    -- Author:		TrucPhan
                    -- Create date: 23-07-2019
                    -- Edit date: 31-07-2019
                    -- Description:	GetLastestMessageOfAConversation
                    -- =============================================
                    ALTER PROCEDURE [dbo].[usp_Message_GetByConversationId]
                    -- Add the parameters for the stored procedure here
                        @ConversationId UniqueIdentifier
                    AS
                    BEGIN
                        -- SET NOCOUNT ON added to prevent extra result sets from
                        -- interfering with SELECT statements.

                        SET NOCOUNT ON;

                                --Insert statements for procedure here
                       SELECT M.ConversationId AS Id,
	                    M.Content AS [Message],
	                    M.SenderId AS [Who],
	                    M.CreatedOn AS [Time],
						UI.DisplayName AS [DisplayName],
						UI.Avatar AS [Avatar]       
                        FROM[Message] M  
						INNER JOIN UserInfo UI on UI.UserId = M.SenderId
                        WHERE M.ConversationId = @ConversationId
                     END
                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
                 DROP PROCEDURE [dbo].[usp_Message_GetByConversationId]
                 GO
                 "
            );
        }
    }
}
