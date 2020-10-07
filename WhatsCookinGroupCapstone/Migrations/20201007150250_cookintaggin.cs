using Microsoft.EntityFrameworkCore.Migrations;

namespace WhatsCookinGroupCapstone.Migrations
{
    public partial class cookintaggin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Preferences");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "98adf513-8232-4875-afee-64cd6c7834f9");

            migrationBuilder.DropColumn(
                name: "PreferencesId",
                table: "Cook");

            migrationBuilder.CreateTable(
                name: "CookTags",
                columns: table => new
                {
                    TagsId = table.Column<int>(nullable: false),
                    CookId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CookTags", x => new { x.CookId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_CookTags_Cook_CookId",
                        column: x => x.CookId,
                        principalTable: "Cook",
                        principalColumn: "CookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CookTags_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "TagsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2a0d8ca8-de30-4f07-acba-e24d2ecfb6a2", "2acff12e-bfa3-4c31-9053-846b5bd71654", "Cook", "COOK" });

            migrationBuilder.CreateIndex(
                name: "IX_CookTags_TagsId",
                table: "CookTags",
                column: "TagsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CookTags");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2a0d8ca8-de30-4f07-acba-e24d2ecfb6a2");

            migrationBuilder.AddColumn<int>(
                name: "PreferencesId",
                table: "Cook",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Preferences",
                columns: table => new
                {
                    PreferencesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PreferencesId1 = table.Column<int>(type: "int", nullable: true),
                    isAPreference = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preferences", x => x.PreferencesId);
                    table.ForeignKey(
                        name: "FK_Preferences_Preferences_PreferencesId1",
                        column: x => x.PreferencesId1,
                        principalTable: "Preferences",
                        principalColumn: "PreferencesId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "98adf513-8232-4875-afee-64cd6c7834f9", "186a317c-19e5-48db-91bc-b36d9bb85995", "Cook", "COOK" });

            migrationBuilder.CreateIndex(
                name: "IX_Preferences_PreferencesId1",
                table: "Preferences",
                column: "PreferencesId1");
        }
    }
}
