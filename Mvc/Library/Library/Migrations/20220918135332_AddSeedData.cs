using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Migrations
{
    public partial class AddSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "ID", "CreatedDate", "FirstName", "Gender", "LastName", "ModifiedDate", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 9, 18, 16, 53, 32, 194, DateTimeKind.Local).AddTicks(6080), "Talha", 0, "Sağdan", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 2, new DateTime(2022, 9, 18, 16, 53, 32, 194, DateTimeKind.Local).AddTicks(6798), "Turna", 1, "Yurtsever", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 3, new DateTime(2022, 9, 18, 16, 53, 32, 194, DateTimeKind.Local).AddTicks(6803), "Ümit", 0, "Fidan", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 4, new DateTime(2022, 9, 18, 16, 53, 32, 194, DateTimeKind.Local).AddTicks(6804), "Pelin", 1, "Toş", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "CreatedDate", "ModifiedDate", "Password", "Role", "Status", "UserName" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 9, 18, 16, 53, 32, 192, DateTimeKind.Local).AddTicks(5014), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "$2a$11$ZPEGXhICHIk6XDl2k8QySeVBSMgAVgIjwoEc/0GPghU9tbmCSlyL2", 1, 0, "administrator" },
                    { 2, new DateTime(2022, 9, 18, 16, 53, 32, 193, DateTimeKind.Local).AddTicks(4809), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "$2a$11$ZR9Ili0QRLMG7r/J6NYwgu.zebQWgnk7cx9gQa0J8rcwnVPZmrFZC", 2, 0, "Umut" }
                });

            migrationBuilder.InsertData(
                table: "StudentDetail",
                columns: new[] { "ID", "BirthDay", "CreatedDate", "ModifiedDate", "PhoneNumber", "SchoolNumber", "Status", "StudentID" },
                values: new object[,]
                {
                    { 1, new DateTime(1997, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 18, 16, 53, 32, 194, DateTimeKind.Local).AddTicks(7344), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "100", 0, 1 },
                    { 2, new DateTime(1994, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 18, 16, 53, 32, 194, DateTimeKind.Local).AddTicks(8009), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "101", 0, 2 },
                    { 3, new DateTime(1992, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 18, 16, 53, 32, 194, DateTimeKind.Local).AddTicks(8014), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "102", 0, 3 },
                    { 4, new DateTime(1995, 11, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 18, 16, 53, 32, 194, DateTimeKind.Local).AddTicks(8016), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "103", 0, 4 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StudentDetail",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "StudentDetail",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "StudentDetail",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "StudentDetail",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "ID",
                keyValue: 4);
        }
    }
}
