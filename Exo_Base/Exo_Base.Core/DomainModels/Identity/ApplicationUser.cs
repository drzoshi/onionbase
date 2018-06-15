using System;
using System.Collections.Generic;
using System.Text;

namespace Exo_Base.Core.DomainModels.Identity
{
    public class ApplicationUser: BaseAuditableEntity
    {
        public ApplicationUser()
        {
            Claims = new List<ApplicationUserClaim>();
            Roles = new List<ApplicationUserRole>();
            Logins = new List<ApplicationUserLogin>();
        }
        public virtual int AccessFailedCount { get; set; }
        public virtual string Email { get; set; }
        public virtual bool EmailConfirmed { get; set; }
        public virtual bool LockoutEnabled { get; set; }
        public virtual DateTime? LockoutEndDateUtc { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual bool PhoneNumberConfirmed { get; set; }
        public virtual string SecurityStamp { get; set; }
        public virtual bool TwoFactorEnabled { get; set; }
        public virtual string UserName { get; set; }

        public virtual ICollection<ApplicationUserLogin> Logins { get; private set; }
        public virtual ICollection<ApplicationUserClaim> Claims { get; private set; }
        public virtual ICollection<ApplicationUserRole> Roles { get; private set; }

        //Additional Fields
        public virtual string FirstName { get; set; }
        public virtual string MiddleName { get; set; }
        public virtual string LastName { get; set; }

        public virtual string FullName { get {
                return String.Format("{0}{1}{2}", FirstName,
                    string.IsNullOrEmpty(MiddleName) ? String.Empty : String.Format(" {0}", MiddleName),
                    string.IsNullOrEmpty(LastName) ? String.Empty : String.Format(" {0}", LastName)
                );
            } }
    }
}
