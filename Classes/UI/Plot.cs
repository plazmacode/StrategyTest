using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyTest
{
    class Plot
    {
        private List<Vector2> dataList = new List<Vector2>();
        public List<Vector2> DataList { get => dataList; set => dataList = value; }

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
            if (dataList.Count > 100)
            {
                //Remove every odd datapoint
                for (int i = 0; i < dataList.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        dataList.RemoveAt(i);
                    }
                }
            }
        }
    }
}
