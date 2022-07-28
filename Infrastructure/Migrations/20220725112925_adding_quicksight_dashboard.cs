using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class adding_quicksight_dashboard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "header",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(100)", nullable: true),
                    Description = table.Column<string>(type: "NVARCHAR(500)", nullable: true),
                    Is_Default = table.Column<bool>(type: "bit", nullable: false),
                    Is_Active = table.Column<bool>(type: "bit", nullable: false),
                    Parent_Of = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    ReportId = table.Column<string>(type: "NVARCHAR(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_header", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Quicksight_Dashboard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dashboard_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dashboard_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dashboard_description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    access_key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    secret_access_key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    aws_account_id = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quicksight_Dashboard", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "header");

            migrationBuilder.DropTable(
                name: "Quicksight_Dashboard");
        }
    }
}
