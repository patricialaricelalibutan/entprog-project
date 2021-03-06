using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FINALTEST1.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 60, nullable: false),
                    LastName = table.Column<string>(maxLength: 60, nullable: false),
                    City = table.Column<string>(maxLength: 60, nullable: false),
                    Email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Monetaries",
                columns: table => new
                {
                    MonetaryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    Image = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Validate = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monetaries", x => x.MonetaryID);
                    table.ForeignKey(
                        name: "FK_Monetaries_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InKindID = table.Column<int>(nullable: true),
                    Item = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: true),
                    UnitCost = table.Column<decimal>(nullable: true),
                    TotalCost = table.Column<decimal>(nullable: true),
                    Out = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionID);
                });

            migrationBuilder.CreateTable(
                name: "InKinds",
                columns: table => new
                {
                    InKindID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(nullable: false),
                    TransactionID = table.Column<int>(nullable: true),
                    Item = table.Column<string>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InKinds", x => x.InKindID);
                    table.ForeignKey(
                        name: "FK_InKinds_Transactions_TransactionID",
                        column: x => x.TransactionID,
                        principalTable: "Transactions",
                        principalColumn: "TransactionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InKinds_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InKinds_TransactionID",
                table: "InKinds",
                column: "TransactionID");

            migrationBuilder.CreateIndex(
                name: "IX_InKinds_UserID",
                table: "InKinds",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Monetaries_UserID",
                table: "Monetaries",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_InKindID",
                table: "Transactions",
                column: "InKindID");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_InKinds_InKindID",
                table: "Transactions",
                column: "InKindID",
                principalTable: "InKinds",
                principalColumn: "InKindID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InKinds_Transactions_TransactionID",
                table: "InKinds");

            migrationBuilder.DropTable(
                name: "Monetaries");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "InKinds");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
