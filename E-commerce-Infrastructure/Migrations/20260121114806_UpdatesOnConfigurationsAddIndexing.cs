using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerce_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatesOnConfigurationsAddIndexing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Variations_Name_CategoryId",
                table: "Variations",
                columns: new[] { "Name", "CategoryId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_FirstName",
                table: "Users",
                column: "FirstName");

            migrationBuilder.CreateIndex(
                name: "IX_Promotions_Name",
                table: "Promotions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductItems_SKU",
                table: "ProductItems",
                column: "SKU",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethods_AccountNumber",
                table: "PaymentMethods",
                column: "AccountNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserName",
                table: "Accounts",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Variations_Name_CategoryId",
                table: "Variations");

            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_FirstName",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Promotions_Name",
                table: "Promotions");

            migrationBuilder.DropIndex(
                name: "IX_Products_Name",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_ProductItems_SKU",
                table: "ProductItems");

            migrationBuilder.DropIndex(
                name: "IX_PaymentMethods_AccountNumber",
                table: "PaymentMethods");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_UserName",
                table: "Accounts");
        }
    }
}
