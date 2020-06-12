using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Paging
    {
        private int _pageSize;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value;
        }

        private int _pageIndex;
        public int PageIndex
        {
            get => _pageIndex;
            set => _pageIndex = value;
        }

        public bool Invalidete()
        {
            if (PageSize <= 0 || PageIndex<1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public int Offset()
        {
            return (PageIndex-1) * PageSize;
        }

        public Paging(int pageSize, int pageIndex)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
        }
    }
}
