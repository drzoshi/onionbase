using Exo_Base.Core.DomainModels;
using Exo_Base.Core.DomainModels.Identity;
using Exo_Base.Data.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Linq;

namespace Exo_Base.Data.Extensions
{
    public static class IdentityExtensions
    {
        public static ApplicationIdentityResult ToApplicationIdentityResult(this IdentityResult identityResult)
        {
            return identityResult == null ? null : new ApplicationIdentityResult(identityResult.Errors, identityResult.Succeeded);
        }
        public static IdentityMessage ToIdentityMessage(this ApplicationIdentityMessage message)
        {
            return message == null ? null : new IdentityMessage() { Body = message.Body, Destination = message.Destination, Subject = message.Subject };
        }

        public static ApplicationUserLoginInfo ToApplicationUserLoginInfo(this UserLoginInfo loginInfo)
        {
            return  loginInfo == null ? null : new ApplicationUserLoginInfo(loginInfo.LoginProvider, loginInfo.ProviderKey);
        }

        public static IList<ApplicationUserLoginInfo> ToApplicationUserLoginInfoList(this IList<UserLoginInfo> list)
        {
            return list.Select(u => u.ToApplicationUserLoginInfo()).ToList();
        }
        public static UserLoginInfo ToUserLoginInfo(this ApplicationUserLoginInfo loginInfo)
        {
            return loginInfo == null ? null : new UserLoginInfo(loginInfo.LoginProvider, loginInfo.ProviderKey);
        }

        public static ApplicationUser ToApplicationUser(this ApplicationIdentityUser applicationIdentityUser)
        {
            if (applicationIdentityUser == null)
                return null;
            var applicationUser = new ApplicationUser();
            return applicationUser.CopyApplicationIdentityUserProperties(applicationIdentityUser);
        }
        public static ApplicationUser CopyApplicationIdentityUserProperties(this ApplicationUser applicationUser, ApplicationIdentityUser applicationIdentityUser)
        {
            if (applicationUser == null)
                return null;
            if (applicationIdentityUser == null)
                return null;
            
            applicationUser.UserName = applicationIdentityUser.UserName;
            applicationUser.Id = applicationIdentityUser.Id;
            applicationUser.AccessFailedCount = applicationIdentityUser.AccessFailedCount;
            applicationUser.Email = applicationIdentityUser.Email;
            applicationUser.EmailConfirmed = applicationIdentityUser.EmailConfirmed;
            applicationUser.LockoutEnabled = applicationIdentityUser.LockoutEnabled;
            applicationUser.LockoutEndDateUtc = applicationIdentityUser.LockoutEndDateUtc;
            applicationUser.PasswordHash = applicationIdentityUser.PasswordHash;
            applicationUser.PhoneNumber = applicationIdentityUser.PhoneNumber;
            applicationUser.PhoneNumberConfirmed = applicationIdentityUser.PhoneNumberConfirmed;
            applicationUser.SecurityStamp = applicationIdentityUser.SecurityStamp;
            applicationUser.TwoFactorEnabled = applicationIdentityUser.TwoFactorEnabled;

            applicationUser.FirstName = applicationIdentityUser.FirstName;
            applicationUser.MiddleName = applicationIdentityUser.MiddleName;
            applicationUser.LastName = applicationIdentityUser.LastName;

            applicationUser.LastModifiedUserId = applicationIdentityUser.LastModifiedUserId;
            applicationUser.InsertedOnUtc = applicationIdentityUser.InsertedOnUtc;
            applicationUser.LastModifiedOnUtc = applicationIdentityUser.LastModifiedOnUtc;

            foreach (var claim in applicationIdentityUser.Claims)
            {
                applicationUser.Claims.Add(new ApplicationUserClaim()
                {
                    ClaimType = claim.ClaimType,
                    ClaimValue = claim.ClaimValue,
                    Id = claim.Id,
                    UserId = claim.UserId
                });
            }
            foreach (var role in applicationIdentityUser.Roles)
            {
                applicationUser.Roles.Add(role.ToApplicationUserRole());
            }
            foreach(var login in applicationIdentityUser.Logins)
            {
                applicationUser.Logins.Add(new ApplicationUserLogin()
                {
                    LoginProvider = login.LoginProvider,
                    ProviderKey = login.LoginProvider,
                    UserId = login.UserId
                });
            }
            return applicationUser;
        }

