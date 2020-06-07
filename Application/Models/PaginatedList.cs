using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Models
{
    public class PaginatedList<T>
    {
        public PaginatedList() { }

        public PaginatedList(IEnumerable<T> list, int page = 0, int pageSize = int.MaxValue)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            Page = page;
            PageSize = pageSize;

            Items = list.Skip(Page * PageSize).Take(PageSize).ToList();
            Total = list.Count();
            if (Items.Count() == 0 && Total > 0)
            {
                Page = page - 1;
                Items = list.Skip(Page * PageSize).Take(PageSize).ToList();
            }
            TotalPages = (int)Math.Ceiling(Convert.ToDecimal(Total) / Convert.ToDecimal(pageSize));
        }

        public IEnumerable<T> Items { get; }

        public int Page { get; }

        public int PageSize { get; }

        public int Total { get; set; }

        public int TotalPages { get; }
    }
}
