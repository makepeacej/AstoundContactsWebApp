using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AstoundContactsWebApp.Data.Migrations
{
    public partial class ContactsUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JobCategory",
                table: "Contact",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobCategory",
                table: "Contact");
        }
    }
}
