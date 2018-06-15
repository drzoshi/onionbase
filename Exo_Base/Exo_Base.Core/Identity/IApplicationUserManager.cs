using Exo_Base.Core.DomainModels;
using Exo_Base.Core.DomainModels.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Exo_Base.Core.Identity
{
    public interface IApplicationUserManager : IDisposable
    {
        Task<ApplicationIdentityResult> RemoveLoginAsync(int userId, ApplicationUserLoginInfo login);
        Task<string> GenerateChangePhoneNumberTokenAsync(int userId, string phoneNumber);
        Task<IList<ApplicationUserLoginInfo>> GetLoginsAsync(int userId);
        Task<string> GetPhoneNumberAsync(int userId);
        Task<bool> GetTwoFactorEnabledAsync(int userId);
        Task SendEmailAsync(int userId, string subject, string body);
        Task SendSmsAsync(int userId, string message);
        Task SendSmsAsync(ApplicationIdentityMessage message);
        Task<ApplicationIdentityResult> SetTwoFactorEnabledAsync(int userId, bool enabled);
        Task<ApplicationIdentityResult> AddLoginAsync(int userId, ApplicationUserLoginInfo login);
        Task<ApplicationUser> FindByIdAsync(int userId);
        ApplicationUser FindById(int userId);
        Task<ApplicationIdentityResult> ChangePhoneNumberAsync(int userId, string phoneNumber, string token);
        Task<ApplicationIdentityResult> SetPhoneNumberAsync(int userId, string phoneNumber);
        Task<ApplicationIdentityResult> ChangePasswordAsync(int userId, string currentPassword, string newPassword);
        Task<ApplicationIdentityResult> AddPasswordAsync(int userId, string password);
        Task<ApplicationUser> FindByNameAsync(string userName);
        Task<bool> IsEmailConfirmedAsync(int userId);

        Task<ApplicationIdentityResult> CreateAsync(ApplicationUser user, string password);
        Task<ApplicationIdentityResult> CreateAsync(ApplicationUser user);
        Task<ClaimsIdentity> CreateIdentityAsync(ApplicationUser user, string authenticationType);
        Task<ApplicationIdentityResult> ConfirmEmailAsync(int userId, string token);
        Task<string> GeneratePasswordResetTokenAsync(int userId);
        Task<ApplicationIdentityResult> ResetPasswordAsync(int userId, string token, string newPassword);
        Task<IList<string>> GetValidTwoFactorProvidersAsync(int userId);
        Task<string> GenerateEmailConfirmationTokenAsync(int userId);

        IEnumerable<ApplicationUser> GetUsers();
        Task<IEnumerable<ApplicationUser>> GetUsersAsync();
        Task<IList<string>> GetRolesAsync(int userId);
        Task<ApplicationIdentityResult> AddToRoleAsync(int userId, string role);
        //Task<ApplicationIdentityResult> AddToRolesAsync(int userId, IList<string> roles);
        Task<ApplicationIdentityResult> AddUserToRolesAsync(int userId, IList<string> roles);

        /// <summary>
        /// Remove a user from a role
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<ApplicationIdentityResult> RemoveFromRoleAsync(int userId, string role);

        /// <summary>
        /// Method to remove user from multiple roles 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        Task<ApplicationIdentityResult> RemoveUserFromRolesAsync(int userId, IList<string> roles);

        Task<ApplicationIdentityResult> UpdateAsync(int userId);
        Task<ApplicationIdentityResult> UpdateAsync(ApplicationUser applicationUser);
        Task<ApplicationIdentityResult> UpdateSecurityStampAsync(int userId);
        Task<ApplicationIdentityResult> DeleteAsync(int userId);

    }
}
