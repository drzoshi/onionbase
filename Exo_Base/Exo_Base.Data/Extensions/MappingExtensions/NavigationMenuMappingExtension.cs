using Exo_Base.Core.DomainModels;
using Exo_Base.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exo_Base.Data.Extensions.MappingExtensions;

namespace Exo_Base.Data.Extensions.MappingExtensions
{
    public static class NavigationMenuMappingExtension
    {
        public static IEnumerable<NavigationMenu> ToNavigationMenuList(this IEnumerable<NavigationMenuEntity> list)
        {
            if (list == null)
                return null;
            return list.Select(x => x.ToNavigationMenu());
        }
        public static NavigationMenu ToNavigationMenu(this NavigationMenuEntity entity)
        {
            if (entity == null)
                return null;
            NavigationMenu navigation = new NavigationMenu();
            return navigation.CopyNavigationMenuEntityProperties(entity);
        }
        public static NavigationMenuEntity ToNavigationMenuEntity(this NavigationMenu model)
        {
            if (model == null)
                return null;
            NavigationMenuEntity NavigationMenuEntity = new NavigationMenuEntity();
            return NavigationMenuEntity.CopyNavigationEntityProperties(model);
        }
        public static NavigationMenu CopyNavigationMenuEntityProperties(this NavigationMenu destEntity, NavigationMenuEntity srcEntity)
        {
            if (destEntity == null)
                return null;
            if (destEntity == null)
                return null;

            destEntity.Id = srcEntity.Id;
            destEntity.Name = srcEntity.Name;
            destEntity.DisplayName = srcEntity.DisplayName;
            destEntity.AreaName = srcEntity.AreaName;
            destEntity.ControllerName = srcEntity.ControllerName;
            destEntity.ActionName = srcEntity.ActionName;
            destEntity.RouteUrl = srcEntity.RouteUrl;
            destEntity.ParentNavigationId = srcEntity.ParentNavigationId;
            destEntity.NavigationTypeId = srcEntity.NavigationTypeId;
            destEntity.DisplayOrder = srcEntity.DisplayOrder;
            destEntity.IsDisabled = srcEntity.IsDisabled;
            destEntity.LastModifiedUserId = srcEntity.LastModifiedUserId;
            destEntity.InsertedOnUtc = srcEntity.InsertedOnUtc;
            destEntity.LastModifiedUserId = srcEntity.LastModifiedUserId;

            foreach (var role in srcEntity.Roles)
            {
                destEntity.Roles.Add(role.ToRoleNavigationEntity());
            }
            return destEntity;
        }
        public static NavigationMenuEntity CopyNavigationEntityProperties(this NavigationMenuEntity destEntity, NavigationMenu srcEntity)
        {
            if (srcEntity == null)
                return null;
            if (destEntity == null)
                return null;

            destEntity.Id = srcEntity.Id;
            destEntity.Name = srcEntity.Name;
            destEntity.DisplayName = srcEntity.DisplayName;
            destEntity.AreaName = srcEntity.AreaName;
            destEntity.ControllerName = srcEntity.ControllerName;
            destEntity.ActionName = srcEntity.ActionName;
            destEntity.RouteUrl = srcEntity.RouteUrl;
            destEntity.ParentNavigationId = srcEntity.ParentNavigationId;
            destEntity.NavigationTypeId = srcEntity.NavigationTypeId;
            destEntity.DisplayOrder = srcEntity.DisplayOrder;
            destEntity.IsDisabled = srcEntity.IsDisabled;
            destEntity.LastModifiedUserId = srcEntity.LastModifiedUserId;
            destEntity.InsertedOnUtc = srcEntity.InsertedOnUtc;
            destEntity.LastModifiedUserId = srcEntity.LastModifiedUserId;

            foreach (var role in srcEntity.Roles)
            {
                destEntity.Roles.Add(new RoleNavigationMenuEntity {
                    RoleId = role.RoleId,
                    NavigationId = role.NavigationId
                });
            }
            return destEntity;
        }
        public static RoleNavigationMenu ToRoleNavigationEntity(this RoleNavigationMenuEntity dto)
        {
            if (dto == null)
                return null;
            return new RoleNavigationMenu() {
                RoleId = dto.RoleId,
                NavigationId = dto.NavigationId
            };
        }

        public static NavigationType ToNavigationType(this NavigationTypeEntity entity)
        {
            if (entity == null)
                return null;
            return new NavigationType()
            {
                Id = entity.Id,
                NavigationTypeName = entity.NavigationTypeName
            };
        }
    }
}
