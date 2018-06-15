using Exo_Base.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exo_Base.Data.EFConfigs
{
    internal class NavigationTypeEntityConfig: EntityTypeConfiguration<NavigationTypeEntity>
    {
        public NavigationTypeEntityConfig()
        {
            ToTable("NavigationType");
            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("NavigationTypeId").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
            Property(p => p.NavigationTypeName).IsRequired().HasMaxLength(50);
        }
    }
}
