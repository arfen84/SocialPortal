using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SocialPortal.Data.Migrations
{
    public partial class AddDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AddDate",
                table: "Zespoly",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Popularity",
                table: "Zespoly",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "verified",
                table: "Zespoly",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "AddDate",
                table: "Utwory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Popularity",
                table: "Utwory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "AddDate",
                table: "Teksty",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "Teksty",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Popularity",
                table: "Teksty",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Youtube",
                table: "Teksty",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "verified",
                table: "Teksty",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "verified",
                table: "Relacje",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "verified",
                table: "Eventy",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "AddDate",
                table: "Autorzy",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Popularity",
                table: "Autorzy",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "verified",
                table: "Autorzy",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "AddDate",
                table: "Albumy",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Popularity",
                table: "Albumy",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "verified",
                table: "Albumy",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddDate",
                table: "Zespoly");

            migrationBuilder.DropColumn(
                name: "Popularity",
                table: "Zespoly");

            migrationBuilder.DropColumn(
                name: "verified",
                table: "Zespoly");

            migrationBuilder.DropColumn(
                name: "AddDate",
                table: "Utwory");

            migrationBuilder.DropColumn(
                name: "Popularity",
                table: "Utwory");

            migrationBuilder.DropColumn(
                name: "AddDate",
                table: "Teksty");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Teksty");

            migrationBuilder.DropColumn(
                name: "Popularity",
                table: "Teksty");

            migrationBuilder.DropColumn(
                name: "Youtube",
                table: "Teksty");

            migrationBuilder.DropColumn(
                name: "verified",
                table: "Teksty");

            migrationBuilder.DropColumn(
                name: "verified",
                table: "Relacje");

            migrationBuilder.DropColumn(
                name: "verified",
                table: "Eventy");

            migrationBuilder.DropColumn(
                name: "AddDate",
                table: "Autorzy");

            migrationBuilder.DropColumn(
                name: "Popularity",
                table: "Autorzy");

            migrationBuilder.DropColumn(
                name: "verified",
                table: "Autorzy");

            migrationBuilder.DropColumn(
                name: "AddDate",
                table: "Albumy");

            migrationBuilder.DropColumn(
                name: "Popularity",
                table: "Albumy");

            migrationBuilder.DropColumn(
                name: "verified",
                table: "Albumy");
        }
    }
}
