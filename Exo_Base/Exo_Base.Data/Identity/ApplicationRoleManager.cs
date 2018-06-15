using Exo_Base.Core.DomainModels.Identity;
using Exo_Base.Core.Identity;
using Exo_Base.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exo_Base.Data.Identity
{
    public class AppRoleManager: IApplicationRoleManager
    {
        private readonly ApplicationRoleManager _roleManager;
        private bool _disposed;

        public AppRoleManager(ApplicationRoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        public virtual IEnumerable<ApplicationRole> GetRoles()
        {
            return _roleManager.Roles.ToList().ToApplicationRoleList();
        }
        public virtual async Task<IEnumerable<ApplicationRole>> GetRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync().ConfigureAwait(false);
            return roles.ToApplicationRoleList();
        }

        public virtual async Task<ApplicationRole> FindByIdAsync(int roleId)
        {
            var identityRole = await _roleManager.FindByIdAsync(roleId).ConfigureAwait(false);
            return identityRole.ToApplicationRole();
        }
        public virtual async Task<ApplicationRole> FindByNameAsync(string roleName)
        {
            var identityRole = await _roleManager.FindByNameAsync(roleName).ConfigureAwait(false);
            return identityRole.ToApplicationRole();
        }
        public virtual async Task<ApplicationIdentityResult> CreateAsync(ApplicationRole role)
        {
            var identityRole = role.ToIdentityRole();
            var identityResult = await _roleManager.CreateAsync(identityRole).ConfigureAwait(false);
            role.CopyApplicationIdentityRoleProperties(identityRole);
            return identityResult.ToApplicationIdentityResult();
        }
        public virtual async Task<ApplicationIdentityResult> UpdateAsync(ApplicationRole role)
        {
            var identityRole = await _roleManager.FindByIdAsync(role.Id);
            if (identityRole == null)
                return new ApplicationIdentityResult(new[] { "Invalid role id" }, false);
            identityRole.Name = role.Name;
            identityRole.Description = role.Description;
            var identityResult = await _roleManager.UpdateAsync(identityRole).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        public virtual async Task<ApplicationIdentityResult> DeleteAsync(int roleId)
        {
            var identityRole = await _roleManager.FindByIdAsync(roleId);
            if (identityRole == null)
                return new ApplicationIdentityResult(new[] { "Invalid role id" }, false);
            var identityResult = await _roleManager.DeleteAsync(identityRole).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                if (_roleManager != null)
                {
                    _roleManager.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
