using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SocialPortal.Data.Migrations
{
    public partial class LikerAddEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Liker",
                table: "Relacje",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Popularity",
                table: "Relacje",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Liker",
                table: "Eventy",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Popularity",
                table: "Eventy",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Liker",
                table: "Relacje");

            migrationBuilder.DropColumn(
                name: "Popularity",
                table: "Relacje");

            migrationBuilder.DropColumn(
                name: "Liker",
                table: "Eventy");

            migrationBuilder.DropColumn(
                name: "Popularity",
                table: "Eventy");
        }
    }
}
