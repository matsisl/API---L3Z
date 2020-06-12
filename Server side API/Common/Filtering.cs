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

        public Filtering(string filter)
        {
            Filter = filter;
        }
    }
}
