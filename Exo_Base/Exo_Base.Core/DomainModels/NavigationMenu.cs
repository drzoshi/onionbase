using Exo_Base.Core.DomainModels.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exo_Base.Core.DomainModels
{
    public class NavigationMenu : BaseAuditableEntity
    {
        public NavigationMenu()
        {
            DisplayOrder = 1;
            IsDisabled = false;
            Roles = new List<RoleNavigationMenu>();
            //NavigationType = new NavigationType();
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
        public virtual ICollection<RoleNavigationMenu> Roles { get; set; }
        public virtual NavigationType NavigationType { get; set; }
    }

    public class RoleNavigationMenu
    {
        public int RoleId { get; set; }
        public int NavigationId { get; set; }
    }
}
