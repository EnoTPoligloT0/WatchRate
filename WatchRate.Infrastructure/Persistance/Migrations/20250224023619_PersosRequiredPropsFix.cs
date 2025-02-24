using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchRate.Infrastucture.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class PersosRequiredPropsFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserWatchlist",
                table: "UserWatchlist");

            migrationBuilder.AlterColumn<string>(
                name: "BirthPlace",
                table: "Person",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "BirthDate",
                table: "Person",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserWatchlist",
                table: "UserWatchlist",
                columns: new[] { "Id", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserWatchlist_MovieId_UserId",
                table: "UserWatchlist",
                columns: new[] { "MovieId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserWatchlist",
                table: "UserWatchlist");

            migrationBuilder.DropIndex(
                name: "IX_UserWatchlist_MovieId_UserId",
                table: "UserWatchlist");

            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserName",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "BirthPlace",
                table: "Person",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BirthDate",
                table: "Person",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserWatchlist",
                table: "UserWatchlist",
                column: "Id");
        }
    }
}
