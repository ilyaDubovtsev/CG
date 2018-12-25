using System;
using System.Drawing;

namespace CG.Painter
{
    public static class Palette
    {
        public static Color GetGreenColor() => Color.Green;

        public static Color ColorGetRandomColor()
        {
            var rainbow = new Color[]
            {
                Color.Red,
                Color.Orange,
                Color.Yellow,
                Color.Green,
                Color.LightSkyBlue,
                Color.RoyalBlue,
                Color.Purple
                
            };
            var random = new Random();
            return rainbow[random.Next(100) % 7];
        }
    }
}