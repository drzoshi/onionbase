using Exo_Base.Core.DomainModels;
using Exo_Base.Data.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Exo_Base.Data
{
    public class EFConfig
    {
        public static void ConfigureEF(DbModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema("Exo_Base");
            ////Desable cascadeDelete convention of entire context
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Entity<ApplicationIdentityUserLogin>().Map(c =>
            {
                c.ToTable("UserLogin");
                c.Properties(p => new
                {
                    p.UserId,
                    p.LoginProvider,
                    p.ProviderKey
                });
            }).HasKey(p => new { p.LoginProvider, p.ProviderKey, p.UserId });

            modelBuilder.Entity<ApplicationIdentityRole>().Map(c =>
            {
                c.ToTable("Role");
                c.Property(p => p.Id).HasColumnName("RoleId");
                c.Properties(p => new
                {
                    p.Name,
                    p.Description
                });
            }).HasKey(p => p.Id);
            modelBuilder.Entity<ApplicationIdentityRole>().Property(x=>x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<ApplicationIdentityRole>().HasMany(c => c.Users).WithRequired().HasForeignKey(c => c.RoleId);

            modelBuilder.Entity<ApplicationIdentityUser>().Map(c =>
            {
                c.ToTable("User");
                c.Property(p => p.Id).HasColumnName("UserId");
                c.Properties(p => new
                {
                    p.AccessFailedCount,
                    p.Email,
                    p.EmailConfirmed,
                    p.PasswordHash,
                    p.PhoneNumber,
                    p.PhoneNumberConfirmed,
                    p.TwoFactorEnabled,
                    p.SecurityStamp,
                    p.LockoutEnabled,
                    p.LockoutEndDateUtc,
                    p.UserName,

                    p.FirstName,
                    p.MiddleName,
                    p.LastName
                });
            }).HasKey(c => c.Id);
            modelBuilder.Entity<ApplicationIdentityUser>().Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<ApplicationIdentityUser>().HasMany(c => c.Logins).WithOptional().HasForeignKey(c => c.UserId);
            modelBuilder.Entity<ApplicationIdentityUser>().HasMany(c => c.Claims).WithOptional().HasForeignKey(c => c.UserId);
            modelBuilder.Entity<ApplicationIdentityUser>().HasMany(c => c.Roles).WithRequired().HasForeignKey(c => c.UserId);

            modelBuilder.Entity<ApplicationIdentityUserRole>().Map(c =>
            {
                c.ToTable("UserRole");
                c.Properties(p => new
                {
                    p.UserId,
                    p.RoleId
                });
            }).HasKey(c => new { c.UserId, c.RoleId });

            modelBuilder.Entity<ApplicationIdentityUserClaim>().Map(c =>
            {
                c.ToTable("UserClaim");
                c.Property(p => p.Id).HasColumnName("UserClaimId");
                c.Properties(p => new
                {
                    p.UserId,
                    p.ClaimValue,
                    p.ClaimType
                });
            }).HasKey(c => c.Id);
            modelBuilder.Entity<ApplicationIdentityUserClaim>().Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //disable cascade Delete For Perticular relationship.
            //modelBuilder.Entity<Devices>().HasRequired(x => x.User).WithRequiredDependent().WillCascadeOnDelete(false);
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();




            //modelBuilder.Entity<Navigation>().Map(c =>
            //{
            //    c.ToTable("Navigation");
            //    c.Property(p => p.Id).HasColumnName("NavigationId");
            //    c.Properties(p => new {
            //        p.Name,
            //        p.DisplayName,
            //        p.ActionName,
            //        p.ControllerName,
            //        p.AreaName,
            //        p.LinkUrl,
            //        p.Disabled,
            //        p.InsertedOn,
            //        p.UpdatedOn
            //    });
            //}).HasKey(p=>p.Id);
            //modelBuilder.Entity<Navigation>().Property(p => p.Name).IsRequired().HasMaxLength(150);
            //modelBuilder.Entity<Navigation>().Property(p => p.DisplayName).IsRequired().HasMaxLength(250);
            //modelBuilder.Entity<Navigation>().Property(p => p.ActionName).IsRequired().HasMaxLength(150);
            //modelBuilder.Entity<Navigation>().Property(p => p.ControllerName).IsRequired().HasMaxLength(150);
            //modelBuilder.Entity<Navigation>().Property(p => p.AreaName).IsOptional().HasMaxLength(150);
            //modelBuilder.Entity<Navigation>().Property(p => p.LinkUrl).IsOptional().HasMaxLength(500);
            //modelBuilder.Entity<Navigation>().Property(p => p.InsertedOn).IsRequired();
            //modelBuilder.Entity<Navigation>().Property(p => p.UpdatedOn).IsOptional();

            //modelBuilder.Entity<Navigation>().HasMany(x => x.Role);

            //modelBuilder.Entity<NavigationRole>().Map(c =>
            //{
            //    c.ToTable("NavigationRole");
            //    c.Properties(p => new
            //    {
            //        p.RoleId,
            //        p.NavigationId,
            //    });
            //}).HasKey(p => new { p.RoleId, p.NavigationId });
            //modelBuilder.Entity<NavigationRole>().HasMany(x=>x.)

            //modelBuilder.Entity<Navigation>().HasKey(p => p.Id).Property(p => p.Id).HasColumnName("NavigationId").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
           


            //modelBuilder.Entity<RoleNavigation>().HasKey(p => new { p.RoleId, p.NavigationId });
            //modelBuilder.Entity<RoleNavigation>().HasRequired(p=>p.Role).WithMany().HasForeignKey(c => c.RoleId);
            //modelBuilder.Entity<RoleNavigation>().HasRequired(p => p.Navigation).WithMany().HasForeignKey(c => c.NavigationId);

        }
    }
}
