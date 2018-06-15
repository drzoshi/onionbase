using System;
using Exo_Base.Core.Data;
using Exo_Base.Core.Services;
using Exo_Base.Core.DomainModels;
using System.Threading.Tasks;
using Exo_Base.Core.Extensions;
using System.Linq;
using System.Collections.Generic;
using Exo_Base.Data.Extensions;
using Exo_Base.Data.Entities;
using AutoMapper;
using Exo_Base.Core.DomainModels.Identity;
using Exo_Base.Data.Identity;
using Exo_Base.Data.Extensions.MappingExtensions;
using Exo_Base.Core.Identity;
using System.Linq.Expressions;

namespace Exo_Base.Services
{
    public class NavigationService: INavigationService
    {
        #region constructor and destructor
        //private IBaseService<NavigationMenuEntity> _baseService;
        //private IApplicationRoleManager _roleManager;
        //private IRepository<RoleNavigationMenuEntity> _roleNavigationRepository;
        //private IUnitOfWork _unitOfWork;
        //private bool _disposed;

        //public NavigationService(
        //    IBaseService<NavigationMenuEntity> baseService, 
        //    IApplicationRoleManager roleManager,
        //    IRepository<RoleNavigationMenuEntity> roleNavigationRepository,
        //    IUnitOfWork unitOfWork
        //)
        //{
        //    _unitOfWork = unitOfWork;
        //    _baseService = baseService;
        //    _roleManager = roleManager;
        //    _roleNavigationRepository = roleNavigationRepository;
        //}

        public IUnitOfWork _unitOfWork; // { get; private set; }
        private readonly IRepository<NavigationMenuEntity> _navigationRepository;
        private readonly IRepository<RoleNavigationMenuEntity> _roleNavigationRepository;
        private readonly IRepository<NavigationTypeEntity> _navigationTypeRepository;
        private readonly IApplicationRoleManager _roleManager;
        private bool _disposed;

        public NavigationService(
            IUnitOfWork unitOfWork,
            IApplicationRoleManager roleManager
            //IRepository<NavigationMenuEntity> navigationRepository,
            //IRepository<RoleNavigationMenuEntity> navigationRoleRepository
        )
        {
            _unitOfWork = unitOfWork;
            _navigationRepository = _unitOfWork.Repository<NavigationMenuEntity>();
            _roleNavigationRepository = _unitOfWork.Repository<RoleNavigationMenuEntity>(); //navigationRoleRepository;
            _navigationTypeRepository = _unitOfWork.Repository<NavigationTypeEntity>();
            _roleManager = roleManager;
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
                //_roleNavigationRepository.Dispose();
                //_baseService.Dispose();
                if (_unitOfWork != null)
                    _unitOfWork.Dispose();
            }
            _disposed = true;
        }
        #endregion
        
        public async Task<PaginatedList<NavigationMenu>> GetAllAsync(int pageIndex, int pageSize)
        {
            var dtoList = await _navigationRepository.GetAllAsync(pageIndex, pageSize);
            var list = dtoList.ToNavigationMenuList();
            return new PaginatedList<NavigationMenu>(list, pageIndex, pageSize, dtoList.TotalCount);
        }

        //public async Task<NavigationMenu> FindByAsync(Expression<Func<NavigationMenu, bool>> predicate)
        //{
        //    Expression<Func<NavigationMenuEntity, bool>> entityPredicate = predicate
        //    var entity = await _navigationRepository.FindBy(predicate);
        //    return entity.ToNavigationMenu();
        //}
        public async Task<NavigationMenu> FindByIdAsync(int id)
        {
            var entity = await _navigationRepository.GetSingleAsync(id);
            return entity.ToNavigationMenu();
        }

