using Microsoft.EntityFrameworkCore.Migrations;

namespace GrooveMessengerDAL.Migrations
{
    public partial class addusp_GetUserContactEmailList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
                -- =============================================
                -- Author:		Cuong.Duong Duy	
                -- Create date: 2019-07-20
                -- Description:	To get an email list from contacts of current user
                -- =============================================
                CREATE PROCEDURE [dbo].[usp_GetUserContactEmailList]
	                -- Add the parameters for the stored procedure here
	                @UserInfoId uniqueidentifier -- This is email address
                AS
                BEGIN
	                -- SET NOCOUNT ON added to prevent extra result sets from
	                -- interfering with SELECT statements.
	                SET NOCOUNT ON;

                    -- Insert statements for procedure here

	                -- Data returned must be match to a model (from ContactService)
	                -- string

	                SELECT	US.Email AS Email

	                FROM UserInfoContact UIC
	                INNER JOIN UserInfo UI ON UI.Id = UIC.ContactId
	                INNER JOIN AspNetUsers US ON US.Id = UI.UserId

	                WHERE UIC.UserId = @UserInfoId AND UIC.Deleted = 0


                END
                GO"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
                DROP PROCEDURE [dbo].[usp_GetUserContactEmailList]
                GO
                "
            );
        }
    }
}