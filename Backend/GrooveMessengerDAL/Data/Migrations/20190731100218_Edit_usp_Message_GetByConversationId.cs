using Microsoft.EntityFrameworkCore.Migrations;

namespace GrooveMessengerDAL.Migrations
{
    public partial class Edit_usp_Message_GetByConversationId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                           @" -- =============================================
                    -- Author:		TrucPhan(Edited by Hien)
                    -- Create date: 31-07-2019
                    -- Description:	GetLastestMessageOfAConversation
                    -- =============================================
                    ALTER PROCEDURE [dbo].[usp_Message_GetByConversationId]
                    -- Add the parameters for the stored procedure here
                        @ConversationId UniqueIdentifier,
						@CreatedOn DateTime2 = NULL
                    AS
                    BEGIN
					
                        -- SET NOCOUNT ON added to prevent extra result sets from
                        -- interfering with SELECT statements.

                        SET NOCOUNT ON;

                                --Insert statements for procedure here
						with cte as (SELECT Top 10 M.ConversationId AS Id,
	                    M.Content AS [Message],
	                    M.SenderId AS [Who],
	                    M.CreatedOn AS [Time],
						UI.DisplayName AS [DisplayName],
						UI.Avatar AS [Avatar],
						M.CreatedOn
                        FROM[Message] M  
						INNER JOIN UserInfo UI on UI.UserId = M.SenderId
                        WHERE M.ConversationId = @ConversationId
						and 
						(NULLIF(@CreatedOn,'') IS NULL OR M.CreatedOn<@CreatedOn) 
						Order By M.CreatedOn desc)

						select * from cte order by CreatedOn asc


                    
                           END
                 "
                       );
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
