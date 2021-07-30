using Microsoft.EntityFrameworkCore.Migrations;

namespace GigHubMVC.Data.Migrations
{
    public partial class addedAttendence : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    GigId = table.Column<int>(nullable: false),
                    AttendeeId = table.Column<string>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => new { x.AttendeeId, x.GigId });
                    table.ForeignKey(
                        name: "FK_Attendances_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attendances_Gigs_GigId",
                        column: x => x.GigId,
                        principalTable: "Gigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_ApplicationUserId",
                table: "Attendances",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_GigId",
                table: "Attendances",
                column: "GigId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendances");
        }
    }
}
