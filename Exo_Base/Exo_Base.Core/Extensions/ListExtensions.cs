using Exo_Base.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exo_Base.Core.Extensions
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class ListExtensions
    {
        public static PaginatedList<T> ToPaginatedList<T>(this IList<T> list, int pageIndex, int pageSize, int total)
        {
            return new PaginatedList<T>(list, pageIndex, pageSize, total);
        }
    }
}
