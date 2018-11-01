using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SocialPortal.Data.Migrations
{
    public partial class confirmAdder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ConfirmAdder",
                table: "Zespoly",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ConfirmAdder",
                table: "Utwory",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ConfirmAdder",
                table: "Teksty",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ConfirmAdder",
                table: "Relacje",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ConfirmAdder",
                table: "Eventy",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ConfirmAdder",
                table: "Autorzy",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ConfirmAdder",
                table: "Albumy",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmAdder",
                table: "Zespoly");

            migrationBuilder.DropColumn(
                name: "ConfirmAdder",
                table: "Utwory");

            migrationBuilder.DropColumn(
                name: "ConfirmAdder",
                table: "Teksty");

            migrationBuilder.DropColumn(
                name: "ConfirmAdder",
                table: "Relacje");

            migrationBuilder.DropColumn(
                name: "ConfirmAdder",
                table: "Eventy");

            migrationBuilder.DropColumn(
                name: "ConfirmAdder",
                table: "Autorzy");

            migrationBuilder.DropColumn(
                name: "ConfirmAdder",
                table: "Albumy");
        }
    }
}
