using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

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
		public Hexagon ZeroHexagon;

		public HexagonBuilder(int width, int height)
		{
			WindowWidth = width;
			WindowHeight = height;
			Radius = width / 20;
			CurrentTick = 0;
			DeltaX = (int)(Math.PI / 6 * Radius);
			Center = new Point(-width / 2);
			ZeroHexagon = new Hexagon();
			ZeroHexagon.Center = new Point(0, 0);
			ZeroHexagon.BorderPoints = new List<Point>();
			for (int i = 0; i < 6; i++)
			{
				ZeroHexagon.BorderPoints.Add(new Point((int)(Radius * Math.Cos(i * Math.PI / 3)), (int)(Radius * Math.Sin(i * Math.PI / 3))));
			}
		}

		public Hexagon GetNext()
		{
			Center.X = (-WindowWidth / 2) + DeltaX * CurrentTick;
			CurrentTick += 1;

			var newBorder = new List<Point>();
			for (int i = 0; i < 6; i++)
			{
				//Поворачиваем гексагон на угол 30 градусов * колчество тиков
				newBorder.Add(MovePoint(TurnPoint(ZeroHexagon.BorderPoints[i], CurrentTick * Math.PI / 6), CurrentTick * DeltaX));
			}

			return new Hexagon()
			{
				Center = new Point(),
				BorderPoints = newBorder
			};
		}

		private Point TurnPoint(Point point, double phi)
		{
			var x = point.X;
			var y = point.Y;

			var newPoint = new Point((int)(x * Math.Cos(HexagonBuilder.DeltaPhi) + y * Math.Sin(HexagonBuilder.DeltaPhi)),
				(int)(-x * Math.Sin(HexagonBuilder.DeltaPhi) + y * Math.Cos(HexagonBuilder.DeltaPhi)));
			return newPoint;
		}

		private Point MovePoint(Point point, int deltaX)
		{
			return new Point(point.X + deltaX, point.Y);
		}
	}
}