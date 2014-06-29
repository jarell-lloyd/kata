using kata.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kata.Data.Mappers
{
	public class AdMapper : EntityTypeConfiguration<Ad>
	{
		public AdMapper()
		{
			this.ToTable("Ads");

			this.HasKey(o => o.Id);
			this.Property(o => o.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
			this.Property(o => o.Id).IsRequired();

			this.Property(o => o.Name).IsRequired();
			this.Property(o => o.Description).IsRequired();

			//this.HasRequired(e => e.Newspaper).WithMany(e => e.Ads).HasForeignKey(k => k.NewspaperId);
		}
	}
}
