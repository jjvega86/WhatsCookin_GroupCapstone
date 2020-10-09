using Microsoft.EntityFrameworkCore.Migrations;

namespace WhatsCookinGroupCapstone.Migrations
{
    public partial class recipeedits2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b95f9d84-6345-422f-b28d-bb608b3efe10");

            migrationBuilder.CreateTable(
                name: "RecipeEdits",
                columns: table => new
                {
                    RecipeEditsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeID = table.Column<int>(nullable: false),
                    CookId = table.Column<int>(nullable: false),
                    SuggestedEdit = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeEdits", x => x.RecipeEditsId);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6937b43f-b1eb-4b65-9379-11f9884ee4e9", "8d326d33-cf6d-4921-85fa-04ad9a1d19ab", "Cook", "COOK" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeEdits");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6937b43f-b1eb-4b65-9379-11f9884ee4e9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b95f9d84-6345-422f-b28d-bb608b3efe10", "0956f459-b00e-4bba-b42e-7c7cb224c3a3", "Cook", "COOK" });
        }
    }
}
