using System;
using System.Collections.Generic;
using System.Text;

namespace RohaniShop.Common
{
    public static class ObjectExtensions
    {
        public static void CheckArgumentIsNull(this object o, string name)
        {
            if (o == null)
                throw new ArgumentNullException(name);
        }
    }
}
