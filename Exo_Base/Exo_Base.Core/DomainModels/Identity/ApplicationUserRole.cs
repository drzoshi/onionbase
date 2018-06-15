using System;
using System.Collections.Generic;
using System.Text;

namespace Exo_Base.Core.DomainModels.Identity
{
    public class ApplicationUserRole 
    {
        public virtual int RoleId { get; set; }
        public virtual int UserId { get; set; }
    }
}
