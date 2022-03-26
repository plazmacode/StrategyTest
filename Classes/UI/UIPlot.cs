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
        private bool isShown;
        private string labelX, labelY;


        string[] xAxisText = new string[4] { "", "", "", "" };
        int[] xAxisTextPosition = new int[4];
        string[] yAxisText = new string[4] { "", "", "", "" };
        int[] yAxisTextPosition = new int[4];

        float pointSize = 5;

        public List<Vector2> DataList { get => dataList; set => dataList = value; }
        public string LabelX { get => labelX; set => labelX = value; }
        public string LabelY { get => labelY; set => labelY = value; }
        public bool IsShown { get => isShown; set => isShown = value; }

        public UIPlot(Vector2 position, Vector2 size, Color backgroundColor, float layer) : base(position, size, backgroundColor, layer)
        {
            IsShown = false;
            rect = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }


        /// <summary>
        /// Draws the graph
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GameWorld.Sprites["pixel"], rect, null, background, default, default, SpriteEffects.None, layer); //Background
            FindPlotRange();
            float scaleX = Size.X / plotRange.X;
            float scaleY = Size.Y / plotRange.Y;

            for (int i = 0; i < DataList.Count; i++)
            {
                float x = (dataList[i].X - smallestNumber.X) * scaleX;
                float y = (dataList[i].Y - smallestNumber.Y) * scaleY;
                Vector2 dataPosition = new Vector2(position.X + x, position.Y + Size.Y - y - pointSize);
                spriteBatch.Draw(GameWorld.Sprites["pixel"], dataPosition, null, Color.Red, default, default, pointSize, SpriteEffects.None, layer+0.01f); //Datapoint

                if (i == (dataList.Count-1) * 0.25)
                {
                    xAxisText[0] = ((1 + Math.Floor(dataList[i].X / 365)) * 0.25).ToString();
                    xAxisTextPosition[0] = (int)dataPosition.X;
                    yAxisTextPosition[0] = (int)dataPosition.Y;
                    yAxisText[0] = NumberFormatter.K10Number(dataList[i].Y);
                }

                if (i == (dataList.Count - 1) * 0.5)
                {
                    xAxisText[1] = ((1 + Math.Floor(dataList[i].X / 365)) * 0.5).ToString();
                    xAxisTextPosition[1] = (int)dataPosition.X;
                    yAxisTextPosition[1] = (int)dataPosition.Y;
                    yAxisText[1] = NumberFormatter.K10Number(dataList[i].Y);
                }

                if (i == (dataList.Count - 1) * 0.75)
                {
                    xAxisText[2] = ((1 + Math.Floor(dataList[i].X / 365)) * 0.75).ToString();
                    xAxisTextPosition[2] = (int)dataPosition.X;
                    yAxisTextPosition[2] = (int)dataPosition.Y;
                    yAxisText[2] = NumberFormatter.K10Number(dataList[i].Y);
                }

                if (i == dataList.Count-1)
                {
                    xAxisText[3] = (1 + Math.Floor(dataList[i].X / 365)).ToString();
                    xAxisTextPosition[3] = (int)dataPosition.X;
                    yAxisTextPosition[3] = (int)dataPosition.Y;
                    yAxisText[3] = NumberFormatter.K10Number(dataList[i].Y);
                }                
            }
            float dataLayer = layer + 0.05f;
            spriteBatch.DrawString(GameWorld.Arial, xAxisText[0], new Vector2(xAxisTextPosition[0], position.Y + Size.Y + 10), Color.White, default, default, 1, SpriteEffects.None, dataLayer);
            spriteBatch.DrawString(GameWorld.Arial, xAxisText[1], new Vector2(xAxisTextPosition[1], position.Y + Size.Y + 10), Color.White, default, default, 1, SpriteEffects.None, dataLayer);
            spriteBatch.DrawString(GameWorld.Arial, xAxisText[2], new Vector2(xAxisTextPosition[2], position.Y + Size.Y + 10), Color.White, default, default, 1, SpriteEffects.None, dataLayer);
            spriteBatch.DrawString(GameWorld.Arial, xAxisText[3] + "years", new Vector2(position.X + Size.X, position.Y + Size.Y + 10), Color.White, default, default, 1, SpriteEffects.None, dataLayer);

            spriteBatch.DrawString(GameWorld.Arial, yAxisText[0], new Vector2(position.X + Size.X + 10, yAxisTextPosition[0]), Color.White, default, default, 1, SpriteEffects.None, dataLayer);
            spriteBatch.DrawString(GameWorld.Arial, yAxisText[1], new Vector2(position.X + Size.X + 10, yAxisTextPosition[1]), Color.White, default, default, 1, SpriteEffects.None, dataLayer);
            spriteBatch.DrawString(GameWorld.Arial, yAxisText[2], new Vector2(position.X + Size.X + 10, yAxisTextPosition[2]), Color.White, default, default, 1, SpriteEffects.None, dataLayer);
            spriteBatch.DrawString(GameWorld.Arial, yAxisText[3] + " people", new Vector2(position.X + Size.X + 10, position.Y), Color.White, default, default, 1, SpriteEffects.None, dataLayer);
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
