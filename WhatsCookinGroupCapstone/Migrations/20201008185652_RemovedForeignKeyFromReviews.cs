using Microsoft.EntityFrameworkCore.Migrations;

namespace WhatsCookinGroupCapstone.Migrations
{
    public partial class RemovedForeignKeyFromReviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Recipes_RecipeID",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_RecipeID",
                table: "Reviews");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Reviews_RecipeID",
                table: "Reviews",
                column: "RecipeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Recipes_RecipeID",
                table: "Reviews",
                column: "RecipeID",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
