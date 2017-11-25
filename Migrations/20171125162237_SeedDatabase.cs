using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace app.Migrations {
	public partial class SeedDatabase : Migration {
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES ('Toyota')");
			migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES ('Honda')");
			migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES ('Suzuki')");

			migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Corolla', (SELECT ID FROM Makes WHERE Name = 'Toyota'))");
			migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Prius', (SELECT ID FROM Makes WHERE Name = 'Toyota'))");
			migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Aqua', (SELECT ID FROM Makes WHERE Name = 'Toyota'))");
			migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Civic', (SELECT ID FROM Makes WHERE Name = 'Honda'))");
			migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('City', (SELECT ID FROM Makes WHERE Name = 'Honda'))");
			migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Accord', (SELECT ID FROM Makes WHERE Name = 'Honda'))");
			migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Life', (SELECT ID FROM Makes WHERE Name = 'Honda'))");
			migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Cultus', (SELECT ID FROM Makes WHERE Name = 'Suzuki'))");
			migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Mehran', (SELECT ID FROM Makes WHERE Name = 'Suzuki'))");
			migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('WagonR', (SELECT ID FROM Makes WHERE Name = 'Suzuki'))");
			migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Liana', (SELECT ID FROM Makes WHERE Name = 'Suzuki'))");
			migrationBuilder.Sql("INSERT INTO Models (Name, MakeId) VALUES ('Alto', (SELECT ID FROM Makes WHERE Name = 'Suzuki'))");
			
		}

		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.Sql("DELETE FROM Makes WHERE Name IN ('Toyota', 'Honda', 'Suzuki')");
		}
	}
}