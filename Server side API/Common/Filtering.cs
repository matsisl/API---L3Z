using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Filtering
    {
        private string _filter;
        public string Filter
        {
            get => _filter;
            set => _filter = value;
        }
        private string _columnOfFiltering;
        private string ColumnOfFiltering
        {
            get => _columnOfFiltering;
            set => _columnOfFiltering = value;
        }

        public Filtering(string filter, string columnOfFiltering="Name")
        {
            Filter = filter;
            ColumnOfFiltering = columnOfFiltering;
        }
    }
}
