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
        
        public static Color4 ColorGetRandomColor(int n = 0)
        {
            var random = new Random();
            return Rainbow[random.Next(100) % 7];
        }

        public static List<Color4> GetRandomColorSet(int n)
        {
            var random = new Random();
            var colorSet = new List<Color4>();
            for (int i = 0; i < n; i++)
            {
                colorSet.Add(Rainbow[random.Next(100) % 7]);
            }
            return colorSet;
        }
        
        public static Color4 GetColorByNumber(int n)
        {
            return Rainbow[n % Rainbow.Length];
        }

        public static List<Color4> UpdateColorSet(List<Color4> originals)
        {
            var updated = new List<Color4>();
            
            foreach (var color in originals)
            {
                updated.Add(UpdateColor(color));
            }

            return updated;
        }
        
        public static Color4 UpdateColor(Color4 original)
        {
            var updated = new Color4();

            updated.R = (original.R + 1) % 255;
            updated.G = (original.G + 1) % 255;
            updated.B = (original.B + 1) % 255;

            return updated;
        }

        private static int RandomOne()
        {
            var random = new Random();
            return random.Next(100) > 50 ? 10 : -10;

        }
    }
}