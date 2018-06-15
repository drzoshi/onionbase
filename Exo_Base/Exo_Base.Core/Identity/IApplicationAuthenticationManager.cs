using Exo_Base.Core.DomainModels.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Exo_Base.Core.Identity
{
    public interface IApplicationAuthenticationManager
    {
        ClaimsIdentity AuthenticationResponseGrant_Identity { get; }
        Task<bool> TwoFactorBrowserRememberedAsync(int userId);
        IEnumerable<ApplicationAuthenticationDescription> GetExternalAuthenticationTypes();
        Task<ApplicationExternalLoginInfo> GetExternalLoginInfoAsync();
        Task<ApplicationExternalLoginInfo> GetExternalLoginInfoAsync(string xsrfKey, string expectedValue);
        void Challenge(string redirectUri, string xsrfKey, int? userId, params string[] authenticationTypes);
        void SignOut(params string[] authenticationTypes);
    }
}
