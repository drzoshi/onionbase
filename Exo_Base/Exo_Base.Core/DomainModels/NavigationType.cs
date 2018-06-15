using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exo_Base.Core.DomainModels
{
    public class NavigationType: BaseEntity
    {
        public virtual string NavigationTypeName { get; set; }
        public virtual ICollection<NavigationMenu> NavigationMenus { get; set; }
    }
}
