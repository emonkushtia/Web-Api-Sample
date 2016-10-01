namespace Demo.Core.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Serializable]
    public class PageableListQueryCommand<TDomainObject> 
        where TDomainObject : class
    {
        private readonly IEnumerable<string> sortOrders = new[] { "asc", "desc" };

        private readonly List<Tuple<string, string>> sortColumMappings =
            new List<Tuple<string, string>>();

        public PageableListQueryCommand()
        {
            this.Offset = 0;
            this.Limit = 10;
        }

        public string Sort { get; set; }

        public int? Offset { get; set; }

        public int Limit { get; set; }
       
        public virtual string GetOrderByClause()
        {
            if (string.IsNullOrWhiteSpace(this.Sort))
            {
                return null;
            }

            var sort = this.Sort.Split(
                new[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries);

            if (sort.Length == 0)
            {
                return null;
            }

            if (sort.Length == 1)
            {
                return sort.First();
            }

            var sortProperty = sort.First();

            if (!this.sortColumMappings.Any())
            {
                return sortProperty + " " + sort[1];
            }

            var map = this.sortColumMappings.FirstOrDefault(row =>
                row.Item1.Equals(sortProperty, StringComparison.OrdinalIgnoreCase));

            if (map != null)
            {
                sortProperty = map.Item2;
            }

            return sortProperty + " " + sort[1];
        }
    }
}