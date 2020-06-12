using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class PageResult<Tentity>
    {
        public Sorting Sorting { get; set; }
        public Paging Paging { get; set; }
        public Filtering Filtering { get; set; }
        public string NextPage { get; set; }
        public IEnumerable<Tentity> Results { get; set; }

        public PageResult(Sorting sorting, Paging paging, Filtering filtering)
        {
            Sorting = sorting;
            Paging = paging;
            Filtering = filtering;
        }

        public PageResult(int pageIndex, int pageSize, TypeOfSorting sort, string filter)
        {
            Paging = new Paging(pageSize, pageIndex);
            Sorting = new Sorting(sort);
            Filtering = new Filtering(filter);
        }

        public void GenerateNextPage(string route)
        {
            
        }
    }
}
