using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyTest
{
    /// <summary>
    /// Formats numbers
    /// </summary>
    static class NumberFormatter
    {
        /// <summary>
        /// Start formatting after 1.000. Returns in thousands
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string KNumber(float n)
        {
            if (n > 1000)
            {
                return (n / 1000).ToString("0.0") + "k";
            }
            else
            {
                return n.ToString("0.00");
            }
        }

        /// <summary>
        /// Starts formatting after 10.000. Returns in thousands
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string K10Number(float n)
        {
            if (n > 10000)
            {
                return (n / 1000).ToString("0.0") + "k";
            } else
            {
                return n.ToString("0");
            }
        }
    }
}
