
using Exo_Base.Core.DomainModels;
using Exo_Base.Data.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Exo_Base.Data.Identity
{
    public class ApplicationIdentityUser : IdentityUser<int, ApplicationIdentityUserLogin, ApplicationIdentityUserRole, ApplicationIdentityUserClaim>, 
        IBaseAuditableEntity
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int LastModifiedUserId { get; set; }
        public DateTime InsertedOnUtc { get; set; }
        public DateTime? LastModifiedOnUtc { get; set; }
    }

    public class ApplicationIdentityRole : IdentityRole<int, ApplicationIdentityUserRole>
    {
        public ApplicationIdentityRole()
        {

        }
        public ApplicationIdentityRole(string name)
        {
            Name = name;
            IsSystemConfig = false;
        }
        public string Description { get; set; }
        public bool IsSystemConfig { get; set; }
        public DateTime InsertedOnUtc { get; set; }
        public virtual ICollection<RoleNavigationMenuEntity> NavigationMenus { get; set; }
    }

    public class ApplicationIdentityUserRole : IdentityUserRole<int>
    {

    }
    
    public class ApplicationIdentityUserClaim : IdentityUserClaim<int>
    {

    }
    public class ApplicationIdentityUserLogin : IdentityUserLogin<int>
    {

    }

    public class ApplicationUserStore :
        UserStore<ApplicationIdentityUser, ApplicationIdentityRole, int, ApplicationIdentityUserLogin, ApplicationIdentityUserRole, ApplicationIdentityUserClaim>,
        IUserStore<ApplicationIdentityUser, int>,
        IDisposable
    {
        public ApplicationUserStore(ApplicationDbContext context) : base(context) { }
    }

    public class ApplicationRoleStore : RoleStore<ApplicationIdentityRole, int, ApplicationIdentityUserRole>
    {
        public ApplicationRoleStore(ApplicationDbContext context) : base(context) { }
    }
}