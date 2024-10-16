using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleAspNetCore8WithIdentityRoleBase.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateOrderAppConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderApps_Customers_CustomerID",
                table: "OrderApps");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderApps_Products_ProductID",
                table: "OrderApps");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "OrderApps",
                newName: "OrderAppId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderApps_Customers_CustomerID",
                table: "OrderApps",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderApps_Products_ProductID",
                table: "OrderApps",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderApps_Customers_CustomerID",
                table: "OrderApps");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderApps_Products_ProductID",
                table: "OrderApps");

            migrationBuilder.RenameColumn(
                name: "OrderAppId",
                table: "OrderApps",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderApps_Customers_CustomerID",
                table: "OrderApps",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderApps_Products_ProductID",
                table: "OrderApps",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