        //ApplicationIdentityUser
        public static ApplicationIdentityUser ToApplicationIdentityUser(this ApplicationUser applicationUser)
        {
            if (applicationUser == null)
                return null;
            var applicationIdentityUser = new ApplicationIdentityUser();
            return applicationIdentityUser.CopyApplicationUserProperties(applicationUser);
        }

        public static ApplicationIdentityUser CopyApplicationUserProperties(this ApplicationIdentityUser applicationIdentityUser, ApplicationUser applicationUser)
        {
            if (applicationIdentityUser == null)
                return null;
            if (applicationUser == null)
                return null;

            applicationIdentityUser.UserName = applicationUser.UserName;
            applicationIdentityUser.Id = applicationUser.Id;
            applicationIdentityUser.AccessFailedCount = applicationUser.AccessFailedCount;
            applicationIdentityUser.Email = applicationUser.Email;
            applicationIdentityUser.EmailConfirmed = applicationUser.EmailConfirmed;
            applicationIdentityUser.LockoutEnabled = applicationUser.LockoutEnabled;
            applicationIdentityUser.LockoutEndDateUtc = applicationUser.LockoutEndDateUtc;
            applicationIdentityUser.PasswordHash = applicationUser.PasswordHash;
            applicationIdentityUser.PhoneNumber = applicationUser.PhoneNumber;
            applicationIdentityUser.PhoneNumberConfirmed = applicationUser.PhoneNumberConfirmed;
            applicationIdentityUser.SecurityStamp = applicationUser.SecurityStamp;
            applicationIdentityUser.TwoFactorEnabled = applicationUser.TwoFactorEnabled;

            applicationIdentityUser.FirstName = applicationUser.FirstName;
            applicationIdentityUser.MiddleName = applicationUser.MiddleName;
            applicationIdentityUser.LastName = applicationUser.LastName;

            applicationIdentityUser.LastModifiedUserId = applicationUser.LastModifiedUserId;
            applicationIdentityUser.InsertedOnUtc = applicationUser.InsertedOnUtc;
            applicationIdentityUser.LastModifiedOnUtc = applicationUser.LastModifiedOnUtc;

            foreach (var claim in applicationUser.Claims)
            {
                applicationIdentityUser.Claims.Add(new ApplicationIdentityUserClaim
                {
                    ClaimType = claim.ClaimType,
                    ClaimValue = claim.ClaimValue,
                    Id = claim.Id,
                    UserId = claim.UserId
                });
            }
            foreach (var role in applicationUser.Roles)
            {
                applicationIdentityUser.Roles.Add(role.ToApplicationIdentityUserRole());
            }
            foreach (var login in applicationIdentityUser.Logins)
            {
                applicationIdentityUser.Logins.Add(new ApplicationIdentityUserLogin
                {
                    UserId = login.UserId,
                    LoginProvider = login.LoginProvider,
                    ProviderKey = login.ProviderKey
                });
            }
            return applicationIdentityUser;
        }

        public static ApplicationUserRole ToApplicationUserRole(this ApplicationIdentityUserRole role)
        {
            return role == null ? null : new ApplicationUserRole() { RoleId = role.RoleId, UserId = role.UserId };
        }
        public static ApplicationIdentityUserRole ToApplicationIdentityUserRole(this ApplicationUserRole role)
        {
            return role == null ? null : new ApplicationIdentityUserRole() { RoleId = role.RoleId, UserId = role.UserId };
        }

