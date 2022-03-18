using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RnAds.DAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ads",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    NameAd = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    TypeAd = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ads_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Avatars",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FilePath = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    UserId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avatars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Avatars_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteUserIdes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    AdId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteUserIdes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteUserIdes_Ads_AdId",
                        column: x => x.AdId,
                        principalTable: "Ads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FilePath = table.Column<string>(nullable: true),
                    AdId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pictures_Ads_AdId",
                        column: x => x.AdId,
                        principalTable: "Ads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TypeItem = table.Column<string>(nullable: true),
                    CategoryItem = table.Column<string>(nullable: true),
                    Condition = table.Column<string>(nullable: true),
                    Availability = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    Area = table.Column<int>(nullable: false),
                    NumberFloor = table.Column<int>(nullable: false),
                    CountRooms = table.Column<int>(nullable: false),
                    Citizenship = table.Column<string>(nullable: true),
                    SphereOfActivity = table.Column<string>(nullable: true),
                    WorkingSchedule = table.Column<string>(nullable: true),
                    Sex = table.Column<string>(nullable: true),
                    WorkExperience = table.Column<string>(nullable: true),
                    FrequencyOfPayments = table.Column<string>(nullable: true),
                    YearReliase = table.Column<int>(nullable: false),
                    Kilometers = table.Column<int>(nullable: false),
                    OwnerCount = table.Column<int>(nullable: false),
                    EngineVolume = table.Column<double>(nullable: false),
                    HorsePower = table.Column<int>(nullable: false),
                    TypeEngine = table.Column<string>(nullable: true),
                    TypeGearBox = table.Column<string>(nullable: true),
                    TypeDrive = table.Column<string>(nullable: true),
                    TypeBody = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    TypeHelm = table.Column<string>(nullable: true),
                    TypeSparePartsAndAcsessories = table.Column<string>(nullable: true),
                    AdId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAttributes_Ads_AdId",
                        column: x => x.AdId,
                        principalTable: "Ads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ads_UserId",
                table: "Ads",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Avatars_UserId1",
                table: "Avatars",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteUserIdes_AdId",
                table: "FavoriteUserIdes",
                column: "AdId");

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_AdId",
                table: "Pictures",
                column: "AdId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_AdId",
                table: "ProductAttributes",
                column: "AdId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Avatars");

            migrationBuilder.DropTable(
                name: "FavoriteUserIdes");

            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropTable(
                name: "ProductAttributes");

            migrationBuilder.DropTable(
                name: "Ads");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
