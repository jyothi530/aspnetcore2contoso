using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Models
{
    public class PaginatedList<T> : List<T>
    {
        public PaginatedList(List<T> items, int pageSize, int count, int pageIndex)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        public int PageIndex { get; private set; }

        public int TotalPages { get; private set; }

        public bool HasPreviousPage
        {
            get
            {
                return PageIndex > 1;
            }
        }

        public bool HasNextPage
        {
            get
            {
                return PageIndex < TotalPages;
            }
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source
                                                , int pageSize, int pageIndex)
        {
            var totalCount = await source.CountAsync();
            var result = await source.Skip(pageSize * (pageIndex - 1))
                                        .Take(pageSize).ToListAsync();

            return new PaginatedList<T>(result, pageSize, totalCount, pageIndex);
        }
    }
}
