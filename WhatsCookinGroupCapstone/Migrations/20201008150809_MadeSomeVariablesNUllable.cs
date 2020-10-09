using Microsoft.EntityFrameworkCore.Migrations;

namespace WhatsCookinGroupCapstone.Migrations
{
    public partial class MadeSomeVariablesNUllable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "51b5197c-139a-4a0d-98d0-e42eb6a0ba68");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9ae2dede-e020-4147-aec6-559a45d77c3b", "99ab2e4a-32ff-46da-acaa-61d7b1879933", "Cook", "COOK" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9ae2dede-e020-4147-aec6-559a45d77c3b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "51b5197c-139a-4a0d-98d0-e42eb6a0ba68", "188e22d9-f1ce-41b6-b9e0-b13e7076d9ff", "Cook", "COOK" });
        }
    }
}
