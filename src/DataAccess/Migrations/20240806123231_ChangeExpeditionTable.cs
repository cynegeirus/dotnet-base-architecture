using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeExpeditionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "expedition");

            migrationBuilder.EnsureSchema(
                name: "management");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "Expedition",
                schema: "expedition",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2024, 8, 6, 15, 32, 31, 620, DateTimeKind.Local).AddTicks(2876)),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsUpdated = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2024, 8, 6, 15, 32, 31, 620, DateTimeKind.Local).AddTicks(2997)),
                    UpdatedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2024, 8, 6, 15, 32, 31, 620, DateTimeKind.Local).AddTicks(3123)),
                    DeletedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    Code = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    TotalDuration = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expedition", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpeditionStation",
                schema: "expedition",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2024, 8, 6, 15, 32, 31, 620, DateTimeKind.Local).AddTicks(3228)),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsUpdated = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2024, 8, 6, 15, 32, 31, 620, DateTimeKind.Local).AddTicks(3364)),
                    UpdatedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2024, 8, 6, 15, 32, 31, 620, DateTimeKind.Local).AddTicks(3486)),
                    DeletedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    ExpeditionId = table.Column<Guid>(type: "uuid", nullable: false),
                    StationId = table.Column<Guid>(type: "uuid", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpeditionStation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                schema: "management",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2024, 8, 6, 15, 32, 31, 619, DateTimeKind.Local).AddTicks(9695)),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsUpdated = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2024, 8, 6, 15, 32, 31, 619, DateTimeKind.Local).AddTicks(9904)),
                    UpdatedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2024, 8, 6, 15, 32, 31, 620, DateTimeKind.Local).AddTicks(285)),
                    DeletedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    Priority = table.Column<int>(type: "integer", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    ControllerName = table.Column<string>(type: "text", nullable: true),
                    ActionName = table.Column<string>(type: "text", nullable: true),
                    Icon = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                schema: "management",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2024, 8, 6, 15, 32, 31, 620, DateTimeKind.Local).AddTicks(980)),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsUpdated = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2024, 8, 6, 15, 32, 31, 620, DateTimeKind.Local).AddTicks(1113)),
                    UpdatedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2024, 8, 6, 15, 32, 31, 620, DateTimeKind.Local).AddTicks(1240)),
                    DeletedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "management",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2024, 8, 6, 15, 32, 31, 620, DateTimeKind.Local).AddTicks(1347)),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsUpdated = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2024, 8, 6, 15, 32, 31, 620, DateTimeKind.Local).AddTicks(1468)),
                    UpdatedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2024, 8, 6, 15, 32, 31, 620, DateTimeKind.Local).AddTicks(1595)),
                    DeletedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemParameter",
                schema: "management",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2024, 8, 6, 15, 32, 31, 620, DateTimeKind.Local).AddTicks(1711)),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsUpdated = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2024, 8, 6, 15, 32, 31, 620, DateTimeKind.Local).AddTicks(1841)),
                    UpdatedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2024, 8, 6, 15, 32, 31, 620, DateTimeKind.Local).AddTicks(1974)),
                    DeletedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemParameter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Train",
                schema: "expedition",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2024, 8, 6, 15, 32, 31, 620, DateTimeKind.Local).AddTicks(3989)),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsUpdated = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2024, 8, 6, 15, 32, 31, 620, DateTimeKind.Local).AddTicks(4112)),
                    UpdatedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2024, 8, 6, 15, 32, 31, 620, DateTimeKind.Local).AddTicks(4239)),
                    DeletedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    Code = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Url = table.Column<string>(type: "text", nullable: true),
                    IsReady = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Train", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "management",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2024, 8, 6, 15, 32, 31, 620, DateTimeKind.Local).AddTicks(2079)),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsUpdated = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2024, 8, 6, 15, 32, 31, 620, DateTimeKind.Local).AddTicks(2203)),
                    UpdatedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2024, 8, 6, 15, 32, 31, 620, DateTimeKind.Local).AddTicks(2335)),
                    DeletedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Username = table.Column<string>(type: "text", nullable: true),
                    MailAddress = table.Column<string>(type: "text", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "bytea", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpeditionExpeditionStation",
                schema: "expedition",
                columns: table => new
                {
                    ExpeditionId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpeditionStationsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpeditionExpeditionStation", x => new { x.ExpeditionId, x.ExpeditionStationsId });
                    table.ForeignKey(
                        name: "FK_ExpeditionExpeditionStation_ExpeditionStation_ExpeditionSta~",
                        column: x => x.ExpeditionStationsId,
                        principalSchema: "expedition",
                        principalTable: "ExpeditionStation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpeditionExpeditionStation_Expedition_ExpeditionId",
                        column: x => x.ExpeditionId,
                        principalSchema: "expedition",
                        principalTable: "Expedition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Station",
                schema: "expedition",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2024, 8, 6, 15, 32, 31, 620, DateTimeKind.Local).AddTicks(3592)),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsUpdated = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2024, 8, 6, 15, 32, 31, 620, DateTimeKind.Local).AddTicks(3735)),
                    UpdatedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2024, 8, 6, 15, 32, 31, 620, DateTimeKind.Local).AddTicks(3879)),
                    DeletedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    ExpeditionStationId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Station", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Station_ExpeditionStation_ExpeditionStationId",
                        column: x => x.ExpeditionStationId,
                        principalSchema: "expedition",
                        principalTable: "ExpeditionStation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MenuRole",
                schema: "management",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2024, 8, 6, 15, 32, 31, 620, DateTimeKind.Local).AddTicks(504)),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsUpdated = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2024, 8, 6, 15, 32, 31, 620, DateTimeKind.Local).AddTicks(680)),
                    UpdatedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2024, 8, 6, 15, 32, 31, 620, DateTimeKind.Local).AddTicks(855)),
                    DeletedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: true),
                    MenuId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuRole_Menu_MenuId",
                        column: x => x.MenuId,
                        principalSchema: "management",
                        principalTable: "Menu",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MenuRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "management",
                        principalTable: "Role",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                schema: "management",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2024, 8, 6, 15, 32, 31, 620, DateTimeKind.Local).AddTicks(2439)),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsUpdated = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2024, 8, 6, 15, 32, 31, 620, DateTimeKind.Local).AddTicks(2597)),
                    UpdatedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2024, 8, 6, 15, 32, 31, 620, DateTimeKind.Local).AddTicks(2756)),
                    DeletedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "management",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "management",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpeditionExpeditionStation_ExpeditionStationsId",
                schema: "expedition",
                table: "ExpeditionExpeditionStation",
                column: "ExpeditionStationsId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuRole_MenuId",
                schema: "management",
                table: "MenuRole",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuRole_RoleId",
                schema: "management",
                table: "MenuRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Station_ExpeditionStationId",
                schema: "expedition",
                table: "Station",
                column: "ExpeditionStationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                schema: "management",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                schema: "management",
                table: "UserRole",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpeditionExpeditionStation",
                schema: "expedition");

            migrationBuilder.DropTable(
                name: "MenuRole",
                schema: "management");

            migrationBuilder.DropTable(
                name: "Notification",
                schema: "management");

            migrationBuilder.DropTable(
                name: "Station",
                schema: "expedition");

            migrationBuilder.DropTable(
                name: "SystemParameter",
                schema: "management");

            migrationBuilder.DropTable(
                name: "Train",
                schema: "expedition");

            migrationBuilder.DropTable(
                name: "UserRole",
                schema: "management");

            migrationBuilder.DropTable(
                name: "Expedition",
                schema: "expedition");

            migrationBuilder.DropTable(
                name: "Menu",
                schema: "management");

            migrationBuilder.DropTable(
                name: "ExpeditionStation",
                schema: "expedition");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "management");

            migrationBuilder.DropTable(
                name: "User",
                schema: "management");
        }
    }
}
