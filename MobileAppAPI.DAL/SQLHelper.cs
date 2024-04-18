using System;
using System.Collections.Generic;
using System.Text;

namespace MobileAppAPI.DAL
{
    public static class SQLHelper
    {
        public static object ToDbNull(object value)
        {
            if (null != value)
                return value;
            return DBNull.Value;
        }
        public static decimal? ToNullableDecimal(this object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToDecimal(@this);
        }

        public static decimal? ToNullableDecimalForShipping(this object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }
            var test = Convert.ToDecimal(@this); 
            return System.Math.Round(test, 4);
        }

        public static int? ToNullableInt(this object @this)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            return Convert.ToInt32(@this);
        }
    }
    
}
