using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkLocator.Migrations
{
    /// <inheritdoc />
    public partial class finTune : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Districts_Regions_RegionId",
                table: "Districts");

            migrationBuilder.DropForeignKey(
                name: "FK_Parks_Districts_DistrictId",
                table: "Parks");

            migrationBuilder.DropForeignKey(
                name: "FK_Streets_Districts_DistrictId",
                table: "Streets");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Streets",
                table: "Streets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Parks",
                table: "Parks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Districts",
                table: "Districts");

            migrationBuilder.RenameTable(
                name: "Streets",
                newName: "Street");

            migrationBuilder.RenameTable(
                name: "Parks",
                newName: "Park");

            migrationBuilder.RenameTable(
                name: "Districts",
                newName: "District");

            migrationBuilder.RenameIndex(
                name: "IX_Streets_Name",
                table: "Street",
                newName: "IX_Street_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Streets_DistrictId",
                table: "Street",
                newName: "IX_Street_DistrictId");

            migrationBuilder.RenameIndex(
                name: "IX_Parks_Name",
                table: "Park",
                newName: "IX_Park_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Parks_DistrictId",
                table: "Park",
                newName: "IX_Park_DistrictId");

            migrationBuilder.RenameIndex(
                name: "IX_Districts_RegionId",
                table: "District",
                newName: "IX_District_RegionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Street",
                table: "Street",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Park",
                table: "Park",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_District",
                table: "District",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    StateProvince = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Locality = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Region_Name",
                table: "Region",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_District_Region_RegionId",
                table: "District",
                column: "RegionId",
                principalTable: "Region",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Park_District_DistrictId",
                table: "Park",
                column: "DistrictId",
                principalTable: "District",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Street_District_DistrictId",
                table: "Street",
                column: "DistrictId",
                principalTable: "District",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_District_Region_RegionId",
                table: "District");

            migrationBuilder.DropForeignKey(
                name: "FK_Park_District_DistrictId",
                table: "Park");

            migrationBuilder.DropForeignKey(
                name: "FK_Street_District_DistrictId",
                table: "Street");

            migrationBuilder.DropTable(
                name: "Region");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Street",
                table: "Street");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Park",
                table: "Park");

            migrationBuilder.DropPrimaryKey(
                name: "PK_District",
                table: "District");

            migrationBuilder.RenameTable(
                name: "Street",
                newName: "Streets");

            migrationBuilder.RenameTable(
                name: "Park",
                newName: "Parks");

            migrationBuilder.RenameTable(
                name: "District",
                newName: "Districts");

            migrationBuilder.RenameIndex(
                name: "IX_Street_Name",
                table: "Streets",
                newName: "IX_Streets_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Street_DistrictId",
                table: "Streets",
                newName: "IX_Streets_DistrictId");

            migrationBuilder.RenameIndex(
                name: "IX_Park_Name",
                table: "Parks",
                newName: "IX_Parks_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Park_DistrictId",
                table: "Parks",
                newName: "IX_Parks_DistrictId");

            migrationBuilder.RenameIndex(
                name: "IX_District_RegionId",
                table: "Districts",
                newName: "IX_Districts_RegionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Streets",
                table: "Streets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Parks",
                table: "Parks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Districts",
                table: "Districts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Locality = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    StateProvince = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Regions_Name",
                table: "Regions",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_Districts_Regions_RegionId",
                table: "Districts",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parks_Districts_DistrictId",
                table: "Parks",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Streets_Districts_DistrictId",
                table: "Streets",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
