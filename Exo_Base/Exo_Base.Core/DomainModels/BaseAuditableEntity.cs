using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exo_Base.Core.DomainModels
{
    
    public abstract class BaseAuditableEntity: BaseEntity
    {
        public BaseAuditableEntity()
        {
            
        }
        public int LastModifiedUserId { get; set; }
        public DateTime InsertedOnUtc { get; set; }
        public DateTime? LastModifiedOnUtc { get; set; }
    }
    public interface IBaseAuditableEntity : IBaseEntity
    {
        int LastModifiedUserId { get; set; }
        DateTime InsertedOnUtc { get; set; }
        DateTime? LastModifiedOnUtc { get; set; }
    }
}
