﻿using System;
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
        private static double lastTimeStep; //The exact time since the previous increase in ingame time

        
        /// <summary>
        /// Assigns initial starting values of the time and date system
        /// </summary>
        static TimeManager()
        {
            currentDay = 1;
            currentMonth = 1;
            totalDays = 0;
            totalMonths = 0;
            years = 1;
            GameSpeed = 1;
        }

        /// <summary>
        /// Value between 1-5 to adjust gamespeed(how fast time passes)
        /// </summary>
        public static int GameSpeed { get => gameSpeed; set => gameSpeed = value; }

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
            GameWorld.DebugTexts.Add(GameSpeed.ToString());
            double timeStep = 150; //how often the time will advance in milliseconds
            double currentTime = GameWorld.GameTimeProp.TotalGameTime.TotalMilliseconds;
            switch (gameSpeed)
            {
                case 1:
                    if (currentTime > (lastTimeStep + timeStep))
                    {
                        lastTimeStep = currentTime;
                        UpdateDate();
                    }
                    break;
                case 2:
                    if (currentTime > (lastTimeStep + timeStep / 1.5f))
                    {
                        lastTimeStep = currentTime;
                        UpdateDate();
                    }
                    break;
                case 3:
                    if (currentTime > (lastTimeStep + timeStep / 3))
                    {
                        lastTimeStep = currentTime;
                        UpdateDate();
                    }
                    break;
                case 4:
                    if (currentTime > (lastTimeStep + timeStep / 5))
                    {
                        lastTimeStep = currentTime;
                        UpdateDate();
                    }
                    break;
                case 5:
                    if (currentTime > (lastTimeStep + timeStep / 8))
                    {
                        lastTimeStep = currentTime;
                        UpdateDate();
                    }
                    break;
                default:
                    throw new Exception("Error: gameSpeed is not between 1 to 5");
            }
        }

        /// <summary>
        /// Updates current day, month year to match correctly
        /// </summary>
        private static void UpdateDate()
        {
            years = 1 + totalDays / 365;
            totalDays++;
            currentDay++;
            if (currentDay > daysInMonthArray[currentMonth-1])
            {
                currentDay = 1;
                currentMonth++;
            }
            if (currentMonth > 12)
            {
                currentMonth = 1;
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
                return totalDays.ToString();
            }
            if (format == "normal")
            {
                string returnString = String.Format("{0}. {1} {2}", currentDay, monthArray[currentMonth-1], years);
                return returnString;
            }
            throw new Exception("wrong format");
        }
    }
}
