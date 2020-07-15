using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieStore.Infrastructure.Migrations
{
    public partial class afterUsersCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cast",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 128, nullable: true),
                    Gender = table.Column<string>(maxLength: 4096, nullable: true),
                    TmdbUrl = table.Column<string>(maxLength: 4096, nullable: true),
                    ProfilePath = table.Column<string>(maxLength: 2084, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cast", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 256, nullable: true),
                    LastName = table.Column<string>(maxLength: 256, nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    HashedPassword = table.Column<string>(nullable: true),
                    Salt = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 4096, nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: true),
                    LockoutEndDate = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    LastLoginDateTime = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    IsLocked = table.Column<bool>(nullable: true),
                    AccessFailedCount = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovieCast",
                columns: table => new
                {
                    MovieId = table.Column<int>(nullable: false),
                    CastId = table.Column<int>(nullable: false),
                    Character = table.Column<string>(maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieCast", x => new { x.MovieId, x.CastId, x.Character });
                    table.ForeignKey(
                        name: "FK_MovieCast_Cast_CastId",
                        column: x => x.CastId,
                        principalTable: "Cast",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieCast_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    MovieId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(3, 2)", nullable: false),
                    ReviewText = table.Column<string>(maxLength: 4096, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => new { x.MovieId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Review_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Review_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieCast_CastId",
                table: "MovieCast",
                column: "CastId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_UserId",
                table: "Review",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieCast");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "Cast");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
