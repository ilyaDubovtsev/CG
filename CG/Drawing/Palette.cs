using System;
using System.Drawing;

namespace CG.Painter
{
    public static class Palette
    {
        private static Color[] Rainbow =
        {
//            Color.Red,
            Color.Orange,
            Color.Yellow,
            Color.Green,
            Color.LightSkyBlue,
            Color.RoyalBlue,
            Color.Purple
        };
        
        public static Color ColorGetRandomColor(int n)
        {
            var random = new Random(n);
            return Rainbow[random.Next(100) % 7];
        }
        
        public static Color GetColorByNumber(int n)
        {
            return Rainbow[n % Rainbow.Length];
        }
    }
}