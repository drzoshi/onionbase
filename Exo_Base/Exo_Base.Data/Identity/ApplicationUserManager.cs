using Exo_Base.Core.DomainModels;
using Exo_Base.Core.DomainModels.Identity;
using Exo_Base.Core.Identity;
using Exo_Base.Data.Extensions;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Exo_Base.Data.Identity
{
    public class AppUserManager : IApplicationUserManager
    {
        private readonly ApplicationUserManager _userManager;
        //private readonly AppRoleManager _roleManager;
        private bool _disposed;
        public AppUserManager( ApplicationUserManager userManager)
        {
            _userManager = userManager;
            //_roleManager = roleManager;
        }

        public virtual async Task<ApplicationIdentityResult> RemoveLoginAsync(int userId, ApplicationUserLoginInfo login)
        {
            var identityResult = await _userManager.RemoveLoginAsync(userId, login.ToUserLoginInfo()).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        public virtual async Task<string> GenerateChangePhoneNumberTokenAsync(int userId, string phoneNumber)
        {
            return await _userManager.GenerateChangePhoneNumberTokenAsync(userId, phoneNumber);
        }

        public virtual async Task SendEmailAsync(int userId, string subject, string body)
        {
            await _userManager.SendEmailAsync(userId, subject, body);
        }
        public virtual async Task SendSmsAsync(int userId, string message)
        {
            await _userManager.SendSmsAsync(userId, message);
        }
        public virtual async Task SendSmsAsync(ApplicationIdentityMessage message)
        {
            if (_userManager.SmsService != null)
            {
                await _userManager.SmsService.SendAsync(message.ToIdentityMessage());
            }
        }

        public virtual async Task<ApplicationIdentityResult> SetTwoFactorEnabledAsync(int userId, bool enabled)
        {
            var identityResult = await _userManager.SetTwoFactorEnabledAsync(userId, enabled);
            return identityResult.ToApplicationIdentityResult();
        }

        public virtual async Task<string> GetPhoneNumberAsync(int userId)
        {
            return await _userManager.GetPhoneNumberAsync(userId);
        }

        public virtual async Task<bool> GetTwoFactorEnabledAsync(int userId)
        {
            return await _userManager.GetTwoFactorEnabledAsync(userId);
        }

        public virtual async Task<IList<ApplicationUserLoginInfo>> GetLoginsAsync(int userId)
        {
            var list = await _userManager.GetLoginsAsync(userId).ConfigureAwait(false);
            return list.ToApplicationUserLoginInfoList();
        }

        public virtual async Task<ApplicationIdentityResult> AddLoginAsync(int userId, ApplicationUserLoginInfo login)
        {
            var identityResult = await _userManager.AddLoginAsync(userId, login.ToUserLoginInfo()).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        public virtual async Task<ApplicationUser> FindByIdAsync(int userId)
        {
            var applicationUser = await _userManager.FindByIdAsync(userId).ConfigureAwait(false);
            return applicationUser.ToApplicationUser();
        }

        public virtual ApplicationUser FindById(int userId)
        {
            return _userManager.FindById(userId).ToApplicationUser();
        }

        public virtual async Task<ApplicationIdentityResult> ChangePhoneNumberAsync(int userId, string phoneNumber, string token)
        {
            var identityResult = await _userManager.ChangePhoneNumberAsync(userId, phoneNumber, token).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        public virtual async Task<ApplicationIdentityResult> SetPhoneNumberAsync(int userId, string phoneNumber)
        {
            var identityResult = await _userManager.SetPhoneNumberAsync(userId, phoneNumber).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }
        public virtual async Task<ApplicationIdentityResult> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
        {
            var identityResult = await _userManager.ChangePasswordAsync(userId, currentPassword, newPassword).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }
        public virtual async Task<ApplicationIdentityResult> AddPasswordAsync(int userId, string password)
        {
            var identityResult = await _userManager.AddPasswordAsync(userId, password);
            return identityResult.ToApplicationIdentityResult();
        }

        public virtual async Task<ApplicationUser> FindByNameAsync(string userName)
        {
            var identityUser = await _userManager.FindByNameAsync(userName).ConfigureAwait(false);
            return identityUser.ToApplicationUser();
        }

        public virtual async Task<bool> IsEmailConfirmedAsync(int userId)
        {
            return await _userManager.IsEmailConfirmedAsync(userId).ConfigureAwait(false);
        }

        public virtual async Task<ApplicationIdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            var identityUser = user.ToApplicationIdentityUser();
            var identityResult = await _userManager.CreateAsync(identityUser, password).ConfigureAwait(false);
            user.CopyApplicationIdentityUserProperties(identityUser);
            return identityResult.ToApplicationIdentityResult();
        }

        public virtual async Task<ApplicationIdentityResult> CreateAsync(ApplicationUser user)
        {
            var identityUser = user.ToApplicationIdentityUser();
            var identityResult =await _userManager.CreateAsync(identityUser).ConfigureAwait(false);
            user.CopyApplicationIdentityUserProperties(identityUser);
            return identityResult.ToApplicationIdentityResult();
        }
        public virtual async Task<ClaimsIdentity> CreateIdentityAsync(ApplicationUser user, string authenticationType)
        {
            var identityUser = user.ToApplicationIdentityUser();
            var claimIdentity =await _userManager.CreateIdentityAsync(identityUser, authenticationType).ConfigureAwait(false);
            user.CopyApplicationIdentityUserProperties(identityUser);
            return claimIdentity;
        }
        public virtual async Task<ApplicationIdentityResult> ConfirmEmailAsync(int userId, string token)
        {
            var identityResult =await _userManager.ConfirmEmailAsync(userId, token).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }
        public virtual async Task<string> GeneratePasswordResetTokenAsync(int userId)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(userId).ConfigureAwait(false);
        }
        public virtual async Task<ApplicationIdentityResult> ResetPasswordAsync(int userId, string token, string newPassword)
        {
            var identityResult = await _userManager.ResetPasswordAsync(userId, token, newPassword).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }
        public virtual async Task<IList<string>> GetValidTwoFactorProvidersAsync(int userId)
        {
            return await _userManager.GetValidTwoFactorProvidersAsync(userId).ConfigureAwait(false);
        }
        public virtual async Task<string> GenerateEmailConfirmationTokenAsync(int userId)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(userId).ConfigureAwait(false);
        }

        public virtual IEnumerable<ApplicationUser> GetUsers()
        {
            return _userManager.Users.ToList().ToApplicationUserList();
        }
        public virtual async Task<IEnumerable<ApplicationUser>> GetUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync().ConfigureAwait(false);
            return users.ToApplicationUserList();
        }


        public virtual async Task<IList<string>> GetRolesAsync(int userId)
        {
            return await _userManager.GetRolesAsync(userId).ConfigureAwait(false);
        }
        public virtual async Task<ApplicationIdentityResult> AddToRoleAsync(int userId, string role)
        {
            var identityResult = await _userManager.AddToRoleAsync(userId, role).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }
        //public virtual async Task<ApplicationIdentityResult> AddToRolesAsync(int userId, IList<string> roles)
        //{
        //    var identityResult = await _userManager.AddToRolesAsync(userId, roles.ToArray()).ConfigureAwait(false);
        //    return identityResult.ToApplicationIdentityResult();
        //}
        public virtual async Task<ApplicationIdentityResult> AddUserToRolesAsync(int userId, IList<string> roles)
        {
            var user = await FindByIdAsync(userId).ConfigureAwait(false);
            if (user == null)
                throw new InvalidOperationException("Invalid user id");

            var userRoles = await GetRolesAsync(userId);
            foreach (var role in roles.Where(r => !userRoles.Contains(r)))
            {
                await AddToRoleAsync(userId, role);
            }
            return await UpdateAsync(userId).ConfigureAwait(false);
        }

        public virtual async Task<ApplicationIdentityResult> RemoveFromRoleAsync(int userId, string role)
        {
            var identityResult = await _userManager.RemoveFromRoleAsync(userId, role).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        public virtual async Task<ApplicationIdentityResult> RemoveUserFromRolesAsync(int userId, IList<string> roles)
        {
            var user = await FindByIdAsync(userId).ConfigureAwait(false);
            if(user == null)
            {
                throw new InvalidOperationException("Invalid user id");
            }
            var userRoles = await GetRolesAsync(user.Id).ConfigureAwait(false);
            foreach(var role in roles.Where(userRoles.Contains)){
                await RemoveFromRoleAsync(user.Id, role).ConfigureAwait(false);
            }
            return await UpdateAsync(userId).ConfigureAwait(false);
        }

        public virtual async Task<ApplicationIdentityResult> UpdateAsync(int userId)
        {
            var applicationUser = await _userManager.FindByIdAsync(userId).ConfigureAwait(false);
            if (applicationUser == null)
                return new ApplicationIdentityResult(new[] { "Invalid user id" }, false);

            var identityResult = await _userManager.UpdateAsync(applicationUser);
            return identityResult.ToApplicationIdentityResult();
        }
        public virtual async Task<ApplicationIdentityResult> UpdateAsync(ApplicationUser applicationUser)
        {
            var identityUser = await _userManager.FindByIdAsync(applicationUser.Id);
            identityUser.UserName = applicationUser.UserName;
            identityUser.Email = applicationUser.Email;

            identityUser.FirstName = applicationUser.FirstName;
            identityUser.MiddleName = applicationUser.MiddleName;
            identityUser.LastName = applicationUser.LastName;
            identityUser.LastModifiedUserId = applicationUser.LastModifiedUserId;
            identityUser.LastModifiedOnUtc = applicationUser.LastModifiedOnUtc;

            var identityResult = await _userManager.UpdateAsync(identityUser);
            return identityResult.ToApplicationIdentityResult();
        }
        
        public virtual async Task<ApplicationIdentityResult> UpdateSecurityStampAsync(int userId)
        {
            var identityResult = await _userManager.UpdateSecurityStampAsync(userId).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        public virtual async Task<ApplicationIdentityResult> DeleteAsync(int userId)
        {
            var identityUser = await _userManager.FindByIdAsync(userId).ConfigureAwait(false);
            if (identityUser == null)
                return new ApplicationIdentityResult(new[] {"Invalid user id"}, false);
            var identityResult = await _userManager.DeleteAsync(identityUser);
            return identityResult.ToApplicationIdentityResult();
        }
        
        //public virtual async Task<ApplicationIdentityResult> DeleteAsync(ApplicationUser user)
        //{
        //    var identityUser = await _userManager.FindByIdAsync(user.Id).ConfigureAwait(false);
        //    var identityResult = await _userManager.DeleteAsync(identityUser);
        //    return identityResult.ToApplicationIdentityResult();
        //}



        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool disposing)
        {
            if(!_disposed && disposing)
            {
                if(_userManager != null)
                {
                    _userManager.Dispose();
                }
            }
            _disposed = true;
        }

        #region authenticationManager
        #endregion
    }
}
