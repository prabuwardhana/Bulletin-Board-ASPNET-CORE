using Microsoft.EntityFrameworkCore.Migrations;

namespace BulletinBoard.Migrations
{
    public partial class AddImageUrlOnUserEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f6d54222-3065-48b8-86e3-8c0c7ad6e7cc",
                column: "ConcurrencyStamp",
                value: "96ffd597-19b1-4122-89fe-37a7b743aaee");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "59d0b519-75a3-40c9-a6a8-2b42ad9e5095",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "79e5d9e2-fe56-4ef5-8f80-8129833d84df", "AQAAAAEAACcQAAAAEDQEiETmjZ1TAOoI6V8gOvbXt+zkM+4BQxK4UZ+E+UZe9FnKfS1zqv0kqGvxnBnglw==", "fd2d41b9-157c-46b5-ba32-5ab9f67ec4c3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f6d54222-3065-48b8-86e3-8c0c7ad6e7cc",
                column: "ConcurrencyStamp",
                value: "9371eafb-7787-45de-98b0-e053c6f6dd6f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "59d0b519-75a3-40c9-a6a8-2b42ad9e5095",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "84c1c222-d549-49d1-ab7c-562dfae6f8e5", "AQAAAAEAACcQAAAAEHA2gOvgy7dBw19s/HzQudtlNHHQPKjLnYWR+N8fb3GjU0hWL21ILDV393eUlPY9WA==", "e4f0be57-78e8-43ad-9cdb-973d181ba733" });
        }
    }
}
