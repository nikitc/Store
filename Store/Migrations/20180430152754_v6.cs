using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Store.Migrations
{
    public partial class v6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Count",
                schema: "public",
                table: "Product",
                newName: "ImageId");

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                schema: "public",
                table: "Product",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "OldPrice",
                schema: "public",
                table: "Product",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Content = table.Column<string>(nullable: true),
                    Extension = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_BrandId",
                schema: "public",
                table: "Product",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ImageId",
                schema: "public",
                table: "Product",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Brand_BrandId",
                schema: "public",
                table: "Product",
                column: "BrandId",
                principalTable: "Brand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Image_ImageId",
                schema: "public",
                table: "Product",
                column: "ImageId",
                principalSchema: "public",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Brand_BrandId",
                schema: "public",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Image_ImageId",
                schema: "public",
                table: "Product");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropTable(
                name: "Image",
                schema: "public");

            migrationBuilder.DropIndex(
                name: "IX_Product_BrandId",
                schema: "public",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ImageId",
                schema: "public",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "BrandId",
                schema: "public",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "OldPrice",
                schema: "public",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                schema: "public",
                table: "Product",
                newName: "Count");
        }
    }
}
