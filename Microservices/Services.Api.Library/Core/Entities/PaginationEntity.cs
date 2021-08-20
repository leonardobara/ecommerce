using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.ProductCatalog.Api.Core.Entities
{
    public class PaginationEntity<TEntity>
    {
        public int PageSize { get; set; }
        public int Page { get; set; }
        public string Sort { get; set; }
        public string SortDirection { get; set; }
        public string Filter { get; set; }
        public int PageQuantity { get; set; }
        public IEnumerable<TEntity> Data { get; set; }

    }
}
