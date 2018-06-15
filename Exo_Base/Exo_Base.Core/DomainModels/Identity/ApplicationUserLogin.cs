using System;
using System.Collections.Generic;
using System.Text;

namespace Exo_Base.Core.DomainModels.Identity
{
    public class ApplicationUserLogin
    {
        public virtual string LoginProvider { get; set; }
        public virtual string ProviderKey { get; set; }
        public virtual int UserId { get; set; }
    }
}
