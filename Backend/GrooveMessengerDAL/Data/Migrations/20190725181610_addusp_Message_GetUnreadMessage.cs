using Microsoft.EntityFrameworkCore.Migrations;

namespace GrooveMessengerDAL.Migrations
{
    public partial class addusp_Message_GetUnreadMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"                
                    -- =============================================
                    -- Author:		TrucPhan
                    -- Create date: 25-7-2019
                    -- Description:	GetUnreadMessage
                    -- =============================================
                    CREATE PROCEDURE [dbo].[usp_Message_GetUnreadMessage] 
	                    -- Add the parameters for the stored procedure here
	                    @UserId uniqueIdentifier, 
	                    @ConversationId uniqueIdentifier
                    AS
                    BEGIN
	                    -- SET NOCOUNT ON added to prevent extra result sets from
	                    -- interfering with SELECT statements.
	                    SET NOCOUNT ON;

                        -- Insert statements for procedure here
	                    SELECT Count(*) 
	                    FROM Message 
	                    WHERE ConversationId = @ConversationId AND SenderId !=@UserId
	                    --AND CHARINDEX(convert(nvarchar(450), @UserId),SeenBy)<0
	                    AND isnull(SeenBy,'') NOT LIKE '%'+cast(@UserId as nvarchar(255))+'%'
                    END
                    GO"
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
              @"
                DROP PROCEDURE [dbo].[usp_Message_GetUnreadMessage]
                GO
                "
              );
        }
    }
}
