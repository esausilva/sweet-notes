using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class Alter_SpecialSomeone_UniqueIdentifier_Length : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UniqueIdentifier",
                table: "SpecialSomeone",
                type: "character varying(26)",
                maxLength: 26,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(45)",
                oldMaxLength: 45);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UniqueIdentifier",
                table: "SpecialSomeone",
                type: "character varying(45)",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(26)",
                oldMaxLength: 26);
        }
    }
}
