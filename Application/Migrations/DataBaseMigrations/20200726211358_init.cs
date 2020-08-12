using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Migrations.DataBaseMigrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Link = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Rating = table.Column<double>(nullable: false),
                    Distance = table.Column<string>(nullable: true),
                    Duration = table.Column<string>(nullable: true),
                    Photo = table.Column<byte[]>(nullable: true),
                    Map = table.Column<byte[]>(nullable: true),
                    Author = table.Column<string>(nullable: true),
                    DifficultyLevel = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Trail = table.Column<string>(nullable: true),
                    KindOfActivity = table.Column<string>(nullable: true),
                    AverageSpeed = table.Column<string>(nullable: true),
                    Exceedance = table.Column<string>(nullable: true),
                    SumUp = table.Column<string>(nullable: true),
                    SumDown = table.Column<string>(nullable: true),
                    Data = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trails", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trails");
        }
    }
}
