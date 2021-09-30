using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TIA.EntityFramework.Migrations
{
    public partial class AddProductDataModelDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductDataModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SomeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Quantity = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    ParentCatalogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentCatalogTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentParentCatalogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentParentCatalogTitle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductDataModels");
        }
    }
}
