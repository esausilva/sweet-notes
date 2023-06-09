using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class Alter_SpecialSomeone_UniqueIdentifier_Unique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UniqueIdentifier",
                table: "SpecialSomeone",
                type: "character varying(45)",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_SpecialSomeone_UniqueIdentifier",
                table: "SpecialSomeone",
                column: "UniqueIdentifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_SpecialSomeone_UniqueIdentifier",
                table: "SpecialSomeone");

            migrationBuilder.AlterColumn<string>(
                name: "UniqueIdentifier",
                table: "SpecialSomeone",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(45)",
                oldMaxLength: 45);
        }
    }
}