        public async Task<List<NavigationMenu>> FindByRoleAsync(string roleName)
        {
            var roleDto = await _roleManager.FindByNameAsync(roleName);
            var roleNavigation = await _roleNavigationRepository.FindByAsync(x => x.RoleId.Equals(roleDto.Id));
            var navigations = new List<NavigationMenu>();
            foreach(var item in roleNavigation)
            {
                var nav = await _navigationRepository.GetSingleAsync(item.NavigationId);
                navigations.Add(nav.ToNavigationMenu());
            }
            return navigations;
        }
        public async Task<NavigationMenu> FindByNameAsync(string navigationName)
        {
            var navigationEntity = await _navigationRepository.FindByAsync(x => x.Name == navigationName);
            if (navigationEntity == null)
                return null;

            return navigationEntity.FirstOrDefault().ToNavigationMenu();
        }
        public async Task<IList<string>> GetRolesAsync(int navigationId)
        {
            var NavigationMenuEntity = await FindByIdAsync(navigationId);
            if (NavigationMenuEntity == null)
                throw new InvalidOperationException("Invalid Navigation Id.");

            var navRoles = new List<string>();
            foreach(var role in NavigationMenuEntity.Roles)
            {
                navRoles.Add((await _roleManager.FindByIdAsync(role.RoleId)).Name);
            }
            return navRoles;
        }

        public async Task<IList<NavigationType>> GetNavigationTypes()
        {
            var navigationEntity = await _navigationTypeRepository.GetAllAsync();
            return navigationEntity.Select(x => x.ToNavigationType()).ToList();
        }

        //public async Task<List<Navigation>> FindByRolesAsync(string[] roles)
        //{
        //    var navigationList = new List<Navigation>();
        //    foreach(var role in roles)
        //    {
        //        navigationList.AddRange(await FindByRoleAsync(role));
        //    }
        //    return navigationList;
        //}

        //public async Task<List<string>> GetActionKeyByRoleAsync(string roleName)
        //{
        //    var roleDto = await _roleManager.FindByNameAsync(roleName);
        //    var roleNavigation = await _roleNavigationRepository.FindByAsync(x => x.RoleId.Equals(roleDto.Id));
        //    var actionKey = new List<string>();
        //    foreach (var item in roleNavigation)
        //    {
        //        var nav = await _navigationRepository.GetSingleAsync(item.NavigationId);
        //        actionKey.Add(String.Format("{0}-{1}",nav.ControllerName, nav.ActionName));
        //    }
        //    return actionKey;
        //}


        public async Task<ApplicationIdentityResult> AddAsync(NavigationMenu navigation)
        {
            var NavigationMenuEntity = (await _navigationRepository.FindByAsync(x => x.Name == navigation.Name)).FirstOrDefault();
            var alreadyExist = NavigationMenuEntity != null;
            if (alreadyExist)
                return new ApplicationIdentityResult(new[] { "Navigation with this name is already Exist" }, false);
                
            var dto = navigation.ToNavigationMenuEntity();
            dto.NavigationType = await _navigationTypeRepository.GetSingleAsync(navigation.NavigationTypeId);
            dto.InsertedOnUtc = DateTime.UtcNow;

            _navigationRepository.Insert(dto);
            await _unitOfWork.SaveChangesAsync();
            navigation = dto.ToNavigationMenu();
            return new ApplicationIdentityResult(new[] { "" }, true);
        }

        public async Task<ApplicationIdentityResult> UpdateAsync(NavigationMenu navigation)
        {
            var entity = await _navigationRepository.GetSingleAsync(navigation.Id);
            var navigationMenuEntity = await _navigationRepository.GetSingleAsync(navigation.Id);
            if (navigationMenuEntity == null)
                return new ApplicationIdentityResult(new[] { "Invalid Navigation Id" }, false);

            navigationMenuEntity.Name = navigation.Name;
            navigationMenuEntity.DisplayName = navigation.DisplayName;
            navigationMenuEntity.AreaName = navigation.AreaName;
            navigationMenuEntity.ControllerName = navigation.ControllerName;
            navigationMenuEntity.ActionName = navigation.ActionName;
            navigationMenuEntity.RouteUrl = navigation.RouteUrl;
            navigationMenuEntity.IsDisabled = navigation.IsDisabled;
            navigationMenuEntity.ParentNavigationId = navigation.ParentNavigationId;
            navigationMenuEntity.NavigationTypeId = navigation.NavigationTypeId;
            navigationMenuEntity.LastModifiedUserId = navigation.LastModifiedUserId;
            navigationMenuEntity.LastModifiedOnUtc = DateTime.UtcNow;
            navigationMenuEntity.NavigationType = await _navigationTypeRepository.GetSingleAsync(navigation.NavigationTypeId);

            _navigationRepository.Update(navigationMenuEntity);
            await _unitOfWork.SaveChangesAsync();
            navigation = navigationMenuEntity.ToNavigationMenu();
            return new ApplicationIdentityResult(new[] { "" }, true);
        }

