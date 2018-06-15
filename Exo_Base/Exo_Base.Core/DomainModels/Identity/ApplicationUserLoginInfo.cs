using System;
using System.Collections.Generic;
using System.Text;

namespace Exo_Base.Core.DomainModels.Identity
{
    public class ApplicationUserLoginInfo
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public ApplicationUserLoginInfo(string loginProvider, string providerKey)
        {
            LoginProvider = loginProvider;
            ProviderKey = providerKey;
        }
    }
}
