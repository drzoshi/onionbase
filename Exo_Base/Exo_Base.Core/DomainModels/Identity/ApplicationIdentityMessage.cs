using System;
using System.Collections.Generic;
using System.Text;

namespace Exo_Base.Core.DomainModels.Identity
{
    public class ApplicationIdentityMessage
    {
        public virtual string Body { get; set; }
        public virtual string Destination { get; set; }
        public virtual string Subject { get; set; }
    }
}
