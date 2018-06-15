using Exo_Base.Core.DomainModels;
using Exo_Base.Core.DomainModels.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exo_Base.Core.Services
{
    public interface INavigationService: IDisposable
    {
        Task<PaginatedList<NavigationMenu>> GetAllAsync(int pageIndex, int pageSize);
        Task<NavigationMenu> FindByIdAsync(int id);
        Task<List<NavigationMenu>> FindByRoleAsync(string roleName);
        Task<NavigationMenu> FindByNameAsync(string navigationName);
        Task<IList<string>> GetRolesAsync(int navigationId);
        //Task<List<Navigation>> FindByRolesAsync(string[] roles);
        //Task<List<string>> GetActionKeyByRoleAsync(string roleName);
        Task<ApplicationIdentityResult> AddAsync(NavigationMenu entity);
        Task<ApplicationIdentityResult> UpdateAsync(NavigationMenu navigation);
        Task<ApplicationIdentityResult> AddNavigationToRolesAsync(int navigationId, IList<string> roles);
        Task<ApplicationIdentityResult> RemoveNavigationFromRoleAsync(int navigationId, IList<string> roles);

        //Task<Navigation> FindByControllerAction(string controllerName, string actionName);
        Task<ApplicationIdentityResult> DeleteAsync(int navigationId);

        Task<IList<NavigationType>> GetNavigationTypes();
        Task<List<NavigationMenu>> FindByTypeAsync(string navigationType);
        Task<NavigationType> FindTypeByNameAsync(string navigationType);
        Task<NavigationType> FindTypeByIdAsync(int navigationTypeId);
    }
}
