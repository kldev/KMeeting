using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KMeeting.Migrations.Meeting
{
    public partial class MeetingContextInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "km_events",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    MaxPeople = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    DayStart = table.Column<DateTime>(nullable: false),
                    TimeStart = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_km_events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "km_profiles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    IdentityId = table.Column<string>(nullable: true),
                    Fullname = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    About = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_km_profiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "km_attendees",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    MeetingEventId = table.Column<string>(nullable: true),
                    Fullname = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    ProfileId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_km_attendees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_km_attendees_km_events_MeetingEventId",
                        column: x => x.MeetingEventId,
                        principalTable: "km_events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_km_attendees_MeetingEventId",
                table: "km_attendees",
                column: "MeetingEventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "km_attendees");

            migrationBuilder.DropTable(
                name: "km_profiles");

            migrationBuilder.DropTable(
                name: "km_events");
        }
    }
}
