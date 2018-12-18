using System;
using System.Collections.Generic;
using System.Drawing;

namespace CG.HexogonFolder
{
    public class HexagonBuilder : IHexagonBuilder
    {
        public Point Center;
        public int Radius;
        public int DeltaX;
        private int WindowWidth;
        private int WindowHeight;
        private int CurrentTick;
        public static double DeltaPhi = Math.PI / 6;

        public HexagonBuilder(int width, int height)
        {
            WindowWidth = width;
            WindowHeight = height;
            CurrentTick = 0;
            DeltaX = (int)(Math.PI / 6 * Radius);
            Center = new Point();
        }

        public Hexagon GetNext()
        {
            Center.X += DeltaX;
            CurrentTick += 1;

            return new Hexagon()
            {
                Center = new Point(),
                BorderPoints = new List<Point>()
            };
        }
    }
}