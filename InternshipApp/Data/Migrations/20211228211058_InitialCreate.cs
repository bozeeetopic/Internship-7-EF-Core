using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class InitialCreate : Migration
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
                    IsOrganiser = table.Column<bool>(type: "bit", nullable: false)
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
                    UpVotes = table.Column<int>(type: "int", nullable: false),
                    DownVotes = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResourceId = table.Column<int>(type: "int", nullable: true),
                    CommentId = table.Column<int>(type: "int", nullable: true),
                    Header = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Domain = table.Column<int>(type: "int", nullable: true)
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
                name: "Reactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsUpVote = table.Column<bool>(type: "bit", nullable: false),
                    ReactorId = table.Column<int>(type: "int", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false)
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
                        name: "FK_Reactions_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "IsOrganiser", "Name", "Password", "ReputationPoints", "Surname", "Username" },
                values: new object[,]
                {
                    { 1, true, "Ivo", "", 20000, "Sanader", "HDZ" },
                    { 2, false, "Marjan", "", 10000, "Marjanovic", "asdasd" },
                    { 3, false, "Ivo", "", 0, "Ivic", "asfffassf" },
                    { 4, false, "Pero", "", 0, "Peric", "sd" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "Date", "Discriminator", "Domain", "DownVotes", "Header", "Text", "UpVotes" },
                values: new object[] { 1, 1, new DateTime(2020, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Resource", 4, 0, "Hehe", "Hehehehehehehehehehe", 0 });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "Date", "Discriminator", "Domain", "DownVotes", "Header", "Text", "UpVotes" },
                values: new object[] { 2, 1, new DateTime(2020, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Resource", 4, 0, "Hoho", "hohohohhohohohohohohoho", 0 });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "Date", "Discriminator", "Domain", "DownVotes", "Header", "Text", "UpVotes" },
                values: new object[] { 3, 2, new DateTime(2020, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Resource", 3, 0, "Nešto smisleno", "Lorem ipsum or sumtin", 0 });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "CommentId", "Date", "Discriminator", "DownVotes", "ResourceId", "Text", "UpVotes" },
                values: new object[,]
                {
                    { 4, 1, null, new DateTime(2020, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Comment", 0, 1, "Hehehehehehehehehehe", 0 },
                    { 7, 1, null, new DateTime(2020, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Comment", 0, 2, "hohohohohoho", 0 }
                });

            migrationBuilder.InsertData(
                table: "Reactions",
                columns: new[] { "Id", "IsUpVote", "PostId", "ReactorId" },
                values: new object[,]
                {
                    { 1, true, 1, 1 },
                    { 2, true, 2, 1 },
                    { 3, true, 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "CommentId", "Date", "Discriminator", "DownVotes", "ResourceId", "Text", "UpVotes" },
                values: new object[] { 5, 2, 4, new DateTime(2020, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Comment", 0, 1, "Hehehehehehehehehehe", 0 });

            migrationBuilder.InsertData(
                table: "Reactions",
                columns: new[] { "Id", "IsUpVote", "PostId", "ReactorId" },
                values: new object[] { 4, true, 4, 1 });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "CommentId", "Date", "Discriminator", "DownVotes", "ResourceId", "Text", "UpVotes" },
                values: new object[] { 6, 3, 5, new DateTime(2020, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Comment", 0, 1, "Hehehehehehehehehehe", 0 });

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
                name: "IX_Reactions_PostId",
                table: "Reactions",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_ReactorId",
                table: "Reactions",
                column: "ReactorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reactions");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Members");
        }
    }
}
