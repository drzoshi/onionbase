using Exo_Base.Data.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exo_Base.Data.EFConfigs
{
    internal class UserConfiguration : EntityTypeConfiguration<ApplicationIdentityUser>
    {
        public UserConfiguration()
        {
            ToTable("User");
            HasKey(x => x.Id)
                    .Property(p => p.Id)
                    .HasColumnName("UserId")
                    .HasColumnType("int")
                    .IsRequired()
                    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.UserName)
                .HasColumnName("UserName")
                .HasColumnType("nvarchar")
                .HasMaxLength(256)
                .IsRequired();

            Property(p => p.PasswordHash)
               .HasColumnName("PasswordHash")
               .HasColumnType("nvarchar")
               .IsMaxLength()
               .IsRequired();

            Property(p => p.SecurityStamp)
               .HasColumnName("SecurityStamp")
               .HasColumnType("nvarchar")
               .IsMaxLength()
               .IsOptional();

            Property(p => p.Email)
               .HasColumnName("Email")
               .HasColumnType("nvarchar")
               .HasMaxLength(256)
               .IsRequired();

            Property(p => p.EmailConfirmed)
               .HasColumnName("EmailConfirmed")
               .HasColumnType("bit")
               .IsRequired();

            Property(p => p.PhoneNumber)
               .HasColumnName("PhoneNumber")
               .HasColumnType("nvarchar")
               .HasMaxLength(15)
               .IsOptional();

            Property(p => p.PhoneNumberConfirmed)
               .HasColumnName("PhoneNumberConfirmed")
               .HasColumnType("bit")
               .IsRequired();

            Property(p => p.TwoFactorEnabled)
               .HasColumnName("TwoFactorEnabled")
               .HasColumnType("bit")
               .IsRequired();

            Property(p => p.LockoutEnabled)
               .HasColumnName("LockoutEnabled")
               .HasColumnType("bit")
               .IsRequired();

            Property(p => p.LockoutEndDateUtc)
               .HasColumnName("LockoutEndDateUtc")
               .HasColumnType("datetime")
               .IsOptional();

            Property(p => p.AccessFailedCount)
              .HasColumnName("AccessFailedCount")
              .HasColumnType("decimal")
              .IsOptional();

            Property(p => p.FirstName)
              .HasColumnName("FirstName")
              .HasColumnType("nvarchar")
              .HasMaxLength(250)
              .IsRequired();

            Property(p => p.MiddleName)
              .HasColumnName("MiddleName")
              .HasColumnType("nvarchar")
              .HasMaxLength(250)
              .IsRequired();

            Property(p => p.LastName)
              .HasColumnName("LastName")
              .HasColumnType("nvarchar")
              .HasMaxLength(250)
              .IsRequired();

            HasMany(c => c.Logins).WithOptional().HasForeignKey(c => c.UserId);
            HasMany(c => c.Claims).WithOptional().HasForeignKey(c => c.UserId);
            HasMany(c => c.Roles).WithRequired().HasForeignKey(c => c.UserId);
        }
    }
}
