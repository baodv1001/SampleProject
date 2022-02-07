using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Core.Helpers
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }

        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasNext => CurrentPage < TotalPages;
        public bool HasPrevious => CurrentPage > 1;

        public PagedList(List<T> items, int count, int pageNumer, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumer;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            AddRange(items);
        }

        public static PagedList<T> ToPagedList(List<T> source, int pageNumer, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumer-1)*pageSize)
                .Take(pageSize)
                .ToList(); 

            return new PagedList<T>(items, count, pageNumer, pageSize);
        }
    }
}
