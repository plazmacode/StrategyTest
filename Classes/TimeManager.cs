using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyTest
{
    static class TimeManager
    {
        private static int currentDay;
        private static int totalDays;
        private static int years;
        private static string[] monthArray = new string[12] { "January", "February", "March", "April", "May", "June",
            "July", "August", "September", "Oktober", "November", "December" };
        private static int totalMonths;
        private static int currentMonth;

        static TimeManager()
        {
            currentDay = 1;
            currentMonth = 0;
            totalDays = 0;
            totalMonths = 0;
            years = 1;
        }

        /// <summary>
        /// Returns a string of the current time
        /// </summary>
        /// <returns></returns>
        public static string CurrentTime(string format)
        {
            if (format == "day")
            {
                return totalDays.ToString();
            }
            if (format == "normal")
            {
                string returnString = String.Format("{0}. {1} {2}", currentDay, monthArray[currentMonth], years);
                return returnString;
            }
            throw new Exception("wrong format");
        }
    }
}
