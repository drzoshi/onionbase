using System.Security.Claims;

namespace Exo_Base.Core.DomainModels.Identity
{
    public class ApplicationExternalLoginInfo
    {
        public ApplicationUserLoginInfo Login { get; set; }
        public string DefaultUserName { get; set; }
        public string Email { get; set; }
        public ClaimsIdentity ExternalIdentity { get; set; }
    }
}
