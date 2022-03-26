using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace StrategyTest
{
    /// <summary>
    /// Manages the in-game date and time
    /// </summary>
    static class TimeManager
    {
        private static int currentDay;
        private static int totalDays;
        private static int years;
        private static string[] monthArray = new string[12] { "January", "February", "March", "April", "May", "June",
            "July", "August", "September", "Oktober", "November", "December" };
        private static int[] daysInMonthArray = new int[12] {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};
        private static int totalMonths;
        private static int currentMonth;
        private static int gameSpeed;
        private static double timeStep = 500; //How much time between each timeStep
        private static double lastTimeStep; //The exact time since the previous increase in ingame time

        
        /// <summary>
        /// Assigns initial starting values of the time and date system
        /// </summary>
        static TimeManager()
        {
            currentDay = 1;
            currentMonth = 1;
            TotalDays = 1;
            totalMonths = 1;
            Years = 1;
            GameSpeed = 1;
        }

        /// <summary>
        /// Value between 1-5 to adjust gamespeed(how fast time passes)
        /// </summary>
        public static int GameSpeed { get => gameSpeed; set => gameSpeed = value; }
        public static int TotalDays { get => totalDays; set => totalDays = value; }
        public static int Years { get => years; set => years = value; }

        public static void ChangeGameSpeed(string input)
        {
            if (input == "up")
            {
                if (GameSpeed < 5)
                {
                    GameSpeed++;
                }
            }
            if (input == "down")
            {
                if (GameSpeed > 1)
                {
                    GameSpeed--;
                }
            }
        }


        /// <summary>
        /// Advances the current in-game time based on the gamespeed
        /// </summary>
        public static void AdvanceTime()
        {
            //gamespeed is in a switch case so the values can be custom instead of linear or logarithmic
            //This might be subject to change
            double currentTime = GameWorld.GameTimeProp.TotalGameTime.TotalMilliseconds;
            switch (gameSpeed)
            {
                case 1:
                    if (currentTime > (lastTimeStep + timeStep))
                    {
                        lastTimeStep = currentTime;
                        DailyTick();
                    }
                    break;
                case 2:
                    if (currentTime > (lastTimeStep + timeStep / 2))
                    {
                        lastTimeStep = currentTime;
                        DailyTick();
                    }
                    break;
                case 3:
                    if (currentTime > (lastTimeStep + timeStep / 5))
                    {
                        lastTimeStep = currentTime;
                        DailyTick();
                    }
                    break;
                case 4:
                    if (currentTime > (lastTimeStep + timeStep / 8))
                    {
                        lastTimeStep = currentTime;
                        DailyTick();
                    }
                    break;
                case 5:
                    if (currentTime > (lastTimeStep + timeStep / 16))
                    {
                        lastTimeStep = currentTime;
                        DailyTick();
                    }
                    break;
                default:
                    throw new Exception("Error: gameSpeed is not between 1 to 5");
            }
        }

        /// <summary>
        /// The daily tick
        /// </summary>
        private static void DailyTick()
        {
            UpdateDate();
            foreach (Player player in MapManager.PlayerDictionary.Values)
            {
                player.DailyUpdate();
            }
        }


        private static void MonthlyTick()
        {
            foreach (Player player in MapManager.PlayerDictionary.Values)
            {
                player.MonthlyUpdate();
            }
        }

        private static void YearlyTick()
        {
            MapManager.YearlyTick();
        }

        /// <summary>
        /// Updates current day, month year to match correctly
        /// </summary>
        private static void UpdateDate()
        {
            TotalDays++;
            currentDay++;
            if (currentDay > daysInMonthArray[currentMonth-1])
            {
                currentDay = 1;
                currentMonth++;
                MonthlyTick();
            }
            if (currentMonth > 12)
            {
                years++;
                currentMonth = 1;
                YearlyTick();
            }
        }

        /// <summary>
        /// Returns a string of the current time
        /// </summary>
        /// <returns></returns>
        public static string CurrentTime(string format)
        {
            if (format == "day")
            {
                return TotalDays.ToString();
            }
            if (format == "normal")
            {
                string returnString = String.Format("{0}. {1} {2}", currentDay, monthArray[currentMonth-1], Years);
                return returnString;
            }
            throw new Exception("wrong format");
        }
    }
}
