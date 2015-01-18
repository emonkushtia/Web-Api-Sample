using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using Demo.Core.ExtensionMethods;

namespace Demo.Core.Infrastructure
{
    [Serializable]
    public class PageableListQueryCommand<TDomainObject> :IValidatableObject
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

        public virtual PageableListQueryCommand<TDomainObject>
            MapSortProperties(params object[] columns)
        {
            if (columns == null)
            {
                throw new ArgumentNullException("columns");
            }

            foreach (var column in columns)
            {
                var stringColumn = column as string;

                if (stringColumn != null)
                {
                    this.SetSorting(stringColumn, stringColumn);
                }
                else
                {
                    foreach (var pair in column.ToDictionary())
                    {
                        this.SetSorting(pair.Key, pair.Value.ToString());
                    }
                }
            }

            return this;
        }

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

        public IEnumerable<ValidationResult> Validate(
           ValidationContext validationContext)
        {
            if (!string.IsNullOrWhiteSpace(this.Sort))
            {
                var sort = this.Sort.Split(
                   new[] { ' ' },
                   StringSplitOptions.RemoveEmptyEntries);

                if (sort.Length > 1 &&
                    !this.sortOrders.Contains(sort[1], StringComparer.OrdinalIgnoreCase))
                {
                    var sortOrderErrorMessage = string.Format(
                        CultureInfo.CurrentCulture,
                        "Invalid sort order, only supports {0} orders.",
                        string.Join(", ", this.sortOrders));

                    yield return new ValidationResult(sortOrderErrorMessage);
                }
            }

            if (this.Offset < 0)
            {
                yield return new ValidationResult(
                    "Offset must be a positive integer.",
                    new[] { "Offset" });
            }

            //if ((this.Limit < 1) || (this.Limit > 100))
            //{
            //    yield return new ValidationResult(
            //        "Limit must be between 1-100.",
            //        new[] { "Limit" });
            //}
        }

        private void SetSorting(string alias, string target)
        {
            var map = this.sortColumMappings.FirstOrDefault(row =>
                row.Item1.Equals(target, StringComparison.OrdinalIgnoreCase));

            if (map != null)
            {
                throw new ArgumentException(
                    "Mapping already exists for \"" +
                    alias +
                    "\".",
                    "alias");
            }

            this.sortColumMappings.Add(new Tuple<string, string>(alias, target));
        }
    }
}