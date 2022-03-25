using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyTest
{
    class Plot
    {
        private List<Vector2> dataList = new List<Vector2>();
        private int maxPoints;
        private Random random;

        public List<Vector2> DataList { get => dataList; set => dataList = value; }

        public Plot()
        {
            maxPoints = 100;
            random = new Random();
        }

        /// <summary>
        /// Updates the content of the graph
        /// <para>Example: totalDays on the x-axis, with some other value on the y-axis</para>
        /// </summary>
        public void AddData(Vector2 data)
        {
            DataList.Add(data);
            Optimise();
        }

        /// <summary>
        /// Start remomving plot points to optimise it
        /// </summary>
        private void Optimise()
        {
            if (dataList.Count > maxPoints)
            {
                for (int i = 0; i < maxPoints/14; i++)
                {
                    dataList.RemoveAt(random.Next(1, dataList.Count-1));
                }
                for (int i = 0; i < maxPoints/7; i++)
                {
                    dataList.RemoveAt(random.Next(dataList.Count/2, dataList.Count-1));
                }
            }
        }
    }
}
