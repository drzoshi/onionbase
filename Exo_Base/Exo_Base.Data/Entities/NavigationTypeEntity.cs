using Exo_Base.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exo_Base.Data.Entities
{
    public class NavigationTypeEntity : BaseEntity
    {
        public virtual string NavigationTypeName { get; set; }
        public virtual ICollection<NavigationMenuEntity> NavigationMenus { get; set; }
    }
}
