using Microsoft.EntityFrameworkCore.Migrations;

namespace GrooveMessengerDAL.Migrations
{
    public partial class addusp_UserInfo_GetByUsername : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
                    -- =============================================
                -- Author:		<Linh ,Tran>
                -- Create date: <2019-07-24>
                -- Description:	<Get User by Username>
                -- =============================================
                        create PROCEDURE [dbo].[usp_UserInfo_GetByUsername]
	            -- Add the parameters for the stored procedure here
	            @Username nvarchar(256) -- This is email address
                AS
                BEGIN

	                SET NOCOUNT ON;

				   SELECT	ui.*
	                FROM AspNetUsers UIC
	                INNER JOIN UserInfo UI ON UI.UserId = UIC.Id

	                WHERE UIC.UserName = @Username 


                END
                GO"
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
                DROP PROCEDURE [dbo].[usp_UserInfo_GetByUsername]
                GO
                "
                );
        }
    }
}
