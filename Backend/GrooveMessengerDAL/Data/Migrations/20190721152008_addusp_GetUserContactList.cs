using Microsoft.EntityFrameworkCore.Migrations;

namespace GrooveMessengerDAL.Migrations
{
    public partial class addusp_GetUserContactList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
                -- =============================================
                -- Author:		Cuong.Duong Duy	
                -- Create date: 2019-07-20
                -- Description:	To get a contact list of current user
                -- =============================================
                CREATE PROCEDURE [dbo].[usp_GetUserContactList]
	                -- Add the parameters for the stored procedure here
	                @UserInfoId uniqueidentifier -- This is email address
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

	                SELECT	UIC.Id AS Id,
            			                UI.UserId AS UserId,
			                UIC.NickName AS DisplayName,
			                UI.Mood AS Mood,
			                UI.Status AS [Status],
			                UI.Avatar AS Avatar

	                FROM UserInfoContact UIC
	                INNER JOIN UserInfo UI ON UI.Id = UIC.ContactId

	                WHERE UIC.UserId = @UserInfoId AND UIC.Deleted = 0


                END
                GO  "
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
                DROP PROCEDURE [dbo].[sp_GetUserContactList]
                GO
                "
            );
        }
    }
}