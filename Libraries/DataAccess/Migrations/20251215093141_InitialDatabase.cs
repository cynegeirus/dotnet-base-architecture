using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "audit");

            migrationBuilder.EnsureSchema(
                name: "main");

            migrationBuilder.EnsureSchema(
                name: "management");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "DatabaseAuditLog",
                schema: "audit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsUpdated = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    TableName = table.Column<string>(type: "text", nullable: true),
                    EntityName = table.Column<string>(type: "text", nullable: true),
                    PrimaryKey = table.Column<string>(type: "text", nullable: true),
                    Operation = table.Column<string>(type: "text", nullable: true),
                    Json = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatabaseAuditLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ErrorLog",
                schema: "audit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsUpdated = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    LoggedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Level = table.Column<string>(type: "text", nullable: true),
                    Source = table.Column<string>(type: "text", nullable: true),
                    Message = table.Column<string>(type: "text", nullable: true),
                    ExceptionType = table.Column<string>(type: "text", nullable: true),
                    ExceptionMessage = table.Column<string>(type: "text", nullable: true),
                    StackTrace = table.Column<string>(type: "text", nullable: true),
                    InnerException = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsUpdated = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
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
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsUpdated = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrafficAuditLog",
                schema: "audit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsUpdated = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    AuditTraceId = table.Column<Guid>(type: "uuid", nullable: false),
                    AuditLoggedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    RequestReceivedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ResponseSendDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    AuditCreatedByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    AuditUpdatedByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    HttpMethod = table.Column<string>(type: "text", nullable: true),
                    HttpRequestPath = table.Column<string>(type: "text", nullable: true),
                    HttpQueryString = table.Column<string>(type: "text", nullable: true),
                    HttpScheme = table.Column<string>(type: "text", nullable: true),
                    HttpProtocol = table.Column<string>(type: "text", nullable: true),
                    HttpRequestHeadersJson = table.Column<string>(type: "text", nullable: true),
                    HttpRequestBody = table.Column<string>(type: "text", nullable: true),
                    HttpResponseStatusCode = table.Column<int>(type: "integer", nullable: true),
                    HttpResponseIsSuccess = table.Column<bool>(type: "boolean", nullable: true),
                    HttpResponseContentType = table.Column<string>(type: "text", nullable: true),
                    HttpResponseContentLength = table.Column<long>(type: "bigint", nullable: true),
                    HttpResponseBodySummary = table.Column<string>(type: "text", nullable: true),
                    ConnectionId = table.Column<string>(type: "text", nullable: true),
                    ClientRemoteIpAddress = table.Column<string>(type: "text", nullable: true),
                    ClientRemotePort = table.Column<string>(type: "text", nullable: true),
                    ServerLocalIpAddress = table.Column<string>(type: "text", nullable: true),
                    ServerLocalPort = table.Column<string>(type: "text", nullable: true),
                    RequestHost = table.Column<string>(type: "text", nullable: true),
                    ServerMachineName = table.Column<string>(type: "text", nullable: true),
                    ApplicationName = table.Column<string>(type: "text", nullable: true),
                    IsAuthenticated = table.Column<bool>(type: "boolean", nullable: true),
                    AuthenticatedUserName = table.Column<string>(type: "text", nullable: true),
                    AuthenticatedUserRoles = table.Column<string>(type: "text", nullable: true),
                    UserAgent = table.Column<string>(type: "text", nullable: true),
                    Referrer = table.Column<string>(type: "text", nullable: true),
                    RequestDurationMilliseconds = table.Column<long>(type: "bigint", nullable: true),
                    HasException = table.Column<bool>(type: "boolean", nullable: true),
                    ExceptionType = table.Column<string>(type: "text", nullable: true),
                    ExceptionMessage = table.Column<string>(type: "text", nullable: true),
                    ExceptionStackTrace = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrafficAuditLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "management",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsUpdated = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
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
                name: "UserRole",
                schema: "management",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    CreatedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsUpdated = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
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
                name: "DatabaseAuditLog",
                schema: "audit");

            migrationBuilder.DropTable(
                name: "ErrorLog",
                schema: "audit");

            migrationBuilder.DropTable(
                name: "Notification",
                schema: "main");

            migrationBuilder.DropTable(
                name: "TrafficAuditLog",
                schema: "audit");

            migrationBuilder.DropTable(
                name: "UserRole",
                schema: "management");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "management");

            migrationBuilder.DropTable(
                name: "User",
                schema: "management");
        }
    }
}
