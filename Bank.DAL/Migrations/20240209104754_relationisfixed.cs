using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bank.DAL.Migrations
{
    public partial class relationisfixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardFeatures_Features_TagId",
                table: "CardFeatures");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "isPoster",
                table: "CardImages");

            migrationBuilder.RenameColumn(
                name: "TagId",
                table: "CardFeatures",
                newName: "FeatureId");

            migrationBuilder.RenameIndex(
                name: "IX_CardFeatures_TagId",
                table: "CardFeatures",
                newName: "IX_CardFeatures_FeatureId");

            migrationBuilder.AddColumn<bool>(
                name: "IsInStock",
                table: "Cards",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_CardFeatures_Features_FeatureId",
                table: "CardFeatures",
                column: "FeatureId",
                principalTable: "Features",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardFeatures_Features_FeatureId",
                table: "CardFeatures");

            migrationBuilder.DropColumn(
                name: "IsInStock",
                table: "Cards");

            migrationBuilder.RenameColumn(
                name: "FeatureId",
                table: "CardFeatures",
                newName: "TagId");

            migrationBuilder.RenameIndex(
                name: "IX_CardFeatures_FeatureId",
                table: "CardFeatures",
                newName: "IX_CardFeatures_TagId");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Cards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "isPoster",
                table: "CardImages",
                type: "bit",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CardFeatures_Features_TagId",
                table: "CardFeatures",
                column: "TagId",
                principalTable: "Features",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
