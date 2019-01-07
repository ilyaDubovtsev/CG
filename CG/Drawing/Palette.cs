using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK.Graphics;

namespace CG.Painter
{
    public static class Palette
    {
        private static Color4[] Rainbow =
        {
            Color4.Red,
            Color4.Orange,
            Color4.Yellow,
            Color4.Green,
            Color4.LightSkyBlue,
            Color4.RoyalBlue,
            Color4.Purple
        };
        
        public static Color4 GetRandomColor(int n = 0)
        {
            var random = new Random(234354 * n);
            return Rainbow[random.Next(100000) % 7];
        }
        
        public static Color4 GetColorByNumber(int n)
        {
            return Rainbow[n % Rainbow.Length];
        }
    }
}