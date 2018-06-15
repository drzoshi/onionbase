using Exo_Base.Core.DomainModels;
using Exo_Base.Core.DomainModels.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Exo_Base.Core.Identity
{
    public interface IApplicationSignInManager:IDisposable
    {
        Task SignInAsync(ApplicationUser user, bool isPersistent, bool rememberBrowser);
        Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout);
        Task<bool> HasBeenVerifiedAsync();
        Task<SignInStatus> TwoFactorSignInAsync(string provider, string code, bool isPersistent, bool rememberBrowser);
        Task<int> GetVerifiedUserIdAsync();
        Task<bool> SendTwoFactorCodeAsync(string provider);
        Task<SignInStatus> ExternalSignInAsync(ApplicationExternalLoginInfo loginInfo, bool isPersistent);
    }
}
