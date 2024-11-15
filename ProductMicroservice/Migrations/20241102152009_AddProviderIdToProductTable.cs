using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductMicroservice.Migrations
{
    /// <inheritdoc />
    public partial class AddProviderIdToProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProviderId",
                table: "Products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProviderId",
                table: "Products");
        }
    }
}
