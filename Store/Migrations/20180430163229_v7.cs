using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Store.Migrations
{
    public partial class v7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Brand",
                newSchema: "public");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "public",
                table: "Image",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                schema: "public",
                table: "Image");

            migrationBuilder.RenameTable(
                name: "Brand",
                schema: "public");
        }
    }
}
