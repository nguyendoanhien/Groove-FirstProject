using Microsoft.EntityFrameworkCore.Migrations;

namespace GrooveMessengerDAL.Migrations
{
    public partial class addusp_Contact_GetUnknownContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"-- =============================================
			        -- Author:		Doan Hien
			        -- Create date: 2019-07-23
			        -- Description:	Get Unknown Contacts
			        -- =============================================
			        CREATE PROCEDURE [dbo].[usp_Contact_GetUnknownContact]
				        -- Add the parameters for the stored procedure here
				        @UserInfoId uniqueidentifier ,
				        @DisplayNameSearch nvarchar(255) = NULL
			        AS
			        BEGIN
				        -- SET NOCOUNT ON added to prevent extra result sets from
				        -- interfering with SELECT statements.
				        SET NOCOUNT ON;

				        -- Insert statements for procedure here
	
				        select *
	
				        from UserInfo ui
				        where ui.Id not in (
					        select contactId
					        from UserInfoContact uic
					        where uic.UserId = @UserInfoId
					        union select @UserInfoId
				        )
				        and (@DisplayNameSearch IS NULL OR DisplayName like '%'+@DisplayNameSearch+'%')
				        and coalesce(ui.Deleted,0) = 0

			        END
			        GO"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
				DROP PROCEDURE [dbo].[usp_Contact_GetUnknownContact]
				GO
				"
            );
        }
    }
}