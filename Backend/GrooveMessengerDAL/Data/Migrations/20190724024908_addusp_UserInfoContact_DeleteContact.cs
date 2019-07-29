using Microsoft.EntityFrameworkCore.Migrations;

namespace GrooveMessengerDAL.Migrations
{
    public partial class addusp_UserInfoContact_DeleteContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
                -- =============================================
                -- Author:		<Linh ,Tran>
                -- Create date: <2019-07-24>
                -- Description:	<Edit User Nickname by UserId>
                -- =============================================
                        Create PROCEDURE [dbo].[usp_UserInfoContact_DeleteContact]
		                -- Add the parameters for the stored procedure here
		                @Id uniqueidentifier 
			    AS
			    BEGIN
				        -- SET NOCOUNT ON added to prevent extra result sets from
				        -- interfering with SELECT statements.
				        SET NOCOUNT ON;

				        -- Insert statements for procedure here
	
				        update  UserInfoContact 
						set UserInfoContact.Deleted = 1
						where UserInfoContact.Id = @Id;

			    END
                GO"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
                DROP PROCEDURE [dbo].[usp_UserInfoContact_DeleteContact]
                GO
                "
            );
        }
    }
}