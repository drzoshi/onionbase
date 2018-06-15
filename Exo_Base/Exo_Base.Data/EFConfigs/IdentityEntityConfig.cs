using Exo_Base.Data.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exo_Base.Data.EFConfigs
{
    public class IdentityEntityConfig
    {
        public static void ConfigureIdentity(DbModelBuilder modelBuilder)
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
                    p.Description,
                    p.IsSystemConfig,
                    p.InsertedOnUtc
                });
            }).HasKey(p => p.Id);
            modelBuilder.Entity<ApplicationIdentityRole>().Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
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
                    p.LastName,
                    p.LastModifiedUserId,
                    p.InsertedOnUtc,
                    p.LastModifiedOnUtc
                });
            }).HasKey(c => c.Id);
            modelBuilder.Entity<ApplicationIdentityUser>().Property(c => c.FirstName).IsRequired().HasMaxLength(150);
            modelBuilder.Entity<ApplicationIdentityUser>().Property(c => c.MiddleName).IsOptional().HasMaxLength(150);
            modelBuilder.Entity<ApplicationIdentityUser>().Property(c => c.LastName).IsOptional().HasMaxLength(150);

            modelBuilder.Entity<ApplicationIdentityUser>().Property(c => c.LastModifiedUserId).IsRequired();
            modelBuilder.Entity<ApplicationIdentityUser>().Property(c => c.InsertedOnUtc).IsRequired();
            modelBuilder.Entity<ApplicationIdentityUser>().Property(c => c.LastModifiedOnUtc).IsOptional();

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

        }
    }
}
