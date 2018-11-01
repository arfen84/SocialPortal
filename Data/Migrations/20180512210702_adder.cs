using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SocialPortal.Data.Migrations
{
    public partial class adder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Adder",
                table: "Zespoly",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Adder",
                table: "Utwory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Adder",
                table: "Teksty",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Adder",
                table: "Relacje",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Adder",
                table: "Eventy",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Adder",
                table: "Autorzy",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Adder",
                table: "Albumy",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adder",
                table: "Zespoly");

            migrationBuilder.DropColumn(
                name: "Adder",
                table: "Utwory");

            migrationBuilder.DropColumn(
                name: "Adder",
                table: "Teksty");

            migrationBuilder.DropColumn(
                name: "Adder",
                table: "Relacje");

            migrationBuilder.DropColumn(
                name: "Adder",
                table: "Eventy");

            migrationBuilder.DropColumn(
                name: "Adder",
                table: "Autorzy");

            migrationBuilder.DropColumn(
                name: "Adder",
                table: "Albumy");
        }
    }
}
