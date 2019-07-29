using Microsoft.EntityFrameworkCore.Migrations;

namespace GrooveMessengerDAL.Migrations
{
    public partial class addusp_Message_EditMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
                    -- =============================================
                    -- Author:		<Linh Tran>
                    -- Create date: <2019-7-24>
                    -- Description:	<Edit Message by EditMessageModel>
                    -- =============================================
                    CREATE PROCEDURE [dbo].[usp_Message_EditMessage]
                    -- Add the parameters for the stored procedure here
	                @Content nvarchar(1000) ,-- This is an string
                    @Id uniqueidentifier --this is a number
                    AS
                    BEGIN
	                SET NOCOUNT ON;
		
	                update Message
		            set Message.Content = @Content
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
                DROP PROCEDURE [dbo].[usp_Message_EditMessage]
                GO
                 "
            );
        }
    }
}