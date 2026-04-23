using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FF14BOM.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BOM",
                columns: table => new
                {
                    Pro_Id = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Mtr_id = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Use_QTY = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BOM", x => new { x.Pro_Id, x.Mtr_id });
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Mtr_id = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Mtr_type = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Mtr_Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NPC_Sell = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Mtr_id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialType",
                columns: table => new
                {
                    Mtr_type = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Mtr_Type_name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialType", x => x.Mtr_type);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Pro_Id = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Pro_Level = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Pro_Type = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Pro_part = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Pro_Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Pro_Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BOM");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "MaterialType");

            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}
