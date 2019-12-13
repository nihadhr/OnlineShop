using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShopPodaci.Migrations
{
    public partial class product : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_manufacturer_ManufacturerID",
                table: "product");

            migrationBuilder.DropForeignKey(
                name: "FK_product_subcategory_SubCategoryID",
                table: "product");

            migrationBuilder.AlterColumn<int>(
                name: "SubCategoryID",
                table: "product",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ManufacturerID",
                table: "product",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_product_manufacturer_ManufacturerID",
                table: "product",
                column: "ManufacturerID",
                principalTable: "manufacturer",
                principalColumn: "ManufacturerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_subcategory_SubCategoryID",
                table: "product",
                column: "SubCategoryID",
                principalTable: "subcategory",
                principalColumn: "SubCategoryID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_manufacturer_ManufacturerID",
                table: "product");

            migrationBuilder.DropForeignKey(
                name: "FK_product_subcategory_SubCategoryID",
                table: "product");

            migrationBuilder.AlterColumn<int>(
                name: "SubCategoryID",
                table: "product",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ManufacturerID",
                table: "product",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_product_manufacturer_ManufacturerID",
                table: "product",
                column: "ManufacturerID",
                principalTable: "manufacturer",
                principalColumn: "ManufacturerID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_product_subcategory_SubCategoryID",
                table: "product",
                column: "SubCategoryID",
                principalTable: "subcategory",
                principalColumn: "SubCategoryID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
