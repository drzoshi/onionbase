using Exo_Base.Core.DomainModels;
using Exo_Base.Data.Identity;

namespace Exo_Base.Data.Entities
{
    public class RoleNavigationMenuEntity: BaseEntity
    {
        public int RoleId { get; set; }
        public int NavigationId { get; set; }
        public virtual ApplicationIdentityRole Role { get; set; }
        public virtual NavigationMenuEntity NavigationMenu { get; set; }
    }
}
