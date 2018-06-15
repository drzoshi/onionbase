using Exo_Base.Core.DomainModels.Identity;
using Exo_Base.Core.Identity;
using Exo_Base.Data.Extensions;
using System;
using System.Threading.Tasks;

namespace Exo_Base.Data.Identity
{
    public class AppSignInManager: IApplicationSignInManager
    {
        private readonly ApplicationSignInManager _signInManager;
        private bool _disposed;
        public AppSignInManager(ApplicationSignInManager signInManager)
        {
            _signInManager = signInManager;
        }

        public virtual async Task SignInAsync(ApplicationUser user, bool isPersistent, bool rememberBrowser)
        {
            await _signInManager.SignInAsync(user.ToApplicationIdentityUser(), isPersistent, rememberBrowser).ConfigureAwait(false);
        }
        public virtual async Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            var status = await _signInManager.PasswordSignInAsync(userName, password, isPersistent, shouldLockout).ConfigureAwait(false);
            return (SignInStatus)status; //Enum.Parse(typeof(Microsoft.AspNet.Identity.Owin.SignInStatus),status.ToString());
        }
        //
        public virtual async Task<bool> HasBeenVerifiedAsync()
        {
            return await _signInManager.HasBeenVerifiedAsync().ConfigureAwait(false);
        }
        public virtual async Task<SignInStatus> TwoFactorSignInAsync(string provider, string code, bool isPersistent, bool rememberBrowser)
        {
            return (SignInStatus)await _signInManager.TwoFactorSignInAsync(provider,code, isPersistent, rememberBrowser).ConfigureAwait(false);
        }

        public virtual async Task<int> GetVerifiedUserIdAsync()
        {
            return await _signInManager.GetVerifiedUserIdAsync().ConfigureAwait(false);
        }

        public virtual async Task<bool> SendTwoFactorCodeAsync(string provider)
        {
            return await _signInManager.SendTwoFactorCodeAsync(provider).ConfigureAwait(false);
        }

        public virtual async Task<SignInStatus> ExternalSignInAsync(ApplicationExternalLoginInfo loginInfo, bool isPersistent)
        {
            var externalLoginInfo = loginInfo.ToExternalLoginInfo();
            return (SignInStatus)await _signInManager.ExternalSignInAsync(externalLoginInfo, isPersistent).ConfigureAwait(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void Dispose(bool disposing)
        {
            if(!_disposed && disposing)
            {
                if(_signInManager != null)
                {
                    _signInManager.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
