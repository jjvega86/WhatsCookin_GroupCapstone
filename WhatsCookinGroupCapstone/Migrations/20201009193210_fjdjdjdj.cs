using Microsoft.EntityFrameworkCore.Migrations;

namespace WhatsCookinGroupCapstone.Migrations
{
    public partial class fjdjdjdj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6937b43f-b1eb-4b65-9379-11f9884ee4e9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dc7cb08c-b138-4d8c-b076-18a6c5d4bbab", "584966ec-5545-462b-a24c-11eafa267995", "Cook", "COOK" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc7cb08c-b138-4d8c-b076-18a6c5d4bbab");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6937b43f-b1eb-4b65-9379-11f9884ee4e9", "8d326d33-cf6d-4921-85fa-04ad9a1d19ab", "Cook", "COOK" });
        }
    }
}
