using Microsoft.EntityFrameworkCore.Migrations;

namespace GrooveMessengerDAL.Migrations
{
    public partial class addusp_Participant_GetContactEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
                -- =============================================
                -- Author:		TrucPhan
                -- Create date: 02-08-2019
                -- Description:	
                -- =============================================
                CREATE PROCEDURE [dbo].[usp_Participant_GetContactEmail] 
	                -- Add the parameters for the stored procedure here
	                @conversationId uniqueIdentifier,
	                @userId uniqueIdentifier
                AS
                BEGIN
	                -- SET NOCOUNT ON added to prevent extra result sets from
	                -- interfering with SELECT statements.
	                SET NOCOUNT ON;

                    -- Insert statements for procedure here
	                SELECT U.UserName
	                FROM Participant P
	                INNER JOIN AspNetUsers U on U.Id = P.UserId
	                WHERE P.ConversationId = @conversationId AND P.UserId != @userId
                END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
                DROP PROCEDURE [dbo].[usp_Participant_GetContactEmail]
                GO
                "
            );
        }
    }
}
