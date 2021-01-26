using Microsoft.EntityFrameworkCore.Migrations;

namespace Insurance.DataAccessLayer.Migrations
{
    public partial class addcontractor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Advisor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    LastName = table.Column<string>(type: "varchar(256)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(10)", nullable: true),
                    HealthStatus = table.Column<string>(type: "varchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advisor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Carrier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessName = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    BusinessAddress = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    BusinessPhoneNumber = table.Column<string>(type: "nvarchar(256)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MGA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessName = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    BusinessAddress = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    BusinessPhoneNumber = table.Column<string>(type: "nvarchar(256)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MGA", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FirstContractor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdvisorId = table.Column<int>(type: "int", nullable: true),
                    CarrierId = table.Column<int>(type: "int", nullable: true),
                    MGAId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirstContractor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FirstContractor_Advisor_AdvisorId",
                        column: x => x.AdvisorId,
                        principalTable: "Advisor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FirstContractor_Carrier_CarrierId",
                        column: x => x.CarrierId,
                        principalTable: "Carrier",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FirstContractor_MGA_MGAId",
                        column: x => x.MGAId,
                        principalTable: "MGA",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SecondContractor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdvisorId = table.Column<int>(type: "int", nullable: true),
                    CarrierId = table.Column<int>(type: "int", nullable: true),
                    MGAId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecondContractor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecondContractor_Advisor_AdvisorId",
                        column: x => x.AdvisorId,
                        principalTable: "Advisor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SecondContractor_Carrier_CarrierId",
                        column: x => x.CarrierId,
                        principalTable: "Carrier",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SecondContractor_MGA_MGAId",
                        column: x => x.MGAId,
                        principalTable: "MGA",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstContractorId = table.Column<int>(type: "int", nullable: false),
                    SecondContractorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contract_FirstContractor_FirstContractorId",
                        column: x => x.FirstContractorId,
                        principalTable: "FirstContractor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contract_SecondContractor_SecondContractorId",
                        column: x => x.SecondContractorId,
                        principalTable: "SecondContractor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contract_FirstContractorId",
                table: "Contract",
                column: "FirstContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_SecondContractorId",
                table: "Contract",
                column: "SecondContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_FirstContractor_AdvisorId",
                table: "FirstContractor",
                column: "AdvisorId");

            migrationBuilder.CreateIndex(
                name: "IX_FirstContractor_CarrierId",
                table: "FirstContractor",
                column: "CarrierId");

            migrationBuilder.CreateIndex(
                name: "IX_FirstContractor_MGAId",
                table: "FirstContractor",
                column: "MGAId");

            migrationBuilder.CreateIndex(
                name: "IX_SecondContractor_AdvisorId",
                table: "SecondContractor",
                column: "AdvisorId");

            migrationBuilder.CreateIndex(
                name: "IX_SecondContractor_CarrierId",
                table: "SecondContractor",
                column: "CarrierId");

            migrationBuilder.CreateIndex(
                name: "IX_SecondContractor_MGAId",
                table: "SecondContractor",
                column: "MGAId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "FirstContractor");

            migrationBuilder.DropTable(
                name: "SecondContractor");

            migrationBuilder.DropTable(
                name: "Advisor");

            migrationBuilder.DropTable(
                name: "Carrier");

            migrationBuilder.DropTable(
                name: "MGA");
        }
    }
}
