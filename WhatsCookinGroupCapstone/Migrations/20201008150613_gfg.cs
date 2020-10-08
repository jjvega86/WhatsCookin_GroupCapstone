using Microsoft.EntityFrameworkCore.Migrations;

namespace WhatsCookinGroupCapstone.Migrations
{
    public partial class gfg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e9fc7ff5-785e-4c0a-b920-f9cd614630f3");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a829f1ad-5e38-4177-8032-5fa1543016fd", "5f62d4dd-7a4f-4246-ad79-e241a32a3f84", "Cook", "COOK" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a829f1ad-5e38-4177-8032-5fa1543016fd");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e9fc7ff5-785e-4c0a-b920-f9cd614630f3", "77e1acf9-5503-410b-a865-947d9a793b82", "Cook", "COOK" });
        }
    }
}
