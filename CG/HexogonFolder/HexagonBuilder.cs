using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace CG.HexogonFolder
{
	public class HexagonBuilder : IHexagonBuilder
	{
		public Vector3 Center;
		public int Radius;
		public int DeltaX;
		private int WindowWidth;
		private int WindowHeight;
		private int CurrentTick;
		public static double DeltaPhi = Math.PI / 6;
		public Hexagon ZeroHexagon;
		public List<Vector3> BorderVectors;

		public HexagonBuilder(int width, int height)
		{
			WindowWidth = width;
			WindowHeight = height;
			Radius = width / 20;
			CurrentTick = 0;

			DeltaX = (int)(Math.PI / 6 * Radius);
			Center = new Vector3(-(width / 2), 0, 1);
			ZeroHexagon = new Hexagon();
			ZeroHexagon.Center = new Vector3(0, 0, 0);
			ZeroHexagon.BorderPoints = new List<Vector3>();
			for (int i = 0; i < 6; i++)
			{
				ZeroHexagon.BorderPoints.Add(new Vector3((int)(Radius * Math.Cos(i * Math.PI / 3)), (int)(Radius * Math.Sin(i * Math.PI / 3)), 0));
			}
		}

		public Hexagon GetNext()
		{
			Center.X = (-WindowWidth / 2) + DeltaX * CurrentTick;
			CurrentTick += 1;

			var newBorder = new List<Vector3>();
			for (int i = 0; i < 6; i++)
			{
				//Поворачиваем гексагон на угол 30 градусов * колчество тиков
				newBorder.Add(MoveVector(TurnPoint(BorderVectors[i], CurrentTick * Math.PI / 6), CurrentTick * DeltaX));
			}

			return new Hexagon()
			{
				Center = new Vector3(),
				BorderPoints = newBorder
			};
		}

		private Vector3 TurnPoint(Vector3 vector, double phi)
		{
			var x = vector.X;
			var y = vector.Y;
			var z = vector.Z;
			var newPoint = new Vector3((int)(x * Math.Cos(HexagonBuilder.DeltaPhi) + y * Math.Sin(HexagonBuilder.DeltaPhi)),
				(int)(-x * Math.Sin(HexagonBuilder.DeltaPhi) + y * Math.Cos(HexagonBuilder.DeltaPhi)), z);
			return newPoint;
		}

		private Vector3 MoveVector(Vector3 vector, int deltaX)
		{
			return new Vector3((int)(vector.X + deltaX), (int)vector.Y, (int)vector.Z);
		}
	}
}