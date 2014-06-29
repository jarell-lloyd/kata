using kata.Data.Entities;
using kata.Data.Mappers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kata.Data
{
	public class KataContext : DbContext
    {
		public KataContext()
			: base("kata")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;

						Database.SetInitializer(new CreateDatabaseIfNotExists<KataContext>());
        }
 
        public virtual DbSet<Ad> Ads { get; set; }
        public virtual DbSet<Newspaper> Newspapers { get; set; }
 
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AdMapper());
            modelBuilder.Configurations.Add(new NewspaperMapper());
 
            base.OnModelCreating(modelBuilder);
        }
    }
}
