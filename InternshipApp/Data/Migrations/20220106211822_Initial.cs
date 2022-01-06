using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReputationPoints = table.Column<int>(type: "int", nullable: false),
                    BannedUntil = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpVotes = table.Column<int>(type: "int", nullable: true),
                    DownVotes = table.Column<int>(type: "int", nullable: true),
                    ResourceId = table.Column<int>(type: "int", nullable: true),
                    CommentId = table.Column<int>(type: "int", nullable: true),
                    Header = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Domain = table.Column<int>(type: "int", nullable: true),
                    SeenCounter = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Members_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Members",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Posts_Posts_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Posts_Posts_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Perceptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PerceiverId = table.Column<int>(type: "int", nullable: false),
                    ResourceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perceptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Perceptions_Members_PerceiverId",
                        column: x => x.PerceiverId,
                        principalTable: "Members",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Perceptions_Posts_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsUpVote = table.Column<bool>(type: "bit", nullable: false),
                    ReactorId = table.Column<int>(type: "int", nullable: false),
                    CommentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reactions_Members_ReactorId",
                        column: x => x.ReactorId,
                        principalTable: "Members",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reactions_Posts_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "BannedUntil", "Name", "Password", "ReputationPoints", "Surname", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ivo", "", 1000000, "Sanader", "HDZ" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Marjan", "", 10000, "Marjanovic", "asdasd" },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ivo", "", 1, "Ivic", "asfffassf" },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pero", "", 1, "Peric", "sd" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "Date", "Discriminator", "Domain", "Header", "SeenCounter", "Text" },
                values: new object[] { 1, 1, new DateTime(2020, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Resource", 4, "Hehe", 4, "Hehehehehehehehehehe" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "Date", "Discriminator", "Domain", "Header", "SeenCounter", "Text" },
                values: new object[] { 2, 1, new DateTime(2020, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Resource", 4, "Hoho", 4, "hohohohhohohohohohohoho" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "Date", "Discriminator", "Domain", "Header", "SeenCounter", "Text" },
                values: new object[] { 3, 2, new DateTime(2020, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Resource", 3, "Nešto smisleno", 4, "Lorem ipsum or sumtin" });

            migrationBuilder.InsertData(
                table: "Perceptions",
                columns: new[] { "Id", "PerceiverId", "ResourceId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 1, 3 }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "CommentId", "Date", "Discriminator", "DownVotes", "ResourceId", "Text", "UpVotes" },
                values: new object[,]
                {
                    { 4, 1, null, new DateTime(2020, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Comment", 1, 1, "Hehehehehehehehehehe", 3 },
                    { 7, 1, null, new DateTime(2020, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Comment", 1, 2, "hohohohohoho", 3 }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "CommentId", "Date", "Discriminator", "DownVotes", "ResourceId", "Text", "UpVotes" },
                values: new object[] { 5, 2, 4, new DateTime(2020, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Comment", 1, 1, "Hehehehehehehehehehe", 3 });

            migrationBuilder.InsertData(
                table: "Reactions",
                columns: new[] { "Id", "CommentId", "IsUpVote", "ReactorId" },
                values: new object[] { 1, 4, true, 1 });

            migrationBuilder.InsertData(
                table: "Reactions",
                columns: new[] { "Id", "CommentId", "IsUpVote", "ReactorId" },
                values: new object[] { 4, 7, true, 1 });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "CommentId", "Date", "Discriminator", "DownVotes", "ResourceId", "Text", "UpVotes" },
                values: new object[] { 6, 3, 5, new DateTime(2020, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Comment", 1, 1, "Hehehehehehehehehehe", 3 });

            migrationBuilder.InsertData(
                table: "Reactions",
                columns: new[] { "Id", "CommentId", "IsUpVote", "ReactorId" },
                values: new object[] { 2, 5, true, 1 });

            migrationBuilder.InsertData(
                table: "Reactions",
                columns: new[] { "Id", "CommentId", "IsUpVote", "ReactorId" },
                values: new object[] { 3, 6, true, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Perceptions_PerceiverId",
                table: "Perceptions",
                column: "PerceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Perceptions_ResourceId",
                table: "Perceptions",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AuthorId",
                table: "Posts",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CommentId",
                table: "Posts",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ResourceId",
                table: "Posts",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_CommentId",
                table: "Reactions",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_ReactorId",
                table: "Reactions",
                column: "ReactorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Perceptions");

            migrationBuilder.DropTable(
                name: "Reactions");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Members");
        }
    }
}
