using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Planner.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_WeekDays_WeekDaysId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PatientWeekDayId",
                table: "Patients");

            migrationBuilder.RenameColumn(
                name: "WeekDaysId",
                table: "Patients",
                newName: "WeekDayId");

            migrationBuilder.RenameIndex(
                name: "IX_Patients_WeekDaysId",
                table: "Patients",
                newName: "IX_Patients_WeekDayId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_WeekDays_WeekDayId",
                table: "Patients",
                column: "WeekDayId",
                principalTable: "WeekDays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_WeekDays_WeekDayId",
                table: "Patients");

            migrationBuilder.RenameColumn(
                name: "WeekDayId",
                table: "Patients",
                newName: "WeekDaysId");

            migrationBuilder.RenameIndex(
                name: "IX_Patients_WeekDayId",
                table: "Patients",
                newName: "IX_Patients_WeekDaysId");

            migrationBuilder.AddColumn<Guid>(
                name: "PatientWeekDayId",
                table: "Patients",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_WeekDays_WeekDaysId",
                table: "Patients",
                column: "WeekDaysId",
                principalTable: "WeekDays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
