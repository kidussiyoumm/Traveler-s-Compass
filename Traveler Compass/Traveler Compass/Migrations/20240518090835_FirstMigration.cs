using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Traveler_Compass.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "agents",
                columns: table => new
                {
                    agentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    agentFristName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    agentLastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    agentGender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    companyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    passwordKey = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    phoneNumber = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agents", x => x.agentId);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    passwordKey = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    phoneNumber = table.Column<long>(type: "bigint", nullable: true),
                    gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isAgent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "packages",
                columns: table => new
                {
                    packageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    price = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false),
                    agentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_packages", x => x.packageId);
                    table.ForeignKey(
                        name: "FK_packages_agents_agentId",
                        column: x => x.agentId,
                        principalTable: "agents",
                        principalColumn: "agentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_packages_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "itineraries",
                columns: table => new
                {
                    itineraryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    price = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: true),
                    packageId = table.Column<int>(type: "int", nullable: true),
                    agentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_itineraries", x => x.itineraryId);
                    table.ForeignKey(
                        name: "FK_itineraries_agents_agentId",
                        column: x => x.agentId,
                        principalTable: "agents",
                        principalColumn: "agentId");
                    table.ForeignKey(
                        name: "FK_itineraries_packages_packageId",
                        column: x => x.packageId,
                        principalTable: "packages",
                        principalColumn: "packageId");
                    table.ForeignKey(
                        name: "FK_itineraries_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_itineraries_agentId",
                table: "itineraries",
                column: "agentId");

            migrationBuilder.CreateIndex(
                name: "IX_itineraries_packageId",
                table: "itineraries",
                column: "packageId");

            migrationBuilder.CreateIndex(
                name: "IX_itineraries_userId",
                table: "itineraries",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_packages_agentId",
                table: "packages",
                column: "agentId");

            migrationBuilder.CreateIndex(
                name: "IX_packages_userId",
                table: "packages",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "itineraries");

            migrationBuilder.DropTable(
                name: "packages");

            migrationBuilder.DropTable(
                name: "agents");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
