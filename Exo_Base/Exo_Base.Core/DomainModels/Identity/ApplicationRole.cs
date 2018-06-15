using System;
using System.Collections.Generic;


namespace Exo_Base.Core.DomainModels.Identity
{
    public class ApplicationRole
    {
        public ApplicationRole()
        {
            Users = new List<ApplicationUserRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsSystemConfig { get; set; }
        public DateTime InsertedOnUtc { get; set; }

        public virtual ICollection<ApplicationUserRole> Users { get; private set; }
    }
}
