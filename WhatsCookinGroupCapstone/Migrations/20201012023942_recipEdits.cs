using Microsoft.EntityFrameworkCore.Migrations;

namespace WhatsCookinGroupCapstone.Migrations
{
    public partial class recipEdits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "453281b7-de06-4b28-af1d-15d04833f6c2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7d3867e2-2e23-4825-826e-84634f6c771d", "b0244742-cb70-4fe9-afed-e31d1dda6029", "Cook", "COOK" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7d3867e2-2e23-4825-826e-84634f6c771d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "453281b7-de06-4b28-af1d-15d04833f6c2", "d53abd46-0468-40b6-9eba-bc02f04ac1b", "Cook", "COOK" });
        }
    }
}
