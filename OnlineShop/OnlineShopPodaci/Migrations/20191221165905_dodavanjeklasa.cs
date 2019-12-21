using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShopPodaci.Migrations
{
    public partial class dodavanjeklasa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branch_city_CityID",
                table: "Branch");

            migrationBuilder.DropForeignKey(
                name: "FK_BranchProduct_Branch_BranchID",
                table: "BranchProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_BranchProduct_product_ProductID",
                table: "BranchProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_Stock_city_CityID",
                table: "Stock");

            migrationBuilder.DropForeignKey(
                name: "FK_StockProduct_product_ProductID",
                table: "StockProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_StockProduct_Stock_StockID",
                table: "StockProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockProduct",
                table: "StockProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stock",
                table: "Stock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BranchProduct",
                table: "BranchProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Branch",
                table: "Branch");

            migrationBuilder.RenameTable(
                name: "StockProduct",
                newName: "stockproduct");

            migrationBuilder.RenameTable(
                name: "Stock",
                newName: "stock");

            migrationBuilder.RenameTable(
                name: "BranchProduct",
                newName: "branchproduct");

            migrationBuilder.RenameTable(
                name: "Branch",
                newName: "branch");

            migrationBuilder.RenameIndex(
                name: "IX_StockProduct_StockID",
                table: "stockproduct",
                newName: "IX_stockproduct_StockID");

            migrationBuilder.RenameIndex(
                name: "IX_Stock_CityID",
                table: "stock",
                newName: "IX_stock_CityID");

            migrationBuilder.RenameIndex(
                name: "IX_BranchProduct_ProductID",
                table: "branchproduct",
                newName: "IX_branchproduct_ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_Branch_CityID",
                table: "branch",
                newName: "IX_branch_CityID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_stockproduct",
                table: "stockproduct",
                columns: new[] { "ProductID", "StockID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_stock",
                table: "stock",
                column: "StockID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_branchproduct",
                table: "branchproduct",
                columns: new[] { "BranchID", "ProductID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_branch",
                table: "branch",
                column: "BranchID");

            migrationBuilder.AddForeignKey(
                name: "FK_branch_city_CityID",
                table: "branch",
                column: "CityID",
                principalTable: "city",
                principalColumn: "CityID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_branchproduct_branch_BranchID",
                table: "branchproduct",
                column: "BranchID",
                principalTable: "branch",
                principalColumn: "BranchID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_branchproduct_product_ProductID",
                table: "branchproduct",
                column: "ProductID",
                principalTable: "product",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_stock_city_CityID",
                table: "stock",
                column: "CityID",
                principalTable: "city",
                principalColumn: "CityID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_stockproduct_product_ProductID",
                table: "stockproduct",
                column: "ProductID",
                principalTable: "product",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_stockproduct_stock_StockID",
                table: "stockproduct",
                column: "StockID",
                principalTable: "stock",
                principalColumn: "StockID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_branch_city_CityID",
                table: "branch");

            migrationBuilder.DropForeignKey(
                name: "FK_branchproduct_branch_BranchID",
                table: "branchproduct");

            migrationBuilder.DropForeignKey(
                name: "FK_branchproduct_product_ProductID",
                table: "branchproduct");

            migrationBuilder.DropForeignKey(
                name: "FK_stock_city_CityID",
                table: "stock");

            migrationBuilder.DropForeignKey(
                name: "FK_stockproduct_product_ProductID",
                table: "stockproduct");

            migrationBuilder.DropForeignKey(
                name: "FK_stockproduct_stock_StockID",
                table: "stockproduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_stockproduct",
                table: "stockproduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_stock",
                table: "stock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_branchproduct",
                table: "branchproduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_branch",
                table: "branch");

            migrationBuilder.RenameTable(
                name: "stockproduct",
                newName: "StockProduct");

            migrationBuilder.RenameTable(
                name: "stock",
                newName: "Stock");

            migrationBuilder.RenameTable(
                name: "branchproduct",
                newName: "BranchProduct");

            migrationBuilder.RenameTable(
                name: "branch",
                newName: "Branch");

            migrationBuilder.RenameIndex(
                name: "IX_stockproduct_StockID",
                table: "StockProduct",
                newName: "IX_StockProduct_StockID");

            migrationBuilder.RenameIndex(
                name: "IX_stock_CityID",
                table: "Stock",
                newName: "IX_Stock_CityID");

            migrationBuilder.RenameIndex(
                name: "IX_branchproduct_ProductID",
                table: "BranchProduct",
                newName: "IX_BranchProduct_ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_branch_CityID",
                table: "Branch",
                newName: "IX_Branch_CityID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockProduct",
                table: "StockProduct",
                columns: new[] { "ProductID", "StockID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stock",
                table: "Stock",
                column: "StockID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BranchProduct",
                table: "BranchProduct",
                columns: new[] { "BranchID", "ProductID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Branch",
                table: "Branch",
                column: "BranchID");

            migrationBuilder.AddForeignKey(
                name: "FK_Branch_city_CityID",
                table: "Branch",
                column: "CityID",
                principalTable: "city",
                principalColumn: "CityID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BranchProduct_Branch_BranchID",
                table: "BranchProduct",
                column: "BranchID",
                principalTable: "Branch",
                principalColumn: "BranchID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BranchProduct_product_ProductID",
                table: "BranchProduct",
                column: "ProductID",
                principalTable: "product",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_city_CityID",
                table: "Stock",
                column: "CityID",
                principalTable: "city",
                principalColumn: "CityID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockProduct_product_ProductID",
                table: "StockProduct",
                column: "ProductID",
                principalTable: "product",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockProduct_Stock_StockID",
                table: "StockProduct",
                column: "StockID",
                principalTable: "Stock",
                principalColumn: "StockID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
