﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Insurance.DataAccessLayer.Migrations
{
    public partial class removecascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_FirstContractor_FirstContractorId",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_Contract_SecondContractor_SecondContractorId",
                table: "Contract");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_FirstContractor_FirstContractorId",
                table: "Contract",
                column: "FirstContractorId",
                principalTable: "FirstContractor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_SecondContractor_SecondContractorId",
                table: "Contract",
                column: "SecondContractorId",
                principalTable: "SecondContractor",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_FirstContractor_FirstContractorId",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_Contract_SecondContractor_SecondContractorId",
                table: "Contract");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_FirstContractor_FirstContractorId",
                table: "Contract",
                column: "FirstContractorId",
                principalTable: "FirstContractor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_SecondContractor_SecondContractorId",
                table: "Contract",
                column: "SecondContractorId",
                principalTable: "SecondContractor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
