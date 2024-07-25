using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TwnTw_WEB.Migrations
{
    /// <inheritdoc />
    public partial class db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Workspaces",
                columns: table => new
                {
                    WSId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WSName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workspaces", x => x.WSId);
                });

            migrationBuilder.CreateTable(
                name: "TaskDetails",
                columns: table => new
                {
                    TaskDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskDetails", x => x.TaskDetailId);
                    table.ForeignKey(
                        name: "FK_TaskDetails_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MemberDetails",
                columns: table => new
                {
                    MemberDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkSpaceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberDetails", x => x.MemberDetailId);
                    table.ForeignKey(
                        name: "FK_MemberDetails_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberDetails_Workspaces_WorkSpaceId",
                        column: x => x.WorkSpaceId,
                        principalTable: "Workspaces",
                        principalColumn: "WSId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MemberDetails_UserId",
                table: "MemberDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberDetails_WorkSpaceId",
                table: "MemberDetails",
                column: "WorkSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskDetails_UserId",
                table: "TaskDetails",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberDetails");

            migrationBuilder.DropTable(
                name: "TaskDetails");

            migrationBuilder.DropTable(
                name: "Workspaces");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
