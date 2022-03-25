using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace StrategyTest
{
    /// <summary>
    /// Plot class for cool statistics(hopefully)
    /// </summary>
    class UIPlot : UIElement
    {
        private Color pointColor;
        private Color lineColor;
        private List<Vector2> dataList = new List<Vector2>();
        private Vector2 largestNumber;
        private Vector2 smallestNumber;
        private Vector2 plotRange;

        public List<Vector2> DataList { get => dataList; set => dataList = value; }

        public UIPlot(Vector2 position, Vector2 size, Color backgroundColor, float layer) : base(position, size, backgroundColor, layer)
        {
            rect = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }


        /// <summary>
        /// Draws the graph
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GameWorld.Pixel, rect, null, background, default, default, SpriteEffects.None, layer); //Background
            FindPlotRange();
            float scaleX = size.X / plotRange.X;
            float scaleY = size.Y / plotRange.Y;
            //if (float.IsInfinity(scaleX))
            //{
            //    scaleX = 0.2f;
            //}
            //if (float.IsInfinity(scaleY))
            //{
            //    scaleY = 0.2f;
            //}
            for (int i = 0; i < DataList.Count; i++)
            {
                float pointSize = 5;
                float x = (dataList[i].X - smallestNumber.X) * scaleX;
                float y = (dataList[i].Y - smallestNumber.Y) * scaleY;
                Vector2 dataPosition = new Vector2(position.X + x, position.Y + size.Y - y - pointSize);
                spriteBatch.Draw(GameWorld.Pixel, dataPosition, null, Color.Red, default, default, pointSize, SpriteEffects.None, layer+0.01f); //Datapoint
            }
        }

        /// <summary>
        /// Returns the largest number in the dataList
        /// </summary>
        /// <returns></returns>
        public void FindLargestNumber()
        {
            float largestX = 1;
            float largestY = 1;
            if (dataList.Count > 0)
            {
                largestX = dataList[0].X;
                for (int i = 0; i < DataList.Count; i++)
                {
                    if (dataList[i].X > largestX)
                    {
                        largestX = dataList[i].X;
                    }
                }
                largestY = dataList[0].Y;
                for (int i = 0; i < DataList.Count; i++)
                {
                    if (dataList[i].Y > largestY)
                    {
                        largestY = dataList[i].Y;
                    }
                }
            }
            largestNumber = new Vector2(largestX, largestY);
        }

        /// <summary>
        /// Finds the smalelst number in the dataList
        /// </summary>
        public void FindSmallestNumber()
        {
            float smallestX = 1;
            float smallestY = 1;
            if (dataList.Count > 0)
            {
                smallestX = dataList[0].X;
                for (int i = 0; i < DataList.Count; i++)
                {
                    if (dataList[i].X < smallestX)
                    {
                        smallestX = dataList[i].X;
                    }
                }
                smallestY = dataList[0].Y;
                for (int i = 0; i < DataList.Count; i++)
                {
                    if (dataList[i].Y < smallestY)
                    {
                        smallestY = dataList[i].Y;
                    }
                }
            }
            smallestNumber = new Vector2(smallestX, smallestY);
        }

        /// <summary>
        /// Finds the range between min and max value. (in case the graph starts at non-zero value)
        /// </summary>
        public void FindPlotRange()
        {
            FindSmallestNumber();
            FindLargestNumber();
            plotRange = new Vector2(1, 1) + largestNumber - smallestNumber;
        }

        public override void Update()
        {
            if (Name == "populationPlot" && MapManager.SelectedProvince.Resources.PopulationPlot != null)
            {
                DataList = MapManager.SelectedProvince.Resources.PopulationPlot.DataList;
            }
        }
    }
}
