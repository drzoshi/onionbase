using Exo_Base.Core.DomainModels;
using Exo_Base.Core.Identity;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exo_Base.Data.Extensions;
using Exo_Base.Core.DomainModels.Identity;
using System.Security.Claims;

namespace Exo_Base.Data.Identity
{
    public class ApplicationAuthenticationManager: IApplicationAuthenticationManager
    {
        public ClaimsIdentity AuthenticationResponseGrant_Identity { get {
                return _authenticationManager.AuthenticationResponseGrant.Identity;
            } }
        private readonly IAuthenticationManager _authenticationManager;
        public ApplicationAuthenticationManager(IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }
        public virtual async Task<bool> TwoFactorBrowserRememberedAsync(int userId)
        {
            return await _authenticationManager.TwoFactorBrowserRememberedAsync(userId.ToString()).ConfigureAwait(false);
        }
        public virtual IEnumerable<ApplicationAuthenticationDescription> GetExternalAuthenticationTypes()
        {
           return _authenticationManager.GetExternalAuthenticationTypes().ToApplicationAuthenticationDescriptionList();
        }
        //
        public virtual async Task<ApplicationExternalLoginInfo> GetExternalLoginInfoAsync()
        {
            var externalLoginInfo = await _authenticationManager.GetExternalLoginInfoAsync().ConfigureAwait(false);
            return externalLoginInfo.ToApplicationExternalLoginInfo();
        }

        public virtual async Task<ApplicationExternalLoginInfo> GetExternalLoginInfoAsync(string xsrfKey, string expectedValue)
        {
            var externalLoginInfo = await _authenticationManager.GetExternalLoginInfoAsync(xsrfKey,expectedValue).ConfigureAwait(false);
            return externalLoginInfo.ToApplicationExternalLoginInfo();
        }
        public virtual void Challenge(string redirectUri, string xsrfKey, int? userId, params string[] authenticationTypes)
        {
            var properties = new AuthenticationProperties { RedirectUri = redirectUri };
            if (userId != null)
            {
                properties.Dictionary[xsrfKey] = userId.ToString();
            }
            _authenticationManager.Challenge(properties, authenticationTypes);
        }
        public virtual void SignOut(params string[] authenticationTypes)
        {
            _authenticationManager.SignOut(authenticationTypes);
        }
    }
}
