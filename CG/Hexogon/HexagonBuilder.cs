using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace CG.HexagonFolder
{
	public class HexagonBuilder : IHexagonBuilder
	{
		private double Radius;
		private static double DeltaX;
		private int CurrentTick;
		private const double DeltaPhi = Math.PI / 6;
		private static Hexagon CurrentHexagon;
		private static float VerticalBalance;
		public int Deep;
		public Vector3 BasicPoint;
		private Vector3 Center;
		public double Coefficient = 0.3;


		public HexagonBuilder(int width, int height, int deep, Vector3 basicPoint)
		{
			CurrentTick = 0;

			var horizontalR = 0.05 * width;
			var verticalR = 0.2 * height;
			VerticalBalance = width / height;
			Center = basicPoint;
			Deep = deep;

			Radius = (horizontalR > verticalR) ? horizontalR / width * Math.Pow(Coefficient, deep) : verticalR / height * Math.Pow(Coefficient, deep);
			BasicPoint = basicPoint;
			DeltaX = (Math.PI / 6 * Radius);
			CurrentHexagon = new Hexagon
				{
					Center = new Vector3(0, 0, 0),
					BorderPoints = new List<Vector3>()
				};

			for (int i = 0; i < 6; i++)
			{
				CurrentHexagon.BorderPoints.Add(new Vector3((float)(Radius * Math.Cos(i * Math.PI / 3)), (float)(Radius * Math.Sin(i * Math.PI / 3)), 0));
			}
		}

		public Hexagon GetNext()
		{
			CurrentTick += 1;

			if (CurrentHexagon.Center.X - 1 + DeltaX * CurrentTick > 1 + Radius)
			{
				CurrentTick = 0;
			}

			var newBorder = new List<Vector3>();
			for (int i = 0; i < 6; i++)
			{
				newBorder.Add(TurnPoints(CurrentHexagon.BorderPoints[i] + BasicPoint, (float)DeltaPhi * CurrentTick));
				newBorder[newBorder.Count - 1] = new Vector3((float)(newBorder[newBorder.Count - 1].X - 1 + DeltaX * CurrentTick), newBorder[newBorder.Count - 1].Y * VerticalBalance, 0);
			}
			Center = TurnPoints(BasicPoint, (float) DeltaPhi * CurrentTick);
			Center.X = Center.X + (float)DeltaX * CurrentTick - 1;
			Center.Y *= VerticalBalance;
			return new Hexagon()
			{
				Center = (this.Center),
				BorderPoints = newBorder
			};
		}

		private Vector3 TurnPoints(Vector3 vector, float angle)
		{
			return MatrixMultiplication(vector, CreateTurnMatrix(angle));
		}

		private static float[,] CreateTurnMatrix(double angle)
		{
			var matrix = new float[3, 3];

			matrix[0, 0] = (float)Math.Cos(angle);
			matrix[0, 1] = (float)Math.Sin(angle);
			matrix[1, 0] = (float)-Math.Sin(angle);
			matrix[1, 1] = (float)Math.Cos(angle);
			matrix[2, 2] = 1;
			return matrix;
		}

		private Vector3 MatrixMultiplication(Vector3 vector, float[,] turnMatrix)
		{
			var result = new Vector3();
			result.X = vector.X * turnMatrix[0, 0] + vector.Y * turnMatrix[0, 1] + vector.Z * turnMatrix[0, 2];
			result.Y = vector.X * turnMatrix[1, 0] + vector.Y * turnMatrix[1, 1] + vector.Z * turnMatrix[1, 2];
			result.Z = vector.X * turnMatrix[2, 0] + vector.Y * turnMatrix[2, 1] + vector.Z * turnMatrix[2, 2];
			return result;
		}
	}
}