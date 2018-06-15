using Exo_Base.Core.DomainModels;
using Exo_Base.Core.Extensions;
using Exo_Base.Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exo_Base.Data.Entities
{
    public class NavigationMenuEntity:  BaseAuditableEntity
    {
        public NavigationMenuEntity()
        {
            DisplayOrder = 1;
            IsDisabled = false;
            Roles = new List<RoleNavigationMenuEntity>();
            //NavigationType = new NavigationTypeEntity();
        }
        public virtual string Name { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string AreaName { get; set; }
        public virtual string ControllerName { get; set; }
        public virtual string ActionName { get; set; }
        public virtual string RouteUrl { get; set; }
        public virtual int ParentNavigationId { get; set; }
        public virtual int NavigationTypeId { get; set; }
        public virtual int DisplayOrder { get; set; }
        public virtual bool IsDisabled { get; set; }
        public virtual ICollection<RoleNavigationMenuEntity> Roles { get; set; }
        public virtual NavigationTypeEntity NavigationType { get; set; }
    }
}
