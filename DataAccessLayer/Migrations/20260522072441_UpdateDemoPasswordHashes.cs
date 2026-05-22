using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDemoPasswordHashes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                UPDATE [Users]
                SET [PasswordHash] = '2A97516C354B68848CDBD8F54A226A0A55B21ED138E207AD6C5CBB9C00AA5AEA'
                WHERE [Email] IN (
                    'customer@example.com',
                    'owner@example.com',
                    'driver@example.com',
                    'admin@example.com'
                );
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                UPDATE [Users]
                SET [PasswordHash] = 'demo'
                WHERE [Email] IN (
                    'customer@example.com',
                    'owner@example.com',
                    'driver@example.com',
                    'admin@example.com'
                );
                """);
        }
    }
}
