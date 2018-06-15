using Exo_Base.Core.DomainModels.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Exo_Base.Core.Identity
{
    public interface IApplicationRoleManager: IDisposable
    {
        IEnumerable<ApplicationRole> GetRoles();
        Task<IEnumerable<ApplicationRole>> GetRolesAsync();
        /// <summary>
        /// Find a role by id
        /// </summary>
        Task<ApplicationRole> FindByIdAsync(int roleId);
        /// <summary>
        /// Find a role by name
        /// </summary>
        Task<ApplicationRole> FindByNameAsync(string roleName);
        /// <summary>
        /// Create a role
        /// </summary>
        Task<ApplicationIdentityResult> CreateAsync(ApplicationRole role);

        /// <summary>
        /// Update an existing role
        /// </summary>
        Task<ApplicationIdentityResult> UpdateAsync(ApplicationRole role);
        /// <summary>
        /// Delete a role
        /// </summary>
        Task<ApplicationIdentityResult> DeleteAsync(int roleId);
    }
}
