using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Store.Migrations
{
    public partial class v13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentInfoId",
                schema: "public",
                table: "Order",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PaymentInfo",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DeliveryWayId = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    HouseAppartmentNumber = table.Column<int>(nullable: true),
                    HouseEntranceNumber = table.Column<int>(nullable: true),
                    HouseNumber = table.Column<int>(nullable: true),
                    HouseStreet = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentInfo", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_PaymentInfoId",
                schema: "public",
                table: "Order",
                column: "PaymentInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_PaymentInfo_PaymentInfoId",
                schema: "public",
                table: "Order",
                column: "PaymentInfoId",
                principalSchema: "public",
                principalTable: "PaymentInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_PaymentInfo_PaymentInfoId",
                schema: "public",
                table: "Order");

            migrationBuilder.DropTable(
                name: "PaymentInfo",
                schema: "public");

            migrationBuilder.DropIndex(
                name: "IX_Order_PaymentInfoId",
                schema: "public",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "PaymentInfoId",
                schema: "public",
                table: "Order");
        }
    }
}