        public static IEnumerable<ApplicationAuthenticationDescription> ToApplicationAuthenticationDescriptionList(this IEnumerable<AuthenticationDescription> list)
        {
            return list.Select(x => x.ToApplicationAuthenticationDescription()).ToList();
        }
        public static ApplicationAuthenticationDescription ToApplicationAuthenticationDescription(this AuthenticationDescription authenticationDescription)
        {
            if (authenticationDescription == null)
                return null;
            var description = new ApplicationAuthenticationDescription { AuthenticationType = authenticationDescription.AuthenticationType, Caption = authenticationDescription.Caption };
            description.Properties.Clear();
            foreach(var property in authenticationDescription.Properties)
            {
                description.Properties.Add(property.Key, property.Value);
            }
            return description;
        }
        public static ApplicationExternalLoginInfo ToApplicationExternalLoginInfo(this ExternalLoginInfo loginInfo)
        {
            return loginInfo == null ? null : new ApplicationExternalLoginInfo()
            {
                DefaultUserName = loginInfo.DefaultUserName,
                Email = loginInfo.Email,
                ExternalIdentity = loginInfo.ExternalIdentity,
                Login = loginInfo.Login.ToApplicationUserLoginInfo()
            };
        }
        public static ExternalLoginInfo ToExternalLoginInfo(this ApplicationExternalLoginInfo loginInfo)
        {
            return loginInfo == null ? null : new ExternalLoginInfo()
            {
                DefaultUserName = loginInfo.DefaultUserName,
                Email = loginInfo.Email,
                ExternalIdentity = loginInfo.ExternalIdentity,
                Login = loginInfo.Login.ToUserLoginInfo()
            };
        }

        public static IEnumerable<ApplicationUser> ToApplicationUserList(this IEnumerable<ApplicationIdentityUser> list)
        {
            return list.Select(x => x.ToApplicationUser()).ToList();
        }

        public static ApplicationRole ToApplicationRole(this ApplicationIdentityRole identityRole)
        {
            if (identityRole == null)
                return null;
            var applicationRole = new ApplicationRole();
            return applicationRole.CopyApplicationIdentityRoleProperties(identityRole);
        }
        public static IEnumerable<ApplicationRole> ToApplicationRoleList(this IEnumerable<ApplicationIdentityRole> list)
        {
            if (list == null)
                return null;

            return list.Select(r => r.ToApplicationRole()).ToList();
        }
        public static ApplicationRole CopyApplicationIdentityRoleProperties(this ApplicationRole applicationRole, ApplicationIdentityRole identityRole)
        {
            if (identityRole == null)
                return null;
            if (applicationRole == null)
                return null;
            applicationRole.Id = identityRole.Id;
            applicationRole.Name = identityRole.Name;
            applicationRole.Description = identityRole.Description;
            applicationRole.IsSystemConfig = identityRole.IsSystemConfig;
            applicationRole.InsertedOnUtc = identityRole.InsertedOnUtc;

            foreach(var userRole in identityRole.Users)
            {
                applicationRole.Users.Add(userRole.ToApplicationUserRole());
            }
            return applicationRole;
        }

        public static ApplicationIdentityRole ToIdentityRole(this ApplicationRole role)
        {
            if (role == null)
                return null;
            var identityRole = new ApplicationIdentityRole();
            return identityRole.CopyApplicationRoleProperties(role);
        }
        private static ApplicationIdentityRole CopyApplicationRoleProperties(this ApplicationIdentityRole identityRole, ApplicationRole applicationRole)
        {
            if (applicationRole == null)
                return null;
            if (identityRole == null)
                return null;
            identityRole.Id = applicationRole.Id;
            identityRole.Name = applicationRole.Name;
            identityRole.Description = applicationRole.Description;
            identityRole.IsSystemConfig = applicationRole.IsSystemConfig;
            identityRole.InsertedOnUtc = applicationRole.InsertedOnUtc;

            foreach (var userRole in applicationRole.Users)
            {
                identityRole.Users.Add(userRole.ToApplicationIdentityUserRole());
            }
            return identityRole;
        }
    }
}