        public async Task<ApplicationIdentityResult> AddNavigationToRolesAsync(int navigationId, IList<string> roles)
        {
            if (roles == null)
                return new ApplicationIdentityResult(new[] { "Bad Request" }, false);

            var roleDtos = (await _roleManager.GetRolesAsync()).ToList();
            foreach (var role in roles)
            {
                var roleDto = roleDtos.Find(X => X.Name.Equals(role));
                var roleNavigationMenuEntity = new RoleNavigationMenuEntity
                {
                    NavigationId = navigationId,
                    RoleId = roleDto.Id
                };
                _roleNavigationRepository.Insert(roleNavigationMenuEntity);
            }
            await _unitOfWork.SaveChangesAsync();
            return new ApplicationIdentityResult(new[] { "" }, true);
        }

        public async Task<ApplicationIdentityResult> RemoveNavigationFromRoleAsync(int navigationId, IList<string> roles)
        {
            if (roles == null)
                return new ApplicationIdentityResult(new[] { "Bad Request" }, false);

            var roleNavigation = await _roleNavigationRepository.FindByAsync(x => x.NavigationId == navigationId);
            foreach(var role in roleNavigation)
            {
                var roleDto = await _roleManager.FindByIdAsync(role.RoleId);
                if (roles.Contains(roleDto.Name))
                    _roleNavigationRepository.Delete(role);
            }
            await _unitOfWork.SaveChangesAsync();
            return new ApplicationIdentityResult(new[] { "" }, true);
        }
        //public async Task<Navigation> FindByControllerAction(string controllerName, string actionName)
        //{
        //    var NavigationMenuEntity = (await _navigationRepository.FindByAsync(x => x.ControllerName.Equals(controllerName) && x.ActionName.Equals(actionName))).FirstOrDefault();
        //    return NavigationMenuEntity.ToNavigationEntity();
        //}

        public async Task<ApplicationIdentityResult> DeleteAsync(int navigationId)
        {
            var NavigationMenuEntity = await _navigationRepository.GetSingleAsync(navigationId).ConfigureAwait(false);
            if (NavigationMenuEntity == null)
                return new ApplicationIdentityResult(new[] { "Invalid Navigation id" }, false);
            //foreach(var roleNav in NavigationMenuEntity.Roles)
            //{
            //    _roleNavigationRepository.Delete(roleNav);
            //}
            _navigationRepository.Delete(NavigationMenuEntity);
            await _unitOfWork.SaveChangesAsync();
            return new ApplicationIdentityResult(new[] { "" }, true);
        }

        public async Task<List<NavigationMenu>> FindByTypeAsync(string navigationType)
        {
            var type = await _navigationTypeRepository.FindByAsync(x => x.NavigationTypeName == navigationType);
            return type.FirstOrDefault().NavigationMenus.Select(x=>x.ToNavigationMenu()).ToList();
        }
        public async Task<NavigationType> FindTypeByNameAsync(string navigationType)
        {
            var type = await _navigationTypeRepository.FindByAsync(x => x.NavigationTypeName == navigationType);
            return type.FirstOrDefault().ToNavigationType();
        }
        public async Task<NavigationType> FindTypeByIdAsync(int navigationTypeId)
        {
            var type = await _navigationTypeRepository.FindByAsync(x => x.Id == navigationTypeId);
            return type.FirstOrDefault().ToNavigationType();
        }


    }
}
