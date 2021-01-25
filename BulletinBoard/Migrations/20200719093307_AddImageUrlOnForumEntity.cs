using Microsoft.EntityFrameworkCore.Migrations;

namespace BulletinBoard.Migrations
{
    public partial class AddImageUrlOnForumEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ForumImageUri",
                table: "Forum",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f6d54222-3065-48b8-86e3-8c0c7ad6e7cc",
                column: "ConcurrencyStamp",
                value: "549e80b0-7f6a-46a7-b958-d83ff13a3c22");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "59d0b519-75a3-40c9-a6a8-2b42ad9e5095",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0b5d7c0a-33dc-4546-98b6-1795e261a0dc", "AQAAAAEAACcQAAAAEG5cJPT8NmWwU65wQc3aaK2nEvgXp0kgJeP0AQ4U/HgMXg5I826qaDqCwrpkhvfe3A==", "270447d0-43a1-42d8-9414-e45d5c9c10b5" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForumImageUri",
                table: "Forum");

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
    }
}
