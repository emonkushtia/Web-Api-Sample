using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Core.Infrastructure
{

    [Serializable]
    public class PagedListResult<TDataTransferObject>
        where TDataTransferObject : class
    {
        public PagedListResult() :
            this(Enumerable.Empty<TDataTransferObject>(), 0L)
        {
        }

        public PagedListResult(
            IEnumerable<TDataTransferObject> list,
            long count)
        {
            this.List = list;
            this.Count = count;
        }

        public IEnumerable<TDataTransferObject> List { get; set; }

        public long Count { get; set; }
    }
}