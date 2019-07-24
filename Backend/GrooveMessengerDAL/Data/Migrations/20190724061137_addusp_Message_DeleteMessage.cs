using Microsoft.EntityFrameworkCore.Migrations;

namespace GrooveMessengerDAL.Migrations
{
    public partial class addusp_Message_DeleteMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
                    -- =============================================
                    -- Author:		<Linh Tran >
                    -- Create date: <2019/07-24>
                    -- Description:	<Delete Message by Message ID>
                    -- =============================================
                 CREATE PROCEDURE [dbo].[usp_Message_DeleteMessage]
	                -- Add the parameters for the stored procedure here
	             @Id uniqueidentifier --this is a number
                 AS
                 BEGIN
	                -- SET NOCOUNT ON added to prevent extra result sets from
	                -- interfering with SELECT statements.
	                SET NOCOUNT ON;
                    update Message
	                set Message.Deleted = 1
	                where Message.Id = @Id
                 END
                 GO
                 "
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
                 DROP PROCEDURE [dbo].[usp_Message_DeleteMessage]
                 GO
                 "
                );
        }
    }
}
