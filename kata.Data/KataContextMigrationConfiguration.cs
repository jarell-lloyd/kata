using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kata.Data
{
	public class KataContextMigrationConfiguration : DbMigrationsConfiguration<KataContext>
	{
		public KataContextMigrationConfiguration()
		{
			//this.AutomaticMigrationsEnabled = false;
			//this.AutomaticMigrationDataLossAllowed = false;
		}

	}
}
