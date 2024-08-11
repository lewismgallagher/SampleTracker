using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class PluralizedSampleandSampleTypeEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sample_Racks_RackId",
                table: "Sample");

            migrationBuilder.DropForeignKey(
                name: "FK_Sample_SampleType_SampleTypeId",
                table: "Sample");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SampleType",
                table: "SampleType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sample",
                table: "Sample");

            migrationBuilder.RenameTable(
                name: "SampleType",
                newName: "SampleTypes");

            migrationBuilder.RenameTable(
                name: "Sample",
                newName: "Samples");

            migrationBuilder.RenameIndex(
                name: "IX_Sample_SampleTypeId",
                table: "Samples",
                newName: "IX_Samples_SampleTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Sample_RackId",
                table: "Samples",
                newName: "IX_Samples_RackId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SampleTypes",
                table: "SampleTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Samples",
                table: "Samples",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Samples_Racks_RackId",
                table: "Samples",
                column: "RackId",
                principalTable: "Racks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Samples_SampleTypes_SampleTypeId",
                table: "Samples",
                column: "SampleTypeId",
                principalTable: "SampleTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Samples_Racks_RackId",
                table: "Samples");

            migrationBuilder.DropForeignKey(
                name: "FK_Samples_SampleTypes_SampleTypeId",
                table: "Samples");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SampleTypes",
                table: "SampleTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Samples",
                table: "Samples");

            migrationBuilder.RenameTable(
                name: "SampleTypes",
                newName: "SampleType");

            migrationBuilder.RenameTable(
                name: "Samples",
                newName: "Sample");

            migrationBuilder.RenameIndex(
                name: "IX_Samples_SampleTypeId",
                table: "Sample",
                newName: "IX_Sample_SampleTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Samples_RackId",
                table: "Sample",
                newName: "IX_Sample_RackId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SampleType",
                table: "SampleType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sample",
                table: "Sample",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sample_Racks_RackId",
                table: "Sample",
                column: "RackId",
                principalTable: "Racks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sample_SampleType_SampleTypeId",
                table: "Sample",
                column: "SampleTypeId",
                principalTable: "SampleType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
