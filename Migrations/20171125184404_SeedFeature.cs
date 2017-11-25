using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace app.Migrations {
	public partial class SeedFeature : Migration {
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('ABS')");
			migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('AC')");
			migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('EFI')");
			migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Power steering')");
			migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Power windows')");
		}

		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.Sql("DELETE FROM Features WHERE Name IN ('ABC', 'AC', 'EFI', 'Power steering', 'Power windows')");
		}
	}
}