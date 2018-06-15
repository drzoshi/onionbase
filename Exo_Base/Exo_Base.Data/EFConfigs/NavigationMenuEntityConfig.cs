using Exo_Base.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Exo_Base.Data.EntityConfig
{
    internal class NavigationMenuEntityConfig: EntityTypeConfiguration<NavigationMenuEntity>
    {
        public NavigationMenuEntityConfig()
        {
            ToTable("NavigationMenu");
            HasKey(p => p.Id);
            Property(x => x.Id).HasColumnName("NavigationMenuId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Name).IsRequired().HasMaxLength(250);
            Property(p => p.DisplayName).IsRequired().HasMaxLength(250);
            Property(p => p.AreaName).IsOptional().HasMaxLength(250);
            Property(p => p.ControllerName).IsRequired().HasMaxLength(250);
            Property(p => p.ActionName).IsRequired().HasMaxLength(250);
            Property(p => p.RouteUrl).IsOptional().HasMaxLength(500);
            Property(p => p.ParentNavigationId).IsRequired();
            Property(p => p.NavigationTypeId).IsRequired();
            Property(p => p.DisplayOrder).IsRequired();
            Property(p => p.IsDisabled).IsRequired();
            Property(p => p.LastModifiedUserId).IsRequired();
            Property(p => p.InsertedOnUtc).IsRequired(); //.HasDefaultValueSql("getDateUtc()");
            Property(p => p.LastModifiedOnUtc).IsOptional();

            HasRequired(p => p.NavigationType).WithMany(p => p.NavigationMenus).HasForeignKey(p => p.NavigationTypeId);
            //HasMany(p => p.Roles).WithMany().Map(m =>
            //{
            //    m.MapLeftKey("NavigationId");
            //    m.MapRightKey("RoleId");
            //    m.ToTable("RoleNavigation");
            //});
        }
    }

    internal class RoleNavigationEntityConfig : EntityTypeConfiguration<RoleNavigationMenuEntity>
    {
        public RoleNavigationEntityConfig()
        {
            ToTable("RoleNavigationMenu");
            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("RoleNavigationMenuId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.RoleId).IsRequired();
            Property(p => p.NavigationId).IsRequired();
            HasRequired(p => p.Role).WithMany(p => p.NavigationMenus).HasForeignKey(p => p.RoleId);
            HasRequired(p => p.NavigationMenu).WithMany(p => p.Roles).HasForeignKey(p => p.NavigationId);
        }
    }
}
