using Microsoft.EntityFrameworkCore.Migrations;

namespace GrooveMessengerDAL.Migrations
{
    public partial class updateusp_Message_GetUnreadMessageAmount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
                -- =============================================
                -- Author:		TrucPhan
                -- Create date: 25-07-2019
				-- Edit date: 02-08-2019
                -- Description:	GetUnreadMessage
                -- =============================================
                ALTER PROCEDURE [dbo].[usp_Message_GetUnreadMessageAmount] 
	                -- Add the parameters for the stored procedure here
	                @userName nvarchar(256), 
	                @conversationId uniqueIdentifier
                AS
                BEGIN
	                -- SET NOCOUNT ON added to prevent extra result sets from
	                -- interfering with SELECT statements.
	                SET NOCOUNT ON;
					--DECLARE @userId nvarchar(450)
					--SET @userId = (SELECT TOP(1)U.Id FROM AspNetUsers U WHERE U.UserName = @userName)
                    -- Insert statements for procedure here
	                SELECT Count(*) AS Amount
	                FROM Message 
					INNER JOIN AspNetUsers U ON U.UserName = @userName 
	                WHERE ConversationId = @conversationId AND SenderId !=U.Id
	                AND isnull(SeenBy,'') NOT LIKE '%'+cast(U.Id as nvarchar(255))+'%'
                END
                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
                DROP PROCEDURE [dbo].[usp_Message_GetUnreadMessageAmount]
                GO
                "
            );
        }
    }
}
