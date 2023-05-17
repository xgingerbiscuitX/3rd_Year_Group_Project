using BusinessEntities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class PopularSorter : IComparer
    {
        int IComparer.Compare(object o, object e)
        {
            IStock o1 = (IStock)o;
            IStock o2 = (IStock)e;
            return o2.Amount.CompareTo(o1.Amount);
        }
    }
}
