/*
 * CR-1 - remove redundant usings - System, System.Collections.Generic, System.Linq, System.Text, System.Threading.Tasks
 */
using FarmApp.DAL.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmApp.DAL.EF
{
    /*
     * CR-1
     * Make class internal
     * Add comments to class and members
     */
    public class FarmContext : DbContext
	{
		public DbSet<Agriculture> Agricultures { get; set; }

		public DbSet<Crop> Crops { get; set; }

		public DbSet<Farm> Farms { get; set; }

		public DbSet<Farmer> Farmers { get; set; }

		public DbSet<Region> Regions { get; set; }


		static FarmContext()
		{
			Database.SetInitializer(new MigrateDatabaseToLatestVersion<FarmContext, Configuration>());
		}

		public FarmContext()
			: base("FarmContext")
		{
            
		}

		public FarmContext(string connectionString)
			: base(connectionString)
		{
		}
	}
}
