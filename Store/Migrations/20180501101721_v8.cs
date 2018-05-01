using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Store.Migrations
{
    public partial class v8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Extension",
                schema: "public",
                table: "Image",
                newName: "Type");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                schema: "public",
                table: "Brand",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Brand_ImageId",
                schema: "public",
                table: "Brand",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brand_Image_ImageId",
                schema: "public",
                table: "Brand",
                column: "ImageId",
                principalSchema: "public",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brand_Image_ImageId",
                schema: "public",
                table: "Brand");

            migrationBuilder.DropIndex(
                name: "IX_Brand_ImageId",
                schema: "public",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "ImageId",
                schema: "public",
                table: "Brand");

            migrationBuilder.RenameColumn(
                name: "Type",
                schema: "public",
                table: "Image",
                newName: "Extension");
        }
    }
}
