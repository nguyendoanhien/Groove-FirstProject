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
                -- Author:		<Author,,Name>
                -- Create date: <Create Date,,>
                -- Description:	<Description,,>
                -- =============================================
                        create PROCEDURE [dbo].[usp_UserInfo_GetByUsername]
	            -- Add the parameters for the stored procedure here
	            @Username nvarchar(256) -- This is email address
                AS
                BEGIN
	                -- SET NOCOUNT ON added to prevent extra result sets from
	                -- interfering with SELECT statements.
	                SET NOCOUNT ON;

                    -- Insert statements for procedure here

	                -- Data returned must be match to a model (from ContactService)
		                --public Guid Id { get; set; }
                  --      public string UserId { get; set; }
                  --      public string DisplayName { get; set; }
                  --      public string Mood { get; set; }
                  --      public string Status { get; set; }
                  --      public string Avatar { get; set; }
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
