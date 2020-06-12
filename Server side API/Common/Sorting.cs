using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Sorting
    {
        private TypeOfSorting _typeOfSorting;
        public TypeOfSorting TypeOfSorting
        {
            get => _typeOfSorting;
            set => _typeOfSorting = value;
        }

        public Sorting(TypeOfSorting typeOfSorting)
        {
            TypeOfSorting = typeOfSorting;
        }
    }
}
