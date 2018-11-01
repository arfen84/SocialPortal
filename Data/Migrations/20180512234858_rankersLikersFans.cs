using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SocialPortal.Data.Migrations
{
    public partial class rankersLikersFans : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Fan",
                table: "Zespoly",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Liker",
                table: "Utwory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ranker",
                table: "Utwory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Liker",
                table: "Teksty",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ranker",
                table: "Teksty",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fan",
                table: "Autorzy",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Liker",
                table: "Albumy",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ranker",
                table: "Albumy",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fan",
                table: "Zespoly");

            migrationBuilder.DropColumn(
                name: "Liker",
                table: "Utwory");

            migrationBuilder.DropColumn(
                name: "Ranker",
                table: "Utwory");

            migrationBuilder.DropColumn(
                name: "Liker",
                table: "Teksty");

            migrationBuilder.DropColumn(
                name: "Ranker",
                table: "Teksty");

            migrationBuilder.DropColumn(
                name: "Fan",
                table: "Autorzy");

            migrationBuilder.DropColumn(
                name: "Liker",
                table: "Albumy");

            migrationBuilder.DropColumn(
                name: "Ranker",
                table: "Albumy");
        }
    }
}
