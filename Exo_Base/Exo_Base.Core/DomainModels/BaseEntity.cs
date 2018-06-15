using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exo_Base.Core.DomainModels
{
    public abstract class BaseEntity 
    {
        public int Id { get; set; }
    }
    public interface IBaseEntity
    {
        int Id { get; set; }
    }
}
