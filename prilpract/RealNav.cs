using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace prilpract
{
    internal class RealNav
    {
        public static SchoolDBEntities1 c;
        public static SchoolDBEntities1 context
        {
            get
            {
                if (c == null)
                    c = new SchoolDBEntities1();

                return c;
            }
        }
    }
}
